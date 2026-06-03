using UnityEngine;
using UnityEngine.InputSystem;

public class Talk : MonoBehaviour
{
    private bool canTalk = false;

    private NPCDialouge npc;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerInput input = GetComponent<PlayerInput>();
        input.actions["Interact"].canceled += ShowDialouge;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ShowDialouge(InputAction.CallbackContext context)
    {
        if (context.canceled && canTalk)
        {
            npc.DisplayNextLine();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.name);
        if (other.CompareTag("NPC"))
        {
            npc = other.gameObject.GetComponent<NPCDialouge>();
            npc.PlayerEnters();
            canTalk = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log(other.name);
        if (other.CompareTag("NPC"))
        {
            npc.PlayerExits();
            npc = null;
            canTalk = false;
        }
    }
    
    
    
    
}
