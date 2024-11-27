using UnityEngine;
using UnityEngine.InputSystem;

public class CameraScript : MonoBehaviour {
    [SerializeField] private GameObject player;
    [SerializeField] private Transform fpvTransform;
    private InputAction lookAction;
    private Vector3 c;
    private bool fpv = true;
    private float mX, mY, sensitivityX, sensitivityY, sensitivityW = 0.35f, fpvRange = 0.6f, maxDistance = 5.0f;
    
    void Start() {
        c = transform.position - player.transform.position;
        mX = transform.eulerAngles.y;
        mY = transform.eulerAngles.x;
        lookAction = InputSystem.actions.FindAction("Look");
        GameState.Subscribe(OnSensitivityChanged, nameof(GameState.sensitivityLookX), nameof(GameState.sensitivityLookY));
        OnSensitivityChanged();
    }
    void Update() {
        if (fpv) {
            Vector2 mouseWheel = Input.mouseScrollDelta * Time.timeScale;
            if (mouseWheel.y != 0) {
                if (c.magnitude > fpvRange) {
                    c = c * (1 + mouseWheel.y * sensitivityW);
                    if (c.magnitude < fpvRange) {
                        c = c * 0.01f;
                        GameState.isFpv = true;
                    }
                } else {
                    if (mouseWheel.y > 0) {
                        c = c / c.magnitude * 1.1f;
                        GameState.isFpv = false;
                    }
                }
                if (c.magnitude > maxDistance) c = c.normalized * maxDistance;
            }

            Vector2 lookValue = lookAction.ReadValue<Vector2>() * Time.deltaTime;
            mX += lookValue.x * sensitivityX;
            float my = -lookValue.y * sensitivityY;
            if (0 <= mY + my && mY + my <= 50) mY += my;
            transform.eulerAngles = new Vector3(mY, mX, 0);
        }
        if (Input.GetKeyDown(KeyCode.Tab) && Time.timeScale != 0.0f) {
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
    private void OnSensitivityChanged() {
        sensitivityX = Mathf.Lerp(1, 20, GameState.sensitivityLookX);
        sensitivityY = Mathf.Lerp(1, 20, GameState.sensitivityLookY);
    }
    private void OnDestroy() => GameState.Unsubscribe(OnSensitivityChanged, nameof(GameState.sensitivityLookX), nameof(GameState.sensitivityLookY));
}