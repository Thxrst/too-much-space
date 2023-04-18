using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bombController : MonoBehaviour
{
    public float Timer;
    public float radius;


    // Update is called once per frame
    void Update()
    {
        //if (isCounting!) return;
        if (gameObject.transform.parent != null)
            Timer -= Time.deltaTime;

        if (Timer <= 0)
        {
            Destroy(gameObject);
        }
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, radius, Vector2.zero);

        // Loop through all the colliders that intersect with the circle
        foreach (RaycastHit2D hit in hits)
        {
            if(hit.collider.tag == "Explodable" && Timer <= 0)
            {
                hit.collider.GetComponent<IDestructable>().ExplodeObject();
                
            }
        }
    }


}
