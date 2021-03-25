﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    private float spawnRange = 9;
    public int enemyCount;
    public int waveNumber = 1;
    public GameObject powerUpPrefab;
    public bool gameOver = false;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        SpawnEnemyWave(3);
        SpawnPowerup();  
    }

    // Update is called once per frame
    void Update()
    {
        gameOver = !GameObject.Find("Player");

        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;

        if (enemyCount == 0 && !gameOver)
        {
            waveNumber++;
            SpawnEnemyWave(waveNumber);
            SpawnPowerup();
        }
    }

    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX;
        float spawnPosZ;
        if(player != null){
            spawnPosX = Random.Range((player.gameObject.transform.position.x - spawnRange), (player.gameObject.transform.position.x + spawnRange));
            spawnPosZ = Random.Range((player.gameObject.transform.position.z - spawnRange), (player.gameObject.transform.position.z + spawnRange));
        }else{
            spawnPosX = Random.Range(-spawnRange, spawnRange);
            spawnPosZ = Random.Range(-spawnRange, spawnRange);
        }
        return new Vector3(spawnPosX, 0, spawnPosZ);
    }

    void SpawnEnemyWave(int enemytoSpawn)
    {
        for (int i = 0; i < enemytoSpawn; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        }
    }

    void SpawnPowerup()
    {
        Instantiate(powerUpPrefab, GenerateSpawnPosition(), powerUpPrefab.transform.rotation);
    }
}
