using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerButtonReactor : ButtonReactor
{
    public int requiredSignals;

    protected bool completed = false;
    protected int currentSignals;

    protected void Update()
    {
        if (!completed)
        {
            if (requiredSignals == currentSignals)
            {
                Debug.Log("trigger completed");
                completed = true;
                GetComponent<Collider>().enabled = true;
                if(animator != null)
                {
                    animator.AnimateForward();
                }
            }
        }
    }

    protected override void LogicEventCalled(LogicGatesBus.LogicGateEvent e)
    {
        if (e.type == LogicGatesBus.LogicGates.triggerButton && e.id == id)
        {
            if (e.actionType == LogicGatesBus.ActionType.exit)
            {
                currentSignals--;
            }
            else if(e.actionType == LogicGatesBus.ActionType.enter)
            {
                currentSignals++;
            }
        }
    }
}
