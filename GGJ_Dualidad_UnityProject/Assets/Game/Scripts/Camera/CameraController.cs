using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif

public enum FollowAlgorithm
{
    simple,
    boundBox,
}

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    private static CameraController _instance;
    public static CameraController Instance
    {
        get
        {
            return _instance;
        }
    }

    public Collider target;

    public Vector3 maxXAndY;
    public Vector3 minXAndY;

    public float dampingHorizontal = 1;
    public float dampingDepth = 1;
    public float dampingVerticalUp = 1;
    public float dampingVerticalDown = 1;

    public float lookAheadFactor = 3;
    public float lookAheadReturnSpeed = 0.5f;
    public float lookAheadMoveThreshold = 0.1f;

    public FollowAlgorithm currentFollowAlgoritm = FollowAlgorithm.simple;

    public float _offsetY;
    public float _offsetX;
    public float _offsetZ;
    public Vector3 focusAreaSize;


    bool lookAheadStopped = true;

    private Vector3 _lastTargetPosition;
    private Vector3 _currentVelocity;
    private Vector3 _lookAheadPos;

    float currentLookAheadX;
    float currentLookAheadZ;

    private FocusArea focusArea;
    

    protected Camera _camera;
    //protected OrthographicSizeScaler _scaler;

    public bool _started = false;

    private void Awake()
    {
        _instance = this;
        _camera = GetComponent<Camera>();
        _camera.enabled = false;
    }

    void OnDestroy()
    {
        _instance = null;
    }

    // Use this for initialization
    private void Start()
    {        
        //_scaler = GetComponent<OrthographicSizeScaler>();
        transform.parent = null;
        _currentVelocity = Vector3.zero;

        if (target != null)
        {
            _lastTargetPosition = target.transform.position;
            transform.position = target.transform.position;
            if (currentFollowAlgoritm == FollowAlgorithm.boundBox)
            {
                focusArea = new FocusArea(target.bounds, focusAreaSize);
            }
        }
    }

    public void SetInitialPlayer(Collider t)
    {
        target = t;
        Vector3 initialPos = target.transform.position + Vector3.forward * _offsetZ + Vector3.right * _offsetX;


        float newX = Mathf.Clamp(initialPos.x, minXAndY.x, maxXAndY.x);
        float newZ = Mathf.Clamp(initialPos.y, minXAndY.z, maxXAndY.z);

        transform.position = new Vector3(newX, _offsetY, newZ);

        _camera.enabled = true;
    }

    public void SetPlayer(Collider t)
    {
        target = t;
        _lastTargetPosition = target.transform.position;
        if (currentFollowAlgoritm == FollowAlgorithm.boundBox)
        {
            focusArea = new FocusArea(target.bounds, focusAreaSize);
        }
        _started = true;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (_started)
        {
            if (currentFollowAlgoritm == FollowAlgorithm.simple)
            {
                SimpleFollowAlgorithm();
            }
            else if (currentFollowAlgoritm == FollowAlgorithm.boundBox)
            {
                BoundBoxAlgorithm();
            }
        }
    }

    void SimpleFollowAlgorithm()
    {
        Vector3 finalTargetPosition = target.transform.position;
        Vector3 delta = finalTargetPosition - _lastTargetPosition;
        _lastTargetPosition = finalTargetPosition;

        finalTargetPosition += Vector3.forward * _offsetZ + Vector3.right * _offsetX;

        if(Mathf.Abs(delta.x) > lookAheadMoveThreshold)
        {
            finalTargetPosition.x += Mathf.Sign(delta.x) * lookAheadFactor;
        }

        if (Mathf.Abs(delta.y) > lookAheadMoveThreshold)
        {
            finalTargetPosition.y += Mathf.Sign(delta.y) * lookAheadFactor;
        }

        if (Mathf.Abs(delta.z) > lookAheadMoveThreshold)
        {
            finalTargetPosition.z += Mathf.Sign(delta.z) * lookAheadFactor;
        }

        float tempX = Mathf.SmoothDamp(transform.position.x, finalTargetPosition.x, ref _currentVelocity.x, dampingHorizontal);
        float tempY = Mathf.SmoothDamp(transform.position.y, finalTargetPosition.y, ref _currentVelocity.y, dampingVerticalUp);
        float tempZ = Mathf.SmoothDamp(transform.position.z, finalTargetPosition.z, ref _currentVelocity.z, dampingDepth);


        float newX = Mathf.Clamp(tempX, minXAndY.x, maxXAndY.x);
        float newZ = Mathf.Clamp(tempZ, minXAndY.z, maxXAndY.z);

        transform.position = new Vector3(newX, _offsetY, newZ);        
    }

    
    public void BoundBoxAlgorithm()
    {
        focusArea.Update(target.bounds);

        Vector3 focusPosition = focusArea.centre + Vector3.forward * _offsetZ + Vector3.up * _offsetX;

        /*Debug.Log("velocity " + focusArea.velocity.x);
        if (focusArea.velocity.x != 0)
        {
            if (Mathf.Abs(focusArea.velocity.x) > lookAheadMoveThreshold)
            {
                if (lookAheadStopped)
                {
                    currentLookAheadX = 0;
                }
                lookAheadStopped = false;
                _lookAheadPos.x = Mathf.Sign(focusArea.velocity.x) * lookAheadFactor;
            }
            else
            {
                if (!lookAheadStopped)
                {
                    lookAheadStopped = true;

                    _lookAheadPos.x = currentLookAheadX - Mathf.Sign(focusArea.velocity.x) * lookAheadFactor;
                }
            }
        }

        currentLookAheadX = Mathf.SmoothDamp(currentLookAheadX, _lookAheadPos.x, ref _currentVelocity.x, dampingHorizontal);
        focusPosition.x += currentLookAheadX;
        

        /*focusPosition.z = Mathf.SmoothDamp(transform.position.z, focusPosition.z, ref _currentVelocity.z, dampingDepth);
        focusPosition += Vector3.right * currentLookAheadX;*/

        float newX = Mathf.Clamp(focusPosition.x, minXAndY.x, maxXAndY.x);
        float newZ = Mathf.Clamp(focusPosition.z, minXAndY.z, maxXAndY.z);
        transform.position = new Vector3(newX, _offsetY, newZ);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, .5f);
        Gizmos.DrawCube(focusArea.centre, focusAreaSize);
    }
