using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemDrops 
{
    // Permet de définir les chances de drops de chaque item sur unity
    public GameObject itemDropPrefab;
    public Item item;
    [Range(0, 100)] public float dropChance;
}
