using UnityEngine;

public class NPCDialouge : MonoBehaviour
{
    public string[] dialogueLines;
    public TextMeshProUGUI dialogueText;
    public GameObject dialogueBox;

    private int currentLine = 0;
    private bool playerInRange = false;
    private bool dialogueActive = false;

    void Start()
    {
        dialogueBox.SetActive(false);
    }

    public void DisplayNextLine()
    {
        if (dialogueActive && Input.GetKeyDown(KeyCode.Space))
        {
            NextLine();
        }
    }

    void StartDialogue()
    {
        dialogueActive = true;
        dialogueBox.SetActive(true);
        currentLine = 0;
        dialogueText.text = dialogueLines[currentLine];
    }

    void NextLine()
    {
        currentLine++;

        if (currentLine >= dialogueLines.Length)
        {
            EndDialogue();
        }
        else
        {
            dialogueText.text = dialogueLines[currentLine];
        }
    }

    void EndDialogue()
    {
        dialogueActive = false;
        dialogueBox.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            if (!dialogueActive)
            {
                StartDialogue();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            EndDialogue();
        }
    }
}
