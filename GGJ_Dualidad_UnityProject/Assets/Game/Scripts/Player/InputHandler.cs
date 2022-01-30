using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    private static InputHandler _instance;
    public static InputHandler Instance
    {
        get
        {
            return _instance;
        }
    }

    public enum ActionTypes
    {
        down,
        press,
        up,
    }

    private Vector2 _moveVector;
    public Vector2 MoveVector
    {
        get
        {
            return _moveVector;
        }
    }

    private bool _active;

    public System.Action<ActionTypes> swapCharacterAction;
    public System.Action<ActionTypes> skillAction;

    private bool _swapCharacter = false;
    private bool _skill = false;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
    }

    private void OnDestroy()
    {
        if (_instance != null && _instance != this)
        {
            _instance = null;
        }
    }

    public void SetActive(bool active)
    {
        if (active)
        {   
            _active = true;
        }
        else
        {
            _moveVector = Vector2.zero;
            _active = false;
        }

    }

    private void Update()
    {
        if (!_active)
        {
            return;
        }

        _moveVector.x = Input.GetAxis("Horizontal");
        _moveVector.y = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.C) && !_swapCharacter)
        {
            _swapCharacter = true;
            swapCharacterAction.Invoke(ActionTypes.down);
        }else if (Input.GetKey(KeyCode.C)){
            swapCharacterAction.Invoke(ActionTypes.press);
        }else if (Input.GetKeyUp(KeyCode.C) && _swapCharacter){
            _swapCharacter = false;
            swapCharacterAction.Invoke(ActionTypes.up);
        }

        if (Input.GetKeyDown(KeyCode.Space) && !_skill)
        {
            _skill = true;
            swapCharacterAction.Invoke(ActionTypes.down);
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            swapCharacterAction.Invoke(ActionTypes.press);
        }
        else if (Input.GetKeyUp(KeyCode.Space) && _skill)
        {
            _skill = false;
            swapCharacterAction.Invoke(ActionTypes.up);
        }
    }


}
