using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class WinCondition : MonoBehaviour
{
    [SerializeField] private List<GameObject> requirementSlots;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private Timer timer;

    private int requiredRemaining = 0;
    private bool gameEnded = false;

    void Start()
    {
        CountActiveSlots();
    }

    void Update()
    {
        if (!gameEnded && requiredRemaining <= 0)
        {
            gameEnded = true;
            StartGameOverSequence(true);
        }
    }

    public void StartGameOverSequence(bool win)
    {
        winPanel.SetActive(true);
        gameOverText.text = win ? "You Win" : "Game Over";
        AudioManager.instance.StopMusic();
        AudioManager.instance.PlaySFX(win ? "Win" : "Lose", 1f);
        timer.PauseTimer();
    }

    public void SlotDeactivated()
    {
        requiredRemaining--;
    }

    private void CountActiveSlots()
    {
        for (int i = 0; i < requirementSlots.Count; i++)
        {
            if (requirementSlots[i].activeSelf)
            {
                requiredRemaining++;
            }
        }
    }
}
