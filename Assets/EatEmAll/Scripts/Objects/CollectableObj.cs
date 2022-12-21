using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableObj : MonoBehaviour
{
    [SerializeField] private Collider _collider;
    [SerializeField] private ObjectConfigSO _objectConfig;

    Vector3 _defaultPosition;
    float _defaultScale;


    // helper objects
    private WaitForSeconds _releaseDelayTimer;

    private void Awake()
    {
        _defaultPosition = transform.position;
        _defaultScale = _objectConfig.weight;

        ResetTransform();
    }

    private void Start()
    {
        MeshCollider[] meshColliders = GetComponentsInChildren<MeshCollider>();
        foreach (MeshCollider mc in meshColliders)
        {
            mc.enabled = false;
        }

        
    }

    public int GetWeight() { return _objectConfig.weight; }

    public void PickedUp() 
    { 
        _collider.enabled = false;
    }

    public void Released()
    {
        transform.SetParent(null);
        ResetTransform();

        _collider.enabled = true;
    }

    private void ResetTransform()
    {
        transform.SetLocalPositionAndRotation(_defaultPosition, Quaternion.identity);
        transform.DOScale(_defaultScale, 0.1f);
    }
}
