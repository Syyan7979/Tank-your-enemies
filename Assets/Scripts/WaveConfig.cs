using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Wave Scriptable Configuration")]
public class WaveConfig : ScriptableObject
{
    [SerializeField] GameObject enemyPrefab, pathPrefab;
    [SerializeField] float movementSpeed = 0.5f, spawnRandomFact = 0.3f, spawnTime = 0.7f;
    [SerializeField] int numberOfSpawns = 5;

    public GameObject GetEnemyPrefab()
    {
        return enemyPrefab;
    }

    public List<Transform> GetWayPoints()
    {
        var wayPoints = new List<Transform>();
        foreach(Transform child in pathPrefab.transform)
        {
            wayPoints.Add(child);
        }

        return wayPoints;
    }

    public float GetMovementSpeed()
    {
        return movementSpeed;
    }

    public float GetSpawnRandomFact()
    {
        return spawnRandomFact;
    }

    public float GetSpawnTime()
    {
        return spawnTime;
    }

    public int GetNumberOfSpawns()
    {
        return numberOfSpawns;
    }
}
