using UnityEngine;

public class CharacterScript : MonoBehaviour {
    private GameObject player;
    private AudioSource ambientSound;

    void Start() {
        player = GameObject.Find("CharacterPlayer");
        ambientSound = GetComponent<AudioSource>();
        GameState.Subscribe(nameof(GameState.ambientVolume), OnAmbientVolumeChanged);
        GameState.Subscribe(nameof(GameState.isMuted), OnMuteChanged);
        OnAmbientVolumeChanged();
    }
    void Update() {
        transform.position = player.transform.position;
        player.transform.localPosition = Vector3.zero;
    }
    private void OnAmbientVolumeChanged() => ambientSound.volume = GameState.isMuted ? 0.0f : GameState.ambientVolume;
    private void OnMuteChanged() => ambientSound.volume = GameState.isMuted ? 0.0f : GameState.ambientVolume;
    private void OnDestroy() {
        GameState.Unsubscribe(nameof(GameState.ambientVolume), OnAmbientVolumeChanged);
        GameState.Unsubscribe(nameof(GameState.isMuted), OnMuteChanged);
    }
}