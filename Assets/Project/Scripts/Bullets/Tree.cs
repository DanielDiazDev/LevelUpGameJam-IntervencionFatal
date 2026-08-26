using UnityEngine;
public class Tree : BaseTimeTravelObject
{
    void Start()
    {

    }

    //TODO move these to a parent class
    public override void OnPast()
    {
        Future.SetActive(false);
        Past.SetActive(true);
    }

    public override void OnFuture()
    {
        Past.SetActive(false);
        Future.SetActive(true);
    }

    public override void OnFire()
    {
        //TODO animación de quemarse
        //TODO aumentar semillas disponibles en el personaje
        Destroy(this);
    }
}
