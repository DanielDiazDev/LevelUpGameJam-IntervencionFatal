using System;
using UnityEngine;

public class ColliderBridge : MonoBehaviour
{
    public event Action<Collider> OnChildTriggerEnter;
    public event Action<Collision> OnChildColliderEnter;

    private void OnTriggerEnter(Collider other)
    {
        OnChildTriggerEnter?.Invoke(other);
    }

    private void OnCollisionEnter(Collision other)
    {
        OnChildColliderEnter?.Invoke(other);
    }
    
}
