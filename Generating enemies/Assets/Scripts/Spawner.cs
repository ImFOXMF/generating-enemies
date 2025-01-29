using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Random = System.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private float _spawnInterval = 2f;
    [SerializeField] private int _poolCapacity = 5;
    [SerializeField] private int _poolMaxSize = 5;

    [SerializeField] private List<SpawnPoint> _spawnersList;

    private ObjectPool<Enemy> _pool;
    private WaitForSeconds _spawnWait;

    private void Awake()
    {
        CreatePool();
        _spawnWait = new WaitForSeconds(_spawnInterval);
    }

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    private void GetAction(Enemy enemy)
    {
        enemy.Move(ChooseSpawner());
        enemy.gameObject.SetActive(true);

        StartCoroutine(ReturnToPoolAfterDelay(enemy));
    }

    private void CreatePool()
    {
        _pool = new ObjectPool<Enemy>(
            createFunc: () =>
            {
                var enemy = Instantiate(_enemyPrefab);
                return enemy;
            },
            actionOnGet: (enemy) => GetAction(enemy),
            actionOnRelease: (enemy) => enemy.gameObject.SetActive(false),
            actionOnDestroy: (enemy) => Destroy(enemy.gameObject),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize);
    }

    private SpawnPoint ChooseSpawner()
    {
        Random random = new Random();

        int index = random.Next(0, _spawnersList.Count);
        SpawnPoint spawnPoint = _spawnersList[index];
        return spawnPoint;
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            _pool.Get();
            yield return _spawnWait;
        }
    }

    private IEnumerator ReturnToPoolAfterDelay(Enemy enemy)
    {
        yield return enemy.WaitForLifeTime;

        _pool.Release(enemy);
    }
}