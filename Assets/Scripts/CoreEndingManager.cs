using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CoreEndingManager : MonoBehaviour
{
    public string[] oblivionDialogue;
    public string[] submissionDialogue;
    public string[] rebirthDialogue;

    public GameObject endingScreen;
    public TextMeshProUGUI endingText;
    public Button playAgainButton;

    void Start()
    {
        string ending = PlayerPrefs.GetString("FinalEnding", "Submission");

        DialogueManager.Instance.OnDialogueComplete = ShowEndingScreen;

        switch (ending)
        {
            case "Oblivion":
                DialogueManager.Instance.StartDialogue(oblivionDialogue);
                break;
            case "Submission":
                DialogueManager.Instance.StartDialogue(submissionDialogue);
                break;
            case "Rebirth":
                DialogueManager.Instance.StartDialogue(rebirthDialogue);
                break;
        }
    }
    void ShowEndingScreen()
    {
        endingScreen.SetActive(true);
 
        string ending = PlayerPrefs.GetString("FinalEnding", "Submission");
        endingText.text = $"You reached the {ending} ending.";
 
        playAgainButton.onClick.RemoveAllListeners();
        playAgainButton.onClick.AddListener(() =>
        {
            // Optional: Reset GameStateManager
            GameStateManager.Instance.oblivionChoices = 0;
            GameStateManager.Instance.submissionChoices = 0;
            GameStateManager.Instance.rebirthChoices = 0;
            GameStateManager.Instance.talkedToDoctor = false;
            GameStateManager.Instance.talkedToReceptionist = false;
            GameStateManager.Instance.talkedToWhisperingPatient = false;
 
            UnityEngine.SceneManagement.SceneManager.LoadScene("House"); 
        });
    }
}
