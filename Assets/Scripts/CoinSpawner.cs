using System;
using System.Collections.Generic;
using EMILtools.Extensions;
using UnityEngine;
using Random = UnityEngine.Random;

public class CoinSpawner : MonoBehaviour
{

    
    public GameObject coinPrefab;
    public float maxY, minY;
    public Vector2 spawnDelay;
    float _spawnDelay;
    public float moveSpeed;
    float spawnTimer;
    public float maxTimeAlive = 3;
    public List<GameObject> spawnedCoins = new();

    void Start()
    {
        _spawnDelay = Random.Range(spawnDelay.x, spawnDelay.y);
    }

    public void FixedUpdate()
    {
         spawnTimer += Time.fixedDeltaTime;
         TrySpawn();
         MoveCoins();
    }

    void MoveCoins()
    {
        for (int i = 0; i < spawnedCoins.Count; i++)
        {
            if(spawnedCoins[i] == null) { spawnedCoins.RemoveAt(i); i--; }
            else spawnedCoins[i].transform.Translate(Vector3.left * moveSpeed);
        }

    }

    void TrySpawn()
    {
        if (spawnTimer < _spawnDelay) return;
        Spawn();
        spawnTimer = 0;
        _spawnDelay = Random.Range(spawnDelay.x, spawnDelay.y);
    }

     void Spawn()
    {
        var coin = Instantiate(coinPrefab, transform.position, Quaternion.identity);
        coin.transform.position = coin.transform.position.With(y: Random.Range(minY, maxY));
        spawnedCoins.Add(coin);
    }
}
