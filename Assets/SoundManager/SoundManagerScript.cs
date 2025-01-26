using UnityEngine;
using System.Collections;

public class SoundManagerScript : MonoBehaviour
{

    public static SoundManagerScript instance;
    [SerializeField] private AudioClip[] SoundListByOrder;
    [SerializeField] private AudioSource[] audioSources;
    bool loop=false;
    bool Pitchloop=false;
    AudioSource selectedSource;

    private void Awake(){

        if (instance==null){
            instance = this;
        }
    }

    public void ButtonSound()
    {
        ActivateSound(20, "OneShot");
    }

    public void ActivateSound(int soundNumber, string HowPlay){
        foreach(AudioSource audio in audioSources){
            if(audio.isPlaying==false){
                selectedSource = audio;
            }
        }

        
        if(HowPlay=="LoopStart"){
            loop=true;
            StartCoroutine(Loop(SoundListByOrder[soundNumber-1], selectedSource));
        }else if(HowPlay=="LoopStop"){
            loop=false;
        }else if(HowPlay=="OneShot"){
            AudioSource source = gameObject.AddComponent<AudioSource>();
            source.clip = SoundListByOrder[soundNumber-1];
            source.Play();
            StartCoroutine(DestroyAfterFinished(source, source.clip.length));
        }else if(HowPlay=="PitchRandomLoopStart"){
            Pitchloop=true;
            StartCoroutine(PitchLoop(SoundListByOrder[soundNumber-1], selectedSource));
        }else if(HowPlay=="PitchRandomLoopStop"){
            Pitchloop=false;
        }
    }

    IEnumerator DestroyAfterFinished(AudioSource source,float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(source);
    }

    IEnumerator Loop(AudioClip clip, AudioSource source){
        AudioClip actualClip = clip;
        AudioSource actualSource = source;
        actualSource.clip = actualClip;
        actualSource.Play();
        yield return new WaitForSeconds(actualClip.length);
        if(loop){
            StartCoroutine(Loop(actualClip, actualSource));
        }
    }

    IEnumerator PitchLoop(AudioClip clip, AudioSource source){
        AudioClip actualClip = clip;
        AudioSource actualSource = source;
        actualSource.clip = actualClip;
        float randomPitch;
        randomPitch = Random.Range(-1.0f, 3.0f);
        while (randomPitch <= 0)
        {
            randomPitch = Random.Range(-1.0f, 3.0f);
        }
        yield return new WaitForSeconds(0.1f);
        actualSource.pitch = randomPitch;
        actualSource.Play();
        yield return new WaitForSeconds(actualClip.length);
        if(Pitchloop){
            StartCoroutine(PitchLoop(actualClip, actualSource));
        }
    }
}