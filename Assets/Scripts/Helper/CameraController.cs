using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform _target;
    private Vector3 _offset;

    [SerializeField] private float moveSpeed = 10f;
    

    private void Start()
    {
       SetValues();
    }

    private void SetValues()
    {
        _offset = transform.position;
        _target = FindObjectOfType<PlayerController>().transform;

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        FollowTarget();
    }

    private void FollowTarget()
    {
        transform.position = Vector3.Lerp
            (transform.position, _target.position + _offset, moveSpeed * Time.deltaTime);

        if (transform.position.y<_offset.y)
        {
            transform.position = new Vector3(transform.position.x, _offset.y, transform.position.z);
        }
    }
}
