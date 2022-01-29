using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicGatesBus : MonoBehaviour
{
    public enum LogicGates
    {
        holdButton,
        triggerButton
    }

    public enum ActionType
    {
        enter,
        exit
    }

    public struct LogicGateEvent
    {
        public LogicGates type;
        public Collider caller;
        public ActionType actionType;
        public int id;
    }

    private static LogicGatesBus _instance;
    public static LogicGatesBus Instance
    {
        get
        {
            return _instance;
        }
    }

    public System.Action<LogicGateEvent> OnLogicGateEvent; 

    private void Awake()
    {
        if(_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
    }

    private void OnDestroy()
    {
        if(_instance != null && _instance != this)
        {
            _instance = null;
        }
    }
}
