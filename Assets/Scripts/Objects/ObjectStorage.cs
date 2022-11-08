using DG.Tweening;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectStorage : MonoBehaviour
{
    [SerializeField] private Transform _suckPoint;

    private MoneyManager _moneyManager;

    private void Start()
    {
        _moneyManager = MoneyManager.Instance;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.TryGetComponent<PlayerCollector>(out PlayerCollector playerCollector))
            {
                Stack<Transform> itemsStack = playerCollector.GetInventoryItems();
                while (itemsStack.Count > 0)
                {
                    Transform item = itemsStack.Pop();
                    float duration = Vector3.Distance(item.position, _suckPoint.position);
                    item.DOMove(_suckPoint.position, duration);
                    item.DOScale(0.2f, duration);
                    CollectableObj itemObject = item.GetComponent<CollectableObj>();
                    itemObject.Invoke("Released", duration + 0.1f);

                    _moneyManager.AddAmount(item.GetComponent<CollectableObj>().GetWeight() * 2);
                    //Destroy(item.gameObject, duration);
                }

                playerCollector.ResetInventory();
            }
        }
    }
}
