using UnityEngine;
using UnityEngine.UI;

public class LevelInitializer : MonoBehaviour
{
    Timer timer;
    GameObject BG;
    public static float koef;
    GameObject[] gameObjects;
    Button dialogue;

    void Awake()
    {
        timer = gameObject.AddComponent<Timer>();
        timer.Duration = 1;

        ScreenUtils.Initialize();
        BG = GameObject.Find("Background");
        float height = Camera.main.orthographicSize * 2f;
        float width = height * Screen.width / Screen.height;
        koef = width / BG.GetComponent<SpriteRenderer>().bounds.size.x;
        dialogue = GameObject.Find("StartDialogue").GetComponent<Button>();

    }

    void Start()
    {
        Physics2D.IgnoreLayerCollision(10, 9, false);
        Physics2D.IgnoreLayerCollision(10, 11, false);
        timer.Run();
        gameObjects = FindObjectsOfType(typeof(GameObject)) as GameObject[];
        foreach (GameObject obj in gameObjects)
        {
            if (obj.tag != "NFS")
            {
                obj.transform.localScale = new Vector3(obj.transform.localScale.x * koef, obj.transform.localScale.y * koef, 1);
            }
        }
        BG.transform.localScale = new Vector3(koef + 0.5f, koef + 0.5f, 1);

    }

    void Update()
    {
        if (timer.Finished)
        {
            timer.Finished = false;
            dialogue.onClick.Invoke();
        }
    }
}
