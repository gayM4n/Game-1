using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float JumpForce = 1;
    public float Gravity = 1;
    public float MovementSpeed = 1;
    public float Friction = 1;
    bool Grounded = false;
    float HorizontalVelocity = 0;
    float VerticalVelocity = 0;
    public Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

    }

    void FixedUpdate()
    {
        //Gravity Simulation
        if (Grounded == false)
        {
            VerticalVelocity = VerticalVelocity - 1 * Gravity;
        }
        

        if (Grounded == true)
        {
            VerticalVelocity = 0;
            if (Input.GetKey("space"))
            {
                VerticalVelocity = VerticalVelocity + 1 * JumpForce;
            }

            if (Input.GetKey("d"))
            {
                HorizontalVelocity = HorizontalVelocity + 1 * MovementSpeed;
            }
            else if (Input.GetKey("a"))
            {
                HorizontalVelocity = HorizontalVelocity - 1 * MovementSpeed;
            }
        }

        //Friction
        if (HorizontalVelocity > 0 && Grounded == true)
        {
            HorizontalVelocity = HorizontalVelocity - Friction;
        }
        else if (HorizontalVelocity < 0 && Grounded == true)
        {
            HorizontalVelocity = HorizontalVelocity + Friction;
        }

        //Movement Update
        rb.velocity = new Vector2(HorizontalVelocity, VerticalVelocity);
    }   

//Checks if grounded
    void OnTriggerStay2D(Collider2D other)
    {
        Grounded = true;
        print("grounded");
    }

//Checks if not grounded
    void OnTriggerExit2D(Collider2D other)
    {
        Grounded = false;
        print("ungrounded");
    }

}
