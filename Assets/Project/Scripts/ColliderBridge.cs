using System;
using UnityEngine;

public class ColliderBridge : MonoBehaviour
{
    public event Action<Collider> OnChildTriggerEnter;

    private void OnTriggerEnter(Collider other)
    {
        OnChildTriggerEnter?.Invoke(other);
    }
}
