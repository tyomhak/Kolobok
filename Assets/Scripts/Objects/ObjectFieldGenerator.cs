using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFieldGenerator : MonoBehaviour
{
    [SerializeField] Transform _gameObjectPrefab;

    [SerializeField] int _countHor;
    [SerializeField] int _countVert;

    [SerializeField] float _distance;

    private void Start()
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
}
