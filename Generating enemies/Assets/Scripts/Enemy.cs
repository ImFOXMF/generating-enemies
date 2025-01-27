using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] public int Lifetime { get; } = 4;
    
    public WaitForSeconds WaitForLifeTime;
}
