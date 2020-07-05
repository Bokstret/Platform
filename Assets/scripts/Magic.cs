using UnityEngine;

public class Magic : MonoBehaviour
{
    GameObject hero;
    Timer timer;
    const int alive = 5;

    void Start()
    {
        hero = GameObject.Find("Hero");
        timer = gameObject.AddComponent<Timer>();
        timer.Duration = alive;
        timer.Run();
    }

    void Update()
    {
        if (timer.Finished)
        {

            GameObject magic = GameObject.FindWithTag("Magic");
            if (magic != null)
            {
                Destroy(magic);
            }

            timer.Run();
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Hero") & HeroController.invulnerability == false)
        {
            hero.GetComponent<HP>().health -= 5;
            if (hero.GetComponent<HP>().health == 0)
            {
                Destroy(hero);
                //to do call lose function
            }
            HeroController.InvulnerabilityOn();
        }
        if (coll.gameObject.CompareTag("Hero") | coll.gameObject.layer == 8)
        {
            Destroy(gameObject);
        }
        

    }
}