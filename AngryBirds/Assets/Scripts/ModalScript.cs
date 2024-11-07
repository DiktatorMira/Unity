using UnityEngine;
using UnityEngine.SceneManagement;

public class ModalScript : MonoBehaviour {
    [SerializeField]
    private GameObject content;
    public static bool isMenu;

    void Start() {
        isMenu = false;
        Time.timeScale = 1.0f;
    }
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            isMenu = !isMenu;
            content.SetActive(isMenu);
            Time.timeScale = isMenu ? 0.0f : 1.0f;
        }
        if (Input.GetKeyDown(KeyCode.R)) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}