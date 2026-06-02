using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Scoring : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI ScoreText;
    private TextMeshProUGUI scoreText;
    private float score;

    [SerializeField] private Sounds SM;

    [SerializeField] private int winScore;
    [SerializeField] private string winLevel;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SM = GameObject.FindGameObjectWithTag("SM").GetComponent<Sounds>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddScore(int number)
    {
        score += number;
        ScoreText.text = score.ToString();
        PlayerPrefs.SetFloat("Score", score);
        SM.Collect();
        Win();
    }

    private void Win()
    {
        if (score >= winScore)
        {
            Debug.Log("You win!");
            SceneManager.LoadScene(winLevel);
        }
    }
}
