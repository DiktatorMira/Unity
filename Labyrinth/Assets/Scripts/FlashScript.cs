using UnityEngine;

public class FlashScript : MonoBehaviour {
    private Rigidbody playerRb;
    private Light spotLight;
    private float chargeTimeout = 15.0f;

    private void Start() {
        playerRb = GameObject.Find("CharacterPlayer").GetComponent<Rigidbody>();
        spotLight = GetComponent<Light>();
        GameState.flashCharge = 1.0f;
    }
    void Update() {
        if (GameState.flashCharge > 0 && GameState.isNight) {
            GameState.flashCharge -= Time.deltaTime / chargeTimeout;
            if (GameState.flashCharge > 0.3f) spotLight.intensity = 1.0f;
            else if (GameState.flashCharge >= 0.01f) spotLight.intensity = Mathf.Lerp(0.5f, 1.0f, (GameState.flashCharge - 0.01f) / (0.3f - 0.01f));
            else {
                GameState.flashCharge = 0;
                spotLight.intensity = 0.0f;
            }
        }
        if (GameState.isFpv) transform.rotation = Camera.main.transform.rotation;
        else {
            if (playerRb.linearVelocity.magnitude > 0.01f) transform.forward = playerRb.linearVelocity.normalized;
        }
    }
}