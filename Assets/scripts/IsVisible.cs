using UnityEngine;

public class IsVisible : MonoBehaviour
{
    Renderer m_Renderer;
    EnemiesRotate rotate;
    RangeAttack attack;

    void Start()
    {
        m_Renderer = GetComponent<Renderer>();
        rotate = GetComponent<EnemiesRotate>();
        attack = GetComponent<RangeAttack>();

    }

    void Update()
    {
        if (m_Renderer.isVisible)
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