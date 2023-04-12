using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
public class LevelManager : MonoBehaviour
{
    public int currentLevelIndex;
    public int nextLevelIndex;
    public bool isLevelEnd;
    public static LevelManager _instance;
    public UnityEvent levelEnd;

    //public static LevelManager Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private void Update()
    {
        if(isLevelEnd)
        {
            if(Input.GetKeyDown(KeyCode.LeftShift))
            {
                SceneManager.LoadScene(nextLevelIndex);
            }
        }
    }


    private void Start()
    {
        isLevelEnd = false;
        currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        nextLevelIndex = currentLevelIndex + 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
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
        levelEnd?.Invoke();
    }



}
