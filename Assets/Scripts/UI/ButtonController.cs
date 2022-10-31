using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private GameObject _enabledBackground;
    [SerializeField] private Button _button;

    public void SetButtonActive(bool isActive)
    {
        _enabledBackground.SetActive(isActive);
        _button.enabled = isActive;
    }
}
