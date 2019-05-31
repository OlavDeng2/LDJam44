using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, IInventoryItem
{
    public string name;
    public Sprite image;

    public string Name
    {
        get
        {
            return name;
        }
    }

    public Sprite Image
    {
        get
        {
            return image;
        }
    }

    public void OnDrop()
    {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        gameObject.SetActive(true);
        gameObject.transform.position = worldPoint;

        Debug.Log("dropping item");
    }

    public void OnPickup()
    {
        gameObject.SetActive(false);
    }

    public virtual void OnUse()
    {
        throw new System.NotImplementedException();
    }
}
