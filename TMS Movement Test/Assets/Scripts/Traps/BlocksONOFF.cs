using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlocksONOFF : MonoBehaviour
{
    [SerializeField] private string tagToTurnOff;
    [SerializeField] private float turnOffInterval = 5f;

    private GameObject[] objectsWithTag;
    private WaitForSeconds waitInterval;

    private void Start()
    {
        objectsWithTag = GameObject.FindGameObjectsWithTag(tagToTurnOff);
        waitInterval = new WaitForSeconds(turnOffInterval);
        StartCoroutine(TurnOffObjectsRoutine());
    }

    private IEnumerator TurnOffObjectsRoutine()
    {
        while (true)
        {
            foreach (GameObject obj in objectsWithTag)
            {
                obj.SetActive(false);
            }
            yield return waitInterval;
            foreach (GameObject obj in objectsWithTag)
            {
                obj.SetActive(true);
            }
            yield return waitInterval;
        }
    }
}