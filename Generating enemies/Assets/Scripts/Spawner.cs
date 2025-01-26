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

    [SerializeField] private List<Transform> _spawnersList;

    private ObjectPool<Enemy> _pool;

    private void Awake()
    {
        CreatePool();
    }

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    private void GetAction(Enemy enemy)
    {
        Transform spawnPoint = ChooseSpawner();

        enemy.transform.position = spawnPoint.transform.position;
        enemy.transform.rotation = spawnPoint.transform.rotation;
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

    private Transform ChooseSpawner()
    {
        Random random = new Random();

        int index = random.Next(0, _spawnersList.Count);
        Transform spawnPoint = _spawnersList[index];
        return spawnPoint;
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            _pool.Get();
            yield return new WaitForSeconds(_spawnInterval);
        }
    }

    private IEnumerator ReturnToPoolAfterDelay(Enemy enemy)
    {
        yield return new WaitForSeconds(enemy.Lifetime);

        _pool.Release(enemy);
    }
}