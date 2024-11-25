using UnityEngine;
using UnityEngine.InputSystem;

public class NewMonoBehaviourScript : MonoBehaviour {
    [SerializeField] private float forceFactor = 2.0f;
    private InputAction moveAction;
    private Rigidbody rb;
    AudioSource[] audioSources;

    void Start() {
        moveAction = InputSystem.actions.FindAction("Move");
        rb = GetComponent<Rigidbody>();
        audioSources = GetComponents<AudioSource>();
    }
    void Update() {
        Vector3 forward = Camera.main.transform.forward, right = Camera.main.transform.right;
        forward.y = 0.0f; 
        forward.Normalize();
        right.y = 0.0f;
        right.Normalize();

        Vector2 moveValue = moveAction.ReadValue<Vector2>();
        Vector3 moveDirection = right * moveValue.x + forward * moveValue.y;
        moveDirection.Normalize(); 
        rb.AddForce(moveDirection * forceFactor, ForceMode.Force);
    }
    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Wall") && !audioSources[0].isPlaying) {
            audioSources[0].volume = GameState.effectVolume;
            audioSources[0].Play();
        }
    }
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Battery") && GameState.isNight) {
            audioSources[1].volume = GameState.effectVolume;
            audioSources[1].Play();
        } else if (other.CompareTag("Key")) {
            audioSources[2].volume = audioSources[3].volume = GameState.effectVolume;
            bool isInTime = GameState.collectedKeys["1"];
            (isInTime ? audioSources[2] : audioSources[3]).Play();
        }
    }
}