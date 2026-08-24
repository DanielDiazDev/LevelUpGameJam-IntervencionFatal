using UnityEngine;

namespace Project.Scripts.Bullets
{
    public class IceProjectile : DirectedProjected
    {
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("ice hit: " + other.gameObject.name);
        }
    }
}