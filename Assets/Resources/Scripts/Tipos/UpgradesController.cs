using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UpgradesController: MonoBehaviour
{
    public static UpgradesController SINGLETON;
    /*
     * List
     * 0 - attack
     * 1 - defenze
     * 2 - income
     * */
    public List<int> playerUpgrades = new List<int>(3);
    public List<int> enemyUpgrades = new List<int>(3);
    public List<int> UpgradeCosts;
    void Awake()
    {
        SINGLETON = this;
        Init();
    }
    public void Init()
    {
        playerUpgrades = new List<int>(3) { 0, 0, 0 };
        enemyUpgrades = new List<int>(3) { 0, 0, 0 };
        if (UpgradeCosts == null)
        {
            UpgradeCosts = new List<int>(3) { 0, 0, 0 };
        }
    }
    // Update is called once per frame

    public void BuyUpgrade(int type, bool isPlayer = false)
    {
        if (isPlayer)
        {
            bool condition = MoneySystem.SINGLETON.Buy(UpgradeCosts[type] * (playerUpgrades[type] + 1), true);
            playerUpgrades[type] += (condition) ? 1 : 0;
        }
        else
        {
            bool condition = MoneySystem.SINGLETON.Buy(UpgradeCosts[type] * (enemyUpgrades[type]+ 1));
            enemyUpgrades[type] += (condition) ? 1 : 0;
        }
    }

    public void BuyUpgradeButtom(int type)
    {
        BuyUpgrade(type, true);
    }
}
