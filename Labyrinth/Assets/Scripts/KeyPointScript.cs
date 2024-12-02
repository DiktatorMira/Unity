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
                GameState.TriggerKeyEvent(keyName, isInTime);
            }
        } 
    }
}