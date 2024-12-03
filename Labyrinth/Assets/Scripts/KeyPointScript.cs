using UnityEngine;

public class KeyPointScript : MonoBehaviour {
    [SerializeField] private string keyName;
    public bool isInTime { get; set; }
    private bool iskeygot;
    public bool isKeyGot {
        get => iskeygot; 
        set {
            iskeygot = value;
            if (value) {
                GameState.collectedKeys.Add(keyName, isInTime);
                GameState.TriggerEvent(keyName, new TriggerPayload() {
                    notification = $"Ключ \"{keyName}\" подобран " + (isInTime ? "воворемя" : "не вовремя"),
                    payload = isInTime
                });
                GameState.score += (isInTime ? 2 : 1) * (GameState.difficutly switch {
                    GameState.GameDifficulty.Easy => 1,
                    GameState.GameDifficulty.Hard => 3,
                    _ => 2
                });
            }
        } 
    }
}