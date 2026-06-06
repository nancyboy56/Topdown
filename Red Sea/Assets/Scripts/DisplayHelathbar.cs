using UnityEngine;
using UnityEngine.UI;

public class DisplayHelathbar : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private float maxHealth = 5;
    [SerializeField] private string health = "Health";
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        image = GetComponent<Image>(); 
        image.material.SetFloat("_Health", PlayerPrefs.GetFloat(health)/maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
