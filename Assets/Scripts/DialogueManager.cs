using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialogueUI;
    public Text dialogueText;
    public Animator npcAnimator;

    public List<string> dialogueLines;
    public int currentLineIndex = 0;
    public bool isTalking = false;
    private void Start()
    {
        dialogueUI.SetActive(false);
    }

    void Update()
    {
        if (isTalking && Input.GetKeyDown(KeyCode.F))
        {
            ShowNextLine();
        }
    }

    public void StartDialogue()
    {
        npcAnimator.SetTrigger("StartTalking"); // Optional NPC animation
        dialogueUI.SetActive(true);
        isTalking = true;
        currentLineIndex = 0;
        dialogueText.text = dialogueLines[currentLineIndex];
    }

    void ShowNextLine()
    {
        currentLineIndex++;
        Debug.Log("Next Line Index: " + currentLineIndex);

        if (currentLineIndex >= dialogueLines.Count)
        {
            Debug.Log("Ending dialogue");
            isTalking = false;
            EndDialogue();
        }
        else
        {
            Debug.Log("Showing: " + dialogueLines[currentLineIndex]);
            dialogueText.text = dialogueLines[currentLineIndex];
        }
    }


    void EndDialogue()
    {
        npcAnimator.SetTrigger("StopTalking");
        dialogueUI.SetActive(false);
        isTalking = false;
    }
}
