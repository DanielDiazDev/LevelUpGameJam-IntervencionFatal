using UnityEngine;

namespace Project.Scripts.Bullets
{
    public class FireProjectile : DirectedProjected
    {
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log(other.gameObject.name);
        }
    }
}