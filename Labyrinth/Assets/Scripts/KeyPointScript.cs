using System.Collections.Generic;
using UnityEngine;

public class KeyPointScript : MonoBehaviour {
    [SerializeField] private string keyName;
    public bool isInTime { get; set; }
    private bool iskeygot;
    public bool isKeyGot  {
        get => iskeygot;
        set {
            iskeygot = value;
            if (value) {
                if (!GameState.collectedKeys.ContainsKey(keyName)) {
                    GameState.collectedKeys.Add(keyName, isInTime);
                    GameState.TriggerEvent(keyName, new Dictionary<string, object> {
                        { "KeyName", keyName },
                        { "IsInTime", isInTime }
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
}