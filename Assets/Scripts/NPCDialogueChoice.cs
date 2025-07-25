using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NPCDialogueChoice : MonoBehaviour
{
    [System.Flags]
    public enum Alignment
    {
        None = 0,
        Oblivion = 1 << 0,
        Submission = 1 << 1,
        Rebirth = 1 << 2
    }

    public GameObject dialogueBox;
    public TextMeshProUGUI dialogueText;
    public Button option1Button;
    public Button option2Button;
    public TextMeshProUGUI option1Text;
    public TextMeshProUGUI option2Text;

    [TextArea] public string initialLine;
    [TextArea] public string playerLineOption1;
    [TextArea] public string playerLineOption2;
    [TextArea] public string[] npcResponseOption1;
    [TextArea] public string[] npcResponseOption2;

    public string option1Label = "Option 1";
    public string option2Label = "Option 2";

    public Alignment option1Alignment = Alignment.None;
    public Alignment option2Alignment = Alignment.None;

    public string npcKey = "Receptionist";

    public GameObject promptUI;

    private bool playerInRange;
 
    private bool hasTalkedToNPC = false;
    private int lastChoiceMade = 0;

    void Start()
    {
        dialogueBox.SetActive(false);
        option1Button.gameObject.SetActive(true);
        option2Button.gameObject.SetActive(true);

        // Check if this NPC has already been talked to
        switch (npcKey)
        {
            case "Receptionist": hasTalkedToNPC = GameStateManager.Instance.talkedToReceptionist; break;
            case "Doctor": hasTalkedToNPC = GameStateManager.Instance.talkedToDoctor; break;
            case "WhisperingPatient": hasTalkedToNPC = GameStateManager.Instance.talkedToWhisperingPatient; break;
        }
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E) && !dialogueBox.activeInHierarchy)
        {
            if (hasTalkedToNPC)
                ShowRepeatResponse();
            else
                ShowInitialDialogue();
        }
    }

    void ShowInitialDialogue()
    {
        dialogueBox.SetActive(true);
        dialogueText.text = initialLine;

        option1Button.onClick.RemoveAllListeners();
        option2Button.onClick.RemoveAllListeners();

        // Show buttons
        option1Button.gameObject.SetActive(true);
        option2Button.gameObject.SetActive(true);

        // Set text
        option1Text.text = option1Label;
        option2Text.text = option2Label;

        // Add new listeners
        option1Button.onClick.AddListener(() => HandleChoice(1));
        option2Button.onClick.AddListener(() => HandleChoice(2));
    }

    void HandleChoice(int choice)
    {
        option1Button.gameObject.SetActive(false);
        option2Button.gameObject.SetActive(false);

        lastChoiceMade = choice;

        string playerLine;
        string[] npcResponse;

        if (choice == 1)
        {
            ApplyAlignment(option1Alignment);
            playerLine = playerLineOption1;
            npcResponse = npcResponseOption1;
        }
        else
        {
            ApplyAlignment(option2Alignment);
            playerLine = playerLineOption2;
            npcResponse = npcResponseOption2;
        }

        MarkNPCAsCompleted();
        hasTalkedToNPC = true;

        // Combine player line and NPC response lines into one string array
        string[] linesToShow = new string[1 + npcResponse.Length];
        linesToShow[0] = playerLine;
        for (int i = 0; i < npcResponse.Length; i++)
        {
            linesToShow[i + 1] = npcResponse[i];
        }

        DialogueManager.Instance.StartDialogue(linesToShow);
    }

    void ApplyAlignment(Alignment alignment)
    {
        if ((alignment & Alignment.Oblivion) != 0)
            GameStateManager.Instance.oblivionChoices++;

        if ((alignment & Alignment.Submission) != 0)
            GameStateManager.Instance.submissionChoices++;

        if ((alignment & Alignment.Rebirth) != 0)
            GameStateManager.Instance.rebirthChoices++;
    }

    void MarkNPCAsCompleted()
    {
        switch (npcKey)
        {
            case "Receptionist": GameStateManager.Instance.talkedToReceptionist = true; break;
            case "Doctor": GameStateManager.Instance.talkedToDoctor = true; break;
            case "WhisperingPatient": GameStateManager.Instance.talkedToWhisperingPatient = true; break;
        }
    }

    void ShowRepeatResponse()
    {
        dialogueBox.SetActive(true);
        option1Button.onClick.RemoveAllListeners();
        option2Button.onClick.RemoveAllListeners();

        option1Button.gameObject.SetActive(false);
        option2Button.gameObject.SetActive(false);

        string[] responseLines = lastChoiceMade == 1 ? npcResponseOption1 : npcResponseOption2;
        DialogueManager.Instance.StartDialogue(responseLines);
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
            dialogueBox.SetActive(false);

            if (promptUI != null)
                promptUI.SetActive(false);
        }
    }
}
