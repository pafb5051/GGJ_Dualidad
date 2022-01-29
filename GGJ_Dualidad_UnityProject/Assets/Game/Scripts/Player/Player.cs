using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerType
{
    Angel = 1,
    Demon = 1 << 1,
}


public class Player : MonoBehaviour
{
    public PlayerType type;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
