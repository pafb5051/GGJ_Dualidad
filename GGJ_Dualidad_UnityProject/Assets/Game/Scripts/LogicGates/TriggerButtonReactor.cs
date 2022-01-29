using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerButtonReactor : ButtonReactor
{
    public int requiredSignals;

    protected bool completed = false;
    protected int currentSignals;
    protected List<Collider> callers = new List<Collider>();

    protected void Update()
    {
        if (!completed)
        {
            if (requiredSignals == currentSignals)
            {
                Debug.Log("trigger completed");
                completed = true;
            }
        }
    }

    protected override void LogicEventCalled(LogicGatesBus.LogicGateEvent e)
    {
        if (e.type == LogicGatesBus.LogicGates.triggerButton && e.id == id)
        {
            if (callers.Contains(e.caller))
            {
                callers.Remove(e.caller);
                currentSignals--;
            }
            else
            {
                callers.Add(e.caller);
                currentSignals++;
            }
        }
    }
}
