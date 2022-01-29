using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldButtonReactor : ButtonReactor
{
    public bool state;

    protected void Update()
    {
        /*if (state)
        {
            Debug.Log("state is true");
        }
        else
        {
            Debug.Log("state is false");
        }*/
    }

    protected override void LogicEventCalled(LogicGatesBus.LogicGateEvent e)
    {
        if(e.type == LogicGatesBus.LogicGates.holdButton && e.id == id)
        {
            Debug.Log("logic pressed");
            state = !state;
        }
    }
}
