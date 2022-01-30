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
    public float moveSpeed;
    public Rigidbody body;
    public Collider collider;

    Vector3 moveVector;
    Vector3 direction;

    [HideInInspector] public bool active;


    public void ProcessActions(Vector2 input)
    {

        moveVector.x = input.x * moveSpeed * Time.deltaTime;
        moveVector.y = 0;
        moveVector.z = input.y * moveSpeed * Time.deltaTime;

        direction = Vector3.Normalize(new Vector3(input.x,0,input.y));

        if (direction.magnitude != 0)
        {
            float angle = Mathf.Atan2(direction.z, -direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, angle, 0);
        }
        
        body.MovePosition(transform.position + moveVector);
    }
}
