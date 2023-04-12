using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;
public class levelSelectMGR : MonoBehaviour
{
    public Button[] lvlButtons;
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

}
