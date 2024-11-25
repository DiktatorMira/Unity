using System.Collections.Generic;

public class GameState {
    public static bool isFpv { get; set; }
    public static bool isNight { get; set; }
    public static float flashCharge { get; set; }
    public static float effectVolume { get; set; } = 1.0f;
    public static Dictionary<string, bool> collectedKeys { get; } = new Dictionary<string, bool>();
}