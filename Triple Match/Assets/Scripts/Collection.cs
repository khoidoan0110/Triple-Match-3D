using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collection : MonoBehaviour
{
    private const int SLOTS = 7;
    private List<ICollectionItem> mItems = new List<ICollectionItem>();

    public event Action<ICollectionItem> ItemAdded;
    public event Action<ICollectionItem> ItemRemoved;
    public event Action<ICollectionItem> ItemRequired;

    [SerializeField] private WinCondition winCondition;

    public void RemoveItem(ICollectionItem item)
    {
        if (mItems.Contains(item))
        {
            mItems.Remove(item);
            ItemRemoved?.Invoke(item);
        }
    }

    public void AddItem(ICollectionItem item)
    {
        if (mItems.Count < SLOTS)
        {
            Collider collider = (item as MonoBehaviour)?.GetComponent<Collider>();
            if (collider != null && collider.enabled)
            {
                collider.enabled = false;

                ItemAdded?.Invoke(item);

                // Add the item to the list
                mItems.Add(item);

                // Disable item
                item.OnCollect();

                // Check 3
                StartCoroutine(CheckForMatches());
            }
        }
    }

    public void AddRequiredItem(ICollectionItem item)
    {
        ItemRequired?.Invoke(item);
    }

    private IEnumerator CheckForMatches()
    {
        Dictionary<string, int> itemCounts = new Dictionary<string, int>();

        // count occurences of each item
        foreach (ICollectionItem item in mItems)
        {
            if (itemCounts.ContainsKey(item.Name))
            {
                itemCounts[item.Name]++;
            }
            else
            {
                itemCounts[item.Name] = 1;
            }
        }

        // add unique items with count >=3 to a list
        List<ICollectionItem> itemsToRemove = new List<ICollectionItem>();
        foreach (ICollectionItem item in mItems)
        {
            if (itemCounts[item.Name] >= 3)
            {
                itemsToRemove.Add(item);
            }
        }

        // Check Game Over
        if (mItems.Count == SLOTS && itemsToRemove.Count == 0)
        {
            winCondition.StartGameOverSequence(false);
        }
        
        // remove items with >= 3 count from list
        yield return new WaitForSeconds(0.5f);
        int deleteCount = 0;
        foreach (ICollectionItem item in itemsToRemove)
        {
            RemoveItem(item);
            deleteCount++;
        }
        if(deleteCount == 3){
            AudioManager.instance.PlaySFX("Point", 1f);
            deleteCount = 0;
        }
        

    }
}
