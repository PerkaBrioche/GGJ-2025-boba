using System;
using UnityEngine;

public class BobaController : MonoBehaviour
{
    private bool _isGrounded;
    
    public enum BobaState
    {
        idle,
        running,
        jumping,
        flying,
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
                break;
            case BobaState.running :
                break;
            case BobaState.jumping :
                jump();
                break;
            case BobaState.flying :
                break;
        }
    }
    
    private void jump()
    {
        if (_isGrounded)
        {
          //  _animator.SetTrigger("Jump");
        }
    }
    
    
    public void SetState(BobaState state)
    {
        actualState = state;
    }
    
    public BobaState GetState()
    {
        return actualState;
    }
    
    
}
