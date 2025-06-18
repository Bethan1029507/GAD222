using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NPCDialogueChoice : MonoBehaviour
{
    public GameObject dialogueBox;
    public TextMeshProUGUI dialogueText;
    public Button option1Button;
    public Button option2Button;
    public TextMeshProUGUI option1Text;
    public TextMeshProUGUI option2Text;

    public string initialLine = "Do you trust me?";
    public string option1Line = "You said yes.";
    public string option2Line = "You said no.";

    private bool playerInRange;

    void Start()
    {
        dialogueBox.SetActive(false);
        option1Button.gameObject.SetActive(true);
        option2Button.gameObject.SetActive(true);
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            ShowDialogue();
        }
    }

    void ShowDialogue()
    {
        dialogueBox.SetActive(true);
        dialogueText.text = initialLine;

        // Show buttons
        option1Button.gameObject.SetActive(true);
        option2Button.gameObject.SetActive(true);

        // Set text
        option1Text.text = "Yes";
        option2Text.text = "No";

        // Clear old listeners
        option1Button.onClick.RemoveAllListeners();
        option2Button.onClick.RemoveAllListeners();

        // Add new listeners
        option1Button.onClick.AddListener(() => ChooseOption(option1Line));
        option2Button.onClick.AddListener(() => ChooseOption(option2Line));
    }

    void ChooseOption(string response)
    {
        dialogueText.text = response;

        // Hide buttons after choice
        option1Button.gameObject.SetActive(false);
        option2Button.gameObject.SetActive(false);
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
            option1Button.gameObject.SetActive(true);
            option2Button.gameObject.SetActive(true);
        }
    }
}
