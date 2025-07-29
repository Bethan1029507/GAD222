using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialogueBox;
    public GameObject continueIndicator;
    public TextMeshProUGUI dialogueText;
    public Button option1Button;
    public Button option2Button;
    public float typingSpeed = 0.05f;
    public bool allowExit = true;

    private string[] currentLines;
    private int currentLineIndex;
    private Coroutine typingCoroutine;
    private bool isTyping;
    private bool cancelTyping;

    private System.Action yesAction;

    public static DialogueManager Instance;

    public System.Action OnDialogueComplete;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        dialogueBox.SetActive(false);

        if (continueIndicator != null)
            continueIndicator.SetActive(false);
    }

    
    private void Update()
    {
        if (dialogueBox.activeInHierarchy)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (isTyping)
                {
                    cancelTyping = true;
                }
                else
                {
                    currentLineIndex++;
                    if (currentLineIndex < currentLines.Length)
                    {
                        StartTyping(currentLines[currentLineIndex]);
                    }
                    else
                    {
                        continueIndicator.SetActive(false);
                        CloseDialogue();
                        OnDialogueComplete?.Invoke();
                    }
                }
            }

            if (allowExit && Input.GetKeyDown(KeyCode.Escape))
            {
                CloseDialogue();
            }
        }
    }

    public void StartDialogue(string[] lines)
    {
        currentLines = lines;
        currentLineIndex = 0;
        dialogueBox.SetActive (true);
        dialogueText.text = "";

        if (continueIndicator != null) continueIndicator.SetActive(false);

        if (option1Button != null)
        {
            option1Button.onClick.RemoveAllListeners();
            option1Button.gameObject.SetActive(false); //Ensure reset
        }

        if (option2Button != null)
        {
            option2Button.onClick.RemoveAllListeners();
            option2Button.gameObject.SetActive(false); //Ensure reset
        }

        StartTyping(currentLines[currentLineIndex]);
    }

    private void StartTyping(string line)
    {
        if (typingCoroutine != null) StopCoroutine(typingCoroutine);

        if (continueIndicator != null)
            continueIndicator.SetActive(true);

        typingCoroutine = StartCoroutine(TypeLine(line));
    }

    IEnumerator TypeLine(string line)
    {
        isTyping = true;
        cancelTyping = false;
        dialogueText.text = "";

        foreach (char letter in line)
        {
            if (cancelTyping)
            {
                dialogueText.text = line;
                break;
            }

            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
        cancelTyping = false;

        //show indicator only if there's more lines or text not finished typing
        if (continueIndicator != null && currentLineIndex < currentLines.Length - 1) 
        {
            continueIndicator.SetActive(true);
        }
    }

    public void CloseDialogue()
    {
        if (typingCoroutine != null) StopCoroutine(typingCoroutine);
        typingCoroutine = null;

        dialogueText.text = "";
        dialogueBox.SetActive(false);

        if (continueIndicator != null)
            continueIndicator.SetActive(false);


        currentLines = null;
        currentLineIndex = 0;
        isTyping = false;
        cancelTyping = false;

        if (option1Button != null)
        {
            option1Button.onClick.RemoveAllListeners();
            option1Button.gameObject.SetActive(false);
        }

        if (option2Button != null)
        {
            option2Button.onClick.RemoveAllListeners();
            option2Button.gameObject.SetActive(false);
        }
    }

    public void StartSceneTransitionPrompt(string prompt, string yesLabel, string noLabel, System.Action onYes)
    {
        CloseDialogue();

        dialogueBox.SetActive(true);
        dialogueText.text = prompt;

        yesAction = onYes;

        if (option1Button != null && option2Button != null)
        {
            option1Button.onClick.RemoveAllListeners();
            option2Button.onClick.RemoveAllListeners();

            option1Button.GetComponentInChildren<TextMeshProUGUI>().text = yesLabel;
            option2Button.GetComponentInChildren<TextMeshProUGUI>().text = noLabel;

            option1Button.gameObject.SetActive(true);
            option2Button.gameObject.SetActive(true);

            option1Button.onClick.AddListener(() =>
            {
                yesAction?.Invoke();
                CloseDialogue();
            });

            option2Button.onClick.AddListener(() =>
            {
                CloseDialogue();
            });
        }
    }
}
