using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody2D))]
public class Player_movement : MonoBehaviour
{
    [SerializeField] private PlayerStats _playerStats;
    private int _runState = Animator.StringToHash("Speed");
    private int _idleState = Animator.StringToHash("idle");
    private int _jumpState = Animator.StringToHash("JumpTrigger");
    private int _onGroundState = Animator.StringToHash("IsOnGround");
    private int _fallingState = Animator.StringToHash("isFalling"); 
    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _sprite;
    private Animator _anim;
    private bool _isOnGround = true;
    private bool _isUseDoubleJump = false;
    private EventBus _eventBus;
    private bool _isDialogActivated;

    [Inject]
    void Constract(EventBus eventBus)
    {
        _eventBus = eventBus;
    }
    private void Awake()
    {
        _eventBus.OnDialogStarted += DialogStarted;
        _eventBus.OnDialogEnded += DialogEnded;
    }
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _sprite = GetComponentInChildren<SpriteRenderer>();
        _anim = GetComponent<Animator>();
    }

    private void DialogStarted() => _isDialogActivated = true;
    private void DialogEnded() => _isDialogActivated = false;
    void Update()
    {
        if (_isDialogActivated == true) return;

        float moveInput = Input.GetAxis("Horizontal");

        if (_rigidbody2D.linearVelocity.y == 0)
        {
            _isOnGround = true;
            _isUseDoubleJump = false;
            _anim.SetBool(_onGroundState, true);
            _anim.SetBool(_fallingState, false); 
        }
        else
        {
            _isOnGround = false;
            _anim.SetBool(_onGroundState, false);

            // Проверка на падение
            if (_rigidbody2D.linearVelocity.y < -2)
            {
                _anim.SetBool(_fallingState, true); 
            }
            else
            {
                _anim.SetBool(_fallingState, false); 
            }
        }

        _rigidbody2D.linearVelocity = new Vector2(moveInput * _playerStats.SpeedMovement, _rigidbody2D.linearVelocity.y);
        if (_rigidbody2D.linearVelocity.magnitude > 0.2f && _isOnGround) 
        {
            _anim.SetFloat(_runState, 1);
        }
        else if (_rigidbody2D.linearVelocity.magnitude < 0.2f && _isOnGround) 
        {
            _anim.SetFloat(_runState, 0);
        }

        if (moveInput > 0)
        {
            transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
        else if (moveInput < 0)
        {
            transform.localRotation = Quaternion.Euler(new Vector3(0, 180, 0));
        }

        if ((Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.W)) && _isOnGround)
        {
            _anim.SetTrigger(_jumpState);
            _anim.SetBool(_onGroundState, false);
            _isOnGround = false;
            _rigidbody2D.AddForce(new Vector2(0, _playerStats.JumpForce), ForceMode2D.Impulse);
        }
        else if ((Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.W)) && !_isUseDoubleJump)
        {
            _anim.SetTrigger(_jumpState);
            _rigidbody2D.AddForce(new Vector2(0, _playerStats.JumpForce), ForceMode2D.Impulse);
            _isUseDoubleJump = true;
        }
    }
}
