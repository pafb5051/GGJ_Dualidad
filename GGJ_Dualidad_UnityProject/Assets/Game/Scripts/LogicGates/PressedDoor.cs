using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressedDoor : HoldButtonReactor
{
    public Collider collider;

    protected override void Start()
    {
        base.Start();
        collider.enabled = initialState;
    }

    // Update is called once per frame
    protected override void ChangeState()
    {
        base.ChangeState();
        collider.enabled = currentState;
    }
}
