using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldButtonReactor : ButtonReactor
{
    public bool initialState;

    protected bool currentState;

    private bool _switch = false;

    private Dictionary<Collider, bool> _individualButtonsStates = new Dictionary<Collider, bool>();

    protected override void Start()
    {
        base.Start();
    }

    protected virtual void Update()
    {
        if (_switch)
        {
            _switch = false;
            currentState = initialState;
            Dictionary<Collider, bool>.Enumerator enumerator = _individualButtonsStates.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (!initialState)
                {
                    currentState |= enumerator.Current.Value;
                }
                else
                {
                    currentState &= enumerator.Current.Value;
                }
            }

            ChangeState();
        }
    }

    protected virtual void ChangeState()
    {

    }

    protected override void LogicEventCalled(LogicGatesBus.LogicGateEvent e)
    {
        if(e.type == LogicGatesBus.LogicGates.holdButton && e.id == id)
        {
            if(e.actionType == LogicGatesBus.ActionType.enter)
            {
                if (_individualButtonsStates.ContainsKey(e.caller))
                {
                    _individualButtonsStates[e.caller] = !initialState;
                }
                else
                {
                    _individualButtonsStates.Add(e.caller, !initialState);
                }
                _switch = true;
            }
            else if( e.actionType == LogicGatesBus.ActionType.exit)
            {
                if (_individualButtonsStates.ContainsKey(e.caller))
                {
                    _individualButtonsStates[e.caller] = initialState;
                }
                else
                {
                    _individualButtonsStates.Add(e.caller, initialState);
                }
                _switch = true;
            }
        }
    }
}
