using UnityEngine;

public class Magic : MonoBehaviour
{
    GameObject hero;
    Timer magicAlive;
    const int alive = 3;
    public float speed = 5f;
    private float x;
    private float y;
    Vector3 lastPos;
    Vector3 currPos;

    void Start()
    {
        hero = GameObject.Find("Hero");
        x = hero.transform.position.x + 5;
        y = hero.transform.position.y;
        Vector3 difference = hero.transform.position - transform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
        magicAlive = gameObject.AddComponent<Timer>();
        magicAlive.Duration = alive;
        magicAlive.Run();
        lastPos = transform.position;
    }

    void Update()
    {
        currPos = transform.position;
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
            if(currPos == lastPos)
            {
                Destroy(gameObject);
            }
            lastPos = currPos;
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
                Destroy(hero);
                //to do call lose function
            }
        }
    Destroy(gameObject);
    }
}