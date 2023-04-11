using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class springBoard : MonoBehaviour
{
    [SerializeField] private float launchForce;

    private Vector2 launchDirection = new Vector2(1, 1);

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody2D playerRb = other.GetComponent<Rigidbody2D>();
            playerRb.velocity = Vector3.zero;
            playerRb.AddForce(launchDirection.normalized * launchForce, ForceMode2D.Impulse);
        }
    }
}
