using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Rigidbody _rb;

    [Space]
    [Header("Movement")]
    [SerializeField] private float _moveSpeedBase;
    private float _moveSpeed;
    private float _moveSpeedSqr;
    private Vector3 _moveDirection;

    private void Awake()
    {
        _moveDirection = Vector3.zero;
        SetSpeedOffset(0);
    }

    private void Update()
    {
        float xDirInput = SimpleInput.GetAxis("Horizontal");
        float yDirInput = SimpleInput.GetAxis("Vertical");

        _moveDirection = new Vector3(xDirInput, 0, yDirInput);
    }

    private void FixedUpdate()
    {
        if (_moveDirection != Vector3.zero)
        {
            _rb.AddForce(_moveDirection * _moveSpeed, ForceMode.Force);

            // limit speed to maxSpeed
            if (_rb.velocity.sqrMagnitude > _moveSpeedSqr)
                _rb.velocity = _rb.velocity.normalized * _moveSpeed;
        }
        else
            _rb.velocity = _rb.velocity * 0.95f;
    }

    public void SetSpeedOffset(float speedBoost)
    {
        _moveSpeed = _moveSpeedBase + speedBoost;
        _moveSpeedSqr = _moveSpeed * _moveSpeed;
    }
}
