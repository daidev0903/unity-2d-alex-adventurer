using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public float volumeChangeAmount = 0.1f;

    public void StartLevel()
    {
        SceneManager.LoadScene(1);
    }
    public void LoadMainMenuLevel()
    {
        SceneManager.LoadScene(0);
        FindObjectOfType<GameSession>().ResetGameSession();
    }
    public void SettingGame()
    {
        SceneManager.LoadScene(4);
    }
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }


    public void IncreaseVolume()
    {
        AudioListener.volume += volumeChangeAmount;
    }

    public void DecreaseVolume()
    {
        AudioListener.volume -= volumeChangeAmount;
    }
}
