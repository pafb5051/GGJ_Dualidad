using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public Collider door;
    public PlayerType allowedTarget;

    public void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponentInParent<Player>();
        PlayerType type = player.type;

        if(type == allowedTarget)
        {
            door.enabled = false;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        Player player = other.GetComponentInParent<Player>();
        PlayerType type = player.type;

        if (type == allowedTarget)
        {
            door.enabled = true;
        }
    }
}
