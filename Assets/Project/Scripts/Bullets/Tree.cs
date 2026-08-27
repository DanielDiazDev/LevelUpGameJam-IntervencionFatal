public class Tree : BaseTimeTravelObject
{
    public ETreeState StatePast;
    public ETreeState StateFuture;
    void Start()
    {
        SetState(ETreeState.SEED);
    }

    public override void OnFire()
    {
        //TODO animación de quemarse
        //TODO aumentar semillas disponibles en el personaje SOLO si era un árbol plantado por el jugador, no si era uno del nivel
        SetState(ETreeState.BURNED);
    }

    void SetState(ETreeState state)
    {
        // if (GlobalTime) //TODO //we're in the past
        {
            StatePast = state;
            if (state == ETreeState.NOT_PLANTED)
            {
                StateFuture = ETreeState.NOT_PLANTED;
            }
            else if (state == ETreeState.SEED)
            {
                StateFuture = ETreeState.TREE;
            }
            else if (state == ETreeState.BURNED)
            {
                StatePast = ETreeState.NOT_PLANTED;
            }
        }
        // else //we're in the future
        {
            StateFuture = state;
            // if (state == ETreeState.NOT_PLANTED)
            // {
            //     StatePast = ETreeState.NOT_PLANTED;
            // }
            // else if (state == ETreeState.SEED)
            // {
            //     StatePast = ETreeState.NOT_PLANTED;
            // }
            // else if (state == ETreeState.BURNED)
            // {
            //     StateFuture = ETreeState.NOT_PLANTED;
            // }
        }
        ShowState();
    }

    void ShowState()
    {
        ETreeState stateCurrent;
        // if (GlobalTime) //we're in the past
        {
            stateCurrent = StatePast;
        }
        // else
        {
            stateCurrent = StateFuture;
        }

        if (stateCurrent == ETreeState.NOT_PLANTED)
        {
            Past.SetActive(false);
            Future.SetActive(false);
        }
        else if (stateCurrent == ETreeState.SEED)
        {
            OnPast();
        }
        else if (stateCurrent == ETreeState.TREE)
        {
            OnFuture();
        }
        else if (stateCurrent == ETreeState.BURNED)
        {
            // TODO add burned GameObject with 3d model
        }
    }
}

public enum ETreeState
{
    NOT_PLANTED,
    SEED,
    TREE,
    BURNED
}