#endif

    private struct FocusArea
    {
        public Vector3 centre;
        public Vector3 velocity;
        private float left, right;
        private float top, bottom;

        public FocusArea(Bounds targetBounds, Vector3 size)
        {
            left = targetBounds.center.x - size.x / 2;
            right = targetBounds.center.x + size.x / 2;
            bottom = targetBounds.center.z - size.z / 2;
            top = targetBounds.center.z + size.z / 2;

            velocity = Vector2.zero;
            centre = new Vector3((left + right) / 2,0, (top + bottom) / 2);
        }

        public void Update(Bounds targetBounds)
        {
            float shiftX = 0f;
            if (targetBounds.min.x < left)
            {
                shiftX = targetBounds.min.x - left;
            }
            else if (targetBounds.max.x > right)
            {
                shiftX = targetBounds.max.x - right;
            }
            left += shiftX;
            right += shiftX;


            float shiftZ = 0f;
            if (targetBounds.min.z < bottom)
            {
                shiftZ = targetBounds.min.z - bottom;
            }
            else if (targetBounds.max.z > top)
            {
                shiftZ = targetBounds.max.z - top;
            }
            bottom += shiftZ;
            top += shiftZ;


            centre = new Vector3((left + right) / 2, 0, (top + bottom) / 2);
            velocity = new Vector3(shiftX, 0, shiftZ);
        }
    }
}
