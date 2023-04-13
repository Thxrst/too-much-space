using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;
public class levelSelectMGR : MonoBehaviour
{
    [SerializeField] private List<string> levelScenes = new List<string>();
    [SerializeField] Button[] levelButtons;
    private string sceneName;

    void Start()
    {
        // Loop through all the scenes in the build settings.
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            // Get the scene path.
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);

            // Extract the scene name from the path.
            sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);

            // Check if the scene name contains "Level".
            if (sceneName.Contains("Level"))
            {
                // Add the scene name to the list.
                levelScenes.Add(sceneName);
            }
        }
        int levelIndex = PlayerPrefs.GetInt("LevelAt",0);
        for (int i = 0; i < levelButtons.Length; i++)
        {
            // Set the name and text of the button.
            levelButtons[i].name = levelScenes[i];
            levelButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = levelScenes[i];
            
            // Check if the level has been completed.


            // If the level has not been completed, disable the button.
            if (i  > levelIndex)
            {
                levelButtons[i].interactable = false;
            }

            // Add an onClick event to the button.
            
            levelButtons[i].onClick.AddListener(() =>
            {
                PlayerPrefs.SetInt("LevelAt", levelIndex );
                // Load the selected level.
                SceneManager.LoadScene(levelScenes[levelIndex]);
            });
        }
        Debug.Log(levelIndex);
        Debug.Log(PlayerPrefs.GetInt("LevelAt"));
    }

        /*
         * Previous Script
         * public Button[] lvlButtons;
        private TextMeshProUGUI[] text;
        private int index;
        // Start is called before the first frame update
        void Start()
        {
            lvlButtons = gameObject.GetComponentsInChildren<Button>();
            text = gameObject.GetComponentsInChildren<TextMeshProUGUI>();
            for (int i = 0; i < lvlButtons.Length; i++)
            {
                string name = lvlButtons[i].name.Replace("Level ", string.Empty);

                text[i].text = name;
                //text[i].text.TrimStart();

            }

            int levelAt = PlayerPrefs.GetInt("LevelAt",2);
            for (int i = 0; i < lvlButtons.Length; i++)
            {
                lvlButtons[i].onClick.AddListener(buttonClick);
                if (i+ 2 > levelAt)
                {
                    lvlButtons[i].interactable = false;

                }
            }
        }


        public void buttonClick()
        {
            for (int i = 0; i < lvlButtons.Length; i++)
            {
                Debug.Log("Button has been pressed");
                string buttonName = EventSystem.current.currentSelectedGameObject.name;
                SceneManager.LoadScene(buttonName);
            }
        }
         */


    }
