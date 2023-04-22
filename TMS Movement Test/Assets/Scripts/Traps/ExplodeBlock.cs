using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeBlock : MonoBehaviour, IDestructable
{
    public void ExplodeObject()
    {
       gameObject.SetActive(false);
    }
}
