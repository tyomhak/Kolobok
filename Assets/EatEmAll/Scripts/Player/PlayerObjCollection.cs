using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace eea
{
    public struct ObjCollectionData
    {
        public CollectableObj obj;

        public ObjCollectionData(CollectableObj obj)
        {
            this.obj = obj;
        }
    }

    [RequireComponent(typeof(Collider))]
    public class PlayerObjCollection : MonoBehaviour
    {
        public delegate void ObjCollection(ObjCollectionData data);
        public ObjCollection OnObjCollected;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent<CollectableObj>(out CollectableObj colObj))
            {
                OnObjCollected?.Invoke(new ObjCollectionData(colObj));
            }
        }
    }
}
