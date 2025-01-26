using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    [Header("PLAYER PARAMETERS")] 
    [SerializeField] private float _timeInvicible;
    [SerializeField] private int _maxPlayerLife;
    public static PlayerManager instance;
    
    [Header("OTHERS")]
    [SerializeField] private Transform _horizontalLaoyutLife;
    [SerializeField] private Animator _bubleAnimator;

    private int _life;
    private bool _isDead;
    private bool _isInvicible;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        _life = _maxPlayerLife;
    }

    public int GetLife()
    {
        return _life;
    }

    public void GetDamage()
    {
        if(_isInvicible){return;}
        _bubleAnimator.SetTrigger("GetHit");
        GameManager.Instance.ShakeCamera(1.7f, 0.6f, 1f);
        SoundManagerScript.instance.ActivateSound(5, "OneShot");
        
        _horizontalLaoyutLife.GetChild(_life - 1).gameObject.SetActive(false);
        
        SubstractLife();
        SetInvicibility();
    }
    private void SubstractLife()
    {
        _life--;
        CheckLife();
    }

    private void CheckLife()
    {
        if (_life <= 0)
        {
            Death();
        }
    }

    private void SetInvicibility()
    {
        StartCoroutine(CoroutineInvicible());
    }
    

    private void Death()
    {
        _isDead = true;
        SoundManagerScript.instance.ActivateSound(6, "OneShot");
        SceneManager.LoadScene(1);
    }

    private IEnumerator CoroutineInvicible()
    {
        _isInvicible = true;
        yield return new WaitForSeconds(_timeInvicible);
        _isInvicible = false;
    }

    public bool IsPlayerDead()
    {
        return _isDead;
    }

    public bool IsInvicible()
    {
        return _isInvicible;
    }
    
    
    
    
    
}
