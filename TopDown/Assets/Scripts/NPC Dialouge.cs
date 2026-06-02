using System;
using System.Collections;

using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class NPCDialouge : MonoBehaviour
{
    //cant seralise a dictionrary, or a 2d array
    [SerializeField]
    private Dialouges[] dialogue;
    [SerializeField]
    private TextMeshProUGUI dialogueText;
    [SerializeField]
    private GameObject dialogueBox;

    [SerializeField] private int currentLine = 0;
  
    [SerializeField] private bool dialogueActive = false;
    
    [SerializeField] private int currentCharacter = 0;
    [SerializeField] private bool isPrinting = false;
    [SerializeField] private int lineType =0;
    [SerializeField] private GameObject desire;
    [SerializeField] private GameObject gift;
    [SerializeField] private bool giveDesire;

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
            Debug.Log(CallLine());
            NextLine();
        }
        else
        {
            Debug.Log("no printing");
        }
    }

    void StartDialogue(int line)
    {
        Debug.Log("starting dialouge;");
        currentLine = 0;
        currentCharacter = 0;
        lineType = line;
        if (!dialogueActive)
        {
            StartCoroutine(ShowText());
            dialogueActive = true;
            dialogueBox.SetActive(true);
        }
       
    }

    IEnumerator ShowText()
    {
        while (dialogueActive)
        {
            isPrinting = true;
            dialogueText.text = CallLine().Substring(0, currentCharacter);
            if (currentCharacter < CallLine().Length)
            {
                currentCharacter++;
            }
            
            yield return new WaitForSeconds(textSpeed);
        }
        
        isPrinting = false;
        
    }

    private string CallLine()
    {
        return dialogue[lineType].lines[currentLine];
    }

    void NextLine()
    {
        if (currentLine < CallLine().Length-1)
        {
            currentLine++;
            currentCharacter = 0;
        }
        else
        {
            Debug.Log("theres no more lines");
            currentCharacter = 0;

            if (lineType == 1)
            {
                if (gift == desire)
                {
                    //correct gift
                    StartDialogue(3);
                    
                }
                else
                {
                    //bad gift
                    
                    StartDialogue(2);
                }
            }
        }
        
        
    }

    public void GiveObject(GameObject obj)
    {
        gift = obj;
        StartDialogue(1);
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
            if (giveDesire)
            {
                StartDialogue(3);
            }
            else
            {
                StartDialogue(0);
            }
            
            
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
