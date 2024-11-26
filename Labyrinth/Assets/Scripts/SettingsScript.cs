using System;
using UnityEngine;
using UnityEngine.UI;

public class SettingsScript : MonoBehaviour {
    private GameObject content;
    private Slider effectsSlider, ambientSlider;

    void Start() {
        content = transform.Find("Content").gameObject;
        effectsSlider = transform.Find("Content").Find("EffectsSlider").GetComponent<Slider>();
        GameState.effectsVolume = effectsSlider.value;
        ambientSlider = transform.Find("Content").Find("AmbientSlider").GetComponent<Slider>();
        GameState.ambientVolume = ambientSlider.value;
        Time.timeScale = content.activeInHierarchy ? 0.0f : 1.0f;
    }
    void Update() {
        if (Input.GetKeyUp(KeyCode.Escape)) {
            Time.timeScale = content.activeInHierarchy ? 1.0f : 0.0f;
            content.SetActive(!content.activeInHierarchy);
        }
    }
    public void OnEffectsVolumeChanged(Single value) => GameState.effectsVolume = value;
    public void OnAmbientVolumeChanged(Single value) => GameState.ambientVolume = value;
    public void OnMuteAllChanged(bool value) => GameState.isMuted = value;
}