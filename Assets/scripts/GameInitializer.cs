using System.Collections;
using System.Collections.Generic;
using UnityEngine.Advertisements;
using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    GameObject BG;
    public static float koef;
    GameObject[] gameObjects;

    void Start()
    {
        BG = GameObject.Find("Background");
        float height = Camera.main.orthographicSize * 2f;
        float width = height * Screen.width / Screen.height;
        koef = width / BG.GetComponent<SpriteRenderer>().bounds.size.x;
        gameObjects = FindObjectsOfType(typeof(GameObject)) as GameObject[];
        foreach (GameObject obj in gameObjects)
        {
            if (obj.layer != 5)
            {
                obj.transform.localScale = new Vector3(koef, koef, 1);
            }
            
        }
        BG.transform.localScale = new Vector3(koef + 0.5f, koef + 0.5f, 1);

    }

    void Awake()
    {
        ScreenUtils.Initialize();
    }
}