using System;
using UnityEngine;

namespace Project.Scripts.Bullets
{
    public abstract class ProjectileBase : MonoBehaviour
    {
        protected Rigidbody rb;

        protected virtual void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }
        public abstract void Initialize(Vector3 origin, Vector3 direction);
    }
}