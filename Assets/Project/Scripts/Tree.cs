using UnityEngine;

public class Tree : MonoBehaviour
{
    [SerializeField] private GameObject Past;
    [SerializeField] private GameObject Future;
    void Start()
    {

    }

    //TODO move these to a parent class
    public void OnPast()
    {
        Future.SetActive(false);
        Past.SetActive(true);
    }

    public void OnFuture()
    {
        Past.SetActive(false);
        Future.SetActive(true);
    }

    public void OnFire()
    {
        //TODO animación de quemarse
        //TODO aumentar semillas disponibles en el personaje
        Destroy(this);
    }
}
