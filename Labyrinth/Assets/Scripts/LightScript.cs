using UnityEngine;
using System.Collections.Generic;

public class LightScript : MonoBehaviour {
    [SerializeField] private Material daySkybox, nightSkybox;
    private List<Light> nightLights, dayLights;

    private void Start() {
        nightLights = new List<Light>();
        dayLights = new List<Light>();
        foreach (var gameObject in GameObject.FindGameObjectsWithTag("NightLight")) nightLights.Add(gameObject.GetComponent<Light>());
        foreach (var gameObject in GameObject.FindGameObjectsWithTag("DayLight")) dayLights.Add(gameObject.GetComponent<Light>());
        GameState.isNight = nightLights[0].isActiveAndEnabled;
    }
    private void Update() {
        if (Input.GetKeyDown(KeyCode.N)) {
            GameState.isNight = !GameState.isNight;
            RenderSettings.skybox = GameState.isNight ? nightSkybox : daySkybox;
            DynamicGI.UpdateEnvironment();
            foreach (var nightLight in nightLights) nightLight.enabled = GameState.isNight;
            foreach (var dayLight in dayLights) dayLight.enabled = !GameState.isNight;
        }
    }
}