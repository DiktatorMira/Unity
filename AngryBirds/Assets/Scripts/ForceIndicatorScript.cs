using UnityEngine;
using UnityEngine.UI;
public class ForceIndicatorScript : MonoBehaviour {
    public static float forceFactor;
    [SerializeField]
    private Image indicatorFg;
    [SerializeField]
    private float sensitivity = 0.01f;
    private Color startColor = Color.green, midColor = Color.yellow, endColor = Color.red;

    void Start() => forceFactor = indicatorFg.fillAmount;
    void Update() {
        float dx = Input.GetAxis("Horizontal");
        if (dx != 0 && !BirdScript.hasFlown) {
            dx *= sensitivity;
            if (0.1f <= indicatorFg.fillAmount + dx && indicatorFg.fillAmount + dx <= 1.0f) {
                forceFactor = indicatorFg.fillAmount += dx;
                if (indicatorFg.fillAmount < 0.5f) indicatorFg.color = Color.Lerp(startColor, midColor, indicatorFg.fillAmount * 2);
                else indicatorFg.color = Color.Lerp(midColor, endColor, (indicatorFg.fillAmount - 0.5f) * 2);
            }
        }
    }
}