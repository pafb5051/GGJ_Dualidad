using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Player[] players;

    private InputHandler _inputHandler;
    private int _currentPlayer;


    // Start is called before the first frame update
    void Start()
    {
        _inputHandler = InputHandler.Instance;

        _inputHandler.swapCharacterAction += SwapCharacter;
    }

    private void OnDestroy()
    {
        _inputHandler.swapCharacterAction -= SwapCharacter;
    }

    private void SwapCharacter(InputHandler.ActionTypes actionType)
    {
        if(actionType == InputHandler.ActionTypes.down)
        {
            players[_currentPlayer].active = false;
            _currentPlayer = (_currentPlayer + 1) % players.Length;
            players[_currentPlayer].active = true;
            Debug.Log("current active player is " + players[_currentPlayer].type.ToString());
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        players[_currentPlayer].ProcessActions(_inputHandler.MoveVector);
    }

    
}
