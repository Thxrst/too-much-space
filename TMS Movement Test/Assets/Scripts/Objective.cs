using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class Objective : MonoBehaviour
{

    //Audio
    [SerializeField] private EventReference objectiveSound;
    //Audio

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
            AudioManager.instance.PlayOneShot(objectiveSound, this.transform.position);
            //Audio
            Debug.Log("Objective/Player Contact");
        }
    }
}
