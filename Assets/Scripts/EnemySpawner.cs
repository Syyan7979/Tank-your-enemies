using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Config Params
    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] int waveConfigIndex = 0;
    [SerializeField] bool looping = false;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(SpawnAllWaves());
        } while (looping);
        
    }

    private IEnumerator SpawnsEnemiesInWaves(WaveConfig waveConfig)
    {
        for (int i = 0; i < waveConfig.GetNumberOfSpawns(); i++)
        {
            var newEnemy = Instantiate(waveConfig.GetEnemyPrefab(), waveConfig.GetWayPoints()[0].transform.position, Quaternion.identity);
            newEnemy.GetComponent<EnemyPath>().SetWaveConfig(waveConfig);
            yield return new WaitForSeconds(waveConfig.GetSpawnTime());
        }
    }

    private IEnumerator SpawnAllWaves()
    {
        for (int waveIndex = waveConfigIndex; waveIndex < waveConfigs.Count; waveIndex++)
        {
            var currentWave = waveConfigs[waveIndex];
            yield return StartCoroutine(SpawnsEnemiesInWaves(currentWave));
        }
    }
}
