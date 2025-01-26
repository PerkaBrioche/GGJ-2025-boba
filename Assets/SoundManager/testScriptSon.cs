using UnityEngine;
using System.Collections;

public class testScriptSon : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(Test());
    }

    IEnumerator Test()
    {
        yield return new WaitForSeconds(3f);
        SoundManagerScript.instance.ActivateSound(7, "OneShot");
    }
}
