using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class Objective : MonoBehaviour
{
    ParticleSystem ps;
    SpriteRenderer sr;
    [SerializeField] public int currentLevelIndex;
    [SerializeField] public int nextLevelIndex;
    [SerializeField] public bool isLevelEnd;
    //public static LevelManager _instance;
    //public UnityEvent levelEnd;

    void Start()
    {
        ps = GetComponentInChildren<ParticleSystem>();
        sr = GetComponent<SpriteRenderer>();
        isLevelEnd = false;
        currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        nextLevelIndex = currentLevelIndex + 1;
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
            isLevelEnd = true;
            if (nextLevelIndex > PlayerPrefs.GetInt("LevelAt"))
            {
                PlayerPrefs.SetInt("LevelAt", nextLevelIndex);
            }
            StartCoroutine(nextLevelSequence());
        }
    }

    IEnumerator nextLevelSequence()
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(nextLevelIndex);
        //levelEnd?.Invoke();
    }
}
