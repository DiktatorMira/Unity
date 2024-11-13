using UnityEngine;

public class CameraScript : MonoBehaviour {
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private Transform fpvTransform;
    private Vector3 c;
    private bool fpv = true;
    
    void Start() => c = transform.position - player.transform.position;
    void Update() {
        if(Input.GetKeyDown(KeyCode.Tab)) {
            fpv = !fpv;
            if(!fpv) {
                transform.position = fpvTransform.position;
                transform.rotation = fpvTransform.rotation;
            }
        }
    }
    void LateUpdate() {
        if(fpv) transform.position = c + player.transform.position;
    }
}