using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColaManager : MonoBehaviour
{
    public static ColaManager SINGLETON;
    public Image[] images = new Image[5];
    public Sprite[] sprites = new Sprite[3];

    private void Awake()
    {
        SINGLETON = this;
    }
    void Start()
    {
        UpdateQueue();
    }

    public void UpdateQueue()
    {
        var queue = SpawnSystem.SINGLETON.player_queue.ToArray();
        for (int i = 0; i < images.Length; i++)
        {
            if(queue.Length - 1 >= i)
            {
                images[i].enabled = true;
                switch (queue[i].name)
                {
                    case "Soldier":
                        {
                            images[i].sprite = sprites[0];
                            break;
                        }
                    case "Archer":
                        {
                            images[i].sprite = sprites[1];
                            break;
                        }
                    case "Siege":
                        {
                            images[i].sprite = sprites[2];
                            break;
                        }
                    default:
                        break;
                }
            }
            else
            {
                images[i].enabled = false;
            }
        }
    }
}
