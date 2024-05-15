using UnityEngine;
using Mirror;

public class ObjectSpawner : NetworkBehaviour
{
    public GameObject[] SpawnPoints;
    public GameObject[] ScrapObjects;
    public int SpawnCount;

    public override void OnStartServer()
    {
        base.OnStartServer();

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

            GameObject obj = Instantiate(scrapObject, SpawnPoints[spawnPointIndex].transform.position, Quaternion.identity);
            NetworkServer.Spawn(obj);
        }
    }
}
