using UnityEngine;
using UnityEngine.SceneManagement;

public class DisplayScript : MonoBehaviour {
    [SerializeField]
    private TMPro.TextMeshProUGUI clockTMP;
    private float gameTime;

    void Start() {
        clockTMP = GameObject.Find("ClockTMP").GetComponent<TMPro.TextMeshProUGUI>();
        gameTime = 0f;
    }
    void Update() {
        gameTime += Time.deltaTime;
        if (clockTMP != null) {
            int hours = Mathf.FloorToInt(gameTime / 3600);
            int minutes = Mathf.FloorToInt((gameTime % 3600) / 60);
            int seconds = Mathf.FloorToInt(gameTime % 60);
            int tenths = Mathf.FloorToInt((gameTime * 10) % 10);
            clockTMP.text = $"{hours:D2}:{minutes:D2}:{seconds:D2}.{tenths}";
        }
    }
}