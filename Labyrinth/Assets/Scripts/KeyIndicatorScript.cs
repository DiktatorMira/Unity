using UnityEngine;
using UnityEngine.UI;

public class KeyIndicatorScript : MonoBehaviour {
    [SerializeField] private float keyTimeout = 3.0f;
    private float activeTime;
    private Image indicator;
    private GameObject content;
    private KeyPointScript parentScript;
    private bool hasEnteredTrigger = false;

    private void Start() {
        parentScript = transform.parent.GetComponent<KeyPointScript>();
        parentScript.isInTime = true;
        indicator = transform.Find("Content/Indicator").GetComponent<Image>();
        content = transform.Find("Content").gameObject;
        content.SetActive(false);
    }
    private void Update() {
        if (content.activeInHierarchy && hasEnteredTrigger) {
            activeTime += Time.deltaTime * (GameState.difficutly switch {
                GameState.GameDifficulty.Easy => 0.5f,
                GameState.GameDifficulty.Hard => 1.5f,
                _ => 1.0f
            });
            if (activeTime >= keyTimeout) {
                parentScript.isInTime = false;
                content.SetActive(false);
                hasEnteredTrigger = false;
            } else {
                indicator.fillAmount = (keyTimeout - activeTime) / keyTimeout;
                indicator.color = new Color(1 - indicator.fillAmount, indicator.fillAmount, 0.2f, 0.5f);
            }
        }
        if (parentScript.isKeyGot) {
            content.SetActive(false);
            hasEnteredTrigger = false;
        }
    }
    private void OnTriggerEnter(Collider other) {
        if (other.name == "Player" && !content.activeInHierarchy && !parentScript.isKeyGot) {
            content.SetActive(true);
            activeTime = 0.0f;
            hasEnteredTrigger = true;
        }
    }
    private void OnTriggerExit(Collider other) {
        if (other.name == "Player") {
            content.SetActive(false);
            hasEnteredTrigger = false;
        }
    }
}