using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CoreDoorTrigger : MonoBehaviour
{
    public string transitionPrompt = "Are you ready to enter the Core?";
    public string yesText = "Enter";
    public string noText = "Not Yet";
    public string targetScene = "Core";
    public string targetSpawnPoint = "CoreSpawn";

    [TextArea] public string[] lockedDialogue = { "A strange force prevents you from proceeding..." };

    public GameObject promptUI;

    private bool playerInRange;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (GameStateManager.Instance.talkedToReceptionist &&
                GameStateManager.Instance.talkedToWhisperingPatient &&
                GameStateManager.Instance.talkedToDoctor)
            {
                DialogueManager.Instance.StartSceneTransitionPrompt(
                    transitionPrompt,
                    yesText,
                    noText,
                    OnPlayerConfirmedEntry
                );
            }
            else
            {
                DialogueManager.Instance.StartDialogue(lockedDialogue);
            }
        }
    }

    void OnPlayerConfirmedEntry()
    {
        //store current ending here
        string ending = GameStateManager.Instance.DetermineEnding();
        PlayerPrefs.SetString("FinalEnding", ending); // Save ending type to be used in Core scene
        PlayerSpawnManager.spawnPointName = targetSpawnPoint;
        SceneManager.LoadScene(targetScene);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            playerInRange = true;

            if (promptUI != null)
                promptUI.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            playerInRange = false;

            if (promptUI != null)
                promptUI.SetActive(false);
        }
    }
}
