using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerEndGame : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene("Win");
        }
    }
}
