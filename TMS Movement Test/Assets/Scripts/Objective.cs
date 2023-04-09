using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : MonoBehaviour
{
    ParticleSystem ps;
    SpriteRenderer sr;

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        sr = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player" && sr.enabled == true)
        {
            sr.enabled = false;
            ps.Play();
            //Audio
            AudioManager.instance.PlayOneShot(FMODEvents.instance.objectiveSFX, this.transform.position);
            //Audio
            Debug.Log("Objective/Player Contact");
        }
    }
}
