using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    public float boostTime = 0.3f;

    private PlayerController controller;

    private void Start()
    {
        controller = GetComponent<PlayerController>();
    }

    public void speedBoost()
    {
        StartCoroutine(boost());
    }

    IEnumerator boost()
    {
        controller.isDashing = true;    
        controller.rb.gravityScale = 0;
        yield return new WaitForSeconds(boostTime);
        controller.isDashing = false;
        controller.rb.angularVelocity = 0;
        controller.rb.velocity = Vector2.zero;
        controller.rb.gravityScale = 3;

    }
}
