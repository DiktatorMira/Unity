using UnityEngine;

public class BirdScript : MonoBehaviour {
    [SerializeField]
    private Transform arrow;
    private Rigidbody2D rb2d;
    [SerializeField]
    private float minForce = 300f, maxForce = 1500f;
    public static bool hasFlown;

    void Start() {
        rb2d = GetComponent<Rigidbody2D>();
        hasFlown = false;
    }
    void Update() {
        if (Input.GetKeyDown(KeyCode.Space) && !hasFlown && !ModalScript.isMenu) {
            float forceAmplitude = minForce + (maxForce - minForce) * ForceIndicatorScript.forceFactor;
            rb2d.AddForce(arrow.right * forceAmplitude);
            hasFlown = true;
        }
    }
}