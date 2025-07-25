using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionTrigger : MonoBehaviour
{
    public string transitionPrompt = "";
    public string yesText = "Yes";
    public string noText = "No";
    public string targetScene;
    public string targetSpawnPoint;

    public GameObject promptUI;

    private bool playerInRange;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            DialogueManager.Instance.StartSceneTransitionPrompt(
                transitionPrompt,
                yesText,
                noText,
                () => TransitionToScene()
            );
        }
    }

    void TransitionToScene()
    {
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
