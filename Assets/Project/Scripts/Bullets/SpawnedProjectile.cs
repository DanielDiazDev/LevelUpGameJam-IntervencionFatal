using UnityEngine;

namespace Project.Scripts.Bullets
{
    //Magia para los de aire y semilla, son los que se generan al frente del jugador,
    public abstract class SpawnedProjectile : ProjectileBase
    {
        public override void Initialize(Vector3 origin, Vector3 dir)
        {
            transform.position = origin + dir.normalized * 1.5f;
        }
    }
}