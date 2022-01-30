using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public int id;
    public Animation animator;
    protected SoundManager soundPlayer;

    public LayerMask allowedPlayers;

    protected LogicGatesBus bus;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        bus = LogicGatesBus.Instance;
        soundPlayer = SoundManager.Instance;
    }
}
