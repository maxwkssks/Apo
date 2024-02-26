using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : BaseManager
{
    public GameObject[] Enemys;
    public Transform[] EnemySpawnTransform;
    public float CoolDownTime;
    public int MaxSpawnEnemyCount;

    private int _spawnCount = 0;
    public int BossSpawnCount = 10;

    private bool _bSpawnBoss = false;

    public GameObject BossA;

    public override void Init(GameManager gameManager)
    {
        base.Init(gameManager);
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        while (!_bSpawnBoss)
        {
            yield return new WaitForSeconds(CoolDownTime);

            int spawnCount = Random.Range(1, EnemySpawnTransform.Length);
            List<int> availablePositions = new List<int>(EnemySpawnTransform.Length);

            for (int i = 0; i < EnemySpawnTransform.Length; i++)
            {
                availablePositions.Add(i);
            }

            for (int i = 0; i < spawnCount; i++)
            {
                int randomEnemy = Random.Range(0, Enemys.Length);
                int randomPositionIndex = Random.Range(0, availablePositions.Count - 1);
                int randomPosition = availablePositions[randomPositionIndex];

                availablePositions.RemoveAt(randomPositionIndex);

                Instantiate(Enemys[randomEnemy], EnemySpawnTransform[randomPosition].position, Quaternion.identity);
            }

            _spawnCount += spawnCount;

            if (_spawnCount >= BossSpawnCount)
            {
                _bSpawnBoss = true;
                Instantiate(BossA, new Vector3(EnemySpawnTransform[1].position.x, EnemySpawnTransform[1].position.y + 1, 0f), Quaternion.identity);
            }
        }
    }
}