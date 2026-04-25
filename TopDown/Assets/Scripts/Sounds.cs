using UnityEngine;

public class Sounds : MonoBehaviour
{
    [SerializeField] AudioSource score, enemy;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Collect()
    {
        score.Play();
    }

     public void Enemy()
    {
        enemy.Play();
    }
}

