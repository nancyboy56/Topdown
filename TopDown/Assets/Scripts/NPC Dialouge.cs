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

    [SerializeField] private int currentLine = 0;
  
    [SerializeField] private bool dialogueActive = false;
    
    [SerializeField] private int currentCharacter = 0;
    [SerializeField] private bool isPrinting = false;

    [SerializeField, Range(0,2)] private float textSpeed = 1;
  

    void Start()
    {
        // get dialouge lines to read from a file
        Debug.Log("ghost npcs");
        dialogueBox.SetActive(false);
        
        PlayerInput input = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInput>();
        
        // works be
        input.actions["Interact"].canceled += DisplayNextLine;
        
        
    }

    public void DisplayNextLine(InputAction.CallbackContext context)
    {
       
       Debug.Log("phase:" + context.phase );
       
        if (context.canceled && dialogueActive)
        {
            Debug.Log("printing.....]");
            Debug.Log(dialogue[currentLine]);
            NextLine();
        }
        else
        {
            Debug.Log("no printing");
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
        
        while (dialogueActive)
        {
            isPrinting = true;
            dialogueText.text = dialogue[currentLine].Substring(0, currentCharacter);
            if (currentCharacter < dialogue[currentLine].Length)
            {
                currentCharacter++;
            }
            
            yield return new WaitForSeconds(textSpeed);
        }
        
        isPrinting = false;
        
    }

    void NextLine()
    {
        if (currentLine < dialogue.Length-1)
        {
            currentLine++;
            currentCharacter = 0;
        }
        else
        {
            Debug.Log("theres no more lines");
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
            
            StartDialogue();
            
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Exiting");
            
            EndDialogue();
        }
    }
}
