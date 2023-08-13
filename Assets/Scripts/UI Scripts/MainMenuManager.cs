using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    
    public void playGame()
    {
        SceneManager.LoadScene("MainScene");//Events for the related buttons
    }

    public void exitGame()
    {
        Application.Quit();//Events for the related buttons
    }
}
