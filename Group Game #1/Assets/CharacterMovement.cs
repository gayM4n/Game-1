using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    CharacterWallTestRight WallTestRight;
    CharacterWallTestLeft WallTestLeft;
    public GameObject Child;
    public GameObject Child1;
    public float WallJumpMultiplier = 1;
    public float JumpForce = 1;
    public float Gravity = 1;
    public float MovementSpeed = 1;
    public float Friction = 1;
    bool Sliding = false;
    public float SlideCooldown = 1f; 
    public float SlideStartup = 1.5f;
    public float SlideSlowdown = 1.1f;
    private float TimeNextSlide;
    bool Grounded = false;
    public float HorizontalMoveLimit = 10;
    public float VerticalMoveLimit = 10;
    float HorizontalVelocity = 0;
    float VerticalVelocity = 0;
    public Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        WallTestRight = Child.GetComponent<CharacterWallTestRight>();
        WallTestLeft = Child1.GetComponent<CharacterWallTestLeft>();
    }

    void Update()
    {

    }

    IEnumerator Slide()
    {
        if (Sliding == true)
        {
            for (int r = 10; r != 0; r--)
            {
                HorizontalVelocity /= SlideSlowdown;
                yield return new WaitForSeconds(0.05f);
            }
        }
    }
    void FixedUpdate()
    {
        //Gravity Simulation
        if (Grounded == false)
        {
            VerticalVelocity = VerticalVelocity - 1 * Gravity;
        }
        
        //Walled Right Movement
        if (WallTestRight.IsWalledRight == true)
        {
            if (Input.GetKey("s"))
            {
                VerticalVelocity -= HorizontalVelocity;
                HorizontalVelocity = 0;

                if (Input.GetKey("space"))
                {
                    HorizontalVelocity = (VerticalVelocity * WallJumpMultiplier);
                    VerticalVelocity = VerticalVelocity / 1.5f;
                }
            }
            else
            {
               VerticalVelocity += HorizontalVelocity;
               HorizontalVelocity = 0;
            
                if (Input.GetKey("space"))
                {
                    HorizontalVelocity = (-VerticalVelocity * WallJumpMultiplier);
                    VerticalVelocity = VerticalVelocity / 1.5f;
                }
            }
        } 

        //Left Wall Movement
        if (WallTestLeft.IsWalledLeft == true)
        {
            if (Input.GetKey("s"))
            {
                VerticalVelocity += HorizontalVelocity;
                HorizontalVelocity = 0;

                if (Input.GetKey("space"))
                {
                    HorizontalVelocity = (-VerticalVelocity * WallJumpMultiplier);
                    VerticalVelocity = VerticalVelocity / 1.5f;
                }
            }
            else
            {
               VerticalVelocity -= HorizontalVelocity;
               HorizontalVelocity = 0;
            
                if (Input.GetKey("space"))
                {
                    HorizontalVelocity = (VerticalVelocity * WallJumpMultiplier);
                    VerticalVelocity = VerticalVelocity / 1.5f;
                }
            }
        }    

        //Grounded movement
        if (Grounded == true)
        {
            //Slide movement, cooldown script taken from https://answers.unity.com/questions/979929/adding-a-cooldown-time-to-a-attack.html
            if (Input.GetKey("u"))
            {
                if (Grounded == true && Time.time > TimeNextSlide)
                {
                    TimeNextSlide = Time.time + SlideCooldown;
                    Sliding = true;
                    HorizontalVelocity *= SlideStartup;
                    StartCoroutine(Slide());
                }
                if (Grounded == false)
                {
                    VerticalVelocity = 1;
                    print("Airsliding");
                }
            }
            else
            {
                Sliding = false;
            }
            if (Input.GetKey("space"))
            {
                VerticalVelocity = VerticalVelocity + 1 * JumpForce;
            }
            else if (Input.GetKey("d"))
            {
                HorizontalVelocity = HorizontalVelocity + 1 * MovementSpeed;
            }
            else if (Input.GetKey("a"))
            {
                HorizontalVelocity = HorizontalVelocity - 1 * MovementSpeed;
            }
        }

        //Friction
        if (HorizontalVelocity > 0 && Grounded == true && Sliding == false )
        {
            HorizontalVelocity = HorizontalVelocity - Friction;
        }
        else if (HorizontalVelocity < 0 && Grounded == true && Sliding == false)
        {
            HorizontalVelocity = HorizontalVelocity + Friction;
        }

        //Movement Limit
        if (HorizontalVelocity > HorizontalMoveLimit)
        {
            HorizontalVelocity = HorizontalMoveLimit;
        }
        else if (HorizontalVelocity < -HorizontalMoveLimit)
        {
            HorizontalVelocity = -HorizontalMoveLimit;
        }
        if (VerticalVelocity > VerticalMoveLimit)
        {
            VerticalVelocity = VerticalMoveLimit;
        }
        else if (VerticalVelocity < -VerticalMoveLimit)
        {
            VerticalVelocity = -VerticalMoveLimit;
        }

        //Movement Update
        rb.velocity = new Vector2(HorizontalVelocity, VerticalVelocity);
    }   

//Checks if grounded
    void OnTriggerStay2D(Collider2D other)
    {
        if (WallTestRight.IsWalledRight == false && WallTestLeft.IsWalledLeft == false)
        {
        Grounded = true;
        }
    }

//Checks if not grounded
    void OnTriggerExit2D(Collider2D other)
    {
        Grounded = false;
    }

}
