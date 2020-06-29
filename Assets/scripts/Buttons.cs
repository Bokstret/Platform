using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    GameObject HUD;
    GameObject yes;
    GameObject no;
    GameObject exit;
    GameObject play;
    GameObject menu;
    GameObject restart;
    GameObject pause;
    GameObject resume;
    GameObject next;
    GameObject back;
    GameObject soundOff;
    GameObject soundOn;
    AudioSource audio;
    Text sure;

    void Start()
    {
        HUD = GameObject.Find("HUD");
        yes = GameObject.Find("Yes");
        no = GameObject.Find("No");
        exit = GameObject.Find("Exit");
        play = GameObject.Find("Play");
        menu = GameObject.Find("Menu");
        restart = GameObject.Find("Restart");
        pause = GameObject.Find("Pause");
        resume = GameObject.Find("Resume");
        next = GameObject.Find("Next");
        back = GameObject.Find("Back");
        soundOff = GameObject.Find("SoundOff");
        soundOn = GameObject.Find("SoundOn");
        audio = GameObject.Find("Main Camera").GetComponent<AudioSource>();
        sure = GameObject.Find("Sure").GetComponent<Text>();

        yes.SetActive(false);
        no.SetActive(false);
        exit.SetActive(false);
        restart.SetActive(false);
        pause.SetActive(false);
        resume.SetActive(false);
        next.SetActive(false);
        back.SetActive(false);

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

    public void PausePressed()
    {
        HUD.SetActive(false);
        pause.SetActive(false);
        resume.SetActive(true);
        restart.SetActive(true);
        exit.SetActive(true);
        Time.timeScale = 0;
    }
    public void ResumePressed()
    {
        HUD.SetActive(true);
        pause.SetActive(true);
        resume.SetActive(false);
        restart.SetActive(false);
        exit.SetActive(false);
        Time.timeScale = 1;
    }

    public void RestartPressed()
    {
        //todo reload CurrentScene
    } 

    public void ExitPressed()
    {
        resume.SetActive(false);
        restart.SetActive(false);
        exit.SetActive(false);
        yes.SetActive(true);
        no.SetActive(true);
        sure.enabled = true;
    }

    public void YesPressed()
    {
        //todo load MenuScene
    }

    public void NoPressed()
    {
        yes.SetActive(false);
        no.SetActive(false);
        sure.enabled = false;
        restart.SetActive(true);
        exit.SetActive(true);
        resume.SetActive(true);
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
