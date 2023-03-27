using UnityEditor;
namespace KingdomOfNight
{
    public partial class SceneLoader
    {
#if UNITY_EDITOR
        [MenuItem("Scenes/Levels/Game")]
        public static void LoadGame() { OpenScene("Assets/scenes/Levels/Game.unity"); }
        [MenuItem("Scenes/Levels/GCGame")]
        public static void LoadGCGame() { OpenScene("Assets/scenes/Levels/GCGame.unity"); }
        [MenuItem("Scenes/Levels/GGame")]
        public static void LoadGGame() { OpenScene("Assets/scenes/Levels/GGame.unity"); }
        [MenuItem("Scenes/Levels/Level 2")]
        public static void LoadLevel2() { OpenScene("Assets/scenes/Levels/Level 2.unity"); }
        [MenuItem("Scenes/Levels/Level3")]
        public static void LoadLevel3() { OpenScene("Assets/scenes/Levels/Level3.unity"); }
        [MenuItem("Scenes/Levels/Level4")]
        public static void LoadLevel4() { OpenScene("Assets/scenes/Levels/Level4.unity"); }
        [MenuItem("Scenes/Levels/LevelsLobby")]
        public static void LoadLevelsLobby() { OpenScene("Assets/scenes/Levels/LevelsLobby.unity"); }
        [MenuItem("Scenes/Menu/Crédits")]
        public static void LoadCrédits() { OpenScene("Assets/scenes/Menu/Crédits.unity"); }
        [MenuItem("Scenes/Menu/Lobby")]
        public static void LoadLobby() { OpenScene("Assets/scenes/Menu/Lobby.unity"); }
        [MenuItem("Scenes/Menu/Menu")]
        public static void LoadMenu() { OpenScene("Assets/scenes/Menu/Menu.unity"); }
        [MenuItem("Scenes/Menu/Parametre")]
        public static void LoadParametre() { OpenScene("Assets/scenes/Menu/Parametre.unity"); }
#endif
    }
}