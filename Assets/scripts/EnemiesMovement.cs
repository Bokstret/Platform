using UnityEngine;

public class EnemiesMovement : MonoBehaviour
{
    Animator anim;
    public LayerMask whatIsWall;
    private bool collision = false;
    public Transform wallCheck;
    private float wallRadius = 0.2f;
    public float Speed = 5f;
    private int move = -1;
    private float koef;
    GameObject hero;

    void Start()
    {
        anim = GetComponent<Animator>();
        hero = GameObject.Find("Hero");
        koef = GameInitializer.koef;
    }

    void FixedUpdate()
    {
        if (anim.GetCurrentAnimatorClipInfo(0)[0].clip.name != "Rise")
        {
            collision = Physics2D.OverlapCircle(wallCheck.position, wallRadius, whatIsWall);
            if (collision)
            {
                move *= -1;
                transform.localScale = new Vector3(transform.localScale.x * -1, koef, 1);
            }
            Vector3 target = new Vector3(transform.position.x + move, transform.position.y, transform.position.z);
            float step = Speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target, step);
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

    }
}
