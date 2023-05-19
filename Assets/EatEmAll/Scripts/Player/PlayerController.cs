using eea;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] PlayerMovement _playerMovement;
    [SerializeField] PlayerCollector _playerCollector;

    private int _speedBoostLevel;
    private int _maxCarryWeightLevel;
    private int _maxItemWeightLevel;

    private const string SpeedLevelS = "SpeedLevel";
    private const string CarryWeightLevelS = "CarryWeightLevel";
    private const string ItemWeightCapLevelS = "ItemWeightCapLevel";


    private void Awake()
    {
        _speedBoostLevel = PlayerPrefs.GetInt(SpeedLevelS);
        _maxCarryWeightLevel = PlayerPrefs.GetInt(CarryWeightLevelS);
        _maxItemWeightLevel = PlayerPrefs.GetInt(ItemWeightCapLevelS);
    }

    private void Start()
    {
        UpdateMovement();
        UpdateCollector();
    }

    private void UpdateMovement()
    {
        _playerMovement.SetSpeedOffset(_speedBoostLevel);
        PlayerPrefs.SetInt(SpeedLevelS, _speedBoostLevel);
    }

    private void UpdateCollector()
    {
        _playerCollector?.SetMaxCarryWeightOffset(_maxCarryWeightLevel);
        PlayerPrefs.SetInt(CarryWeightLevelS, _maxCarryWeightLevel);

        _playerCollector?.SetMaxItemWeightOffset(_maxItemWeightLevel);
        PlayerPrefs.SetInt(ItemWeightCapLevelS, _maxItemWeightLevel);
    }

    public void UpgradeSpeed()
    {
        _speedBoostLevel++;
        UpdateMovement();
    }

    public void UpgradeCarryWeight()
    {
        _maxCarryWeightLevel++;
        UpdateCollector();
    }

    public void UpgradeItemWeight()
    {
        _maxItemWeightLevel++;
        UpdateCollector();
    }
}
