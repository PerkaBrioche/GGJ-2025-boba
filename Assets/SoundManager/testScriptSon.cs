using UnityEngine;
using System.Collections;

public class testScriptSon : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(waitForTest());
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator waitForTest()
    {
        Debug.Log("test PitchLoop 1");
        SoundManagerScript.instance.ActivateSound(1, "PitchRandomLoopStart");
        yield return new WaitForSeconds(20);
        //SoundManagerScript.instance.ActivateSound(1, "PitchRandomLoopStop");
        //Debug.Log("test PitchLoop 2");
    }
}
