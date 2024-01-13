using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelExit : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (currentSceneIndex == 2)
        {
            Debug.Log("Level 2");
            var currentGameSession = FindObjectOfType<GameSession>();
            if (currentGameSession != null)
            {
                //TODO: code test
                currentGameSession.SetScore(currentGameSession.GetScores() + 1000);

                if (currentGameSession.GetScores() >= 1000)
                    currentGameSession.BonusLive();
            }
        }

        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
