using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float JumpForce = 1;
    public float MovementSpeed = 1;
    public float Friction = 1;
    public Rigidbody2D rb;
    float HorizontalVelocity = 0;
    float VerticalVelocity = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

    }

    void FixedUpdate()
    {
        if (Input.GetKey("d"))
        {
            HorizontalVelocity = HorizontalVelocity + 1 * MovementSpeed;
        }
        else if (Input.GetKey("a"))
        {
            HorizontalVelocity = HorizontalVelocity - 1 * MovementSpeed;
        }
        
        
        if (HorizontalVelocity > 0)
        {
            HorizontalVelocity = HorizontalVelocity - Friction;
        }
        else if (HorizontalVelocity < 0)
        {
            HorizontalVelocity = HorizontalVelocity + Friction;
        }

        if (Input.GetKey("space"))
        {
            print("Cum");
        }

        rb.velocity = new Vector2(HorizontalVelocity, -1);
    }   

}
