using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    public GameObject dialogueBox;
    public TextMeshProUGUI dialogueText;
    public Button option1Button;
    public Button option2Button;
    public float typingSpeed = 0.05f;

    private string[] currentLines;
    private int currentLineIndex;
    private Coroutine typingCoroutine;
    private bool isTyping;
    private bool cancelTyping;

    public static DialogueManager Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        dialogueBox.SetActive(false);
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
                }
            }

            if (Input.GetKeyDown(KeyCode.Escape))
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

        if (option1Button != null) option1Button.gameObject.SetActive(false);
        if (option2Button != null) option2Button.gameObject.SetActive(false);

        StartTyping(currentLines[currentLineIndex]);
    }

    private void StartTyping(string line)
    {
        if (typingCoroutine != null) StopCoroutine(typingCoroutine);
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
    }

    public void CloseDialogue()
    {
        if (typingCoroutine != null) StopCoroutine(typingCoroutine);
        typingCoroutine = null;

        dialogueText.text = "";
        dialogueBox.SetActive (false);
        currentLines = null;
        currentLineIndex = 0;
        isTyping = false;
        cancelTyping = false;
    }
}
