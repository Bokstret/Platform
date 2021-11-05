using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonsChoose : MonoBehaviour
{
    [SerializeField]
    Sprite levelDone;
    public List<Transform> levels;
    GameObject buttons;
    GameObject back;
    public Transform music;
    AudioSource audio;

    void Start()
    {
        buttons = GameObject.Find("BUTTONS CHOOSE");
        back = GameObject.Find("Back");
        foreach (Transform child in buttons.transform)
        {
            levels.Add(child);
        }

        if(PlayerPrefs.GetInt("Initialize") == 0)
        {
            for (int i = 1; i < levels.Count; i++)
            {
                PlayerPrefs.SetInt("Level" + i.ToString(), 0);
            }
            PlayerPrefs.SetInt("Level0", 1);
            PlayerPrefs.SetInt("Initialize", 1);
        }

        for (int i = 0; i < levels.Count; i++)
        {
            if (PlayerPrefs.GetInt("Level" + i.ToString()) == 2)
            {
                levels[i].GetComponent<Image>().sprite = levelDone;
                if(PlayerPrefs.GetInt("Level" + (i + 1).ToString()) != 2)
                {
                    PlayerPrefs.SetInt("Level" + (i + 1).ToString(), 1);
                }

            }
            
            if (PlayerPrefs.GetInt("Level" + i.ToString()) == 0)
            {
                levels[i].GetComponent<Image>().enabled = false;
            }
        }

        if (!GameObject.Find("MusicMenu(Clone)"))
        {
            Instantiate(music);
        }
        audio = GameObject.Find("MusicMenu(Clone)").GetComponent<AudioSource>();

        if (PlayerPrefs.GetInt("SoundOn") == 0)
        {
            audio.mute = true;
        }
        else
        {
            audio.mute = false;
        }
    }

    public void BackPressed()
    {
        buttons.SetActive(false);
        back.SetActive(false);
        SceneChange.sceneId = 0;
        SceneChange.sceneEnd = true;
        SceneChange.sceneStarting = false;
    }

    public void Load(int levelNumber)
    {
        buttons.SetActive(false);
        back.SetActive(false);
        SceneChange.sceneId = levelNumber;
        SceneChange.sceneEnd = true;
        SceneChange.sceneStarting = false;
        Destroy(GameObject.Find("MusicMenu(Clone)"));
    }
}
