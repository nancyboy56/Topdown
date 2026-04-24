using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Collect : MonoBehaviour
{
    Scoring scoring;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoring = GameObject.FindGameObjectWithTag("GM").GetComponent<Scoring>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            scoring.AddScore(1);
            
            //might have an aimation beforehand
            //destory colletable
            Destroy(gameObject);
        }
    }
}
