using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PausePanel : Panel{


    private const string MUSIC_GROUP = "MusicVolume";
    private const string SFX_GROUP = "SFXVolume";
    private const string UI_GROUP = "UIVolume";

    [SerializeField] private AudioMixer mixer;

    [SerializeField] private Image musicToggleImage;
    [SerializeField] private Image sfxToggleImage;
    [SerializeField] private Image uiToggleImage;

    [SerializeField] private Sprite enabledSoundSprite;
    [SerializeField] private Sprite disabledSoundSprite;

    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider uiSlider;

    private bool musicEnabled = true;
    private bool sfxEnabled = true;
    private bool uiEnabled = true;

    public void ToggleMusic(){

        musicEnabled = !musicEnabled;
        mixer.SetFloat(MUSIC_GROUP, musicEnabled ? Mathf.Log10(musicSlider.value) * 20 : -80);
        musicToggleImage.sprite = musicEnabled ? enabledSoundSprite : disabledSoundSprite;

    }
    public void SetMusicValue(float value){
        if(musicEnabled){
            mixer.SetFloat(MUSIC_GROUP, Mathf.Log10(value) * 20);
        }
    }

    public void ToggleSfx(){

        sfxEnabled = !sfxEnabled;
        mixer.SetFloat(SFX_GROUP, sfxEnabled ? Mathf.Log10(sfxSlider.value) * 20 : -80);
        sfxToggleImage.sprite = sfxEnabled ? enabledSoundSprite : disabledSoundSprite;

    }
    public void SetSfxValue(float value){
        if(sfxEnabled){
            mixer.SetFloat(SFX_GROUP, Mathf.Log10(value) * 20);
        }
    }
    public void ToggleUI(){

        uiEnabled = !uiEnabled;
        mixer.SetFloat(UI_GROUP, uiEnabled ? Mathf.Log10(uiSlider.value) * 20 : -80);
        uiToggleImage.sprite = uiEnabled ? enabledSoundSprite : disabledSoundSprite;

    }
    public void SetUIValue(float value){
        if(uiEnabled){
            mixer.SetFloat(UI_GROUP, Mathf.Log10(value) * 20);
        }
    }
    public override void Close(){
        PanelManager.Instance.ClosePause();
    }

    public override void ResetUI(){
        RefreshUI();
    }

    public override void RefreshUI(){
        
    }
    public void Restart(){
        Close();
        SceneManager.LoadScene(0);
    }
}
