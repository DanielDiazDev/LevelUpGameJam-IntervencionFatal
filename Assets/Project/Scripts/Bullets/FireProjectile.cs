using UnityEngine;

namespace Project.Scripts.Bullets
{
    public class FireProjectile : DirectedProjected
    {
        private void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);
        }
    }
}