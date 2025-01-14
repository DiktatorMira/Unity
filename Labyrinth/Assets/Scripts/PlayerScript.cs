using UnityEngine;
using UnityEngine.InputSystem;

public class NewMonoBehaviourScript : MonoBehaviour {
    [SerializeField] private float forceFactor = 2.0f;
    private InputAction moveAction;
    private Rigidbody rb;
    private AudioSource[] audioSources;

    private void Start() {
        moveAction = InputSystem.actions.FindAction("Move");
        if (moveAction == null) {
            Debug.LogError("Move action not found!");
            return;
        }

        rb = GetComponent<Rigidbody>();
        audioSources = GetComponents<AudioSource>();

        GameState.Subscribe(OnEffectsVolumeChanged, nameof(GameState.effectsVolume), nameof(GameState.isMuted));
        OnEffectsVolumeChanged();
    }
    private void Update() {
        if (moveAction == null) return;

        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;

        forward.y = 0.0f;
        right.y = 0.0f;
        forward.Normalize();
        right.Normalize();

        Vector2 moveValue = moveAction.ReadValue<Vector2>();
        Vector3 moveDirection = right * moveValue.x + forward * moveValue.y;

        if (moveDirection != Vector3.zero) moveDirection.Normalize();
        if (Input.GetKey(KeyCode.Keypad8)) moveDirection += Time.deltaTime * 60 * forward;
        rb.AddForce(moveDirection * forceFactor, ForceMode.Force);
    }
    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Wall") && !audioSources[0].isPlaying) audioSources[0].Play();
    }
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Battery") && GameState.isNight) audioSources[1].Play();
        else if (other.CompareTag("Key")) {
            bool isInTime = GameState.collectedKeys["1"];
            (isInTime ? audioSources[2] : audioSources[3]).Play();
        }
    }
    private void OnEffectsVolumeChanged() {
        foreach (var audioSource in audioSources) audioSource.volume = GameState.isMuted ? 0.0f : GameState.effectsVolume;
    }
    private void OnDestroy() {
        GameState.Unsubscribe(OnEffectsVolumeChanged, nameof(GameState.effectsVolume), nameof(GameState.isMuted));
    }
}