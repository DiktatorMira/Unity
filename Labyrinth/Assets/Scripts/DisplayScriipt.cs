using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayScriipt : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI clock;
    private Image keyImage;
    private float gameTime;

    private void Start() {
        gameTime = 0.0f;
        clock = transform.Find("Content/Background/ClockTMP").GetComponent<TextMeshProUGUI>();
        keyImage = transform.Find("Content/Background/KeyImage").GetComponent<Image>();
        GameState.SubscribeTrigger(BroadcastTriggerListener);
    }
    private void Update() => gameTime += Time.deltaTime;
    private void LateUpdate() {
        int hour = (int)gameTime / 3600, min = ((int)gameTime % 3600) / 60, sec = (int)gameTime % 60;
        clock.text = $"{hour:D2}:{min:D2}:{sec:D2}";
    }
    private void BroadcastTriggerListener(string type, object payload) {
        Debug.Log(type);
        switch (type) {
            case "1":
                keyImage.enabled = true;
                break;
        }
    }
    private void OnDestroy() => GameState.UnsubscribeTrigger(BroadcastTriggerListener);
}