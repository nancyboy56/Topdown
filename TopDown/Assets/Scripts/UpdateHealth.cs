using System;
using UnityEngine;
using UnityEngine.UI;

public class UpdateHealth : MonoBehaviour
{
   
    [SerializeField] private Health health;
    [SerializeField] private Image image;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        health = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        image = GetComponent<Image>();
        
    }

    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Health()
    {
        Debug.Log(health.GetHealth()/health.GetMaxHealth());
        image.material.SetFloat("_Health", health.GetHealth()/health.GetMaxHealth());
    }
}
