using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using TMPro.Examples;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SettingsScript : MonoBehaviour {
    private GameObject content;
    private Slider effectsSlider, ambientSlider, sensXSlider, sensYSlider;
    private Toggle muteToggle;
    private List<object> defalutValues = new List<object>();

    void Start() {
        content = transform.Find("Content").gameObject;

        muteToggle = transform.Find("Content").Find("SoundsToggle").GetComponent<Toggle>();
        defalutValues.Add(GameState.isMuted);
        if (PlayerPrefs.HasKey(nameof(GameState.isMuted))) {
            GameState.isMuted = PlayerPrefs.GetInt(nameof(GameState.isMuted)) == 1;
            muteToggle.isOn = GameState.isMuted;
        } else GameState.isMuted = muteToggle.isOn;

        effectsSlider = transform.Find("Content").Find("EffectsSlider").GetComponent<Slider>();
        defalutValues.Add(effectsSlider.value);
        if (PlayerPrefs.HasKey(nameof(GameState.effectsVolume))) {
            GameState.effectsVolume = PlayerPrefs.GetFloat(nameof(GameState.effectsVolume));
            effectsSlider.value = GameState.effectsVolume;
        } else GameState.effectsVolume = effectsSlider.value;

        ambientSlider = transform.Find("Content").Find("AmbientSlider").GetComponent<Slider>();
        defalutValues.Add(ambientSlider.value);
        if (PlayerPrefs.HasKey(nameof(GameState.ambientVolume))) {
            GameState.ambientVolume = PlayerPrefs.GetFloat(nameof(GameState.ambientVolume));
            ambientSlider.value = GameState.ambientVolume;
        } else GameState.ambientVolume = ambientSlider.value;

        sensXSlider = transform.Find("Content").Find("SensXSlider").GetComponent<Slider>();
        defalutValues.Add(sensXSlider.value);
        if (PlayerPrefs.HasKey(nameof(GameState.sensitivityLookX))) {
            GameState.sensitivityLookX = PlayerPrefs.GetFloat(nameof(GameState.sensitivityLookX));
            sensXSlider.value = GameState.sensitivityLookX;
        } else GameState.sensitivityLookX = sensXSlider.value;

        sensYSlider = transform.Find("Content").Find("SensYSlider").GetComponent<Slider>();
        defalutValues.Add(sensYSlider.value);
        if (PlayerPrefs.HasKey(nameof(GameState.sensitivityLookY))) {
            GameState.sensitivityLookY = PlayerPrefs.GetFloat(nameof(GameState.sensitivityLookY));
            sensYSlider.value = GameState.sensitivityLookY;
        } else GameState.sensitivityLookY = sensYSlider.value;

        Time.timeScale = content.activeInHierarchy ? 0.0f : 1.0f;
    }
    void Update() {
        if (Input.GetKeyUp(KeyCode.Escape)) OnCloseButtonClick();
    }
    public void OnCloseButtonClick() {
        Time.timeScale = content.activeInHierarchy ? 1.0f : 0.0f;
        content.SetActive(!content.activeInHierarchy);
    }
    public void OnResetButtonClick() {
        muteToggle.isOn = (bool)defalutValues[0];
        effectsSlider.value = (float)defalutValues[1];
        ambientSlider.value = (float)defalutValues[2];
        sensXSlider.value = (float)defalutValues[3];
        sensYSlider.value = (float)defalutValues[4];
    }
    public void OnSaveButtonClick() {
        PlayerPrefs.SetFloat(nameof(GameState.ambientVolume), GameState.ambientVolume);
        PlayerPrefs.SetFloat(nameof(GameState.effectsVolume), GameState.effectsVolume);
        PlayerPrefs.SetFloat(nameof(GameState.sensitivityLookX), GameState.sensitivityLookX);
        PlayerPrefs.SetFloat(nameof(GameState.sensitivityLookY), GameState.sensitivityLookY);
        PlayerPrefs.SetInt(nameof(GameState.isMuted), GameState.isMuted ? 1 : 0);
        try { PlayerPrefs.Save(); }
        catch (Exception ex) { Debug.LogError($"Ошибка сохранения настроек: {ex.Message}"); }
        OnCloseButtonClick();
    }
    public void OnEffectsVolumeChanged(Single value) => GameState.effectsVolume = value;
    public void OnAmbientVolumeChanged(Single value) => GameState.ambientVolume = value;
    public void OnMuteAllChanged(bool value) => GameState.isMuted = value;
    public void OnSensitivityXChanged(Single value) => GameState.sensitivityLookX = value;
    public void OnSensitivityYChanged(Single value) => GameState.sensitivityLookY = value;
}