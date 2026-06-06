using System;
using UnityEngine;
using UnityEngine.UI;

public class ShowDesire : MonoBehaviour
{
    [SerializeField] private string desire;
    [SerializeField] private Sprite outline;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    { 
        //finds if the ghosts desire has been found by the player
        //kinda hacky way to do this
        // ill have to rewrite it later as player prefs cant have arrays or complex data types
        if (!PlayerPrefs.GetString("Desires").Contains(desire, StringComparison.OrdinalIgnoreCase))
        {
            Image im = GetComponent<Image>();
            im.sprite = outline;
            im.SetNativeSize();
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
