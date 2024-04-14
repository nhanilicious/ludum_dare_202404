using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FrogCharacterController : MonoBehaviour
{
    private CharacterController _characterController;
    private Animator _animator;
    public Transform Cam;

    private float _speed;
    public float WalkSpeed;
    public float SprintSpeed;
    public float TurnSmoothTime = 0.1f;
    private float _turnSmoothVelocity;
    private float _currentSpeed;
    private bool _isJumping;

    Vector3 moveDir;

    // Gravity & jumping
    private float _gravity = -9.81f;
    [SerializeField] private float _gravityMultiplier = 3f;
    private float _velocity;
    [SerializeField] private float _jumpPower;

    // Animation IDs
    private int _speedID;
    private int _jumpID;
    private int _attackID;

    // Attack
    private float nextAttackTime;
    public float AttackCooldown = 0.5f;

    private void Awake()
    {
        nextAttackTime = Time.time + AttackCooldown;
    }

    // Start is called before the first frame update
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        _speedID = Animator.StringToHash("Speed");
        _jumpID = Animator.StringToHash("Jump");
        _attackID = Animator.StringToHash("Attack");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Cursor.lockState = CursorLockMode.Locked;

        ApplySprint();
        ApplyMovement();
        ApplyGravity();
        ApplyJump();
        

        _characterController.Move(moveDir * Time.deltaTime * _speed);
    }

    private void Update()
    {
        _isJumping = Input.GetKey(KeyCode.Space);
        _animator.SetBool("Jump", _isJumping);

        Attack();
    }

    private void ApplyMovement()
    {
        //float horizontal = Input.GetAxis("Horizontal");
        //float vertical = Input.GetAxis("Vertical");

        float horizontal = Input.GetKey(KeyCode.A) ? -1 : 0 + (Input.GetKey(KeyCode.D) ? 1 : 0);
        float vertical = Input.GetKey(KeyCode.W) ? 1 : 0 + (Input.GetKey(KeyCode.S) ? -1 : 0);

        moveDir = new Vector3(horizontal, 0, vertical).normalized;

        if (moveDir.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(moveDir.x, moveDir.z) * Mathf.Rad2Deg + Cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, TurnSmoothTime);
            transform.rotation = Quaternion.Euler(0, angle, 0);

            moveDir = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;

            _currentSpeed = _currentSpeed > _speed / 2 ? Mathf.Lerp(_currentSpeed, _speed, Time.deltaTime * 5f) : _speed;
        }
        else
        {
            _currentSpeed = Mathf.Lerp(_currentSpeed, 0, Time.deltaTime * 5f);
        }

        _animator.SetFloat(_speedID, _currentSpeed);
    }

    private void ApplyGravity()
    {
        if (_characterController.isGrounded && _velocity < 0)
        {
            _velocity = -1f;
        }
        else
        {
            _velocity += _gravity * _gravityMultiplier * Time.deltaTime;
        }

        moveDir.y = _velocity;
    }

    private void ApplyJump()
    {
        _isJumping = Input.GetKey(KeyCode.Space);
        if (_isJumping && _characterController.isGrounded)
        {
            _velocity += _jumpPower;
        }
        _animator.SetBool(_jumpID, _isJumping);
    }

    private void ApplySprint()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            _speed = SprintSpeed;
        }
        else
        {
            _speed = WalkSpeed;
        }
    }

    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time > nextAttackTime)
        {
            _animator.SetTrigger(_attackID);
            nextAttackTime = Time.time + AttackCooldown;
        }
    }
}
