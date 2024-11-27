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

    public static void ShowToast(string message, float? timeout = null) {
        if (toastMessages.Count > 0 && toastMessages.Last.Value.message == message) message += "2";
        toastMessages.AddLast(new ToastMessage {
            message = message, 
            timeout = timeout ?? instance.timeout 
        });
    }
    void Start() {
        instance = this;
        if (content.activeInHierarchy) content.SetActive(false);
    }
    void Update() {
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
}