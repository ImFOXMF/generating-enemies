using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _lifetime = 4;
    
    private ObjectPool<Enemy> _pool;

    private void Start()
    {
        StartCoroutine(ReturnToPoolAfterDelay());
    }
    
    public void SetPool(ObjectPool<Enemy> pool)
    {
        _pool = pool;
    }

    private IEnumerator ReturnToPoolAfterDelay()
    {
        yield return new WaitForSeconds(_lifetime);

        if (_pool != null)
        {
            _pool.Release(this);
        }
    }
}
