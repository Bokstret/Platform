using UnityEngine;

public class RangeAttack : MonoBehaviour
{
    [SerializeField]
    GameObject prefabMagic;

    Timer timer;
    float xEnemy;
    float yEnemy;

    void Start()
    {
        timer = gameObject.AddComponent<Timer>();
        timer.Duration = 2.5f;
        timer.Run();
    }

    void Update()
    {
        if (timer.Finished)
        {
            xEnemy = gameObject.transform.position.x;
            yEnemy = gameObject.transform.position.y;
            Vector3 location = new Vector3(xEnemy, yEnemy, 0);
            Instantiate<GameObject>(prefabMagic, location, Quaternion.identity);
            timer.Run();
        }  
    }
}
