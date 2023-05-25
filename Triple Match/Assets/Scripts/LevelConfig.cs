using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelConfig : MonoBehaviour
{
    [SerializeField] private List<GameObject> requiredSlots;
    [SerializeField] private int numberOfRequiredSlots;
    [SerializeField] private TextMeshProUGUI levelText;
    private int level;

    void Start(){
        level = 1;
        levelText.text = level.ToString();
        for (int i = 0; i < numberOfRequiredSlots; i++){
            requiredSlots[i].SetActive(true);
        }
    }

    public void OnValidate(){
        numberOfRequiredSlots = Mathf.Clamp(numberOfRequiredSlots, 1, 5);
    }
}
