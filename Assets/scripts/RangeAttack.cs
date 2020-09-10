using UnityEngine;

public class RangeAttack : MonoBehaviour
{
    [SerializeField]
    GameObject prefabMagic;

    Timer timer;
    GameObject hero;
    float xEnemy;
    float yEnemy;

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
            xEnemy = gameObject.transform.position.x;
            yEnemy = gameObject.transform.position.y;
            Vector3 location = new Vector3(xEnemy, yEnemy, 0);
            Instantiate<GameObject>(prefabMagic, location, Quaternion.identity);
            timer.Run();
        }  
    }
}
