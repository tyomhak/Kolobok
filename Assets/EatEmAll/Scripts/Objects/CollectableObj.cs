using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CollectableObj : MonoBehaviour
{
    public int ID { get; private set; }
    private static int __global__ID = 0;

    [SerializeField] private Collider _collider;
    [SerializeField] private ObjectConfigSO _objectConfig;

    Vector3 _defaultPosition;
    float _defaultScale;

    private void Awake()
    {
        ID = ++__global__ID;

        _defaultPosition = transform.position;
        RemoveParent();
        SetupCollider();
    }

    private void Start()
    {
        SetupConfig();
        ResetTransform();
    }

    private void RemoveParent()
    {
        transform.SetParent(null);
    }

    private void SetupCollider()
    {
        RemoveChildColliders();

        if (_collider == null)
        {
            Collider[] colliderList = GetComponents<Collider>();
            if (colliderList.Length == 1)
            {
                _collider = colliderList[0];
            }
            else
            {
                foreach (Collider col in colliderList)
                    col.enabled = false;

                MeshCollider collider = transform.AddComponent<MeshCollider>();
                collider.convex = true;

                _collider = collider;
            }

            _collider.enabled = true;
            _collider.isTrigger = true;
        }
    }

    private void RemoveChildColliders()
    {
        Collider[] meshColliders = GetComponentsInChildren<Collider>();
        foreach (Collider col in meshColliders)
        {
            col.enabled = false;
        }
    }

    private void SetupConfig()
    {
        if (_objectConfig == null && _collider != null)
        {
            float radius = 0;
            if (_collider is SphereCollider)
                radius = ((SphereCollider)_collider).radius;
            else if (_collider is BoxCollider)
                radius = ((BoxCollider)_collider).size.x;
            else
            {
                var bounds = GetComponent<Renderer>().bounds;
                var size = bounds.size;
                radius = Mathf.Max(size.x, size.y, size.z);
            }

            if (GlobalData.Instance)
                _objectConfig = GlobalData.Instance.GetObjectConfig(radius);
        }

        if (_objectConfig && (GlobalData.Instance != null ? GlobalData.Instance.ScaleFromConfig() : true))
        {
            _defaultScale = _objectConfig.weight;
        }
        else
            _defaultScale = transform.localScale.x;
    }

    public int GetWeight() 
    { 
        return _objectConfig.weight; 
    }

    public void PickedUp() 
    {
        transform.DOKill();
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
        transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        transform.SetLocalPositionAndRotation(_defaultPosition, Quaternion.identity);
        transform.DOScale(_defaultScale, 0.1f);
    }
}
