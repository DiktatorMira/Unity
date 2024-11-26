using System;
using System.Collections.Generic;

public class GameState {
    public static Dictionary<string, bool> collectedKeys { get; } = new Dictionary<string, bool>();
    private static readonly Dictionary<string, List<Action>> subscribers = new Dictionary<string, List<Action>>();
    public static bool isFpv { get; set; }
    public static bool isNight { get; set; }
    public static float flashCharge { get; set; }

    private static float effectsvolume = 1.0f, ambientvolume = 1.0f;
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
            }
        }
    }

    private static void Notify(string propertyName) {
        if (subscribers.ContainsKey(propertyName)) subscribers[propertyName].ForEach(action => action());
    }
    public static void Subscribe(string propertyName, Action action) {
        if (!subscribers.ContainsKey(propertyName)) subscribers[propertyName] = new List<Action>();
        subscribers[propertyName].Add(action);
    }
    public static void Unsubscribe(string propertyName, Action action) {
        if (subscribers.ContainsKey(propertyName)) subscribers[propertyName].Remove(action);
    }
}