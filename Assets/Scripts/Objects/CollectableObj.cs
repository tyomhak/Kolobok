using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableObj : MonoBehaviour
{
    [SerializeField] private Collider _collider;
    [SerializeField] private ObjectConfigSO _objectConfig;

    Vector3 _defaultPosition;
    Vector3 _defaultScale;


    // helper objects
    private WaitForSeconds _releaseDelayTimer;

    private void Start()
    {
        MeshCollider[] meshColliders = GetComponentsInChildren<MeshCollider>();
        foreach (MeshCollider mc in meshColliders)
        {
            mc.enabled = false;
        }

        _defaultPosition = transform.position;
        _defaultScale = transform.localScale;
    }

    public int GetWeight() { return _objectConfig.weight; }

    public void PickedUp() 
    { 
        _collider.enabled = false;
    }

    public void Released()
    {
        transform.SetParent(null);
        transform.SetLocalPositionAndRotation(_defaultPosition, Quaternion.identity);
        transform.DOScale(_defaultScale.x, 0.1f);
        _collider.enabled = true;
    }
}
