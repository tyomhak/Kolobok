using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance { get; private set; }
    private int _money;

    public delegate void MoneyChanged();
    public event MoneyChanged OnMoneyChanged;

    [Space]
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI _moneyCollectedUI;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Debug.Log("Second MoneyManager::Awake(). Destroying Object");
            DestroyImmediate(gameObject);
        }

        _money = PlayerPrefs.GetInt("Money");
        _moneyCollectedUI.text = _money.ToString();
    }

    public bool CanAfford(int amount)
    {
        if (amount <= _money)
            return true;

        return false;
    }

    public void Spend(int amount)
    {
        _money -= amount;
        SaveMoney();

        OnMoneyChanged?.Invoke();
    }

    public void AddAmount(int amount)
    {
        _money += amount;
        SaveMoney();

        OnMoneyChanged?.Invoke();
    }

    private void SaveMoney() 
    { 
        PlayerPrefs.SetInt("Money", _money);
        _moneyCollectedUI.text = _money.ToString();
    }
}
