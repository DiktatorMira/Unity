using UnityEngine;

public class BatteryScript : MonoBehaviour {
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.name == "Player" && GameState.isNight) {
            GameState.TriggerEvent("Battery", Random.Range(0.5f, 1.0f));
            Destroy(gameObject);
        }
    }
}