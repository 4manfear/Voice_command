using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_Spawner : MonoBehaviour
{
    public GameObject[] enemy_Spawner_arrray;
    public GameObject enemy;

    private void Start()
    {
        int randomIndex = Random.Range(0, enemy_Spawner_arrray.Length);
        SpawnEnemy(randomIndex);
    }

    private void Update()
    {
        enemy_Spawner_arrray = GameObject.FindGameObjectsWithTag("enemy_Spawner");
    }

    private void FixedUpdate()
    {
        

    }

    void SpawnEnemy(int spawnPointIndex)
    {
        if (spawnPointIndex < enemy_Spawner_arrray.Length)
        {
            // Instantiate at the specified position and rotation of the spawn point
            Instantiate(enemy, enemy_Spawner_arrray[spawnPointIndex].transform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Spawn point index out of range.");
        }
    }

}
