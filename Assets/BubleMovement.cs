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
    [SerializeField] private float _inputTimeLocker;
    
    
    [Header("OTHERS")]

    
    [SerializeField] private MeshRenderer _bulleMaterial;
    [SerializeField] private Transform _boueTransform;
   [SerializeField] private BobaController _bobaController;

    
    private float _propulsionForce;
    [SerializeField] private float _raycastDownRange = 1.8f;
    [SerializeField] private float time;
    [SerializeField] private float secondTime;
    private float alpha;

    private PlayerInput _bubbleInput;
    private InputAction _leftJoystick;
    private InputAction _jumpAction;
    private InputAction _propulsionAction;
    
    private Rigidbody _rigidbody;
    private Vector2 _playerInput;

    private bool _resetingVelocity;
    private bool _canJump = true;
    private bool isJumping = false;
    private bool _canMoove;
    
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
            _bobaController.jump();
            StartCoroutine(WaitForImpulsion());
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


            if (_playerInput.x != 0 || _playerInput.y != 0)
            {

                if (IsGrounded())
                {
                    _bobaController.SetState(BobaController.BobaState.running);
                }
                
                _bobaController.UpdatePlayerInput(-_playerInput);
            }
            else
            {
                _bobaController.SetState(BobaController.BobaState.idle);
            }
        
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
        if (!_canJump && isJumping && IsGrounded())
        {
            _bobaController.ChildFollowY();
            isJumping = false;
            StartCoroutine(CoolDownJump());
            StartCoroutine(BubleRecuperation());
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

        if (PlayerManager.instance.IsPlayerDead()) {return;}

        
        StartCoroutine(StopPlayerInput());
        _rigidbody.linearVelocity = Vector3.zero;
        Vector3 bouePosiiton = _boueTransform.position;
        Vector3 Obstacle = ObstaclePosition;
        Vector3 BOdir =  (bouePosiiton - Obstacle).normalized;
        _rigidbody.AddForce(BOdir * _bumpForce, ForceMode.Impulse);
        
        if(PlayerManager.instance.IsInvicible()){return;}
        
        PlayerManager.instance.GetDamage();
    }
    

    private IEnumerator CoolDownJump()
    {
        _canJump = false;
        yield return new WaitForSeconds(_cooldownJump);
        _canJump = true;
    }

    private IEnumerator StopPlayerInput()
    {
        _canMoove = false;
        yield return new WaitForSeconds(_inputTimeLocker);
        _canMoove = true;
    }
    
    private IEnumerator WaitForImpulsion()
    {
        _rigidbody.AddForce(Vector3.up * _forceJump, ForceMode.Impulse);
        yield return new WaitForSeconds(time);
        _bobaController.ChildFollowY();
        yield return new WaitForSeconds(secondTime);

        isJumping = true;
    }

    private IEnumerator BubleRecuperation()
    {
        float alpha = 1;
        Color baseColor = _bulleMaterial.material.color;
        while (alpha > 0)
        {
            alpha -= Time.deltaTime / _cooldownJump;
            _bulleMaterial.material.color = Color.Lerp(baseColor , Color.green, alpha);
            yield return null;
        }
    }
}