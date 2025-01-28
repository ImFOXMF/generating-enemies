using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _lifetime;
    public WaitForSeconds WaitForLifeTime{ get; private set; }

    private void Awake()
    {
        WaitForLifeTime = new WaitForSeconds(_lifetime);
    }

    public void Move(MovementDirection spawnPoint)
    {
        Mover mover = GetComponent<Mover>();
        
        transform.position = spawnPoint.StartPoint;
        mover.Initialize(spawnPoint.UnitVector);
    }
}
