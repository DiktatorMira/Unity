using UnityEngine;
using UnityEngine.InputSystem;

public class NewMonoBehaviourScript : MonoBehaviour {
    [SerializeField] private float forceFactor = 1.0f;
    private InputAction moveAction;
    private Rigidbody rb;

    void Start() {
        moveAction = InputSystem.actions.FindAction("Move");
        rb = GetComponent<Rigidbody>();
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
}