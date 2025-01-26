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
    [SerializeField] bool isSongPlayed;
    [SerializeField] int songNumber;
    [SerializeField] private AudioSource songSource;

    private void Awake(){

        if (instance==null){
            instance = this;
        }
    }

    private void Start()
    {
        if (isSongPlayed)
        {
            songSource.enabled=true;
            songSource.clip = SoundListByOrder[songNumber - 1];
            Debug.Log("JOUE LE SON LA");
        }
        else
        {
            songSource.enabled=false;
        }
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
            selectedSource.clip = SoundListByOrder[soundNumber-1];
            selectedSource.Play();
        }else if(HowPlay=="PitchRandomLoopStart"){
            Pitchloop=true;
            StartCoroutine(PitchLoop(SoundListByOrder[soundNumber-1], selectedSource));
        }else if(HowPlay=="PitchRandomLoopStop"){
            Pitchloop=false;
        }
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