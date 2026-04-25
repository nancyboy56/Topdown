using UnityEngine;
using TMPro;

public class Display : MonoBehaviour
{
    [SerializeField] private string displayType;
    private TextMeshProUGUI displayText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        displayText = GetComponent<TextMeshProUGUI>();
        displayText.text = PlayerPrefs.GetFloat(displayType).ToString();
    }

    
}