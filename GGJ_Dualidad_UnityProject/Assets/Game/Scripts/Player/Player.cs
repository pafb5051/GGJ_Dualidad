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

    [HideInInspector] public bool active;

    public void ProcessActions(Vector2 input)
    {
        moveVector.x = input.x * moveSpeed * Time.deltaTime;
        moveVector.z = input.y * moveSpeed * Time.deltaTime;
        body.MovePosition(transform.position + moveVector);
    }
}
