using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldButton : Button
{
    private void OnTriggerEnter(Collider other)
    {
        if ((1 << other.gameObject.layer & allowedPlayers) != 0)
        {
            bus.OnLogicGateEvent.Invoke(new LogicGatesBus.LogicGateEvent() { type = LogicGatesBus.LogicGates.holdButton, caller = other, actionType = LogicGatesBus.ActionType.enter, id = id });
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((1 << other.gameObject.layer & allowedPlayers) != 0)
        {
            bus.OnLogicGateEvent.Invoke(new LogicGatesBus.LogicGateEvent() { type = LogicGatesBus.LogicGates.holdButton, caller = other, actionType = LogicGatesBus.ActionType.exit, id = id });
        }
    }
}
