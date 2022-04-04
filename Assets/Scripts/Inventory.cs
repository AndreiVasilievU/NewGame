using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private GameObject UICoinPrefab;
    [SerializeField] private Transform parent;

    public void AddToInventory()
    {
        Instantiate(UICoinPrefab, parent);
    }
}
