using UnityEngine;

public class MovementDirection : MonoBehaviour
{
    [SerializeField] private float _x;
    [SerializeField] private float _y;
    [SerializeField] private float _z;
    
    public Vector3 UnitVector { get; private set; }
    public Vector3 StartPoint => transform.position;

    private void Start()
    {
        UnitVector = new Vector3(_x, _y, _z).normalized;
    }
}
