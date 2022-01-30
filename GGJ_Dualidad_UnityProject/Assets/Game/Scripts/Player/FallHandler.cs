using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallHandler : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        other.GetComponentInParent<Player>().ResetPlayerPosition();
    }
}
