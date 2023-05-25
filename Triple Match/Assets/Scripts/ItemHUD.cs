using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ItemHUD : MonoBehaviour
{
    [SerializeField] private Collection collection;
    [SerializeField] private Transform inventoryPanel;
    [SerializeField] private RectTransform canvasRectTransform;
    private void OnEnable()
    {
        collection.ItemAdded += Collection_ItemAdded;
        collection.ItemRemoved += Collection_ItemRemoved;
    }

    private void OnDisable()
    {
        collection.ItemAdded -= Collection_ItemAdded;
        collection.ItemRemoved -= Collection_ItemRemoved;
    }


    private void Collection_ItemAdded(ICollectionItem item)
    {
        // Instantiate a new Image object
        GameObject spriteObject = new GameObject("ItemSprite");
        Image newImage = spriteObject.AddComponent<Image>();

        // Set the sprite from the item's Image property
        newImage.sprite = item.Image;

        // Set the position to the mouse position in screen space
        RectTransform rectTransform = spriteObject.GetComponent<RectTransform>();
        Vector2 canvasPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRectTransform, Input.mousePosition, null, out canvasPosition);
        rectTransform.anchoredPosition = canvasPosition;

        // Set the Canvas as the parent of the sprite object
        spriteObject.transform.SetParent(canvasRectTransform, false);

        // Iterate through each slot
        foreach (Transform slot in inventoryPanel)
        {
            Image itemImage = slot.GetChild(0).GetChild(0).GetComponent<Image>();
            if (!itemImage.enabled)
            {
                // Animate the sprite to the slot's position
                rectTransform.DOMove(slot.GetComponent<RectTransform>().anchoredPosition + new Vector2(0f, 120f), 0.2f)
                    .OnComplete(() =>
                    {
                        // Set the sprite to the slot's image and disable the temporary sprite object
                        itemImage.sprite = item.Image;
                        itemImage.enabled = true;
                        Destroy(spriteObject);
                    });

                break;
            }
        }
    }

    private void Collection_ItemRemoved(ICollectionItem item)
    {
        // Iterate through each slot
        foreach (Transform slot in inventoryPanel)
        {
            Image itemImage = slot.GetChild(0).GetChild(0).GetComponent<Image>();
            if (itemImage.sprite == item.Image)
            {
                itemImage.sprite = null;
                itemImage.enabled = false;
            }
        }
    }
}
