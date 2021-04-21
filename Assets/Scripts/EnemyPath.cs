using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPath : MonoBehaviour
{
    // Config Params
    WaveConfig waveConfig;

    // State
    int waypointsIndex = 0;
    List<Transform> waypoints;

    // Start is called before the first frame update
    private void Awake()
    {
        if (waveConfig == null && enabled) // && this.enabled is for debugging purposes only
        {
            gameObject.SetActive(false);
        }
    }
    void Start()
    {
        waypoints = waveConfig.GetWayPoints();
        transform.position = waypoints[waypointsIndex].transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        EnemyMovement();
    }

    public void SetWaveConfig(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig;
        gameObject.SetActive(true);
    }

    private void EnemyMovement()
    {
        if (waypointsIndex == waypoints.Count)
        {
            Destroy(gameObject);
        }
        else
        {
            var targetPosition = waypoints[waypointsIndex].transform.position;
            var frameSpeed = waveConfig.GetMovementSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, frameSpeed);
            if (transform.position == targetPosition)
            {
                waypointsIndex++;
            }
        }
    }
}
