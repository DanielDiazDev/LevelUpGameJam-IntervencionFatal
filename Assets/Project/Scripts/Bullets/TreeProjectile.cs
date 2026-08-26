using UnityEngine;
using Project.Scripts.Bullets;

public class TreeProjectile : SpawnedProjectile
{
    [SerializeField] private Tree TreePrefab;

    void Start()
    {
        Tree instance = Instantiate(TreePrefab, transform.position, Quaternion.identity);
        Destroy(this);
    }
}
