using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DisplayScriipt : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI clock;
    private TextMeshProUGUI scoreTMP;
    private List<Image> keyImages = new();
    private float gameTime;
    private void Start() {
        gameTime = 0.0f;
        clock = transform.Find("Content/Background/ClockTMP").GetComponent<TextMeshProUGUI>();
        scoreTMP = transform.Find("Content/Background/ScoreTMP").GetComponent<TextMeshProUGUI>();
        keyImages.Add(transform.Find("Content/Background/KeyImage1").GetComponent<Image>());
        keyImages.Add(transform.Find("Content/Background/KeyImage2").GetComponent<Image>());
        GameState.SubscribeTrigger(BroadcastTriggerListener);
    }
    private void Update() {
        gameTime += Time.deltaTime;
        scoreTMP.text = GameState.score.ToString();
    }
    private void LateUpdate() {
        int hour = (int)gameTime / 3600, min = ((int)gameTime % 3600) / 60, sec = (int)gameTime % 60;
        clock.text = $"{hour:D2}:{min:D2}:{sec:D2}";
    }
    private void BroadcastTriggerListener(string type, object payload) {
        if (int.TryParse(type, out int keyIndex) && keyIndex >= 1 && keyIndex <= 2) keyImages[keyIndex - 1].enabled = true;
    }
    private void OnDestroy() => GameState.UnsubscribeTrigger(BroadcastTriggerListener);
}