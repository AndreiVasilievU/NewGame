using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private float angle;
    void Update()
    {
        transform.RotateAround(transform.position, transform.up, angle * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameManager.Instance.AddCoin();
            gameObject.SetActive(false);
        }
    }
}
