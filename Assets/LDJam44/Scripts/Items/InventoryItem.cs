using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "item", menuName ="items/item")]
public class InventoryItem : ScriptableObject
{
    public GameObject itemPrefab = null;

    public string name = "Item";
    public Sprite image;


}