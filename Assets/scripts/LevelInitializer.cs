using UnityEngine;
using UnityEngine.UI;

public class LevelInitializer : MonoBehaviour
{
    public Transform music;
    Timer timer;
    GameObject BG;
    public static float koef;
    GameObject[] gameObjects;
    Button dialogue;
    AudioSource audio;

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
        if (!GameObject.Find("MusicLevel(Clone)"))
        {
            Instantiate(music);
        }
        audio = GameObject.Find("MusicLevel(Clone)").GetComponent<AudioSource>();

        if (PlayerPrefs.GetInt("SoundOn") == 0)
        {
            audio.mute = true;
        }
        else
        {
            audio.mute = false;
        }

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
            HeroController.HPBarCheck();
            dialogue.onClick.Invoke();
        }
    }
}
