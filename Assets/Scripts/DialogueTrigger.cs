using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public GameObject promptUI;
    
    public string[] dialogueLines;

    private bool playerInRange;

    void Update()
    {
       if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            DialogueManager.Instance.StartDialogue(dialogueLines);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;

            if (promptUI != null)
                promptUI.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            
            if (promptUI != null)
                promptUI.SetActive(false);

            DialogueManager.Instance.CloseDialogue();
        }
    }
}
