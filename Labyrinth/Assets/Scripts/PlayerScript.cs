using UnityEngine;
using UnityEngine.InputSystem;

public class NewMonoBehaviourScript : MonoBehaviour {
    [SerializeField] private float forceFactor = 1.5f;
    private InputAction moveAction;
    private Rigidbody rb;

    void Start() {
        moveAction = InputSystem.actions.FindAction("Move");
        rb = GetComponent<Rigidbody>();
    }
    void Update() {
        Vector2 moveValue = moveAction.ReadValue<Vector2>();
        Vector3 correctedForward = Camera.main.transform.forward;
        correctedForward.y = 0.0f;
        correctedForward.Normalize();
        rb.AddForce(forceFactor * Camera.main.transform.right * moveValue.x + correctedForward * moveValue.y);
    }
}