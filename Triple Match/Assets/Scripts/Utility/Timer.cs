using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public float timeValue;
    public TextMeshProUGUI timerText;
    private bool timerActive = true;
    [SerializeField] private WinCondition winCondition;

    void Update()
    {
        timerText.color = Color.white;
        if (timerActive)
        {
            StartTimer();
            DisplayTime(timeValue);
        }
        if (Mathf.RoundToInt(timeValue) <= 10)
        {
            timerText.color = Color.red;
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        if (timeToDisplay < 0)
        {
            timeToDisplay = 0;
            winCondition.StartGameOverSequence(false);
        }
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void StartTimer()
    {
        if (timeValue > 0)
        {
            timeValue -= Time.deltaTime;
        }
        else
        {
            timeValue = 0;
        }
    }

    public void PauseTimer()
    {
        timerActive = false;
    }
}
