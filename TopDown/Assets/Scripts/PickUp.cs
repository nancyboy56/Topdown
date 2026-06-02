using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PickUp : MonoBehaviour
{
    [SerializeField] GameObject holdSpot;
    [SerializeField] public GameObject holding;
    [SerializeField] GameObject canHold;

    [SerializeField] private bool canPickUp;
    [SerializeField] private bool canGive;
    [SerializeField] private NPCDialouge npc;

    [SerializeField] private float throwSpeed = 3;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerInput input = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInput>();
        
        // works be
        input.actions["Pickup"].canceled += ItemInteract;
    }


    public void ItemInteract(InputAction.CallbackContext context)
    {
        Debug.Log("pressing to hold object");
        if (context.canceled )
        {
            if (canHold != null)
            {
                Debug.Log("can hold object");
                if (holding == null)
                {
                    
                    PickUpObject();
                    
                }
                else
                {
                    if (canGive)
                    {
                        Give();
                    }
                    else
                    {
                        Throw();
                        PickUpObject();
                    }
                    
                }
            }
            else
            {
                if (canGive && holding !=null)
                {
                    Give();
                }
                else
                {
                    // throws when it has nothing to hold
                    Throw();
                }
               
            }
        }
    }

    private void Give()
    {
        npc.GiveObject(holding, this);
    }

    private void PickUpObject()
    {
        holding = canHold;
        canHold = null;
        
        // i think this has to be a ridbody body at some point 
        holdSpot.transform.position = Vector3.zero;
        holding.transform.SetParent(holdSpot.transform, true);
        foreach (BoxCollider2D box in holding.GetComponentsInChildren<BoxCollider2D>())
        {
            box.enabled = false;
        }
        
    }

    // i want to throw the object in the direction of the player
    //do later
    private void Throw()
    {
        holding.transform.position = transform.position + Vector3.down * throwSpeed;
        holding.transform.SetParent(null, true);
        
        foreach (BoxCollider2D box in holding.GetComponentsInChildren<BoxCollider2D>())
        {
            box.enabled = true;
        }
        holding = null;
        
    }
    

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Item"))
        {
            canHold = other.gameObject;
            canPickUp = true;
            
        } else if (other.CompareTag("NPC"))
        {
            canGive = true;
            npc = other.gameObject.GetComponent<NPCDialouge>();
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Item"))
        {
            canHold = null;
            canPickUp = false;
            
        } else if (other.CompareTag("NPC"))
        {
            canGive = false;
            npc = null;
        }
    }
}
