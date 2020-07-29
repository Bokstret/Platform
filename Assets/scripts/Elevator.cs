using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField]
    private int duration;
    [SerializeField]
    private Vector3 velocity;
    Timer timer;
    int pause = 2;

    void Start()
    {
        timer = gameObject.AddComponent<Timer>();
        timer.Duration = duration;
        timer.Run();
    }

    void FixedUpdate()
    {
        if(timer.Finished & timer.Duration == duration)
        {
            velocity.x = -velocity.x;
            velocity.y = -velocity.y;
            timer.Duration = pause;
            timer.Run();
        }

        if (timer.Finished & timer.Duration == pause)
        {
            timer.Duration = duration;
            timer.Run();
        }

        if (!timer.Finished & timer.Duration == duration)
        {
            transform.position += (velocity * Time.deltaTime);
        }
    }
}
