using System;
using TMPro;
using UnityEngine;


public class Instructions : MonoBehaviour
{
    [SerializeField] private GameObject instructions;
    [SerializeField] private GameObject canvas;
    [SerializeField] private RectTransform rect;
    [SerializeField] private string text = "Press Q to Pickup";
    [SerializeField] private float fontSize = 24;
    [SerializeField] private Color colour = Color.white;
    [SerializeField] private TMP_FontAsset font;
    
    
    // high the instructions are above the item
    [SerializeField] private float height = 1.5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("World Canvas");
        CreateInstructions();
        instructions.SetActive(false);
    }

    private void CreateInstructions()
    {
        instructions = new GameObject("Instructions "+ name);
        instructions.transform.SetParent(canvas.transform, true);
        TextMeshPro textMesh = instructions.AddComponent<TextMeshPro>();
        
        //Vector2 preferredSize = textMesh.GetPreferredValues(text);
        
        textMesh.text = text;
        textMesh.fontSize = fontSize;
        textMesh.color = colour;
        textMesh.alignment = TextAlignmentOptions.Center;
        textMesh.font = font;
        textMesh.ForceMeshUpdate();
        float actualWidth = textMesh.renderedWidth;
        float actualHeight = textMesh.renderedHeight;
       // instructions.transform.position = new Vector3(0, 0, 0);
        rect = instructions.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(actualWidth+ 1, actualHeight);
        //rect.sizeDelta = preferredSize;
        instructions.SetActive(false);
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            instructions.SetActive(true);
            // if i add a rigidbody ill have to change this
           // instructions.transform.position = transform.position + Vector3.up * height;
           rect.position= transform.position + Vector3.up * height;
        }
        
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            instructions.SetActive(false);
        }
        
    }

  
}
