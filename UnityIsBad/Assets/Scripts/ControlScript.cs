using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour {
    private Rigidbody2D rb2d;
    public float moveSpeed = 5f, torqueAmount = 10f;

    void Start() => rb2d = GetComponent<Rigidbody2D>();
    void Update() {
        rb2d.AddForce(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * moveSpeed);
        if (Input.GetKey(KeyCode.DownArrow)) rb2d.AddTorque(-torqueAmount);
        else if (Input.GetKey(KeyCode.UpArrow)) rb2d.AddTorque(torqueAmount);
        else if (Input.GetKeyDown(KeyCode.Escape)) {
            rb2d.angularVelocity = 0f;
            rb2d.linearVelocity = Vector2.zero;
        }
    }
}