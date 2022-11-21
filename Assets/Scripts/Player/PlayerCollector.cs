using Cinemachine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class PlayerCollector : MonoBehaviour
{
    [SerializeField] private Cinemachine.CinemachineVirtualCamera _camera_cm;
    [SerializeField] private bool _shiftCameraPosition;
    CinemachineTransposer _cam_cm_transposer;
    Vector3 _followOffsetBase;

    [SerializeField] private int _maxCarryWeightBase;
    private int _maxCarryWeight;
    private int _currCarryWeight;

    [SerializeField] private int _maxItemWeightBase;
    private int _maxItemWeight;

    private Stack<Transform> _inventoryItems;

    private void Awake()
    {
        _inventoryItems = new Stack<Transform>();
        _currCarryWeight = 0;
    }

    private void Start()
    {
        if (_shiftCameraPosition)
        {
            _cam_cm_transposer = _camera_cm.GetCinemachineComponent<CinemachineTransposer>();
            _followOffsetBase = _cam_cm_transposer.m_FollowOffset;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CollectableObj"))
        {
            if (other.gameObject.TryGetComponent<CollectableObj>(out CollectableObj colObj))
            {
                int objWeight = colObj.GetWeight();
                if (CanCarry(objWeight))
                {
                    _currCarryWeight += objWeight;
                    colObj.transform.SetParent(transform, true);
                    colObj.PickedUp();

                    _inventoryItems.Push(colObj.transform);
                }
            }
        }
    }


    private bool CanCarry(int weight)
    {
        if (weight <= _maxItemWeight && weight <= _maxCarryWeight - _currCarryWeight)
            return true;
        return false;
    }

    public void SetMaxCarryWeightOffset(int weightOffset)
    {
        _maxCarryWeight = _maxCarryWeightBase + weightOffset;
    }

    public void SetMaxItemWeightOffset(int weightOffset)
    {
        _maxItemWeight = _maxItemWeightBase + weightOffset;
        transform.DOScale(1f + ((float)weightOffset), 0.2f);

        if (_shiftCameraPosition)
            _camera_zoom_out(weightOffset, 0.2f);
    }


    public Stack<Transform> GetInventoryItems()
    {
        return _inventoryItems;
    }

    public void ResetInventory()
    {
        _inventoryItems.Clear();
        _currCarryWeight = 0;
    }


    private void _camera_zoom_out(float camDistanceOffset, float scaleDuration)
    {
        if (camDistanceOffset <= 1f)
            camDistanceOffset = 1f;

        var transposer = _camera_cm.GetCinemachineComponent<CinemachineTransposer>();
        var newOffset = _followOffsetBase* camDistanceOffset;
        //transposer.m_FollowOffset = newOffset;
        StartCoroutine(_camera_zoom(newOffset));
    }

    IEnumerator _camera_zoom(Vector3 newFollowOffset)
    {

        while (_cam_cm_transposer.m_FollowOffset != newFollowOffset)
        {
            _cam_cm_transposer.m_FollowOffset = Vector3.Slerp(_cam_cm_transposer.m_FollowOffset, newFollowOffset, 0.1f);
            yield return new WaitForSeconds(0.01f);
        }
    }
}
