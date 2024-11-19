using UnityEngine;

public class BatteryScript : MonoBehaviour {
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.name == "Player" && GameState.isNight) {
            GameState.flashCharge = Mathf.Clamp(GameState.flashCharge + Random.Range(0.5f, 1.0f), 0.0f, 1.0f);
            Destroy(gameObject);
        }
    }
}