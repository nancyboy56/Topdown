using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
   [SerializeField] private float startTime = 30;
    private float currentTime = 0;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentTime = startTime;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime-= Time.deltaTime;
        timerText.text = Mathf.Floor(currentTime).ToString();
        CheckTimer();
    }

    void CheckTimer()
    {
        if (currentTime < 0)
        {
            SceneManager.LoadScene("Loss");
        }
    }
    
}
