using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMover : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody _rigidbody;
    private float directionX, directionY;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            directionX = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
            directionY = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        }
        else
        {
            directionX = 0;
            directionY = 0;
        }
    }

    private void FixedUpdate()
    {
        Rotate();
        _rigidbody.velocity = new Vector3(directionX,0,directionY) * Time.fixedTime;
    }

    private void Rotate()
    {
        var hAxis = directionX;
        var vAxis = directionY;

        if (hAxis == 0 && vAxis == 0)
        {
            return;
        }
        
        var zAxis = Mathf.Atan2(hAxis , vAxis) * Mathf.Rad2Deg;
        _rigidbody.rotation = Quaternion.Euler(0, zAxis, 0);
    }
}
