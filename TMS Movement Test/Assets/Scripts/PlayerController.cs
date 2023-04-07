 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    [SerializeField] float speed;
    [SerializeField] float thrust;
    [SerializeField] LayerMask Ground;
    [SerializeField] private int lrValue;
    [SerializeField] private int nextAction = 0;
    [SerializeField] private int jumpNum;
    [SerializeField] private bool hasJump = true;
    private BoxCollider2D coll;
    private Rigidbody2D rb;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        nextAction = 0;
    }


    void Update()
    {
       
        //playermovementOne();
        playerMovementTwo();

        // boolcheck for grounded
        
    }

    void FixedUpdate()
    {
        hasJump = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, 0.1f, Ground);
        rb.velocity = new Vector2(lrValue * (100 * speed) * Time.deltaTime, rb.velocity.y);
    }

    private int jumpCheck()
    {
        if (hasJump)
        {
            jumpNum = 1;
            Debug.Log(jumpNum);
        }
            
        return jumpNum;
    }


    void playermovementOne()
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

        else if (jumpCheck() == 1)
        {
            if (nextAction == 3 && Input.GetKeyDown("space"))
            {
                lrValue = 0;
                rb.AddForce(transform.up * thrust, ForceMode2D.Impulse);
                hasJump = false;
                jumpNum = 0;
                nextAction = 0;
                Debug.Log("jump2");
            }

            if (nextAction == 1 && Input.GetKeyDown("space"))
            {
                lrValue = 0;
                rb.AddForce(transform.up * thrust, ForceMode2D.Impulse);
                jumpNum = 0;
                nextAction++;
                Debug.Log("jump1");
            }
        }
    }

    //Refractored the code to be in a case statement style. see if you like this? It's first step to an event system.
    //little more robust as we can call mutilple functions / scripts in a clean manner rather than have a bunch of
    //if statements 
    void playerMovementTwo()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            switch(nextAction)
            {
                case 0:
                    Move(-1); // int variable to push character to left
                    jumpCheck(); //check for jumpCache only when grounded;
                    break;
                case 1:
                    Jump(); //jump function and leading to next action.
                    nextAction++;
                    break;
                case 2:
                    Move(1); // int variable to push character to right
                    jumpCheck(); //check for jumpCache only when grounded;
                    break;
                case 3:
                    Jump(); // jump function leading back to 0
                    nextAction = 0;
                    break;
            }
        }
    }

    void Jump()
    {
        if (jumpCheck() == 1)
        {
            lrValue = 0;
            rb.AddForce(transform.up * thrust, ForceMode2D.Impulse);
            jumpNum = 0;
            hasJump = false;
        }
    }

    void Move(int direction)
    {
        lrValue = direction;
        nextAction++;
        
    }

}
