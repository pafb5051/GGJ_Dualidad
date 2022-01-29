using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonReactor : MonoBehaviour
{
    public int id;

    public Animation animator;
    protected LogicGatesBus bus;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        bus = LogicGatesBus.Instance;
        bus.OnLogicGateEvent += LogicEventCalled;
    }

    protected virtual void OnDestroy()
    {
        bus.OnLogicGateEvent -= LogicEventCalled;
    }

    protected virtual void LogicEventCalled(LogicGatesBus.LogicGateEvent e)
    {
        
    }
}
