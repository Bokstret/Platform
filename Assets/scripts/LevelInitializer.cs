﻿using UnityEngine;

public class LevelInitializer : MonoBehaviour
{
    GameObject BG;
    public static float koef;
    GameObject[] gameObjects;

    void Awake()
    {
        ScreenUtils.Initialize();
        BG = GameObject.Find("Background");
        float height = Camera.main.orthographicSize * 2f;
        float width = height * Screen.width / Screen.height;
        koef = width / BG.GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Start()
    {
        gameObjects = FindObjectsOfType(typeof(GameObject)) as GameObject[];
        foreach (GameObject obj in gameObjects)
        {
            if (obj.layer != 5)
            {
                obj.transform.localScale = new Vector3(obj.transform.localScale.x * koef, obj.transform.localScale.y * koef, 1);
            }
        }
        GameObject.Find("Grid").layer = 8;
        GameObject.Find("Tilemap").layer = 8;
        BG.transform.localScale = new Vector3(koef + 0.5f, koef + 0.5f, 1);
    }
}