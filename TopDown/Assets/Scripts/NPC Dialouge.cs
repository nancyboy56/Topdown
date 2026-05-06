using System;
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class NPCDialouge : MonoBehaviour
{
    [SerializeField]
    private string[] dialogue;
    [SerializeField]
    private TextMeshProUGUI dialogueText;
    [SerializeField]
    private GameObject dialogueBox;

    private int currentLine = 0;
    private bool playerInRange = false;
    private bool dialogueActive = false;
    
    private int currentCharacter = 0;

    [SerializeField, Range(0,2)] private float textSpeed = 1;
  

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
            Debug.Log(dialogue[currentLine]);
            NextLine();
        }
    }

    void StartDialogue()
    {
        Debug.Log("starting dialouge;");
        dialogueActive = true;
        dialogueBox.SetActive(true);
        currentLine = 0;
        currentCharacter = 0;
        
       // dialogueText.text = dialogue[currentLine];
       StartCoroutine(ShowText());
    }

    IEnumerator ShowText()
    {
        
        while (currentCharacter < dialogue[currentLine].Length)
        {
            dialogueText.text = dialogue[currentLine].Substring(0, currentCharacter);
            currentCharacter++;
            yield return new WaitForSeconds(textSpeed);
        }
        
    }

    void NextLine()
    {
        currentLine++;

        if (currentLine >= dialogue.Length)
        {
            EndDialogue();
        }
        else
        {
            currentCharacter = 0;
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
