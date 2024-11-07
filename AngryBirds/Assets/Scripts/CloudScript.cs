using UnityEngine;

public class CloudScript : MonoBehaviour {
    private Vector3 startPosition, moveDirection = Vector3.left;
    private float speed = 0.001f;

    void Start() => startPosition = transform.position;
    void Update() {
        transform.Translate(moveDirection * speed);
        moveDirection = transform.position.x < -11 ? Vector3.right : transform.position.x > 11 ? Vector3.left : moveDirection;
    }
}