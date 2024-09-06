using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class UIButtons : MonoBehaviour
{
    public void RestartButton()
    {
        SceneManager.LoadScene(0);
    }
    public void StartButton()
    {
        GameManager.onGameStarted?.Invoke();
        UIController.instance.currentScore = 1;
    }
    public void ShopButton()
    {
        
    }
    public void ExitButton()
    {
        Application.Quit();
    }
}
