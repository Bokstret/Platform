﻿using UnityEngine;

public class EnemiesMovement : MonoBehaviour
{
    Animator anim;
    public float howLong = 2;
    public float Speed = 5f;
    public int move = -1;
    GameObject hero;
    Timer timer;

    void Start()
    {
        timer = gameObject.AddComponent<Timer>();
        timer.Duration = howLong;
        timer.Run();
        anim = GetComponent<Animator>();
        hero = GameObject.Find("Hero");
    }

    void FixedUpdate()
    {
        if (timer.Finished)
        {
            move *= -1;
            gameObject.GetComponent<SpriteRenderer>().flipX = !gameObject.GetComponent<SpriteRenderer>().flipX;
            timer.Run();
        }
        Vector3 target = new Vector3(transform.position.x + move, transform.position.y, transform.position.z);
        float step = Speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target, step);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Hero") & HeroController.invulnerability == false)
        {
            anim.SetBool("Attacking", true);
            enabled = false;
            Invoke("ToIdle", 0.6f);
            hero.GetComponent<HP>().health -= 10;

            HeroController.InvulnerabilityOn();
            HeroController.HPBarCheck();
            if (hero.GetComponent<HP>().health == 0)
            {
                HeroController.Death();
            }
        }
    }
    private void ToIdle()
    {
        enabled = true;
        anim.SetBool("Attacking", false);
        gameObject.GetComponent<SpriteRenderer>().flipX = !gameObject.GetComponent<SpriteRenderer>().flipX;
        move *= -1;
    }
}
