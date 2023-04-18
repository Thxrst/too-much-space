using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public Vector3 carryPosition;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Trap")
        {
            gameObject.GetComponent<PlayerController>().death();
        }

        if(collision.gameObject.tag == "PickUp")
        {
            PickupObject(collision.gameObject);
        }

    }


    private void PickupObject(GameObject obj)
    {
        //obj.GetComponent<Rigidbody2D>().isKinematic = true; // disable physics
        obj.transform.parent = transform; // make object a child of the player
        obj.transform.position = transform.localPosition + carryPosition; // move object to carry position
        
       
    }
}
