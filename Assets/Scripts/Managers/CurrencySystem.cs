using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class CurrencySystem : Singleton<CurrencySystem>
{
    [SerializeField] private int coinTest;


    private string CURRENCY_SAVE_KEY = "MYGAME_CURRENCY";


   public int TotalCoins { get; set; }


    private void Start()
    {
        PlayerPrefs.DeleteKey(CURRENCY_SAVE_KEY);
        LoadCoins();
    }

    private void LoadCoins()
    {
        TotalCoins = PlayerPrefs.GetInt(CURRENCY_SAVE_KEY , coinTest);
    }


    public void AddCoins (int amount)
    {
        TotalCoins += amount;
        PlayerPrefs.SetInt(CURRENCY_SAVE_KEY, TotalCoins);
        PlayerPrefs.Save();
    }

    public void RemoveCoins(int amount)
    {
        if (TotalCoins >= amount)
        {
            TotalCoins -= amount;
            PlayerPrefs.SetInt(CURRENCY_SAVE_KEY, TotalCoins);
            PlayerPrefs.Save();
        }
    }

    private int RandomAdd()
    {
        int add = UnityEngine.Random.Range(1, 3);
            return add;
    }

    private void AddCoins(Enemy enemy)
    {
        AddCoins(RandomAdd());
    }

    private void OnEnable()
    {
        EnemyHealth.OnEnemyKilled += AddCoins;
    }

    private void OnDisable()
    {
        EnemyHealth.OnEnemyKilled -= AddCoins;
    }

}
