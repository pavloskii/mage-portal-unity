using System;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
//[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    [Header("Player")]
    [Tooltip("Move speed of the character in m/s")]
    [SerializeField] private float _moveSpeed = 8.0f;
    private Vector3 playerVelocity;
    private bool _isGrounded;

    [Tooltip("The character uses its own gravity value. The engine default is -9.81f")]
    public float _gravity = -9.81f;

    [Header("Attack")]
    [Tooltip("If there is enemy in range this needs to be true")]
    public bool _shoot = false;
    public ProjectileSpawner _projectileSpawner;

    private Animator _animator;
    private CharacterController _controller;

    // animation IDs
    private int _animIDIsMoving;
    private int _animIDShoot;

    //Inputs
    private Vector2 moveInput;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _controller = GetComponent<CharacterController>();
        //GetComponent<PlayerInput>().neverAutoSwitchControlSchemes = true;

        AssignAnimationIDs();
    }

    private void Update()
    {
        Move();
        Gravity();
        Attack();
    }

    private void Attack()
    {
        //TODO trigger this when someone is in the collision zone instead of having it in update
        _animator.SetBool(_animIDShoot, _shoot);
    }

    private void Gravity()
    {
        //TODO optimazi gravity
        _isGrounded = _controller.isGrounded;
        if (_isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        playerVelocity.y += _gravity * Time.deltaTime;
        _controller.Move(playerVelocity * Time.deltaTime);
    }

    private void Move()
    {
        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y).normalized;
        _controller.Move(_moveSpeed * Time.deltaTime * move);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
            _animator.SetBool(_animIDIsMoving, true);
        }
        else
        {
            _animator.SetBool(_animIDIsMoving, false);
        }
    }

    public void MoveInput(Vector2 moveDirection)
    {
        moveInput = moveDirection;
    }

    private void AssignAnimationIDs()
    {
        _animIDIsMoving = Animator.StringToHash("IsMoving");
        _animIDShoot = Animator.StringToHash("Shoot");
    }

    //Animation Events
    public void OnAttackEvent()
    {
        _projectileSpawner.SpawnProjectile();
    }
}
