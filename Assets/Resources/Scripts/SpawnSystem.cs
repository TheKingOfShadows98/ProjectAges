using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Tk.ProyectAge;
public class SpawnSystem: MonoBehaviour
{
    [HideInInspector]
    public static SpawnSystem SINGLETON;

    public UnitData soldier;
    public UnitData archer;
    public UnitData siege;
    public float player_train_time = 10f;
    public float enemy_train_time = 10f;
    public Queue<UnitData> player_queue = new Queue<UnitData>();
    public Queue<UnitData> Enemy_queue = new Queue<UnitData>();

    void Awake()
    {
        SINGLETON = this;
    }

    private void Update()
    {
        if(player_train_time <= 0f)
        {
            if(player_queue.Count > 0)
            {
                PlayerQueueDispatch();
            }

        }
        else
        {
            player_train_time -= Time.deltaTime;
        }
        if (enemy_train_time <= 0f)
        {
            if (Enemy_queue.Count > 1)
                EnemyQueueDispatch();
        }
        else
        {
            enemy_train_time -= Time.deltaTime;
        }
    }

    private GameObject SpawnUnit(Vector3 position)
    {
        GameObject unit = PoolerSystem.SINGLETON.getFromPool(); // get unit of pool
        unit.transform.position = position;

        return unit;
    }

    public void setData(GameObject unit, UnitData data, bool isplayer)
    {
        var upgrades = (isplayer) ? UpgradesController.SINGLETON.playerUpgrades : UpgradesController.SINGLETON.enemyUpgrades;
        NpcController unit_npc = unit.GetComponent<NpcController>();
        unit_npc.speed = data.speed;
        unit_npc.MaxLife = data.maxLife;
        unit_npc.baseAttack = data.baseAttack + (upgrades[0] * data.attack_multiplier);
        unit_npc.defense = data.defense + (upgrades[1] * data.defenze_multiplier);
        unit_npc.Range = data.attackRange;
        foreach (Transform f in unit.transform)
        {
            if (f.gameObject.name.Equals(data.name)) { 
                f.gameObject.SetActive(true);
                unit_npc.anim = f.gameObject.GetComponent<Animator>();
            }
            else
                f.gameObject.SetActive(false);
        }
        unit_npc.Init();
        unit.SetActive(true);
    }

    private GameObject SpawnAllyUnit()
    {
        GameObject unit = SpawnUnit(Values.Equipos[TeamColor.Azul].Base.transform.position);
        unit.GetComponent<NpcController>().Destino = Values.Equipos[TeamColor.Rojo].Base;
        unit.GetComponent<NpcController>().Team = TeamColor.Azul;
        return unit;
    }

    private GameObject SpawnEnemyUnit()
    {
        GameObject unit = SpawnUnit(Values.Equipos[TeamColor.Rojo].Base.transform.position);
        unit.GetComponent<NpcController>().Destino = Values.Equipos[TeamColor.Azul].Base;
        unit.GetComponent<NpcController>().Team = TeamColor.Rojo;
        return unit;
    }

    GameObject Spawn( bool _isPlayer = false)
    {
        GameObject unit = null;
        if (_isPlayer)
        {
                unit = SpawnAllyUnit();
        }
        else
        {
                unit = SpawnEnemyUnit();
        }
        return unit;
    }

    public void SpawnSoldier(bool _isPlayer = false)
    {
        GameObject unit = Spawn( _isPlayer);
        if (unit) {
            setData(unit, soldier, _isPlayer);
                };
    }

    public void SpawnArcher(bool _isPlayer = false)
    {
        GameObject unit = Spawn( _isPlayer);
        if (unit) setData(unit, archer, _isPlayer);
    }

    public void SpawnSiege(bool _isPlayer = false)
    {
        GameObject unit = Spawn( _isPlayer);
        if (unit) setData(unit, siege, _isPlayer);
    }

    public void PlayerAddQueue(string unit_type)
    {
        if (player_queue.Count < 5)
        {
            switch (unit_type)
            {
                case "Soldier":
                    {
                        var cost = soldier.moneyCost;
                        if (MoneySystem.SINGLETON.Buy(cost, true))
                            player_queue.Enqueue(soldier);
                        break;
                    }
                case "Archer":
                    {
                        var cost = archer.moneyCost;
                        if (MoneySystem.SINGLETON.Buy(cost, true))
                            player_queue.Enqueue(archer);
                        break;
                    }
                case "Siege":
                    {
                        var cost = siege.moneyCost;
                        if (MoneySystem.SINGLETON.Buy(cost, true))
                            player_queue.Enqueue(siege);
                        break;
                    }
            }
            ColaManager.SINGLETON.UpdateQueue();
            if (player_queue.Count > 0)
            {
                player_train_time = player_queue.Peek().train_time;
            }
        }
    }

    public void EnemyAddQueue(string unit_type)
    {
        switch (unit_type)
        {
            case "Soldier":
                {
                    var cost = soldier.moneyCost;
                    if (MoneySystem.SINGLETON.Buy(cost))
                        Enemy_queue.Enqueue(soldier);
                    break;
                }
            case "Archer":
                {
                    var cost = archer.moneyCost;
                    if (MoneySystem.SINGLETON.Buy(cost))
                        Enemy_queue.Enqueue(archer);
                    break;
                }
            case "Siege":
                {
                    var cost = siege.moneyCost;
                    if (MoneySystem.SINGLETON.Buy(cost))
                        Enemy_queue.Enqueue(siege);
                    break;
                }
        }
        if (Enemy_queue.Count > 0)
        {
            enemy_train_time = Enemy_queue.Peek().train_time;
        }
    }

    private void PlayerQueueDispatch()
    {
        switch (player_queue.Peek().name)
        {
            case "Soldier":
                {
                    SpawnSoldier(true);
                    break;
                }
            case "Archer":
                {
                    SpawnArcher(true);
                    break;
                }
            case "Siege":
                {
                    SpawnSiege(true);
                    break;
                }
        }
        player_queue.Dequeue();
        ColaManager.SINGLETON.UpdateQueue();

        if (player_queue.Count > 0)
        {
            player_train_time = player_queue.Peek().train_time;
        }
    }

    private void EnemyQueueDispatch()
    {
        switch (Enemy_queue.Peek().name)
        {
            case "Soldier":
                {
                    SpawnSoldier();
                    break;
                }
            case "Archer":
                {
                    SpawnArcher();
                    break;
                }
            case "Siege":
                {
                    SpawnSiege();
                    break;
                }
        }
        Enemy_queue.Dequeue();
        if(Enemy_queue.Count > 0)
        {
            enemy_train_time = Enemy_queue.Peek().train_time;
        }
    }
}