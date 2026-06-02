using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PickUp : MonoBehaviour
{
    [SerializeField] GameObject holdSpot;
    [SerializeField] GameObject holding;
    [SerializeField] GameObject canHold;

    [SerializeField] private bool canPickUp;

    [SerializeField] private float throwSpeed = 3;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerInput input = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInput>();
        
        // works be
        input.actions["PickUp"].canceled += ItemInteract;
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
                    Throw();
                    PickUpObject();
                }
            }
            else
            {
                // throws when it has nothing to hold
                Throw();
            }
        }
    }

    private void PickUpObject()
    {
        holding = canHold;
        canHold = null;
        
        // i think this has to be a ridbody body at some point 
        holdSpot.transform.position = transform.position;
        holding.transform.SetParent(holdSpot.transform, false);
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
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Item"))
        {
            canHold = null;
            canPickUp = false;
        }
    }
}
