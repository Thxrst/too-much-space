 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float spd;
    [SerializeField] float thrust;
    [SerializeField] LayerMask Ground;
    private int lrValue;
    //string nextAction = "left";
    private int nextAction = 0;
    private bool pressSpace;
    private bool hasJump = true;
    private BoxCollider2D coll;

    // float [] nextAction = {"left","jump1","right","jump2"};

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        nextAction = 0;
    }


    void Update()
    {
            if (nextAction == 0 && Input.GetKeyDown("space"))
            {
                lrValue = -1;
                nextAction++;
                Debug.Log("left");
            }
                
            else if (nextAction == 2 && Input.GetKeyDown("space"))
            {
                lrValue = 1;
                nextAction++;
                Debug.Log("right");
            }


            else if (hasJump)
            {
                if (nextAction == 3 && Input.GetKeyDown("space"))
                {
                    lrValue = 0;
                    rb.AddForce(transform.up * thrust, ForceMode2D.Impulse);
                    hasJump = false;
                    nextAction++;
                    Debug.Log("jump2");
                }

                if (nextAction == 1 && Input.GetKeyDown("space"))
                {
                    lrValue = 0;
                    rb.AddForce(transform.up * thrust, ForceMode2D.Impulse);
                    hasJump = false;
                    nextAction++;
                    Debug.Log("jump1");
                }
            }

        if (nextAction > 3)
        {
            nextAction = 0;
        }

        hasJump = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, Ground);
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(lrValue * (100 * spd) * Time.deltaTime, rb.velocity.y);
    }

}
