using System;
using Project.Scripts.Bullets;
using Project.Scripts.Timer;
using UnityEngine;


public class Waterfall : BaseTimeTravelObject, IChangeState
{
    public bool StatePast; //false=liquid, true=solid
    public bool StateFuture; //false=liquid, true=solid
    void Start()
    {
        ColliderBridge projectileTriggerPast = Past.GetComponentInChildren<ColliderBridge>(true);
        ColliderBridge projectileTriggerFuture = Future.GetComponentInChildren<ColliderBridge>(true);
        projectileTriggerPast.OnChildTriggerEnter += OnWaterfallIceHit;
        projectileTriggerFuture.OnChildTriggerEnter += OnWaterfallFireHit;
    }

    void Update()
    {

    }

    void OnWaterfallIceHit(Collider collider)
    {
        if (collider.GetComponent<IceProjectile>())
        {
            OnIce();
        }
    }

    void OnWaterfallFireHit(Collider collider)
    {
        if (collider.GetComponent<FireProjectile>())
        {
            OnFire();
        }
    }

    public override void OnIce()
    {
        print("waterfall OnIce");
        base.OnIce();
        if (TimeManager.Instance.CurrentState == TimeState.Past)
        {
            StatePast = true;
            // StateFuture = true;
        }
        else
        {
            StateFuture = true;
            // StatePast = false;

        }

        //TODO get if it's the past or future and set the variable
        // if (!GlobalTime) StatePast = true;
        // else StateFuture = true;

        base.OnFuture(); //base.OnFuture shows solid version
    }

    public override void OnFire()
    {
        print("waterfall OnFire");
        base.OnFire();
        if (TimeManager.Instance.CurrentState == TimeState.Present)
        {
            // StatePast = true;
            StateFuture = false;
        }
        else
        {
            StateFuture = false;
            // StatePast = false;
        }
        //TODO get if it's the past or future and set the variable
        // if (!GlobalTime) StatePast = false;
        // else StateFuture = false;
        base.OnPast(); //base.OnPast shows liquid version
    }

    public override void OnPast() //overriden OnPast shows saved Past version
    {
        print("waterfall OnPast");
        if (!StatePast) base.OnPast();
        else base.OnFuture();
    }

    public override void OnFuture() //overriden OnFuture shows saved Future version
    {
        print("waterfall OnFuture");
        if (!StateFuture) base.OnPast();
        else base.OnFuture();
    }

    public void ChangeState(TimeState currentState)
    {
        if (currentState == TimeState.Past)
        {
            OnPast();
        }
        else
        {
            OnFuture();
        }
    }
}