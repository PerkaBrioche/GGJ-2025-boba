using System;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    [SerializeField] Vector2 _cameraShakeBase;
    
    [SerializeField] CinemachineCamera _camera;
    public static GameManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void ShakeCamera(float amplitude, float frequency, float duration)
    {
        _camera.GetComponent<CinemachineBasicMultiChannelPerlin>().AmplitudeGain = amplitude;
        _camera.GetComponent<CinemachineBasicMultiChannelPerlin>().FrequencyGain = frequency;
        StartCoroutine(ShakeCameraLerp(amplitude, frequency, duration));
    }

    private IEnumerator ShakeCameraLerp(float amplitude, float frequency, float duration)
    {
        float alpha = 1;
        while (alpha > 0)
        {
            alpha -= Time.deltaTime / duration;
            _camera.GetComponent<CinemachineBasicMultiChannelPerlin>().AmplitudeGain = Mathf.Lerp(_cameraShakeBase.x, amplitude, alpha);
            _camera.GetComponent<CinemachineBasicMultiChannelPerlin>().FrequencyGain = Mathf.Lerp(_cameraShakeBase.y, frequency, alpha);
            yield return null;
        }
    }
}
