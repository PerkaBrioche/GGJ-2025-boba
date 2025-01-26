using System;
using UnityEngine;

public class BobaController : MonoBehaviour
{
    private bool _isGrounded;
    [SerializeField] private Animator _BobaAnimator;
    private bool _canJump;
    
    public Vector3 _playerInput;
    [SerializeField] private float speedRotation;
    public enum BobaState
    {
        idle,
        running,
        jumping,
        flying,
    }
    
    public void UpdatePlayerInput(Vector2 playerInput)
    {
        _playerInput = playerInput;
    }

    private BobaState actualState;
    
    private void Start()
    {
        //_animator = GetComponent<Animator>();
    }
    
    

    private void Update()
    {
        switch (actualState)
        {
            case BobaState.idle :
                _BobaAnimator.SetBool("Running", false);
                break;
            case BobaState.running :
                _BobaAnimator.SetBool("Running", true);
                break;
            case BobaState.jumping :
                jump();
                break;
            case BobaState.flying :
                break;
        }
        
        
        Vector3 targetDirection = new Vector3(_playerInput.x, 0, _playerInput.y);
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speedRotation * Time.deltaTime);

        
        // Vector3 H = transform.position;
        // Vector3 C = _playerInput;
        // Vector3 HC = H - C;
        // float radianAngle = Mathf.Atan2(HC.y, HC.x);
        

        //  Vector2 newRotation =  P + new Vector2(Mathf.Cos(angleRadian), Mathf.Sin(angleRadian)) * magitude;
        //  viseur.transform.position = newRotation;


    }
    
    public void jump()
    {
        if (!_canJump) { return;}

        _BobaAnimator.SetTrigger("Jump");
    }

    public void ChildFollowY()
    {
        GetComponent<childFollower>().ChangeAxisY();
    }
    
    
    
    public void SetState(BobaState state)
    {
        actualState = state;
        _canJump = true;
    }
    
    public BobaState GetState()
    {
        return actualState;
    }
    
    
    
}
