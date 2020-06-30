using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;

public class ButtonsMenu: MonoBehaviour
{
    GameObject play;
    GameObject soundOff;
    GameObject soundOn;
    AudioSource audio;
    Scene choose;

    void Start()
    {
        soundOff = GameObject.Find("SoundOff");
        soundOn = GameObject.Find("SoundOn");
        play = GameObject.Find("Play");
        audio = GameObject.Find("Main Camera").GetComponent<AudioSource>();

        if (PlayerPrefs.GetInt("SoundOn") == 0)
        {
            audio.enabled = false;
            soundOn.SetActive(true);
            soundOff.SetActive(false);
        }
        else
        {
            audio.enabled = true;
            soundOn.SetActive(false);
            soundOff.SetActive(true);
        }
    }

    public void PlayPressed()
    {
        SceneManager.LoadScene("ChooseLevel");
    }


    public void SoundOff()
    {
        soundOn.SetActive(true);
        soundOff.SetActive(false);
        audio.enabled = false;
        PlayerPrefs.SetInt("SoundOn", 0);
    }

    public void SoundOn()
    {
        soundOn.SetActive(false);
        soundOff.SetActive(true);
        audio.enabled = true;
        PlayerPrefs.SetInt("SoundOn", 1);
    }
}
