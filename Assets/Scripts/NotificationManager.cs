using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject notificationCanvas;
    private GameSession gameSession;

    private void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
        HideNotification();
    }

    private void Update()
    {
        if (gameSession.GetScores() > 200)
        {
            ShowNotification();
        }
    }

    public void ShowNotification()
    {
        notificationCanvas.SetActive(true);
    }

    public void HideNotification()
    {
        notificationCanvas.SetActive(false);
    }
}
