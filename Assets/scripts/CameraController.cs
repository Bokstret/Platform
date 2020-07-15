using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float dumping = 1.5f;
    private Vector2 offset = new Vector2(2f, 2f);
    private bool isLeft;
    private Transform hero;
    private int lastX;

    void Start()
    {
        offset = new Vector2(Mathf.Abs(offset.x), offset.y);
        FindPlayer(isLeft);
    }

    void Update()
    {
        if (hero)
        {
            int currentX = Mathf.RoundToInt(hero.position.x);
            if (currentX > lastX)
            {
                isLeft = false;
            }
            else if(currentX < lastX)
            {
                isLeft = true;
            }
            lastX = Mathf.RoundToInt(hero.position.x);

            Vector3 target;
            if (isLeft)
            {
                target = new Vector3(hero.position.x - offset.x, hero.position.y + offset.y, transform.position.z);
            }
            else
            {
                target = new Vector3(hero.position.x + offset.x, hero.position.y + offset.y, transform.position.z);
            }
            Vector3 currentPosition = Vector3.Lerp(transform.position, target, dumping * Time.deltaTime);
            transform.position = currentPosition;
        }
    }

    public void FindPlayer(bool playerIsLeft)
    {
        hero = GameObject.Find("Hero").transform;
        lastX = Mathf.RoundToInt(hero.position.x);
        if (playerIsLeft)
        {
            transform.position = new Vector3(hero.position.x - offset.x, hero.position.y + offset.y, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(hero.position.x + offset.x, hero.position.y + offset.y, transform.position.z);
        }
    }
}
