using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/ObjectConfigList")]
public class ObjectConfigListSO : ScriptableObject
{
    [SerializeField] public List<ObjectConfigSO> objConfigList;

    public ObjectConfigSO GetConfig(float objRadius)
    {
        int radius = (int)objRadius;
        if (radius < objConfigList.Count)
            return objConfigList[radius];
        return objConfigList[objConfigList.Count - 1];
    }
}
