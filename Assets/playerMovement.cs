using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerMovement : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _forceJump;
    [SerializeField] private float _propulsionForce;
    [SerializeField] private float _raycastDownRange;
    private PlayerInput _bubbleInput;
    private InputAction _leftJoystick;
    private InputAction _jumpAction;
    private InputAction _propulsionAction;
    
    private Rigidbody _rigidbody;
    private Vector2 _playerInput;

    private float alpha;
    private bool _resetingVelocity; 
    
    private void OnEnable()
    {
        _bubbleInput = GetComponent<PlayerInput>();
        
        _leftJoystick = _bubbleInput.actions["LeftJoystick"];
        _jumpAction = _bubbleInput.actions["Jump"];
        _propulsionAction = _bubbleInput.actions["Propulsion"];
        
        _leftJoystick.performed += OnMoveInput;
        _jumpAction.performed += Jump;
        _propulsionAction.performed += Propulsion;
    }
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    
    public void OnMoveInput(InputAction.CallbackContext context)
    {
    }

    public void Propulsion(InputAction.CallbackContext context)
    {
        Vector3 A = transform.position;
        Vector3 B = Vector3.up;

        Vector3 direction = B - A;
        
        _rigidbody.AddForce(direction * _propulsionForce, ForceMode.Impulse);
        
    }
    
    public void Jump(InputAction.CallbackContext context)
    {
        if (IsGrounded())
        {
            _rigidbody.AddForce(Vector3.up * _forceJump, ForceMode.Impulse);
        }
    }

    public void MovementBall()
    {

       // print(_playerInput);
        
        // if (_playerInput.x == 0 && _playerInput.y == 0)
        // {
        //     if (!_resetingVelocity)
        //     {
        //         _resetingVelocity = true;
        //         StartCoroutine("ResetVelocity");
        //     }
        // }
        // else
        // {
            // print("MOVINGG");
            // if (_resetingVelocity)
            // {
            //     StopCoroutine("ResetVelocity");
            //     _resetingVelocity = false;
            // }
            
            
            _rigidbody.AddTorque(new Vector3(_playerInput.y, 0f, -_playerInput.x) * _rotationSpeed);
            _rigidbody.AddForce(new Vector3(_playerInput.x, 0f, _playerInput.y) * _moveSpeed, ForceMode.Acceleration);
       // }
        

    }
    void FixedUpdate()
    {
        MovementBall();
    }

    private bool IsGrounded()
    {
        if (Physics.Raycast(transform.position, -Vector3.up, _raycastDownRange, LayerMask.GetMask("Ground")))
        {
            return true;
        }
        return false;
    }

    private void Update()
    {
        _playerInput = _leftJoystick.ReadValue<Vector2>();
        print(_rigidbody.linearVelocity);
    }
    
    private IEnumerator ResetVelocity()
    {
        alpha = 0;
        while (alpha < 1 )
        {
            alpha += Time.deltaTime;
            _rigidbody.linearVelocity = Vector3.Lerp(_rigidbody.linearVelocity, Vector3.zero, alpha);
            yield return null;
        }
        
    }
}
