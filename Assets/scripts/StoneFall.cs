using UnityEngine;

public class StoneFall : MonoBehaviour
{
    [SerializeField]
    GameObject prefabStone1;
    [SerializeField]
    GameObject prefabStone2;
    [SerializeField]
    GameObject prefabStone3;

    public float spawnTime;
    float[] heroPosition = new float[2];
    float[] stonePosition = new float[2];
    GameObject[] prefabs = new GameObject[3];
    Timer timer;
    GameObject hero;

    void Start()
    {
        prefabs[0] = prefabStone1;
        prefabs[1] = prefabStone2;
        prefabs[2] = prefabStone3;
        hero = GameObject.Find("Hero");
        timer = gameObject.AddComponent<Timer>();
        timer.Duration = spawnTime;
        timer.Run();
    }

    void Update()
    {
        if (timer.Finished)
        {
            heroPosition[0] = hero.transform.position.x;
            heroPosition[1] = hero.transform.position.y;
            stonePosition[0] = Random.Range(heroPosition[0] - 3, heroPosition[0] + 3);
            stonePosition[1] = heroPosition[1] + 10;
            Vector3 location = new Vector3(stonePosition[0], stonePosition[1], 0);
            GameObject stone = Instantiate(prefabs[Random.Range(0, prefabs.Length)], location, Quaternion.identity);
            stone.transform.localScale = new Vector3(stone.transform.localScale.x * LevelInitializer.koef, stone.transform.localScale.y * LevelInitializer.koef, 1);
            timer.Run();
        }  
    }
}
