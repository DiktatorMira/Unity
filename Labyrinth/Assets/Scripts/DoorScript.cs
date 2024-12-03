using System;
using System.Linq;
using UnityEngine;

public class DoorScript : MonoBehaviour {
    [SerializeField] private string keyName = "1";
    private bool isOpen, isLocked;
    private float inTime = 2.0f, OutTime = 20.0f, openTime;
    private AudioSource[] audioSources;

    private void Start() {
        isLocked = true;
        isOpen = false;
        openTime = 0.0f;
        audioSources = GetComponents<AudioSource>();
        GameState.Subscribe(OnEffectsVolumeChanged, nameof(GameState.effectsVolume));
        OnEffectsVolumeChanged();
    }
    private void Update() {
        if (openTime > 0.0f && !isLocked && !isOpen) {
            openTime -= Time.deltaTime;
            transform.Translate(Time.deltaTime / openTime * Vector3.up);
            if (openTime <= 0.0f) isOpen = true;
        }
    }
    private void OnCollisionEnter(Collision collision) {
        if (GameState.collectedKeys.Keys.Contains(keyName) && isLocked) {
            bool isInTime = GameState.collectedKeys[keyName];
            openTime = (isInTime ? inTime : OutTime) * (GameState.difficutly switch {
                GameState.GameDifficulty.Easy => 0.5f,
                GameState.GameDifficulty.Hard => 1.5f,
                _ => 1.0f
            });
            GameState.TriggerEvent("Door", new TriggerPayload() {
                notification = $"Ключ \"{keyName}\" открыл",
                payload = "Открыто"
            });
            isLocked = false;
            (isInTime ? audioSources[1] : audioSources[2]).Play();
        } else {
            GameState.TriggerEvent("Door", new TriggerPayload() {
                notification = $"Для этой вери необходим ключ \"{keyName}\"",
                payload = "Закрыто"
            });
            audioSources[0].Play();
        }
    }
    public void OnEffectsVolumeChanged() {
        foreach (var audioSource in audioSources) audioSource.volume = GameState.isMuted ? 0.0f : GameState.effectsVolume;
    }
    private void OnDestroy() => GameState.Unsubscribe(OnEffectsVolumeChanged, nameof(GameState.effectsVolume));
}