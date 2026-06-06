using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [SerializeField, Range(0, 10)] private float maxhealth = 5;
    [SerializeField] private float currenthealth;

    [SerializeField] private string lossScene = "Loss";
    
    [SerializeField] private TextMeshProUGUI healthText;

    [SerializeField] private Sounds SM;
    [SerializeField] private UpdateHealth healthBar;
    
    private PlayerController playerController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currenthealth = maxhealth;
        
        playerController = GetComponent<PlayerController>();
        UpdateHealth();
        PlayerPrefs.GetFloat("Health",currenthealth);
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
        
        UpdateHealth();
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
            //layerController.ResetPlayer();
        }
        UpdateHealth();
        SM.Enemy();
    }

    private void UpdateHealth()
    {
        //this I want to put on the health text
        //maybe I should add an event system
        healthText.text = currenthealth.ToString();
        
        //updates UI
        healthBar.Health();
        PlayerPrefs.SetFloat("Health", currenthealth);
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
