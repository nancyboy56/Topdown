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
    //had to make a hack around
    [SerializeField] private Dialouges[] dialogue;
    [SerializeField] private TextMeshProUGUI dialogueText;
    
    [SerializeField] private GameObject dialogueBox;

    [SerializeField] private int currentLine = 0;
  
    [SerializeField] private bool dialogueActive = false;
    
    [SerializeField] private int currentCharacter = 0;
    [SerializeField] private int branchType =0;
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
        
        // i just hard coded the dialouge boxes 
        FlipDialouge(false);
        
       // PlayerInput input = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInput>();
        playerhealth= GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        score = GameObject.FindGameObjectWithTag("GM").GetComponent<Scoring>();
        // works be
        //input.actions["Interact"].canceled += DisplayNextLine;
        
        outline.SetActive(true);
        item.SetActive(false);
        
        
    }

    // turns dialouge on or off
    private void FlipDialouge(bool b)
    {
        dialogueBox.SetActive(b);
        dialogueText.gameObject.SetActive(b);
    }

    /*public void DisplayNextLine(InputAction.CallbackContext context)
    {
        Debug.Log(gameObject.name +" phase: + context.phase, dialouge: "+ dialogueActive);
       
       //Debug.Log("phase:" + context.phase, );
       
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
    }*/

    public void DisplayNextLine()
    {
        NextLine();
    }

    void StartDialogue(int branch)
    {
        Debug.Log(gameObject.name);
       
        currentLine = 0;
        currentCharacter = 0;
        branchType = branch;
        

        if (dialogueActive)
        {
            
            NextLine();
        }
        else
        {
            dialogueActive = true;
            FlipDialouge(true);
            StartCoroutine(ShowText());
        }
        
    }
    
    IEnumerator ShowText()
    {
        while (dialogueActive)
        {
            // prints letter by letter
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
        return dialogue[branchType].lines[currentLine];
    }

    void NextLine()
    {
        if (currentLine < dialogue[branchType].lines.Length-1)
        {
            Debug.Log("next lines a go");
            currentLine++;
            currentCharacter = 0;
        }
        else
        {
            Debug.Log("theres no more lines");
            currentCharacter = 0;

            if (branchType == 1)
            {
                if (gift == desire)
                {
                    //correct gift
                    GoodGift();
                }
                else
                {
                    //bad gift
                    
                    BadGift();
                }
            }
        }
        
        
    }

    private void BadGift()
    {
        NextBranch(2);
        gift.transform.SetParent(null, true);
        foreach (Collider2D c in gift.GetComponents<Collider2D>())
        {
            c.enabled = true;
        }
        playerItems.holding = null;
        playerItems = null;
        gift.transform.position = new Vector3(Random.Range(itemRange, -itemRange), Random.Range(itemRange, -itemRange), 0);
        gift = null;
                    
        //do damamge
        playerhealth.TakeDamage(1);
    }

    private void GoodGift()
    {
        NextBranch(3);
        //gift.transform.position = Vector3.zero;
        gift.transform.SetParent(holding.transform, false);
                    
        //not efficent but whatever
        gift.GetComponent<SpriteRenderer>().sortingLayerID = 8;
        gift.transform.localPosition = Vector3.zero;
                    
        playerItems.holding = null;
        playerItems = null;
        outline.SetActive(false);
        item.SetActive(true);
                    
        //update score
        score.AddScore(1);
    }

    private void NextBranch(int i)
    {
        branchType = i;
        currentLine = 0;
        currentCharacter = 0;
    }

    public void GiveObject(GameObject obj, PickUp p)
    {
        gift = obj;
        playerItems = p;

        NextBranch(1);
        //StartDialogue(1);
    }

    void EndDialogue()
    {
        dialogueActive = false;
        FlipDialouge(false);
    }
    

    // when player enters the ghost trigger collider
    public void PlayerEnters()
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

    public void PlayerExits()
    {
        Debug.Log("Exiting");
            
        EndDialogue();
    }

    
}
