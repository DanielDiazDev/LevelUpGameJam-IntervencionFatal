using Project.Scripts.Bullets;
using Project.Scripts.Timer;
using UnityEngine;

public class TreeProjectile : SpawnedProjectile
{
    [SerializeField] private Tree TreePrefab;

    void Start()
    {
        Tree instance = Instantiate(TreePrefab, transform.position, Quaternion.identity, TimeManager.Instance.ObjectAffectedTime);
        Destroy(gameObject);
    }
}
