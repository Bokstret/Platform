﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsChoose : MonoBehaviour
{

    GameObject back;
    void Start()
    {
        back = GameObject.Find("Back");
    }

    public void BackPressed()
    {
        SceneManager.LoadScene(0);
    }

    public void Load(int levelNumber)
    {
        SceneManager.LoadScene(levelNumber);
    }
}
