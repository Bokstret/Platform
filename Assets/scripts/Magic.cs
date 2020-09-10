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
        if (hero.transform.position.x < transform.position.x)
        {
            x = hero.transform.position.x - 50;
        }
        else
        {
            x = hero.transform.position.x + 50;
        }

        y = hero.transform.position.y;
        Vector3 difference = hero.transform.position - transform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
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
        }

        else
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(x, y), step);
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Hero") & HeroController.invulnerability == false)
        {
        hero.GetComponent<HP>().health -= 0;
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