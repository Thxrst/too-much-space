using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class speedBoost : MonoBehaviour
{
    public float boostAmount;
    public float punchScaleAmount = 1.2f; // Amount to scale the object by during the punch effect
    public float punchDuration = 0.3f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            transform.DOPunchScale(new Vector3(punchScaleAmount, punchScaleAmount, punchScaleAmount), punchDuration);
            other.GetComponent<PlayerDash>().speedBoost();
            other.GetComponent<Rigidbody2D>().AddForce(transform.right * boostAmount, ForceMode2D.Impulse);
        }
    }
}
