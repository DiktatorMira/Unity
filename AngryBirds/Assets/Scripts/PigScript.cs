using Assets.Scripts;
using UnityEngine;

public class PigScript : MonoBehaviour {
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("PigDestroy")) {
            GameState.isLevelCompleted = true;
            GameState.Pause();
        }
    }
}