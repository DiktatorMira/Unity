namespace Assets.Scripts {
    class GameState {
        public static ModalScript modalScript;
        public static bool isLevelCompleted = false;
        public static void Pause() => modalScript.ShowModal(true, "Победа", "Вы победили!", "Начать заново");
    }
}