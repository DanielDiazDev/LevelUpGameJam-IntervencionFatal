using System;
using System.Collections;
using System.Threading;
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

        void Start()
        {
            StartCoroutine(TimeoutDestroy());
        }

        public abstract void Initialize(Vector3 origin, Vector3 direction);

        private IEnumerator TimeoutDestroy()
        {
            yield return new WaitForSeconds(10);
            Destroy(gameObject);
        }
    }
}