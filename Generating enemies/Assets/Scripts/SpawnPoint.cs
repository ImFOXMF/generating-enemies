using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private Vector3 _direction;
    
    public Vector3 UnitVector { get; private set; }
    public Vector3 Start => transform.position;

    private void Awake()
    {
        UnitVector = _direction.normalized;
    }
}
