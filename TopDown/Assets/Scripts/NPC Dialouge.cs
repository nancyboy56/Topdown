using System;
using System.Collections;

using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using Random = UnityEngine.Random;

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
    [SerializeField] private int lineType =0;
    [SerializeField] private GameObject desire;
    [SerializeField] private GameObject gift;
    [SerializeField] private bool giveDesire;

    [SerializeField] private GameObject outline;
    [SerializeField] private GameObject item;
    
    [SerializeField] private GameObject holding;
    [SerializeField] private PickUp playerItems;

    [SerializeField, Range(0,2)] private float textSpeed = 0.3f;
    [SerializeField, Range(0, 30)] private float itemRange = 10;

    [SerializeField] private Health playerhealth;
    [SerializeField] private Scoring score;
   
  

    void Start()
    {
        // get dialouge lines to read from a file
        Debug.Log("ghost npcs");
        dialogueBox.SetActive(false);
        
        PlayerInput input = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInput>();
        playerhealth= GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        score = GameObject.FindGameObjectWithTag("GM").GetComponent<Scoring>();
        // works be
        input.actions["Interact"].canceled += DisplayNextLine;
        
        outline.SetActive(true);
        item.SetActive(false);
        
        
    }

    public void DisplayNextLine(InputAction.CallbackContext context)
    {
        Debug.Log(gameObject.name);
       
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
        Debug.Log(gameObject.name);
        Debug.Log("starting dialouge;");
        currentLine = 0;
        currentCharacter = 0;
        lineType = line;
        Debug.Log("diag:"+ dialogueActive);

        if (dialogueActive)
        {
            NextLine();
        }
        else
        {
            dialogueActive = true;
            dialogueBox.SetActive(true);
            StartCoroutine(ShowText());
        }
        
        



    }

    IEnumerator ShowText()
    {
        Debug.Log("ieieie");
        while (dialogueActive)
        {
            Debug.Log("Prinitng next letter");
            Debug.Log(CallLine().Substring(0, currentCharacter));
            dialogueText.text = CallLine().Substring(0, currentCharacter);
            if (currentCharacter < CallLine().Length)
            {
                currentCharacter++;
            }
            
            yield return new WaitForSeconds(textSpeed);
        }
        
        
        
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
                    gift.transform.position = Vector3.zero;
                    gift.transform.SetParent(holding.transform, true);
                    playerItems.holding = null;
                    playerItems = null;
                    outline.SetActive(false);
                    item.SetActive(true);
                    
                    //update score
                    score.AddScore(1);
                }
                else
                {
                    //bad gift
                    
                    StartDialogue(2);
                    gift.transform.SetParent(holding.transform, true);
                    playerItems.holding = null;
                    playerItems = null;
                    gift.transform.position = new Vector3(Random.Range(itemRange, -itemRange), Random.Range(itemRange, -itemRange), 0);
                    gift = null;
                    
                    //do damamge
                    playerhealth.TakeDamage(1);
                }
            }
        }
        
        
    }

    public void GiveObject(GameObject obj, PickUp p)
    {
        gift = obj;
        playerItems = p;
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
                Debug.Log("statoing");
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
