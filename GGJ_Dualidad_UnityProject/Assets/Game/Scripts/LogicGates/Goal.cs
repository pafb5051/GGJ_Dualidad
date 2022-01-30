using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : ButtonReactor
{
    public int requiredSignals;
    public GameObject award;

    protected bool completed = false;
    protected int currentSignals;
    private GameManager gameManager;

    protected override void Start()
    {
        base.Start();
        gameManager = GameManager.Instance;
    }


    protected void Update()
    {
        if (!completed)
        {
            if (requiredSignals == currentSignals)
            {
                //Debug.Log("level ended completed");
                completed = true;
                gameManager.EndGame();
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
            else if (e.actionType == LogicGatesBus.ActionType.enter)
            {
                currentSignals++;
            }
        }
    }
}
