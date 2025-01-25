using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class BubleMovement : MonoBehaviour
{
    [Header("BUBLE PARAMETERS")]
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _forceJump;
    [SerializeField] private float _bumpForce;
    [SerializeField] private float _cooldownJump;
    
    [Header("OTHERS")]

    [SerializeField] private Transform _boueTransform;

    
    private float _propulsionForce;
    private float _raycastDownRange = 1.8f;
    private float alpha;

    private PlayerInput _bubbleInput;
    private InputAction _leftJoystick;
    private InputAction _jumpAction;
    private InputAction _propulsionAction;
    
    private Rigidbody _rigidbody;
    private Vector2 _playerInput;

    private bool _resetingVelocity;
    private bool _canJump;
    private bool _restingJump;
    
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
        if (IsGrounded() && _canJump)
        {
            _canJump = false;
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

        if (!_canJump && !_restingJump)
        {
            _restingJump = true;
            StartCoroutine(CoolDownJump());
        }
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

    public void GetBounce(Vector3 ObstaclePosition)
    {
        if(PlayerManager.instance == null){return;}
        PlayerManager.instance.GetDamage();
        if (PlayerManager.instance.IsPlayerDead())
        {
            // ECLATER LA BULLE ICI
        }
        else
        {
            Vector3 bouePosiiton = _boueTransform.position;
            Vector3 Obstacle = ObstaclePosition;
            Vector3 POdir =  (bouePosiiton - Obstacle).normalized;
            _rigidbody.AddForce(POdir * _bumpForce, ForceMode.Impulse);
        }
    }

    private IEnumerator CoolDownJump()
    {
        _canJump = false;
        yield return new WaitForSeconds(_cooldownJump);
        _canJump = true;
        _restingJump = false;
    }
}
