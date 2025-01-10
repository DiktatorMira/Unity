using UnityEngine;
using UnityEngine.InputSystem;
public class KeyCollectScript : MonoBehaviour {
    private KeyPointScript parentScript;
    private AudioSource audioSource;

    private void Start() {
        parentScript = transform.parent.GetComponent<KeyPointScript>();
        parentScript.isInTime = true;
        audioSource = GetComponent<AudioSource>();
    }
    private void Update() {
        transform.Rotate(120.0f * Time.deltaTime, 0, 0);
    }
    private void OnTriggerEnter(Collider other) {
        if (other.name == "Player" && !parentScript.isKeyGot) {
            if (audioSource != null) {
                audioSource.volume = GameState.isMuted ? 0.0f : GameState.effectsVolume;
                audioSource.Play();
            }
            parentScript.isKeyGot = true;
            Destroy(gameObject);
        }
    }
}