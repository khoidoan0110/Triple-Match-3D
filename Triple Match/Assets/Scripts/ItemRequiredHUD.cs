using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemRequiredHUD : MonoBehaviour
{
    [SerializeField] private Collection collection;
    [SerializeField] private Transform itemRequiredPanel;
    [SerializeField] private WinCondition WinCondition;

    private void OnEnable()
    {
        collection.ItemAdded += Collection_ItemRequiredFound;
        collection.ItemRequired += Collection_ItemRequiredAdded;
    }

    private void OnDisable()
    {
        collection.ItemAdded -= Collection_ItemRequiredFound;
        collection.ItemRequired -= Collection_ItemRequiredAdded;
    }

    private void Collection_ItemRequiredFound(ICollectionItem item)
    {
        foreach (Transform slot in itemRequiredPanel)
        {
            if (slot.gameObject.activeSelf)
            {
                Image itemImage = slot.GetChild(0).GetChild(0).GetComponent<Image>();
                if (itemImage.sprite == item.Image)
                {
                    TextMeshProUGUI requiredAmountTxt = slot.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>();
                    int amount = int.Parse(requiredAmountTxt.text);
                    if (amount > 1)
                    {
                        --amount;
                        requiredAmountTxt.text = amount.ToString();
                    }
                    else
                    {
                        slot.gameObject.SetActive(false);
                        WinCondition.SlotDeactivated();
                    }
                }
            }
        }
    }

    private void Collection_ItemRequiredAdded(ICollectionItem item)
    {
        foreach (Transform slot in itemRequiredPanel)
        {
            if (slot.gameObject.activeSelf)
            {
                Image itemImage = slot.GetChild(0).GetChild(0).GetComponent<Image>();
                if (!itemImage.enabled)
                {
                    itemImage.sprite = item.Image;
                    itemImage.enabled = true;
                    break;
                }
            }
        }
    }
}
