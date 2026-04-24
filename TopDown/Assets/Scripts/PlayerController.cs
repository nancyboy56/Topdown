using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Vector2 direction;
    private Rigidbody2D rb;
    [SerializeField, Range(0,20)]
    private float speed = 3.0f;
    Animator animator;
    SpriteRenderer spriteRenderer;
    

    [SerializeField] private Vector2 resetPLayer;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        resetPLayer = transform.position;
        
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move(InputAction.CallbackContext context)
    {
        Debug.Log(context.ReadValue<Vector2>().ToString());
        direction = context.ReadValue<Vector2>();
        rb.linearVelocity = direction * speed;
        Animations();
       
    }

    void Animations()
    {
        animator.SetFloat("MoveX", direction.x);
        animator.SetFloat("MoveY", direction.y);
        
        //checks if character needs to flip
        Flip();
    }

    void Flip()
    {
        if (direction.x > 0)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }

    public void ResetPlayer()
    {
        transform.position = resetPLayer;
    }
    
}
