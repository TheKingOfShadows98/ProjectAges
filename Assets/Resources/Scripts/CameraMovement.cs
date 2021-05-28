using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float Speed = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void MoveLeft()
    {
        transform.position += Vector3.left * Speed * Time.deltaTime;
    }
    public void MoveRight()
    {
        transform.position += Vector3.right * Speed * Time.deltaTime;
        Debug.Log($"Libertad");
    }
    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.right * Speed * Time.deltaTime * Input.GetAxisRaw("Horizontal");
    }
}
