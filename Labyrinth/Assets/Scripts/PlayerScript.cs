using UnityEngine;
using UnityEngine.InputSystem;

public class NewMonoBehaviourScript : MonoBehaviour {
    [SerializeField]
    private float forceFactor = 1.5f;
    private InputAction moveAction;
    private Rigidbody rb;

    void Start() {
        moveAction = InputSystem.actions.FindAction("Move");
        rb = GetComponent<Rigidbody>();
    }
    void Update() {
        Vector2 moveValue = moveAction.ReadValue<Vector2>();
        rb.AddForce(forceFactor * new Vector3(moveValue.x, 0.0f, moveValue.y));
    }
}