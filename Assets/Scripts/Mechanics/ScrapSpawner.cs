using UnityEngine;

public class ScrapSpawner : MonoBehaviour
{
    public GameObject[] SpawnPoints;
    public GameObject[] ScrapObjects;
    public int SpawnCount;

    void Start()
    {

        bool[] usedSpawnPoints = new bool[SpawnPoints.Length];

        for (int i = 0; i < SpawnCount; i++)
        {
            int spawnPointIndex;
            do
            {
                spawnPointIndex = Random.Range(0, SpawnPoints.Length);
            } while (usedSpawnPoints[spawnPointIndex]);

            usedSpawnPoints[spawnPointIndex] = true;

            GameObject scrapObject = ScrapObjects[Random.Range(0, ScrapObjects.Length)];

            Instantiate(scrapObject, SpawnPoints[spawnPointIndex].transform.position, Quaternion.identity);
        }
        gameObject.SetActive(false);
    }
}
