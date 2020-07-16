using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsChoose : MonoBehaviour
{

    GameObject buttons;
    void Start()
    {
        buttons = GameObject.Find("BUTTONS CHOOSE");
    }

    public void BackPressed()
    {
        buttons.SetActive(false);
        SceneChange.sceneId = 0;
        SceneChange.sceneEnd = true;
        SceneChange.sceneStarting = false;
    }

    public void Load(int levelNumber)
    {
        buttons.SetActive(false);
        SceneChange.sceneId = levelNumber;
        SceneChange.sceneEnd = true;
        SceneChange.sceneStarting = false;
    }
}
