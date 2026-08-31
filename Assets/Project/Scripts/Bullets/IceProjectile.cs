using UnityEngine;

namespace Project.Scripts.Bullets
{
    public class IceProjectile : DirectedProjected
    {
        private void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);
        }
    }
}