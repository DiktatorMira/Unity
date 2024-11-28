using TMPro;
using UnityEngine;

public class DisplayScriipt : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI clock;
    private float gameTime;

    private void Start() => gameTime = 0.0f;
    private void Update() => gameTime += Time.deltaTime;
    private void LateUpdate() {
        int hour = (int)gameTime / 3600, min = ((int)gameTime % 3600) / 60, sec = (int)gameTime % 60;
        clock.text = $"{hour:D2}:{min:D2}:{sec:D2}";
    }
}