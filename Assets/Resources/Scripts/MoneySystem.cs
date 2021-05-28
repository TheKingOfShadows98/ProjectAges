using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneySystem: MonoBehaviour
{
    [HideInInspector]
    public static MoneySystem SINGLETON;

    public TMPro.TextMeshProUGUI player_display;

    [SerializeField] public int PlayerMoney = 0;
    [SerializeField] public int EnemyMoney = 0;
    [SerializeField] public int moneyPerTime = 5;
    [SerializeField] public float TimeOfCycle = 3;
    public bool STOPNOW = false;
    
    void Awake()
    {
        SINGLETON = this;
    }
    private void Start()
    {
        StartCoroutine(GiftMoney(TimeOfCycle));
    }
    public int GetPlayerMoney()
    {
        return PlayerMoney;
    }
    public int GetEnemyMoney()
    {
        return EnemyMoney;
    }
    public bool Buy(int cost, bool isplayer = false)
    {
        switch (isplayer)
        {
            case false:
                {
                    if (cost > EnemyMoney) return false;
                    EnemyMoney -= cost;
                    break;
                }
            case true:
                {
                    if (cost > PlayerMoney) return false;
                    PlayerMoney -= cost;
                    player_display.text = $"{PlayerMoney}";
                    break;
                }
            default:
                break;
        }
        return true;
    }

    private void addMoney()
    {
        var extra = (UpgradesController.SINGLETON.playerUpgrades[2] * 2);
        PlayerMoney += moneyPerTime + extra;
        extra = (UpgradesController.SINGLETON.enemyUpgrades[2] * 2);
        EnemyMoney += moneyPerTime + extra;
        if (player_display)
        {
            player_display.text = $"{PlayerMoney}";
        }
    }

    IEnumerator GiftMoney(float time)
    {
        addMoney();
        yield return new WaitForSeconds(time);
        if(!STOPNOW)
        StartCoroutine(GiftMoney(time));
    }
}
