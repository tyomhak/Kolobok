using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFieldGenerator : MonoBehaviour
{
    
    [SerializeField] Transform _gameObjectPrefab;

    [SerializeField] int _countHor;
    [SerializeField] int _countVert;


    [Space]
    [SerializeField] GridGeneration m_GridGenMethod;

    [Header("Dynamic Grid Params")]
    [SerializeField] float m_Width;
    [SerializeField] float m_Length;

    [Header("Fixed Grid Params")]
    [SerializeField] float _distance;

    

    private void Start()
    {
        if (m_GridGenMethod == GridGeneration.FixedDistance)
            CreateObjectFieldOld();
        else
            CreateObjectField();
    }

    private void CreateObjectField()
    {
        if (_countHor == 0 || _countVert == 0)
            return;

        Vector3 position = transform.position;
        Vector3 originOffset = Vector3.zero;

        float widthInterval = m_Width / _countHor;
        float lengthInterval = m_Length / _countVert;

        for (int row = 0; row < _countHor; ++row)
        {
            originOffset.z = row * lengthInterval;
            for (int col = 0; col < _countVert; ++col)
            {
                originOffset.x = col * widthInterval;
                position = transform.position + originOffset;
                Instantiate(_gameObjectPrefab, position, Quaternion.identity);
            }
        }

    }

    private void CreateObjectFieldOld()
    {
        Vector3 position = transform.position;
        Vector3 originOffset = Vector3.zero;
        for (int row = 0; row < _countVert; ++row)
        {
            originOffset.z = row * _distance;
            for (int col = 0; col < _countHor; ++col)
            {
                originOffset.x = col * _distance;
                position = transform.position + originOffset;
                Instantiate(_gameObjectPrefab, position, Quaternion.identity);
            }
        }
    }


    enum GridGeneration
    {
        FixedDistance,
        Dynamic
    }
}
