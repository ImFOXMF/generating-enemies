using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] public int Lifetime { get; } = 4;

    private WaitForSeconds _waitForLifeTime;

    public void ChangeLifetimeWaiting(float seconds)
    {
        _waitForLifeTime = new WaitForSeconds(seconds);
    }
    
    public WaitForSeconds WaitForLifeTime => _waitForLifeTime;
}
