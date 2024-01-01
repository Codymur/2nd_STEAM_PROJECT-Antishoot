using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
public class PlayerController : MonoBehaviour
{
    
    Vector3 _moveDirection;
    //Movement // Jump // Check // Reference // Inputs // StateHandler //Slope
    public enum CurrentState
    {
        freeze,
        swinging,
        walking,
        wallRunning,
        sliding,
        dashing,
        air
    }

    public bool freeze;
    public bool activeGrapple;
    public bool swinging;

    public CurrentState State;
    [Header("Movement")]
    [SerializeField] float _walkingSpeed = 15f;
    private float _moveSpeed = 7f;
    private float _moveSpeedMultip = 10f;
    private float _multiplierInAir = 0.4f;
    public float SwingSpeed;

    [Space]
    [Header("Jump")]
    [SerializeField] float _jumpForce = 12f;
    [SerializeField] float _jumpCooldown = 0.2f;
    [SerializeField] bool _readyToJump;
    [SerializeField] float minJumpHeight;

    [Space]
    [Header("Check")]
    [SerializeField] float _groundCheckDistance = 0.4f;
    [SerializeField] LayerMask _groundLayer;
    public bool _isGround;
    [SerializeField] float _playerHeight = 2f;

    [Space]
    [Header("Slope")]
    [SerializeField] float _maxAngleSlope = 40f;
    bool _isExitSlope;
    float _slopeDistance = 0.4f;
    RaycastHit _slopeHit;

    [Space]
    [Header("Sliding")]
    public bool IsSlide;

    [Space]
    [Header("WallRun")]
    public bool isWallRunning;
    [SerializeField] float _wallRunSpeed = 17f;

    [Space]
    [Header("Inputs")]
    public float _horizontalMove;
    public float _verticalMove;
    KeyCode _jumpKey = KeyCode.Space;
    KeyCode _jumpKeyController = KeyCode.JoystickButton0;
    KeyCode _sprintKey = KeyCode.LeftShift;

    [Space]
    [Header("References")]
    [SerializeField] Rigidbody _rb;
    [SerializeField] Transform _orientaion;
    [SerializeField] CameraController _camScript;
    [SerializeField] GunBreath BreathScript;

    [Space]
    [Header("Dashing")]
    public float DashSpeed;
    public bool Dashing;

    [Space]
    [Header("Smash")]
    //public bool Smash = false;



    //public TMP_Text ScoreText;


    float fJumpPressedRemember = 0;
    [SerializeField]
    float fJumpPressedRememberTime = 0.2f;

    public int JumpingTimer = 0;


