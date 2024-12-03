using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsScript : MonoBehaviour {
    private GameObject content;
    private Slider effectsSlider, ambientSlider, sensXSlider, sensYSlider;
    private Toggle muteToggle;
    private TMPro.TMP_Dropdown difficultyDropdown;
    private List<object> defalutValues = new List<object>();
    private int defaultDifficulty;

    private void Start() {
        content = transform.Find("Content").gameObject;

        muteToggle = transform.Find("Content").Find("Sounds/SoundsToggle").GetComponent<Toggle>();
        defalutValues.Add(GameState.isMuted);
        if (PlayerPrefs.HasKey(nameof(GameState.isMuted))) {
            GameState.isMuted = PlayerPrefs.GetInt(nameof(GameState.isMuted)) == 1;
            muteToggle.isOn = GameState.isMuted;
        } else GameState.isMuted = muteToggle.isOn;

        effectsSlider = transform.Find("Content").Find("Sounds/EffectsSlider").GetComponent<Slider>();
        defalutValues.Add(effectsSlider.value);
        if (PlayerPrefs.HasKey(nameof(GameState.effectsVolume))) {
            GameState.effectsVolume = PlayerPrefs.GetFloat(nameof(GameState.effectsVolume));
            effectsSlider.value = GameState.effectsVolume;
        } else GameState.effectsVolume = effectsSlider.value;

        ambientSlider = transform.Find("Content").Find("Sounds/AmbientSlider").GetComponent<Slider>();
        defalutValues.Add(ambientSlider.value);
        if (PlayerPrefs.HasKey(nameof(GameState.ambientVolume))) {
            GameState.ambientVolume = PlayerPrefs.GetFloat(nameof(GameState.ambientVolume));
            ambientSlider.value = GameState.ambientVolume;
        } else GameState.ambientVolume = ambientSlider.value;

        sensXSlider = transform.Find("Content").Find("Control/SensXSlider").GetComponent<Slider>();
        defalutValues.Add(sensXSlider.value);
        if (PlayerPrefs.HasKey(nameof(GameState.sensitivityLookX))) {
            GameState.sensitivityLookX = PlayerPrefs.GetFloat(nameof(GameState.sensitivityLookX));
            sensXSlider.value = GameState.sensitivityLookX;
        } else GameState.sensitivityLookX = sensXSlider.value;

        sensYSlider = transform.Find("Content").Find("Control/SensYSlider").GetComponent<Slider>();
        defalutValues.Add(sensYSlider.value);
        if (PlayerPrefs.HasKey(nameof(GameState.sensitivityLookY))) {
            GameState.sensitivityLookY = PlayerPrefs.GetFloat(nameof(GameState.sensitivityLookY));
            sensYSlider.value = GameState.sensitivityLookY;
        } else GameState.sensitivityLookY = sensYSlider.value;

        difficultyDropdown = transform.Find("Content/Difficulty/Dropdown").GetComponent<TMPro.TMP_Dropdown>();
        defalutValues.Add(difficultyDropdown.value);
        if (PlayerPrefs.HasKey(nameof(GameState.difficutly))) {
            GameState.difficutly = (GameState.GameDifficulty)PlayerPrefs.GetInt(nameof(GameState.difficutly));
            difficultyDropdown.value = (int)GameState.difficutly;
        } else GameState.difficutly = (GameState.GameDifficulty)difficultyDropdown.value;

        Time.timeScale = content.activeInHierarchy ? 0.0f : 1.0f;
    }
    private void Update() {
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
        difficultyDropdown.value = (int)defalutValues[5];
    }
    public void OnSaveButtonClick() {
        PlayerPrefs.SetFloat(nameof(GameState.ambientVolume), GameState.ambientVolume);
        PlayerPrefs.SetFloat(nameof(GameState.effectsVolume), GameState.effectsVolume);
        PlayerPrefs.SetFloat(nameof(GameState.sensitivityLookX), GameState.sensitivityLookX);
        PlayerPrefs.SetFloat(nameof(GameState.sensitivityLookY), GameState.sensitivityLookY);
        PlayerPrefs.SetInt(nameof(GameState.isMuted), GameState.isMuted ? 1 : 0);
        PlayerPrefs.SetInt(nameof(GameState.difficutly), (int)GameState.difficutly);
        PlayerPrefs.Save();
    }
    public void OnEffectsVolumeChanged(Single value) => GameState.effectsVolume = value;
    public void OnAmbientVolumeChanged(Single value) => GameState.ambientVolume = value;
    public void OnMuteAllChanged(bool value) => GameState.isMuted = value;
    public void OnSensitivityXChanged(Single value) => GameState.sensitivityLookX = value;
    public void OnSensitivityYChanged(Single value) => GameState.sensitivityLookY = value;
    public void OnDifficultyChanged(int selectedIndex) => GameState.difficutly = (GameState.GameDifficulty)selectedIndex;
}