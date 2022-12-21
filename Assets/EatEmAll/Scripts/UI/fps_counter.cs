using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class fps_counter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _text_ui;

    [SerializeField] float _timer_max;
    float _timer_curr;

    private void Awake()
    {
        _timer_curr = _timer_max;
    }

    private void Update()
    {
        _timer_curr -= Time.deltaTime;

        if (_timer_curr <= 0)
        {
            _timer_curr = _timer_max;

            _text_ui.text = (1f / Time.unscaledDeltaTime).ToString("F0");
        }
    }

}
