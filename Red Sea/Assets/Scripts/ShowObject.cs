using Unity.VisualScripting;
using UnityEngine;

public class ShowObject : MonoBehaviour
{

    [SerializeField] private GameObject show; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Flip()
    {
        show.SetActive(!show.activeSelf);
    }

    public void Hide()
    {
        show.SetActive(false);
    }

    public void Show()
    {
        show.SetActive(true);
    }
    
}
