using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectField : MonoBehaviour
{
    [SerializeField] float m_Width;
    [SerializeField] float m_Height;

    [SerializeField] List<ObjectSetup> m_ObjectTypes;

    private void Start()
    {
        float widthPerType = m_Width / m_ObjectTypes.Count;
        float heightPerType = m_Height / m_ObjectTypes.Count;

        for (int typeIndex = 0; typeIndex < m_ObjectTypes.Count; typeIndex++)
        {
            ObjectSetup objectSetup = m_ObjectTypes[typeIndex];

            //float widthInterval = widthPerType / objectSetup.
        }
    }


    [Serializable]
    public class ObjectSetup
    {
        public Transform _objectPrefab;
        public int _count;
        public int _scale;
    }
}
