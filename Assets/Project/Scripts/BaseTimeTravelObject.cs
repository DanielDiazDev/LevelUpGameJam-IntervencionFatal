using UnityEngine;


public abstract class BaseTimeTravelObject : MonoBehaviour
{
    [SerializeField] public GameObject Past; //Fase 1 
    [SerializeField] public GameObject Future; //Fase 2
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
