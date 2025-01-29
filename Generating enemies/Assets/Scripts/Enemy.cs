using UnityEngine;

[RequireComponent(typeof(Mover))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private int _lifetime;
    private Mover _mover;
    
    public WaitForSeconds WaitForLifeTime{ get; private set; }

    private void Awake()
    {
        WaitForLifeTime = new WaitForSeconds(_lifetime);
        _mover = GetComponent<Mover>();
    }

    public void Move(SpawnPoint spawnPoint)
    {
        transform.position = spawnPoint.Start;
        _mover.Initialize(spawnPoint.UnitVector);
    }
}
