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
    
    [SerializeField] private List<GameObject> _spawnersList;
    
    private ObjectPool<Enemy> _pool;

    private void Awake()
    {
        CreatePool();
    }
    
    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    private void GetAction(Enemy enemy)
    {
        GameObject spawnPoint = ChooseSpawner();
        Vector3 spawnPosition = spawnPoint.transform.position;
        Quaternion spawnRotation = spawnPoint.transform.rotation;
        
        enemy.transform.position = spawnPosition;
        enemy.transform.rotation = spawnRotation;
        enemy.gameObject.SetActive(true);
    }

    private void CreatePool()
    {
        _pool = new ObjectPool<Enemy>(
        createFunc: () =>
        {
            var enemy = Instantiate(_enemyPrefab);
            enemy.SetPool(_pool);
            return enemy;
        },
        actionOnGet: (enemy) => GetAction(enemy),
        actionOnRelease: (enemy) => enemy.gameObject.SetActive(false),
        actionOnDestroy: (enemy) => Destroy(enemy.gameObject),
        collectionCheck: true,
        defaultCapacity: _poolCapacity,
        maxSize: _poolMaxSize);
    }

    private GameObject ChooseSpawner()
    {
        Random random = new Random();
        
        int index = random.Next(0, _spawnersList.Count);
        GameObject spawnPoint = _spawnersList[index];
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
}
