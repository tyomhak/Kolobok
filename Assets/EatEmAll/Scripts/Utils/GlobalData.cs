using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalData : MonoBehaviour
{
    public static GlobalData Instance { get; private set; }

    [SerializeField] private ObjectConfigListSO objConfigListSO;

    [SerializeField] private bool scaleFromConfig;

    private void OnEnable()
    {
        if (Instance == null || Instance == this)
            Instance = this;
        else
            Destroy(this);
    }

    private void OnDisable()
    {
        if (Instance == this)
            Instance = null;
    }

    public ObjectConfigSO GetObjectConfig(float objectRadius)
    {
        return objConfigListSO?.GetConfig(objectRadius);
    }

    public bool ScaleFromConfig()
    {
        return scaleFromConfig;
    }
}
