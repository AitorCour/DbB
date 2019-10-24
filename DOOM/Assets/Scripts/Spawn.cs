using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spawn : MonoBehaviour
{
    public int maxEnemies;
    public GameObject enemyPrefab;
    public Transform enemiesTransform;
    public EnemyBehaviour[] enemies;
    private int currentEnemy = 0;

    private float timeCounter;
    public float spawnTime;
    // Start is called before the first frame update
    void Start()
    {
        CreateEnemies();
    }

    // Update is called once per frame
    void Update()
    {
        if (timeCounter >= spawnTime)
        {
            SpawnEnemy();
            timeCounter = 0;
        }
        else timeCounter += Time.deltaTime;
    }

    void CreateEnemies()
    {
        enemies = new EnemyBehaviour[maxEnemies];

        for(int i = 0; i < maxEnemies; i++)
        {
            Vector3 spawnPos = enemiesTransform.position;
            spawnPos.z -= i * 2;
            GameObject b = Instantiate(enemyPrefab, spawnPos, Quaternion.identity, enemiesTransform);
            b.name = "Enemy_" + i;
            enemies[i] = b.GetComponent<EnemyBehaviour>();
        }
    }
    void SpawnEnemy()
    {
        if (enemies[currentEnemy].isInUse == false)
        {
            enemies[currentEnemy].transform.position = transform.position;
            enemies[currentEnemy].canMove = true;
            enemies[currentEnemy].animator.enabled = true;
            enemies[currentEnemy].isInUse = true;
            currentEnemy++;
            if (currentEnemy >= maxEnemies) currentEnemy = 0;
        }
        else if(enemies[currentEnemy].isInUse == true)
        {
            currentEnemy++;
            if (currentEnemy >= maxEnemies) currentEnemy = 0;
            //SpawnEnemy();
        }
    }
}
