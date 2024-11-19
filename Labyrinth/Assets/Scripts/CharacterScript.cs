using UnityEngine;

public class CharacterScript : MonoBehaviour {
    private GameObject player;

    void Start() => player = GameObject.Find("CharacterPlayer");
    void Update() {
        transform.position = player.transform.position;
        player.transform.localPosition = Vector3.zero;
    }
}