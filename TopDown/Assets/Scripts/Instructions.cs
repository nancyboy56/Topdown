using UnityEngine;

public class Instructions : MonoBehaviour
{
    [SerializeField] private GameObject instructions;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instructions.SetActive(false);
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            instructions.SetActive(true);
        }
        
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            instructions.SetActive(false);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
