using UnityEngine;
using UnityEngine.InputSystem;

public class CameraScript : MonoBehaviour {
    [SerializeField] private GameObject player;
    [SerializeField] private Transform fpvTransform;
    private InputAction lookAction;
    private Vector3 c;
    private bool fpv = true;
    private float mX, mY, sensitivityX = 10.0f, sensitivityY = 10.0f, sensitivityW = 0.1f, fpvRange = 0.6f, maxDistance = 5.0f;
    
    void Start() {
        c = transform.position - player.transform.position;
        mX = transform.eulerAngles.y;
        mY = transform.eulerAngles.x;
        lookAction = InputSystem.actions.FindAction("Look");
    }
    void Update() {
        if (fpv) {
            Vector2 mouseWheel = Input.mouseScrollDelta;
            if (mouseWheel.y != 0) {
                if (c.magnitude > fpvRange) {
                    c = c * (1 + mouseWheel.y * sensitivityW);
                    if (c.magnitude < fpvRange) c = c * 0.01f;
                } else {
                    if (mouseWheel.y > 0) c = c / c.magnitude * 1.1f;
                }
                if (c.magnitude > maxDistance) c = c.normalized * maxDistance;
            }

            Vector2 lookValue = lookAction.ReadValue<Vector2>() * Time.deltaTime;
            mX += lookValue.x * sensitivityX;
            float my = -lookValue.y * sensitivityY;
            if (0 <= mY + my && mY + my <= 50) mY += my;
            transform.eulerAngles = new Vector3(mY, mX, 0);
        }
        if (Input.GetKeyDown(KeyCode.Tab)) {
            fpv = !fpv;
            if (!fpv) {
                transform.position = fpvTransform.position;
                transform.rotation = fpvTransform.rotation;
            }
        }
    }
    void LateUpdate() {
        if (fpv) transform.position = Quaternion.Euler(0, mX, 0) * c + player.transform.position;
    }
}