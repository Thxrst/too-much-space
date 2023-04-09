using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FMODEvents : MonoBehaviour
{
    [field: Header("Jump SFX")]
    [field: SerializeField] public EventReference jumpSFX { get; private set; }
    [field: Header("Objective SFX")]
    [field: SerializeField] public EventReference objectiveSFX { get; private set; }
    [field: Header("Death SFX")]
    [field: SerializeField] public EventReference deathSFX { get; private set; }


    public static FMODEvents instance { get; private set; }


    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one FMOD Events instance in the scene.");
        }

        instance = this;
    }
}
