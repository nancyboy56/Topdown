using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [SerializeField, Range(0, 10)]
    private float maxhealth = 5;
    [SerializeField]
    private float currenthealth;

    [SerializeField] private string lossScene = "Loss";
    
    [SerializeField]
    private TextMeshProUGUI healthText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currenthealth = maxhealth;
        healthText.text = currenthealth.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Heal(float amount)
    {
        if (currenthealth + amount > maxhealth)
        {
            currenthealth = maxhealth;
        }
        else
        {
            currenthealth += amount;
        }
        
        healthText.text = currenthealth.ToString();
    }

    public void TakeDamage(float amount)
    {
        if (currenthealth - amount <= 0)
        {
            currenthealth = 0;
            SceneManager.LoadScene(lossScene);
        }
        else
        {
            currenthealth -= amount;
        }
        healthText.text = currenthealth.ToString();
    }

    public float GetHealth()
    {
        return currenthealth;
    }
    
    public float GetMaxHealth()
    {
        return maxhealth;
    }
    
}
