using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class PlayerCollector : MonoBehaviour
{
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CollectableObj"))
        {
            if (other.gameObject.TryGetComponent<CollectableObj>(out CollectableObj colObj))
            {
                int objWeight = colObj.Weight();
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
        transform.DOScale(1f + ((float)weightOffset) / 10f, 0.2f);
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
}
