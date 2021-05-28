using UnityEngine;
using System.Collections;


public class EnemyAI: MonoBehaviour
{
    System.Action movimiento;
    // Use this for initialization
    void Start()
    {
        movimiento = Agresivo;
        StartCoroutine(debug_cycle());
    }

    // Update is called once per frame
    void Update()
    {

    }
    void Economico()
    {

    }
    void Agresivo()
    {
        SpawnSystem.SINGLETON.EnemyAddQueue("Soldier");
    }
    IEnumerator debug_cycle()
    {
        movimiento();

        yield return new WaitForSeconds(Random.Range(3,8));
        StartCoroutine(debug_cycle());
    }
}
