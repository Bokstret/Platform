using UnityEngine;

public class EnemiesMovement : MonoBehaviour
{
    Animator anim;
    public int howLong = 2;
    public float Speed = 5f;
    private int move = -1;
    private float koef;
    GameObject hero;
    Timer timer;

    void Start()
    {
        timer = gameObject.AddComponent<Timer>();
        timer.Duration = howLong;
        timer.Run();
        anim = GetComponent<Animator>();
        hero = GameObject.Find("Hero");
        koef = GameInitializer.koef;
    }

    void FixedUpdate()
    {
        if (anim.GetCurrentAnimatorClipInfo(0)[0].clip.name != "Rise")
        {
            if (timer.Finished)
            {
                move *= -1;
                transform.localScale = new Vector3(transform.localScale.x * -1, koef, 1);
                timer.Run();
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
            anim.SetBool("Attacking", true);
            enabled = false;
            Invoke("ToIdle", 0.6f);
            hero.GetComponent<HP>().health -= 5;
            if (hero.GetComponent<HP>().health == 0)
            {
                Destroy(hero);
                //to do call lose function
            }
            HeroController.InvulnerabilityOn();
        }
    }
    private void ToIdle()
    {
        enabled = true;
        anim.SetBool("Attacking", false);
    }
}
