using UnityEngine;

public class RangeAttack : MonoBehaviour
{
    [SerializeField]
    GameObject prefabMagic;

    Timer timer;
    GameObject hero;
    Vector2 thrustDirection;
    float xEnemy;
    float yEnemy;
    const float ThrustForce = 8;

    void Start()
    {
        hero = GameObject.Find("Hero");
        timer = gameObject.AddComponent<Timer>();
        timer.Duration = 1;
        timer.Run();
    }

    void Update()
    {
        if (timer.Finished)
        {
            thrustDirection = new Vector2(hero.transform.position.x, hero.transform.position.y + 2.5f);
            xEnemy = gameObject.transform.position.x;
            yEnemy = gameObject.transform.position.y;
            Vector3 location = new Vector3(xEnemy, yEnemy, 0);
            GameObject magic = Instantiate<GameObject>(prefabMagic, location, Quaternion.identity);
            Rigidbody2D rb2DB = magic.GetComponent<Rigidbody2D>();
            rb2DB.AddForce(ThrustForce * (thrustDirection * 20), ForceMode2D.Force);
            timer.Run();
        }
        
    }
}
