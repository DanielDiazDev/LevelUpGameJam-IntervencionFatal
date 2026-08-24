using System;
using UnityEngine;

namespace Project.Scripts.Bullets
{
    //Magia para los de aire y semilla, son los que se generan al frente del jugador,
    public abstract class SpawnedProjectile : ProjectileBase
    {
        [SerializeField] protected float xPosition = 1f;
        [SerializeField] protected float yPosition;
        public override void Initialize(Vector3 origin, Vector3 dir)
        {
            dir.Normalize();
            int side = (int)Mathf.Sign(dir.x);
            Vector3 newPos = origin + new Vector3(side * xPosition, yPosition, 0f);
            transform.position = newPos;
        }

        
    }
}