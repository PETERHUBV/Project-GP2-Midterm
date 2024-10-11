using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject EnemyPrefab; 
    public float SpawnInterval = 2f; 
    public float SpawnRange = 10f; 

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    // Update is called once per frame
  public IEnumerator SpawnEnemies()
    {
        while (true)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(SpawnInterval); 
        }
    }

    public void SpawnEnemy()
    {
       
        Vector3 spawnPosition = new Vector3(
            Random.Range(-SpawnRange, SpawnRange),
            0, 
            Random.Range(-SpawnRange, SpawnRange)
        );

       
        Instantiate(EnemyPrefab, spawnPosition, Quaternion.identity);
    }
}