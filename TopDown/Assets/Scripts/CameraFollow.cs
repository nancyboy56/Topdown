using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // the z must be negative to make depth of field work
    [SerializeField] Vector3 offset = new Vector3 (0,0 -10);
    [SerializeField] Transform player;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = player.position + offset;
        
    }
}
