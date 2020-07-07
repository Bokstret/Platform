using UnityEngine;

public class Magic : MonoBehaviour
{
    GameObject hero;
    Timer magicAlive;
    const int alive = 5;

    void Start()
    {
        hero = GameObject.Find("Hero");
        magicAlive = gameObject.AddComponent<Timer>();
        magicAlive.Duration = alive;
        magicAlive.Run();
    }

    void Update()
    {
            if (magicAlive.Finished)
        {

            GameObject magic = GameObject.FindWithTag("Magic");
            if (magic != null)
            {
                Destroy(magic);
            }

            magicAlive.Run();
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