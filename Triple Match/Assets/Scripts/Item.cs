using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, ICollectionItem
{
    [SerializeField] private string ItemName;
    [SerializeField] private Sprite ItemImage;
    public string Name{
        get {
            return ItemName;
        }
    }

    public Sprite Image{
        get {
            return ItemImage;
        }
    }

    public void OnCollect()
    {
        gameObject.SetActive(false);
    }

}
