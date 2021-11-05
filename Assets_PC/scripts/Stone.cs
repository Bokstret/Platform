using UnityEngine;

public class Stone : MonoBehaviour
{
    GameObject hero;
    Timer stoneAlive;
    const int alive = 8;
    public float speed = 20f;

    void Start()
    {
        hero = GameObject.Find("Hero");
        stoneAlive = gameObject.AddComponent<Timer>();
        stoneAlive.Duration = alive;
        stoneAlive.Run();
    }

    void Update()
    {
        if (stoneAlive.Finished)
        {
            GameObject stone = GameObject.FindWithTag("Magic");
            if (stone != null)
            {
                Destroy(stone);
            }
        }

        else
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, transform.position.y - 1), step);
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Hero") & HeroController.invulnerability == false)
        {
        hero.GetComponent<HP>().health -= 10;
        HeroController.InvulnerabilityOn();
        HeroController.HPBarCheck();
            if (hero.GetComponent<HP>().health == 0)
            {
                HeroController.Death();
            }
        }
    Destroy(gameObject);
    }
}