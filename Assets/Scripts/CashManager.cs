using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CashManager : MonoBehaviour
{
    public static CashManager instance;

    private int coins;
    private string keyCoins = "keyCoins";

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        LoadCash();
        DisplayCoins();
    }

    public bool TryBuyThisUnit(int price)
    {
        if (GetCoins() >= price)
        {
            SpendCoin(price);
            return true;
        }
        return false;
    }

    public int GetCoins()
    {
        return coins;
    }

    public void AddCoin(int price)
    {
        coins += price;
        DisplayCoins();
    }

    public void SpendCoin(int price)
    {
        coins -= price;
        DisplayCoins();
    }

    public void ExchangeProduct(ProductData productData)
    {
        AddCoin(productData.productPrice);
    }

    private void DisplayCoins()
    {
        UIManager.instance.ShowCoinCountOnScreen(coins);
        SaveCash();
    }

    private void SaveCash()
    {
        PlayerPrefs.SetInt(keyCoins, coins);
    }

    private void LoadCash()
    {
        coins = PlayerPrefs.GetInt(keyCoins, 0); // zero is default here in case there is no coin at the beginning
    }
}
