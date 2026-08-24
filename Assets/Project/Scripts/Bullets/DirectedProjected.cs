using UnityEngine;

namespace Project.Scripts.Bullets
{
    //Para la magia de fuego y hielo, son los que se pueden apuntar con la mira
    public abstract class DirectedProjected : ProjectileBase
    {
        [SerializeField] private float speed = 10f;
        private Vector3 direction;

        public override void Initialize(Vector3 origin, Vector3 dir)
        {
            transform.position = origin;
            rb.linearVelocity = dir.normalized * speed;
        }
        
    }
}