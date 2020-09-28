using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailScript : MonoBehaviour
{
    // Global Değişkenler ve Component ler
    public float moveSpeed = 1f;
    public LayerMask playerLayer;

   
    private Rigidbody2D myBody;
    private Animator anim;
    private bool moveLeft;
    private bool canMove;
    private bool stunned;

    public Transform left_Collision, right_Collision, top_Collision,down_Collision;
    private Vector3 left_Collision_Pos, right_Collision_Pos;



    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        left_Collision_Pos = left_Collision.position;
        right_Collision_Pos = right_Collision.position;
    }


    void Start()
    {
        moveLeft = true;
        canMove = true;
    }

   
    void Update()
    {
        if (canMove)
        {
            if (moveLeft)
            {
                myBody.velocity = new Vector2(-moveSpeed, myBody.velocity.y);
            }
            else
            {
                myBody.velocity = new Vector2(moveSpeed, myBody.velocity.y);
            }
        }

        CheckCollision();
    }


    void CheckCollision()
    {
        RaycastHit2D leftHit = Physics2D.Raycast(left_Collision.position, Vector2.left, .1f, playerLayer);
        RaycastHit2D rightHit = Physics2D.Raycast(right_Collision.position, Vector2.right, .1f, playerLayer);

        Collider2D topHit = Physics2D.OverlapCircle(top_Collision.position, .2f, playerLayer);



        // Eğer topHit Circle Collider etkileşime girdiyse.
        if (topHit != null) 

        {
            // Etkileşime girilen GameObject tag'ı Player ise.
            if (topHit.gameObject.tag == "Player") 
            {
                // Snail Stunned durumunda değil ise
                if (!stunned) 
                              
                {
                    // Player Rigidbody si alınır ve +Y yönünde jump artırılır.
                    topHit.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(topHit.gameObject.GetComponent<Rigidbody2D>().velocity.x, 16f); 

                    canMove = false;
                    myBody.velocity = new Vector2(0, 0);
                    anim.Play("Stunned");
                    stunned = true;


                    // BEETLE CODE HERE
                }
            }
        }


        // Eğer Collision yok ise  ne yapılacak {}

        if (!Physics2D.Raycast(down_Collision.position, Vector2.down, .1f))
        {
            ChangeDirection();
        }
    }


    void ChangeDirection()
    {
        moveLeft = !moveLeft;

        Vector3 tempScale = transform.localScale;

        if (moveLeft)
        {
            tempScale.x = Mathf.Abs(tempScale.x);

            left_Collision.position = left_Collision_Pos;
            right_Collision.position = right_Collision_Pos;
        }
        else
        {
            tempScale.x = -Mathf.Abs(tempScale.x);

            left_Collision.position = right_Collision_Pos;
            right_Collision.position = left_Collision_Pos;
        }
        transform.localScale = tempScale;
    }
}
