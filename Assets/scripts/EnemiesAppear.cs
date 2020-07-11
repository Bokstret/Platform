using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesAppear : MonoBehaviour
{
    Animator anim;
    Timer timer;

    void Start()
    {
        timer = gameObject.AddComponent<Timer>();
        timer.Duration = 0.8f;
        anim = GetComponent<Animator>(); 
    }

    void Update()
    {
        if (timer.Finished)
        {
            anim.Play("Idle");
        }
    }

    void OnBecameVisible()
    {
        anim.Play("Rise");
        timer.Run();
    }
}
