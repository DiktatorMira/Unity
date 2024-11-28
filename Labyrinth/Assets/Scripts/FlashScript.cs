using UnityEngine;

public class FlashScript : MonoBehaviour {
    private Rigidbody playerRb;
    private Light spotLight;
    private float chargeTimeout = 15.0f, flashCharge;

    void Start() {
        playerRb = GameObject.Find("CharacterPlayer").GetComponent<Rigidbody>();
        spotLight = GetComponent<Light>();
        flashCharge = 1.0f;
        GameState.SubscribeTrigger(BatteryTriggerListener, "Battery");
    }
    void Update() {
        if (flashCharge > 0 && GameState.isNight) {
            flashCharge -= Time.deltaTime / chargeTimeout;
            if (flashCharge > 0.3f) spotLight.intensity = 1.0f;
            else if (flashCharge >= 0.01f) spotLight.intensity = Mathf.Lerp(0.5f, 1.0f, (flashCharge - 0.01f) / (0.3f - 0.01f));
            else {
                flashCharge = 0;
                spotLight.intensity = 0.0f;
            }
        }
        if (GameState.isFpv) transform.rotation = Camera.main.transform.rotation;
        else {
            if (playerRb.linearVelocity.magnitude > 0.01f) transform.forward = playerRb.linearVelocity.normalized;
        }
    }
    private void BatteryTriggerListener(string type, object payload) {
        if (type == "Battery") flashCharge += (float)payload;
    }
    private void OnDestroy() => GameState.UnsubscribeTrigger(BatteryTriggerListener, "Battery");
}