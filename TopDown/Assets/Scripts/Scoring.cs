using UnityEngine;
using TMPro;

public class Scoring : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI ScoreText;
    TextMeshProUGUI scoreText;
    float score;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddScore(int number)
    {
        score += number;
        ScoreText.text = score.ToString();
    }
}
