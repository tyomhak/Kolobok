using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;

public class PlayerUpgrader : MonoBehaviour
{
    private MoneyManager _moneyManager;

    [Header("Config")]
    [SerializeField] private PlayerController _playerController;

    [Space]
    [Header("UI")]
    [SerializeField] private GameObject _upgradesStoreUI;
    [SerializeField] private BuyButtonController _buySpeedBtn;
    [SerializeField] private BuyButtonController _buyMaxCarryWeightBtn;
    [SerializeField] private BuyButtonController _buyMaxItemWeightBtn;

    [SerializeField] private int _speedPriceDefault;
    [SerializeField] private int _carryWeightPriceDefault;
    [SerializeField] private int _itemWeightPriceDefault;
    
    private int _speedPrice;
    private int _carryWeightPrice;
    private int _itemWeightPrice;

    private const string SpeedPriceS = "SpeedPrice";
    private const string CarryWeightPriceS = "CarryWeightPrice";
    private const string ItemWeightPriceS = "ItemWeightPrice";

    private void Awake()
    {
        LoadPricingData();
    }

    private void Start()
    {
        _moneyManager = MoneyManager.Instance;
        _moneyManager.OnMoneyChanged += UpdateShopUI;
        UpdateShopUI();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _upgradesStoreUI.SetActive(true);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _upgradesStoreUI.SetActive(false);
        }
    }

    #region UI_onClick
    public void BuySpeed()
    {
        if (_moneyManager.CanAfford(_speedPrice))
        {
            _playerController.UpgradeSpeed();

            _moneyManager.Spend(_speedPrice);
            _speedPrice *= 2;
            SavePricingData();

            UpdateShopUI();
        }
    }

    public void BuyCarryWeight()
    {
        if (_moneyManager.CanAfford(_carryWeightPrice))
        {
            _playerController.UpgradeCarryWeight();

            _moneyManager.Spend(_carryWeightPrice);
            _carryWeightPrice *= 2;
            SavePricingData();

            UpdateShopUI();
        }
    }

    public void BuyItemWeight()
    {
        if (_moneyManager.CanAfford(_itemWeightPrice))
        {
            _playerController.UpgradeItemWeight();

            _moneyManager.Spend(_itemWeightPrice);
            _itemWeightPrice *= 2;
            SavePricingData();

            UpdateShopUI();
        }
    }
    #endregion

    private void UpdateShopUI()
    {
        UpdateShopPricingUI();
        UpdateShopButtonsUI();
    }

    private void UpdateShopButtonsUI()
    {
        _buySpeedBtn.SetButtonActive(_moneyManager.CanAfford(_speedPrice));
        _buyMaxCarryWeightBtn.SetButtonActive(_moneyManager.CanAfford(_carryWeightPrice));
        _buyMaxItemWeightBtn.SetButtonActive(_moneyManager.CanAfford(_itemWeightPrice));
    }

    private void UpdateShopPricingUI()
    {
        _buySpeedBtn.SetPrice(_speedPrice);
        _buyMaxCarryWeightBtn.SetPrice(_carryWeightPrice);
        _buyMaxItemWeightBtn.SetPrice(_itemWeightPrice);
    }

    private void LoadPricingData()
    {
        _speedPrice = PlayerPrefs.GetInt(SpeedPriceS, _speedPriceDefault);
        _carryWeightPrice = PlayerPrefs.GetInt(CarryWeightPriceS, _carryWeightPriceDefault);
        _itemWeightPrice = PlayerPrefs.GetInt(ItemWeightPriceS, _itemWeightPriceDefault);
    }

    private void SavePricingData()
    {
        PlayerPrefs.SetInt(SpeedPriceS, _speedPrice);
        PlayerPrefs.SetInt(CarryWeightPriceS, _carryWeightPrice);
        PlayerPrefs.SetInt(ItemWeightPriceS, _itemWeightPrice);
    }
}
