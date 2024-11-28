using System;
using System.Collections.Generic;

public class GameState {
    public static Dictionary<string, bool> collectedKeys { get; } = new();
    private static readonly Dictionary<string, List<Action>> subscribers = new();
    private static Dictionary<string, List<Action<string, object>>> eventListeners = new();
    public static bool isFpv { get; set; }
    public static bool isNight { get; set; }

    private static float effectsvolume = 1.0f, ambientvolume = 1.0f, sensitivityx = 3.5f, sensitivityy = 3.5f;
    private static bool ismuted = false;
    public static float effectsVolume {
        get => effectsvolume;
        set {
            if (effectsvolume != value) {
                effectsvolume = value;
                Notify(nameof(effectsVolume));
            }
        }
    }
    public static float ambientVolume {
        get => ambientvolume;
        set {
            if (ambientvolume != value) {
                ambientvolume = value;
                Notify(nameof(ambientVolume));
            }
        }
    }
    public static bool isMuted {
        get => ismuted;
        set {
            if (ismuted != value) {
                ismuted = value;
                Notify(nameof(isMuted));
                Notify(nameof(effectsVolume));
            }
        }
    }
    public static float sensitivityLookX {
        get => sensitivityx;
        set {
            if (sensitivityx != value) {
                sensitivityx = value;
                Notify(nameof(sensitivityLookX));
            }
        }
    }
    public static float sensitivityLookY {
        get => sensitivityy;
        set {
            if (sensitivityy != value)  {
                sensitivityy = value;
                Notify(nameof(sensitivityLookY));
            }
        }
    }

    public static void TriggerKeyEvent(string keyName, bool isInTime) {
        var payload = new Dictionary<string, object> {
            { "KeyName", keyName },
            { "IsInTime", isInTime }
        };
        TriggerEvent("KeyCollected", payload);
    }
    public static void TriggerEvent(string type, object payload = null) {
        if (eventListeners.ContainsKey(type)) {
            foreach (var eventListener in eventListeners[type]) eventListener(type, payload);
        } if (eventListeners.ContainsKey("Broadcast")) {
            foreach (var eventListener in eventListeners["Broadcast"]) eventListener(type, payload);
        }
    }
    public static void SubscribeTrigger(Action<string, object> action, params string[] types) {
        if (types.Length == 0) types = new string[1] { "Broadcast" };
        foreach (var type in types) {
            if (!eventListeners.ContainsKey(type)) eventListeners[type] = new List<Action<string, object>>();
            eventListeners[type].Add(action);
        }
    }
    public static void UnsubscribeTrigger(Action<string, object> action, params string[] types) {
        if (types.Length == 0) types = new string[1] { "Broadcast" };
        foreach (var type in types) {
            if (!eventListeners.ContainsKey(type)) {
                eventListeners[type].Remove(action);
                if (eventListeners[type].Count == 0) eventListeners.Remove(type);
            }
        }
    }
    private static void Notify(string propertyName) {
        if (subscribers.ContainsKey(propertyName)) subscribers[propertyName].ForEach(action => action());
    }
    public static void Subscribe(Action action, params string[] propertyNames) {
        if (propertyNames.Length == 0) throw new ArgumentException($"{ nameof(propertyNames) } must have at least 1 value");
        foreach (var propertyName in propertyNames) {
            if (!subscribers.ContainsKey(propertyName)) subscribers[propertyName] = new List<Action>();
            subscribers[propertyName].Add(action);
        }
    }
    public static void Unsubscribe(Action action, params string[] propertyNames) {
        if (propertyNames.Length == 0) throw new ArgumentException($"{nameof(propertyNames)} must have at least 1 value");
        foreach (var propertyName in propertyNames) {
            if (subscribers.ContainsKey(propertyName)) subscribers[propertyName].Remove(action);
        }
    }
}