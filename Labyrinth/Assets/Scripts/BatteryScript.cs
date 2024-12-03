using UnityEngine;

public class BatteryScript : MonoBehaviour {
    private int baseScore = 1;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.name == "Player" && GameState.isNight) {
            var addedCharge = Random.Range(0.5f, 1.0f) * GameState.difficutly switch {
                GameState.GameDifficulty.Easy => 1.5f,
                GameState.GameDifficulty.Hard => 0.5f,
                _ => 1.0f
            };
            GameState.TriggerEvent("Battery", addedCharge);
            GameState.score += baseScore * GameState.difficutly switch {
                GameState.GameDifficulty.Easy => 1,
                GameState.GameDifficulty.Hard => 3,
                _ => 2
            };
            Destroy(gameObject);
        }
    }
}