using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerButton : Button
{
    private void OnTriggerEnter(Collider other)
    {
        if ((1 << other.gameObject.layer & allowedPlayers) != 0)
        {
            bus.OnLogicGateEvent.Invoke(new LogicGatesBus.LogicGateEvent() { type = LogicGatesBus.LogicGates.triggerButton, caller=other, id = id });
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((1 << other.gameObject.layer & allowedPlayers) != 0)
        {
            bus.OnLogicGateEvent.Invoke(new LogicGatesBus.LogicGateEvent() { type = LogicGatesBus.LogicGates.triggerButton, caller = other, id = id });
        }
    }
}
