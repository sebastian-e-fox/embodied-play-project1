using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject credits;
    public void StartGame()
    {
        SceneManager.LoadScene("Game"); // Replace with your game scene name
    }

    public void OpenCredits()
    {
        // Load settings scene or open UI panel
        Debug.Log("Settings clicked!");
        credits.SetActive(true);
    }

    public void CloseCredits()
    {
        Debug.Log("Settings closed!");
        credits.SetActive(false);
    }

    public void QuitGame()
    {
        Debug.Log("Quit clicked!");
        Application.Quit();
    }
}
