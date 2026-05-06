using Unity.VisualScripting;
using UnityEngine;

public class WorldBounds : MonoBehaviour
{
    [SerializeField]
    private bool flipX = false;
    
    [SerializeField]
    private bool flipY = false;
    
    // so it doesnt spawn into the trigger enter collider and loop forever
    [SerializeField] private Vector3 offset;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // only works if map is symmertical and square
    // will have to work out when i have art bc i want it to look like a never ending sea
    // not notice that you teleport
    
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (flipX)
            {
                collision.transform.position = new Vector3(-collision.transform.position.x, 
                    collision.transform.position.y,
                    collision.transform.position.z) + offset;
            }

            if (flipY)
            {
                collision.transform.position = new Vector3(collision.transform.position.x, 
                    -collision.transform.position.y,
                    collision.transform.position.z) + offset;
                
            }
        }
        
    }
    
}
