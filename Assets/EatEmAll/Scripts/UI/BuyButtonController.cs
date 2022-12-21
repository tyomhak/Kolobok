using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuyButtonController : ButtonController
{
    [SerializeField] private TextMeshProUGUI _buttonText;
    [SerializeField] private TextMeshProUGUI _priceText;

    public void SetText(string text) { _buttonText.text = text; }
    public void SetPrice(float price) { _priceText.text = price.ToString("F2"); }
    public void SetPrice(int price) { _priceText.text = price.ToString(); }
}
