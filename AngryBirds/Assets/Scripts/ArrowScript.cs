using UnityEngine;

public class ArrowScript : MonoBehaviour {
    [SerializeField]
    private Transform rotAnchor;
    private float minRotAngle = -50.0f, maxRotAngle = 70.0f;

    void Update() {
        float dy = Input.GetAxis("Vertical"), curRotAngle = transform.eulerAngles.z, rotAngle = Mathf.Clamp(curRotAngle + dy, minRotAngle, maxRotAngle);

        if (curRotAngle > 180) curRotAngle -= 360;
        if (curRotAngle + dy > minRotAngle && curRotAngle + dy < maxRotAngle) transform.RotateAround(rotAnchor.position, Vector3.forward, dy);
    }
}