    public PlayerHealth PlayerHealthScript;


    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true;
    }
    private void FixedUpdate()
    {
        MovePlayer();
    }
    private void Update()
    {
        if (PlayerHealthScript.HealthBar.fillAmount > 0f)
        {
            MyInputs();
            CheckGround();
            ControlSpeed();
            OnSlope();
            ControlDrag();
            StateHandler();
        }
        else
        {
            _moveSpeed = 0;
        }
    }
    void StateHandler()
    {

        if (freeze)
        {
            State = CurrentState.freeze;
            _moveSpeed = 0;
            _rb.velocity = Vector3.zero;
        }
        else if (swinging)
        {
            State = CurrentState.swinging;
            _moveSpeed = SwingSpeed;
        }
        else if (Dashing)
        {
            State = CurrentState.dashing;
            _moveSpeed = DashSpeed;
        }
        else if (isWallRunning)
        {
            State = CurrentState.wallRunning;
            _moveSpeed = _wallRunSpeed;
        }
        else if (IsSlide && _isGround)
        {
            State = CurrentState.sliding;
            _moveSpeed = 20f;
        }
        else if (Input.GetKey(_sprintKey) && _isGround)
        {
            State = CurrentState.walking;
            _moveSpeed = _walkingSpeed;
        }
        else if (_isGround)
        {
            State = CurrentState.walking;
            _moveSpeed = _walkingSpeed;
        }
        else
        {
            State = CurrentState.air;
        }
    }
    void MyInputs()
    {
        _horizontalMove = Input.GetAxisRaw("Horizontal");
        _verticalMove = Input.GetAxisRaw("Vertical");

        fJumpPressedRemember -= Time.deltaTime;
        if (Input.GetKeyDown(_jumpKey) || Input.GetKeyDown(_jumpKeyController))
        {
            fJumpPressedRemember = fJumpPressedRememberTime;

            
        }

        if ((fJumpPressedRemember > 0) && _isGround && _readyToJump)
        {
            fJumpPressedRemember = 0;
            _readyToJump = false;
            _isExitSlope = true;

            Jump();
            Invoke("ResetJump", _jumpCooldown);
        }


    }
    void Jump()
    {
        _rb.velocity = new Vector3(_rb.velocity.x, 0, _rb.velocity.z);

        //force
        _rb.AddForce(transform.up * _jumpForce, ForceMode.Impulse);
    }
    void MovePlayer()
    {
        if (activeGrapple) return;

        _moveDirection = _orientaion.right * _horizontalMove + _orientaion.forward * _verticalMove;

        if (OnSlope() && !_isExitSlope)
        {
            _rb.AddForce(GetSlopeDirection(_moveDirection) * _moveSpeed * _moveSpeedMultip, ForceMode.Force);
            if (_rb.velocity.y > 0)
                _rb.AddForce(Vector3.down * 30, ForceMode.Force);

        }
        else if (_isGround)
            _rb.AddForce(_moveDirection.normalized * _moveSpeed * _moveSpeedMultip, ForceMode.Force);
        else if (!_isGround)
            _rb.AddForce(_moveDirection.normalized * _moveSpeed * _moveSpeedMultip * _multiplierInAir, ForceMode.Force);


        _rb.useGravity = !OnSlope();

        _camScript.DoFov(_camScript.CameraFieldOfView);
        //bu elave
        if (_horizontalMove > 0 && !isWallRunning && !IsSlide && !Dashing)
        {
            if (PlayerHealthScript.HealthBar.fillAmount > 0f)
            {
                _camScript.DoTilt(-3.5f);
            }
            
        }
        else if (_horizontalMove < 0 && !isWallRunning && !IsSlide && !Dashing)
        {
            if (PlayerHealthScript.HealthBar.fillAmount > 0f)
            {
                _camScript.DoTilt(3.5f);
            }
        }
        else if (_horizontalMove == 0 && !isWallRunning && !IsSlide && !Dashing)
        {
            _camScript.DoFov(_camScript.CameraFieldOfView);
            if (PlayerHealthScript.HealthBar.fillAmount > 0f)
            {
                _camScript.DoTilt(0f);
            }
        }


        if (_verticalMove > 0 && !isWallRunning && !IsSlide && !Dashing)
        {
            if (PlayerHealthScript.HealthBar.fillAmount > 0f)
            {
                _camScript.DoTiltX(3.5f);
            }
        }
        else if (_verticalMove < 0 && !isWallRunning && !IsSlide && !Dashing)
        {
            if (PlayerHealthScript.HealthBar.fillAmount > 0f)
            {
                _camScript.DoTiltX(-3.5f);
            }
        }
        else if (_horizontalMove == 0 && !isWallRunning && !IsSlide && !Dashing)
        {
            if (PlayerHealthScript.HealthBar.fillAmount > 0f)
            {
                _camScript.DoTiltX(0f);
            }
        }

        if ((_horizontalMove != 0 || _verticalMove != 0) && _isGround && State != CurrentState.sliding)
        {
            BreathScript.Frequency = 2f;
        }
        else if (_horizontalMove == 0 || _verticalMove == 0)
        {
            BreathScript.Frequency = 0.2f;
        }

    }
    void CheckGround()
    {
        _isGround = Physics.Raycast(transform.position, Vector3.down, _playerHeight * 0.5f + _groundCheckDistance, _groundLayer);
    }

    private bool AboveGround()
    {
        return !Physics.Raycast(transform.position, Vector3.down, minJumpHeight, _groundLayer);
    }

    void ResetJump()
    {
        _isExitSlope = false;
        _readyToJump = true;
    }
    void ControlDrag()
    {
        if (_isGround && State != CurrentState.dashing && !activeGrapple)
        {
            _rb.drag = 7f;
            //Smash = false;
        }
        else
            _rb.drag = 0f;
    }
    void ControlSpeed()
    {
        if (activeGrapple) return;


        if (OnSlope() && !_isExitSlope)
        {
            if (_rb.velocity.magnitude > _moveSpeed)
            {
                _rb.velocity = _rb.velocity.normalized * _moveSpeed;
            }
        }
        else
        {

            Vector3 flatVel = new Vector3(_rb.velocity.x, 0, _rb.velocity.z);

            if (flatVel.magnitude > _moveSpeed)
            {
                Vector3 limitedVel = _rb.velocity.normalized * _moveSpeed;
                _rb.velocity = new Vector3(limitedVel.x, _rb.velocity.y, limitedVel.z);
            }
        }

    }

    public bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out _slopeHit, _playerHeight * 0.5f + _slopeDistance))
        {
            float angle = Vector3.Angle(Vector3.up, _slopeHit.normal);
            return angle < _maxAngleSlope && angle != 0;
        }
        return false;
    }
    public Vector3 GetSlopeDirection(Vector3 direction)
    {
        return Vector3.ProjectOnPlane(direction, _slopeHit.normal).normalized;
    }

    public Vector3 CalculateJumpVelocity(Vector3 startPoint, Vector3 endPoint, float trajectoryHeight)
    {
        float gravity = Physics.gravity.y;
        float displacementY = endPoint.y - startPoint.y;
        Vector3 displacementXZ = new Vector3(endPoint.x - startPoint.x, 0f, endPoint.z - startPoint.z);

        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * trajectoryHeight);
        Vector3 velocityXZ = displacementXZ / (Mathf.Sqrt(-2 * trajectoryHeight / gravity) + Mathf.Sqrt(2 * (displacementY - trajectoryHeight) / gravity));
        return velocityXZ + velocityY;
    }

    public void JumpToPosition(Vector3 targetPosition, float trajectoryHeight)
    {
        activeGrapple = true;

        velocityToSet = CalculateJumpVelocity(transform.position, targetPosition, trajectoryHeight);
        Invoke(nameof(SetVelocity), 0.1f);

        Invoke(nameof(ResetRestrictions), 3f);
    }

    private Vector3 velocityToSet;
    private bool enableMovementOnNextTouch;
    private void SetVelocity()
    {
        enableMovementOnNextTouch = true;
        _rb.velocity = velocityToSet;

        _camScript.DoFov(95f);
    }

    public void ResetRestrictions()
    {
        activeGrapple = false;
        _camScript.DoFov(_camScript.CameraFieldOfView);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (enableMovementOnNextTouch)
        {
            enableMovementOnNextTouch = false;
            ResetRestrictions();
            GetComponent<Grappling>().StopGrapple();
        }
    }

}
