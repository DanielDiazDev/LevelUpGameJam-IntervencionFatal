using System;
using Project.Scripts.Bullets;
using Project.Scripts.Player;
using Project.Scripts.Timer;
using UnityEngine;

public enum TreeState
{
    None,
    Seed,
    Full,
    Destroyed
}
public class Tree : BaseTimeTravelObject, IChangeState
{
    [SerializeField] private bool IsLevelOwned = false;
    public TreeState pastState;
    public TreeState futureState;

    void Start()
    {
        ColliderBridge projectileTriggerFuture = Future.GetComponentInChildren<ColliderBridge>(true);
        projectileTriggerFuture.OnChildTriggerEnter += OnTreeFireHit;
        if (!IsLevelOwned)
        {
            if (TimeManager.Instance.CurrentState == TimeState.Past)
            {
                pastState = TreeState.Seed;
                futureState = TreeState.Full;
                OnPast();
            }
            else
            {
                futureState = TreeState.Seed;
                pastState = TreeState.None;
                OnFuture();
            }
        }
        // else
        // {
        //     pastState = TreeState.Seed;
        //     futureState = TreeState.Full;
        // }
    }

    private void OnTreeFireHit(Collider collider)
    {
        if (collider.GetComponent<FireProjectile>())
        {
            OnFire();
        }
    }

    //TODO move these to a parent class
    public override void OnPast()
    {
        if (futureState == TreeState.Destroyed && pastState == TreeState.Destroyed)
        {
            Past.SetActive(false);
            Future.SetActive(false);
        }
        if (futureState == TreeState.Destroyed && pastState == TreeState.Full)
        {
            Past.SetActive(false);
            Future.SetActive(true);
        }
        if (futureState == TreeState.Destroyed && pastState == TreeState.Seed)
        {
            Past.SetActive(true);
            Future.SetActive(false);
        }
        if (futureState == TreeState.Full && pastState == TreeState.Full)
        {
            Past.SetActive(false);
            Future.SetActive(true);
        }
        if (futureState == TreeState.Full && pastState == TreeState.Seed)
        {
            Past.SetActive(true);
            Future.SetActive(false);
        }
        if (futureState == TreeState.Seed && pastState == TreeState.None)
        {
            Past.SetActive(false);
            Future.SetActive(false);
        }
    }

    public override void OnFuture()
    {
        if (futureState == TreeState.Destroyed && pastState == TreeState.Full)
        {
            Past.SetActive(false);
            Future.SetActive(false);
        }
        if (futureState == TreeState.Destroyed && pastState == TreeState.Seed)
        {
            Past.SetActive(false);
            Future.SetActive(false);
        }
        if (futureState == TreeState.Full && pastState == TreeState.Full)
        {
            Past.SetActive(false);
            Future.SetActive(true);
        }
        if (futureState == TreeState.Full && pastState == TreeState.Seed)
        {
            Past.SetActive(false);
            Future.SetActive(true);
        }
        if (futureState == TreeState.Seed && pastState == TreeState.None)
        {
            Past.SetActive(true);
            Future.SetActive(false);
        }

    }

    public override void OnFire()
    {

        if (TimeManager.Instance.CurrentState == TimeState.Past)
        {
            pastState = TreeState.Destroyed;
            futureState = TreeState.Destroyed;

        }
        else
        {
            futureState = TreeState.Destroyed;
            // pastState = TreeState.Destroyed;
        }
        Future.SetActive(false);

        //TODO animación de quemarse
        //TODO aumentar semillas disponibles en el personaje
        // Destroy(this);
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.gameObject.TryGetComponent(out PlayerController playerController))
            {
                playerController.Rb.AddForce(Vector3.right * 20f, ForceMode.Impulse);
            }
        }
    }

    public void ChangeState(TimeState currentState)
    {
        if (currentState == TimeState.Past)
            OnPast();
        else
            OnFuture();
    }
}
