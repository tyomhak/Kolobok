using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

        if (_collider == null)
        {
            Collider[] colliderList = GetComponents<Collider>();
            if (colliderList.Length == 1)
            {
                _collider = colliderList[0];

                //foreach (Collider col in colliderList)
                //    col.isTrigger = true;
            }
            else
            {
                foreach (Collider col in colliderList)
                    col.enabled = false;

                SphereCollider collider = transform.AddComponent<SphereCollider>();
                collider.radius = collider.radius * 0.6f;
                _collider = collider;
            }

            //if (TryGetComponent<Collider>(out Collider[] colliders))
            //{
            //    _collider = GetComponent<Collider>();
            //}
            //else
            //{
            //    _collider = transform.AddComponent<SphereCollider>();
            //}

            _collider.isTrigger = true;
        }

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
