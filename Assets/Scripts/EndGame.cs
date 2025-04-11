using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Game"); // Replace with your game scene name
    }

    public void QuitGame()
    {
        Debug.Log("Quit clicked!");
        Application.Quit();
    }
}
