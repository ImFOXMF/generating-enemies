using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    
    private Vector3 _direction;

    private void Update()
    {
        transform.Translate(_direction * (_speed * Time.deltaTime));
    }

    public void Initialize(Vector3 direction)
    {
        _direction = direction;
    }
}
