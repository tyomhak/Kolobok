using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableObj : MonoBehaviour
{
    [SerializeField] private Collider _collider;
    [SerializeField] private ObjectConfigSO _objectConfig;

    private void Start()
    {
        MeshCollider[] meshColliders = GetComponentsInChildren<MeshCollider>();
        foreach (MeshCollider mc in meshColliders)
        {
            mc.enabled = false;
        }
    }

    public int Weight() { return _objectConfig.weight; }

    public void PickedUp() { _collider.enabled = false; }
}
