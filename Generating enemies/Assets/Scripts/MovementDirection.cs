using UnityEngine;

public class MovementDirection : MonoBehaviour
{
    [SerializeField] private float _xDirection;
    [SerializeField] private float _yDirection;
    [SerializeField] private float _zDirection;
    
    public Vector3 StartPoint => transform.position;
    public Vector3 Direction { get; private set; }

    private void Start()
    {
        Direction = new Vector3(_xDirection, _yDirection, _zDirection).normalized;
    }
}
