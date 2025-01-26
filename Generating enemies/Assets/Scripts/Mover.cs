using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] float _speed = 5f;

    void Update()
    {
        transform.Translate(Vector3.forward * (_speed * Time.deltaTime));
    }
}
