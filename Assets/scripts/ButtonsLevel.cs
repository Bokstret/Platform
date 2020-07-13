using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class ButtonsLevel : MonoBehaviour
{
    GameObject HUD;
    GameObject yes;
    GameObject no;
    GameObject exit;
    GameObject menu;
    GameObject restart;
    GameObject pause;
    GameObject resume;
    GameObject next;
    Text sure;

    void Start()
    { 
        HUD = GameObject.Find("HUD");
        yes = GameObject.Find("Yes");
        no = GameObject.Find("No");
        exit = GameObject.Find("Exit");
        menu = GameObject.Find("Menu");
        restart = GameObject.Find("Restart");
        pause = GameObject.Find("Pause");
        resume = GameObject.Find("Resume");
        next = GameObject.Find("Next");
        sure = GameObject.Find("Sure").GetComponent<Text>();

        yes.SetActive(false);
        no.SetActive(false);
        exit.SetActive(false);
        restart.SetActive(false);
        resume.SetActive(false);
        next.SetActive(false);
        menu.SetActive(false);       
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
        SceneManager.LoadScene(0);
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

    public void NextPressed()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
