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
    public CapsuleCollider playerCollider;
    public float slopeLimit;
    public float respawnTime;

    private bool allowInput = true;
    private bool isGrounded;
    
    private Vector3 _lastSafePosition;

    private float capsuleHeight;
    private Vector3 capsuleBottom;
    private float radius;

    private Vector3 moveVector;
    private Vector3 direction;

    [HideInInspector] public bool active;

    public void Start()
    {
        capsuleHeight = Mathf.Max(playerCollider.radius * 2f, playerCollider.height);
        
        radius = transform.TransformVector(playerCollider.radius, 0f, 0f).magnitude;
    }

    [ContextMenu("ResetPlayer")]
    public void ResetPlayerPosition()
    {
        transform.position = _lastSafePosition;
        allowInput = false;

        StartCoroutine(ReactivateControl());
    }

    IEnumerator ReactivateControl()
    {
        yield return new WaitForSeconds(respawnTime);

        allowInput = true;
    }


    public void ProcessActions(Vector2 input)
    {
        if(allowInput == false)
        {
            return;
        }

        IsGrounded();
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

    public bool IsGrounded()
    {
        isGrounded = false;
        capsuleBottom = transform.TransformPoint(playerCollider.center - Vector3.up * capsuleHeight / 2f);
        Ray ray = new Ray(capsuleBottom + transform.up * .01f, -transform.up);
        Debug.DrawRay(capsuleBottom + transform.up * .01f, -transform.up,Color.red);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, radius * 5f))
        {
            if (hit.collider.gameObject.layer != LayerMask.NameToLayer("DeathZone"))
            {
                float normalAngle = Vector3.Angle(hit.normal, transform.up);
                if (normalAngle < slopeLimit)
                {
                    float maxDist = radius / Mathf.Cos(Mathf.Deg2Rad * normalAngle) - radius + .02f;
                    if (hit.distance < maxDist)
                    {
                        isGrounded = true;
                        _lastSafePosition = transform.position+transform.right*radius;
                    }
                }
            }
        }

        return isGrounded;
    }
}
