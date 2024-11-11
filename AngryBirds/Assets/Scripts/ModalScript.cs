using Assets.Scripts;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ModalScript : MonoBehaviour {
    [SerializeField]
    private GameObject content;
    [SerializeField]
    private TMPro.TextMeshProUGUI title_tmp;
    [SerializeField]
    private TMPro.TextMeshProUGUI messageTMP;
    [SerializeField]
    private TMPro.TextMeshProUGUI buttonLabelTMP;
    public static bool isMenu;

    void Start() {
        ShowModal(content.activeInHierarchy);
        GameState.modalScript = this;
    }
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) ShowModal(!content.activeInHierarchy);
        if (Input.GetKeyDown(KeyCode.R)) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ShowModal(bool isShown, string title = "Пауза", string message = "Для продолжения нажмине кнопку 'Продолжить' либо ESC", string buttonText = "Продолжить") {
        if (isShown) {
            Time.timeScale = 0f;
            content.SetActive(true);
            title_tmp.text = title;
            messageTMP.text = message;
            buttonLabelTMP.text = buttonText;
            isMenu = true;
        } else {
            Time.timeScale = 1.0f;
            content.SetActive(false);
            isMenu = false;
        }
    }
    public void OnGoButtonClick() {
        if (GameState.isLevelCompleted) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        else ShowModal(false);
    }
    public void OnExitButtonClick() {
        #if UNITY_STANDALONE
                Application.Quit();
        #endif
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}