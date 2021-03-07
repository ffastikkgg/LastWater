using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerUpgrade : MonoBehaviour
{
    [SerializeField] private int upgradeInitialCost;
    [SerializeField] private int upgradeCostIncremental;
    [SerializeField] private float damageIncremental;
    [SerializeField] private float delayReduce;


    [Header("Sell")]
    [Range(0,1)]
    [SerializeField] private float sellPert;

    public float SellPerc { get; set; }
    public int UpgradeCost { get; set; }
    public int Level { get; set; }

    private TowerProjectiles towerProjectile;

    // Start is called before the first frame update
    void Start()
    {
        towerProjectile = GetComponent<TowerProjectiles>();
        UpgradeCost = upgradeInitialCost;

        SellPerc = sellPert;
        Level = 1;
    }


    public void UpgradeTower()
    {
        if (CurrencySystem.Instance.TotalCoins >= UpgradeCost)
        {
            towerProjectile.Damage += damageIncremental;
            towerProjectile.DelayPerShot -= delayReduce;
            UpdateUpgrade();
        }
    }


    public int GetSellValue()
    { 
        int sellValue = Mathf.RoundToInt(UpgradeCost * SellPerc);
        return sellValue;
    }

    private void UpdateUpgrade()
    {
        CurrencySystem.Instance.RemoveCoins(UpgradeCost);
        UpgradeCost += upgradeCostIncremental;
        Level++;
    }

}


