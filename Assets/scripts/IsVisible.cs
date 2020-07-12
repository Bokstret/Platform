using UnityEngine;

public class IsVisible : MonoBehaviour
{
    Renderer Enemy;
    EnemiesRotate rotate;
    RangeAttack attack;

    void Start()
    {
        Enemy = GetComponent<Renderer>();
        rotate = GetComponent<EnemiesRotate>();
        attack = GetComponent<RangeAttack>();
    }

    void Update()
    {
        if (Enemy.isVisible)
        {
            rotate.enabled = true;
            attack.enabled = true;
        }
        else
        {
            rotate.enabled = false;
            attack.enabled = false;
        }
    }
}