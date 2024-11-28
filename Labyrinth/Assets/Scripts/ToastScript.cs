using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class ToastScript : MonoBehaviour {
    [SerializeField] private float timeout = 3.0f;
    [SerializeField] private TMPro.TextMeshProUGUI toastTMP;
    [SerializeField] private GameObject content;
    private static ToastScript instance;
    private static float showTime;
    private static readonly LinkedList<ToastMessage> toastMessages = new LinkedList<ToastMessage>();

    private class ToastMessage {
        public string message;
        public float timeout;
    }

    private void Start() {
        instance = this;
        if (content.activeInHierarchy) content.SetActive(false);
        GameState.SubscribeTrigger(BroadCastListener);
    }
    private void Update() {
        if (showTime > 0.0f) {
            showTime -= Time.deltaTime;
            if (showTime <= 0.0f) {
                showTime = 0.0f;
                toastMessages.RemoveFirst();
                content.SetActive(false);
            }
        } else if (toastMessages.Count > 0) {
            toastTMP.text = toastMessages.First.Value.message;
            showTime = toastMessages.First.Value.timeout;
            content.SetActive(true);
        }
    }
    public static void ShowToast(string message, float? timeout = null) {
        if (toastMessages.Count > 0 && toastMessages.Last.Value.message == message) message += "2";
        toastMessages.AddLast(new ToastMessage {
            message = message,
            timeout = timeout ?? instance.timeout
        });
    }
    private void BroadCastListener(string type, object payload) {
        string[] toastedTypes = { "Battery", "KeyCollected" };
        if (toastedTypes.Contains(type)) {
            if (type == "KeyCollected" && payload is Dictionary<string, object> keyData) {
                string keyName = keyData.ContainsKey("KeyName") ? keyData["KeyName"].ToString() : "Unknown";
                bool isInTime = keyData.ContainsKey("IsInTime") && (bool)keyData["IsInTime"];
                ShowToast($"Ключ {keyName} найден {(isInTime ? "вовремя" : "поздно")}", 1.5f);
            } else if (type == "Battery" && payload is float chargeAmount) {
                int chargePercent = Mathf.RoundToInt(chargeAmount * 100);
                ShowToast($"Заряд пополнен на {chargePercent}%");
            }
        }
    }
    private void OnDestroy() => GameState.UnsubscribeTrigger(BroadCastListener);
}