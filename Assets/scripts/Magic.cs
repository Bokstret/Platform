using UnityEngine;

public class Magic : MonoBehaviour
{
    GameObject hero;
    Timer magicAlive;
    const int alive = 3;
    public float speed = 5f;
    private float x;
    private float y;

    void Start()
    {
        hero = GameObject.Find("Hero");
        x = hero.transform.position.x;
        y = hero.transform.position.y - 1;
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
        else
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(x,y), step);
        }

    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Hero") & HeroController.invulnerability == false)
        {
            hero.GetComponent<HP>().health -= 5;
            HeroController.InvulnerabilityOn();
            if (hero.GetComponent<HP>().health == 0)
            {
                Destroy(hero);
                //to do call lose function
            }
        }
        if (coll.gameObject.CompareTag("Hero") | coll.gameObject.layer == 8)
        {
            Destroy(gameObject);
        }
    }
}