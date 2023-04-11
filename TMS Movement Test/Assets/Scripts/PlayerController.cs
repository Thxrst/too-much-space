 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] float thrust;
    [SerializeField] LayerMask Ground;               
    [SerializeField] private int jumpCount;
    [SerializeField] private bool isGrounded = true;
    [SerializeField] private bool isWallTouch = false;
    [SerializeField] private float maxDistance;
    private int lrValue;
    private int nextAction = 0;
    internal bool isDashing;
    private BoxCollider2D coll;
    internal Rigidbody2D rb;
    private Vector3 startPosition;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        nextAction = 0;
        startPosition = transform.position;
    }


    void Update()
    {
        isGrounded = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, 0.08f, Ground);
        isWallTouch = Physics2D.Raycast(transform.position, new Vector2(transform.localScale.x, 0), maxDistance, Ground);
        Debug.DrawRay(transform.position, new Vector2(transform.localScale.x, 0) * maxDistance, Color.yellow);
        //playermovementOne();
        playerMovementTwo();
        if (isWallTouch && !isGrounded)
        {
            jumpCount = 1;
        }
    }

    void FixedUpdate()
    {
        if (isDashing) return;
        rb.velocity = new Vector2(lrValue * (100 * speed) * Time.deltaTime, rb.velocity.y);
    }
    void playerMovementTwo()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            switch (nextAction)
            {
                case 0:
                    jumpCount = 0;
                    Move(-1); // int variable to push character to left
                    break;
                case 1:
                    Jump(2);//jump function and leading to next action.
                    break;
                case 2:
                    jumpCount = 0;
                    Move(1); // int variable to push character to right
                    break;
                case 3:
                    Jump(0); // jump function leading back to 0
                    break;
            }
        }
    }

    void Jump(int nxtAction)
    {
        if (jumpCheck() == 1)
        {
            //Audio
            AudioManager.instance.PlayOneShot(FMODEvents.instance.jumpSFX, this.transform.position);
            //Audio
            rb.velocity = Vector2.zero;
            lrValue = 0;
            rb.AddForce(transform.up * thrust, ForceMode2D.Impulse);
            nextAction = nxtAction;
        }
        
    }

    private int jumpCheck()
    {
        if (isGrounded)
        {
            jumpCount = 1;
        }

        return jumpCount;
    }

    void Move(int direction)
    {
        lrValue = direction;
        nextAction++;
        transform.localScale = new Vector3(direction, 1, 1);
        
    }

    public void death()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.deathSFX, this.transform.position);
        transform.position = startPosition;
    }

    /*private void OnDrawGizmosSelected()
    {
        if (jumpCheck() == 1)
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(coll.bounds.center, coll.bounds.size);
    }
    */


}
