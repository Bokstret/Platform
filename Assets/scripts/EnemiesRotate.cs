using UnityEngine;

public class EnemiesRotate : MonoBehaviour
{
    GameObject hero;
    SpriteRenderer spriteEnemy;

    void Start()
    {
        hero = GameObject.Find("Hero");
        spriteEnemy= GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Vector3 difference = hero.transform.position - transform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
        if (hero.transform.position.x < transform.position.x)
        {
            spriteEnemy.flipY = true;
        }
        else
        {
            spriteEnemy.flipY = false;
        }
    }
}
