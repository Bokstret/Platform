using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonsLevel : MonoBehaviour
{
    Timer timer;
    GameObject HUD;
    GameObject buttons;
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
        timer = gameObject.AddComponent<Timer>();
        timer.Duration = 1.5f;

        HUD = GameObject.Find("HUD");
        buttons = GameObject.Find("BUTTONS LEVEL");
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

    void Update()
    {
        if (timer.Finished)
        {
            HUD.SetActive(true);
            pause.SetActive(true);
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
        Time.timeScale = 1;
        HUD.SetActive(false);
        pause.SetActive(false);
        timer.Run();
        SceneChange.sceneId = SceneManager.GetActiveScene().buildIndex;
        SceneChange.sceneEnd = true;
        SceneChange.sceneStarting = false;
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
        Time.timeScale = 1;
        HUD.SetActive(false);
        buttons.SetActive(false);
        SceneChange.sceneId = 1;
        SceneChange.sceneEnd = true;
        SceneChange.sceneStarting = false;
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
        Time.timeScale = 1;
        HUD.SetActive(false);
        buttons.SetActive(false);
        SceneChange.sceneId = SceneManager.GetActiveScene().buildIndex + 1;
        SceneChange.sceneEnd = true;
        SceneChange.sceneStarting = false;
    }

    public void MenuPressed()
    {
        Time.timeScale = 1;
        HUD.SetActive(false);
        buttons.SetActive(false);
        SceneChange.sceneId = 0;
        SceneChange.sceneEnd = true;
        SceneChange.sceneStarting = false;
    }
}
