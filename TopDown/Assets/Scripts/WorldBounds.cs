using Unity.VisualScripting;
using UnityEngine;

public class WorldBounds : MonoBehaviour
{
    [SerializeField]
    private bool flipX = false;
    
    [SerializeField]
    private bool flipY = false;
    
    

    [SerializeField] private Vector3 offset;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
