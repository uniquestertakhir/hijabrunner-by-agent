using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform player;
    public GameObject obstaclePrefab;
    public GameObject coinPrefab;
    public float firstSpawnZ = 20f;
    public float stepZ = 12f;
    public int aheadSegments = 30;
    public float laneOffset = 2f;

    private float nextZ;
    private readonly List<Transform> spawned = new();

    void Start()
    {
        nextZ = firstSpawnZ;
        for (int i = 0; i < aheadSegments; i++)
        {
            SpawnOne();
        }
    }

    void Update()
    {
        if (player == null) return;
        while (nextZ < player.position.z + aheadSegments * stepZ)
        {
            SpawnOne();
        }

        for (int i = spawned.Count - 1; i >= 0; i--)
        {
            var t = spawned[i];
            if (t == null)
            {
                spawned.RemoveAt(i);
                continue;
            }
            if (t.position.z < player.position.z - 10f)
            {
                Destroy(t.gameObject);
                spawned.RemoveAt(i);
            }
        }
    }

    void SpawnOne()
    {
        int laneObs = Random.Range(0, 3);
        Vector3 posObs = new Vector3((laneObs - 1) * laneOffset, 0.5f, nextZ);
        if (obstaclePrefab != null)
        {
            var inst = Instantiate(obstaclePrefab, posObs, Quaternion.identity);
            spawned.Add(inst.transform);
        }

        var lanes = new List<int> { 0, 1, 2 };
        lanes.Remove(laneObs);
        int laneCoin = lanes[Random.Range(0, lanes.Count)];
        Vector3 posCoin = new Vector3((laneCoin - 1) * laneOffset, 1.0f, nextZ + 2f);
        if (coinPrefab != null)
        {
            var instCoin = Instantiate(coinPrefab, posCoin, Quaternion.identity);
            spawned.Add(instCoin.transform);
        }

        nextZ += stepZ;
    }
}
