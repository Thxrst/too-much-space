using UnityEngine;
public class Jumping : MonoBehaviour
{
    int dir = 0;
    [SerializeField] float jumpHeight;
    Rigidbody2D rb;
    float directionX;
    private void Start()
    {
        //directionX = -jumpHeight;
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        rb.velocity = new Vector2(directionX, rb.velocity.y);
    }
    void Jump()
    {
        if(dir == 0){directionX = -jumpHeight; dir++; }
        else if(dir == 2){directionX = jumpHeight; dir++;}
        else if(DetectSurface() && (dir == 1 || dir == 3)) {directionX = 0; rb.AddForce(new Vector2(0, jumpHeight * 1.5f), ForceMode2D.Impulse); dir++; };
        if (dir >= 4) { dir = 0; }
    }
    bool DetectSurface() { return Physics2D.BoxCast(GetComponent<BoxCollider2D>().bounds.center, new Vector2(transform.localScale.x + .15f, transform.localScale.y + .15f), 0, Vector2.down, 1.5f, LayerMask.GetMask("Surface")); }
}