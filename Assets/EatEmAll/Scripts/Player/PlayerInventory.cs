using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System;


namespace eea
{
    //public interface IInventory<ObjectType>
    //{
    //    public void AddObject(ObjectType obj);
    //    public void RemoveObject(ObjectType obj);

    //    public void ApplyAction(Action<ObjectType> someAction);
    //}

    //public class InventoryDict : IInventory<CollectableObj>
    //{
    //    private Dictionary<int, CollectableObj> m_InventoryDict;

    //    public void AddObject(CollectableObj obj)
    //    {

    //    }

    //    public void RemoveObject(CollectableObj obj)
    //    {

    //    }

    //    public void ApplyAction(Action<CollectableObj> someAction)
    //    {
    //        foreach (var item in m_InventoryDict)
    //        {
    //            someAction(item.Value);
    //        }
    //    }
    //}

    public class PlayerInventory : MonoBehaviour
    {
        [Header("Inventory Config")]
        [SerializeField] int m_MaxCapacity = 10;
        [SerializeField] int m_MaxItemLevel = 1;

        [Space]
        [Header("Object Consumption")]

        [SerializeField] [Min(0f)] 
        float consumptionSpeed = 0.2f;

        [SerializeField] [Min(0.01f)] 
        float consumptionDistance = 0.2f;


        private List<CollectableObj> m_CollectedObjects;


        private void Awake()
        {
            m_CollectedObjects = new List<CollectableObj>();
        }

        private void OnEnable()
        {
            if (TryGetComponent<PlayerObjCollection>(out var ObjCollectionComonent))
            {
                ObjCollectionComonent.OnObjCollected += OnObjectCollision;
            }
        }

        private void OnDisable()
        {
            if (TryGetComponent<PlayerObjCollection>(out var ObjCollectionComonent))
            {
                ObjCollectionComonent.OnObjCollected -= OnObjectCollision;
            }
        }

        public void CollectObject(CollectableObj obj)
        {
            if (!IsFull() && m_MaxItemLevel >= obj.GetWeight())
            {
                obj.PickedUp();
                m_CollectedObjects.Add(obj);
                obj.transform.SetParent(transform, true);
            }
        }

        private void ConsumeObject(CollectableObj obj)
        {
            if (m_CollectedObjects.Contains(obj))
            {
                m_CollectedObjects.Remove(obj);
                obj.Released();
            }
        }

        private void OnObjectCollision(ObjCollectionData data)
        {
            CollectObject(data.obj);
        }

        bool IsFull()
        {
            return (m_MaxCapacity - m_CollectedObjects.Count) == 0;
        }


        private void Update()
        {
            List<CollectableObj> toRemove = new List<CollectableObj>();

            foreach (var obj in m_CollectedObjects)
            {
                obj.transform.Translate((transform.position - obj.transform.position) * consumptionSpeed * Time.deltaTime, Space.World);
                if (Vector3.Magnitude(obj.transform.position - transform.position) < consumptionDistance)
                {
                    toRemove.Add(obj);
                }
            }

            foreach (var obj in toRemove)
            {
                ConsumeObject(obj);
            }

        }
    }
}
