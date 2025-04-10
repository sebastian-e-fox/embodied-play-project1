using UnityEngine;

public class NPCTrigger : MonoBehaviour
{
    public GameObject talkPromptUI;
    public DialogueManager dialogueManager;
    private bool isPlayerNear;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            talkPromptUI.SetActive(true);
            isPlayerNear = true;
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && !dialogueManager.isTalking)
        {
            talkPromptUI.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            talkPromptUI.SetActive(false);
            isPlayerNear = false;
        }
    }

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.F) && dialogueManager.isTalking == false)
        {
            talkPromptUI.SetActive(false);
            dialogueManager.StartDialogue();
        }
    }
}
