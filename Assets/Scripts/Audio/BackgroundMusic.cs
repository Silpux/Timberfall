using UnityEngine;
using UnityEngine.Audio;

public class BackgroundMusic : MonoBehaviour{

    [SerializeField] private AudioMixerSnapshot normalSnapshot;
    [SerializeField] private AudioMixerSnapshot pausedSnapshot;

    [SerializeField] private AudioClip[] audioClips;

    private AudioSource audioSource;
    private int currentClipIndex;

    private void Awake(){
        audioSource = GetComponent<AudioSource>();
    }

    private void Update(){
        if(!audioSource.isPlaying){
            PlayNextClip();
        }
    }

    public void PlayNextClip(){
        currentClipIndex = (currentClipIndex + 1) % audioClips.Length;
        audioSource.clip = audioClips[currentClipIndex];
        audioSource.Play();
    }

    public void SetPausedMode(){
        pausedSnapshot.TransitionTo(0.1f);
    }

    public void SetNormalMode(){
        normalSnapshot.TransitionTo(0.1f);
    }

}
