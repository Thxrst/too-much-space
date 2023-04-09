using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class tweenRotate : MonoBehaviour
{
    [SerializeField] private float rotationTime;
    [SerializeField] private Vector3 directionAxis;
    // Start is called before the first frame update
    void Start()
    {
        Rotation();
    }

    void Rotation()
    {
        transform.DOLocalRotate(directionAxis, rotationTime).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);
    }
}
