using UnityEngine;
using System.Collections.Generic;

public class LightScript : MonoBehaviour {
    [SerializeField] private Material daySkybox, nightSkybox;
    private List<Light> nightLights, dayLights;
    private bool isNight;

    void Start() {
        nightLights = new List<Light>();
        dayLights = new List<Light>();
        foreach (var gameObject in GameObject.FindGameObjectsWithTag("NightLight")) nightLights.Add(gameObject.GetComponent<Light>());
        foreach (var gameObject in GameObject.FindGameObjectsWithTag("DayLight")) dayLights.Add(gameObject.GetComponent<Light>());
        isNight = nightLights[0].isActiveAndEnabled;
    }
    void Update() {
        if (Input.GetKeyDown(KeyCode.N)) {
            isNight = !isNight;
            RenderSettings.skybox = isNight ? nightSkybox : daySkybox;
            DynamicGI.UpdateEnvironment();
            foreach (var nightLight in nightLights) nightLight.enabled = isNight;
            foreach (var dayLight in dayLights) dayLight.enabled = !isNight;
        }
    }
}