using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class NPCDialouge : MonoBehaviour
{
    [SerializeField]
    private string[] dialogueLines;
    [SerializeField]
    private TextMeshProUGUI dialogueText;
    [SerializeField]
    private GameObject dialogueBox;

    private int currentLine = 0;
    private bool playerInRange = false;
    private bool dialogueActive = false;
    

    void Start()
    {
        // get dialouge lines to read from a file
        
        dialogueBox.SetActive(false);
        
        PlayerInput input = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInput>();
        input.actions["Interact"].performed += DisplayNextLine;
    }

    public void DisplayNextLine(InputAction.CallbackContext context)
    {
       
        if (context.canceled && dialogueActive)
        {
            Debug.Log(dialogueLines[currentLine]);
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
