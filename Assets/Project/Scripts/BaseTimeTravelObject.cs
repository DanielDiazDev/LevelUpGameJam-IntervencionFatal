using UnityEngine;

public abstract class BaseTimeTravelObject : MonoBehaviour
{
    [SerializeField] public GameObject Past;
    [SerializeField] public GameObject Future;
    public virtual void OnPast()
    {
        Future.SetActive(false);
        Past.SetActive(true);
    }

    public virtual void OnFuture()
    {
        Past.SetActive(false);
        Future.SetActive(true);
    }

    public virtual void OnFire()
    {
    }
    public virtual void OnIce()
    {
    }
}
