using UnityEngine;
using UnityEngine.InputSystem;

public class KeyCollectScript : MonoBehaviour {
    private KeyPointScript parentScript;

    void Start() {
        parentScript = transform.parent.GetComponent<KeyPointScript>();
        parentScript.isInTime = true;
    }
    void Update() => transform.Rotate(120.0f * Time.deltaTime, 0, 0);
    private void OnTriggerEnter(Collider other) {
        if (other.name == "Player") {
            parentScript.isKeyGot = true;
            Destroy(gameObject);
        }
    }
}