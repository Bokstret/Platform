using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonsMenu: MonoBehaviour
{
    GameObject play;
    GameObject soundOff;
    GameObject soundOn;
    public Transform music;
    AudioSource audio;

    void Start()
    {
        soundOff = GameObject.Find("SoundOff");
        soundOn = GameObject.Find("SoundOn");
        play = GameObject.Find("Play");
        if (!GameObject.Find("MusicMenu(Clone)"))
        {
            Instantiate(music);
        }
        audio = GameObject.Find("MusicMenu(Clone)").GetComponent<AudioSource>();

        if (PlayerPrefs.GetInt("SoundOn") == 0)
        {
            audio.mute = true;
            soundOn.SetActive(true);
            soundOff.SetActive(false);
        }
        else
        {
            audio.mute = false;
            soundOn.SetActive(false);
            soundOff.SetActive(true);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void PlayPressed()
    {
        SceneChange.sceneId = 1;
        SceneChange.sceneEnd = true;
        SceneChange.sceneStarting = false;
        soundOn.SetActive(false);
        soundOff.SetActive(false);
        play.SetActive(false);
    }


    public void SoundOff()
    {
        soundOn.SetActive(true);
        soundOff.SetActive(false);
        audio.mute = true;
        PlayerPrefs.SetInt("SoundOn", 0);
    }

    public void SoundOn()
    {
        soundOn.SetActive(false);
        soundOff.SetActive(true);
        audio.mute = false;
        PlayerPrefs.SetInt("SoundOn", 1);
    }
}