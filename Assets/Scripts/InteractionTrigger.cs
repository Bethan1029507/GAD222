using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InteractionTrigger : MonoBehaviour
{
    public GameObject dialogueBox;
    public TextMeshProUGUI dialogueText;
    public string dialogueLine = "This is a test interaction.";

    public Button option1Button;
    public Button option2Button;
    
    private bool playerInRange;
        
    // Update is called once per frame
    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            dialogueBox.SetActive(true);
            dialogueText.text = dialogueLine;

            //disable choice buttons
            if (option1Button != null) option1Button.gameObject.SetActive(false);
            if (option2Button != null) option2Button.gameObject.SetActive(false);
        }

        // Close dialogue with Escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            dialogueBox.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            dialogueBox.SetActive(false);
        }
    }
}
