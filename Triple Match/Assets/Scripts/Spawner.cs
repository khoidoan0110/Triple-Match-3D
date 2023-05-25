using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] objectsToSpawn;
    [SerializeField] private int numberToSpawn;
    private List<ICollectionItem> uniqueItemList;
    [SerializeField] private Collection collection;

    void Start()
    {
        uniqueItemList = new List<ICollectionItem>();
        StartSpawn();
        int minActiveSlots = Mathf.Min(uniqueItemList.Count, 5);

        PlayerPrefs.SetInt("minActiveSlots", minActiveSlots);
        for (int i = 0; i < minActiveSlots; ++i){
            collection.AddRequiredItem(uniqueItemList[i]);
        }
    }

    private void StartSpawn()
    {
        int numGroups = numberToSpawn / 3;

        for (int i = 0; i < numGroups; i++)
        {
            SpawnGroupOfObjects();
        }

        int remainingObjects = numberToSpawn % 3;
        if (remainingObjects > 0)
        {
            SpawnGroupOfObjects(remainingObjects);
        }
    }

    private void SpawnGroupOfObjects(int groupSize = 3)
    {
        int randomPrefabIndex = Random.Range(0, objectsToSpawn.Length);

        for (int i = 0; i < groupSize; i++)
        {
            Vector3 randomSpawnPosition = new Vector3(Random.Range(-4, 4), 2, Random.Range(-4, 4));
            GameObject spawnedObj = Instantiate(objectsToSpawn[randomPrefabIndex], randomSpawnPosition, Quaternion.Euler(5f, 0f, 0f));

            ICollectionItem item = spawnedObj.GetComponent<ICollectionItem>();
            bool isDuplicate = false;

            // Check if the item name already exists in mItems
            foreach (ICollectionItem existingItem in uniqueItemList)
            {
                if (existingItem.Name == item.Name)
                {
                    isDuplicate = true;
                    break;
                }
            }

            if (!isDuplicate)
            {
                uniqueItemList.Add(item);
            }
        }
    }
}
