using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Public Components
    public float speed = 5f;


    // Private components
    private Rigidbody2D myBody;
    private Animator anim;



    // Functions
    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

  


    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        PlayerWalk();
    }


    void PlayerWalk()
    {
        float h = Input.GetAxisRaw("Horizontal");

        if (h > 0)

        {
            myBody.velocity = new Vector2(speed, myBody.velocity.y);

            ChangeDirection(1);
        }
        else if (h<0)
        {
            myBody.velocity = new Vector2(-speed, myBody.velocity.y);

            ChangeDirection(-1);
        }
        else
        {
            myBody.velocity = new Vector2(0f, myBody.velocity.y);
        }


        anim.SetInteger("Speed", Mathf.Abs((int)myBody.velocity.x));
    }


    // Changing Player Face Direction

    void ChangeDirection(int direction)
    {
        Vector3 tempScale = transform.localScale;
        tempScale.x = direction;
        transform.localScale = tempScale;
    }



    void OnCollisionEnter2D(Collision2D target)
    {
        if (target.gameObject.tag == "Ground")

        {
            print("Collision has happened");
        }
       
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }


}
