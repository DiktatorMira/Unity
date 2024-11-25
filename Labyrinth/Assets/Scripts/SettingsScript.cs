using System;
using UnityEngine;
using UnityEngine.UI;

public class SettingsScript : MonoBehaviour {
    private GameObject content;
    private Slider effectsVolumeSlider;

    void Start() {
        Transform contentTransform = transform.Find("Content");
        content = contentTransform.gameObject;
        effectsVolumeSlider = contentTransform.Find("EffectsSlider").GetComponent<Slider>();
        GameState.effectVolume = effectsVolumeSlider.value;
    }
    void Update() {
        if (Input.GetKeyUp(KeyCode.Escape)) content.SetActive(!content.activeInHierarchy);
    }
    public void OnEffectsVolumeChanged(Single value) {
        GameState.effectVolume = value;
    }
}