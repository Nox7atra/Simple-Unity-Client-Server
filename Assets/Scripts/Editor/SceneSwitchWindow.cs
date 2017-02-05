using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;

/// <summary>
/// SceneSwitchWindow class.
/// </summary>
public class SceneSwitchWindow : EditorWindow
{

    /// <summary>
    /// Tracks scroll position.
    /// </summary>
    private Vector2 scrollPos;
    /// <summary>
    /// Initialize window state.
    /// </summary>
    [MenuItem("Tools/Scene Switch Window")]
    internal static void Init()
    {
        // EditorWindow.GetWindow() will return the open instance of the specified window or create a new
        // instance if it can't find one. The second parameter is a flag for creating the window as a
        // Utility window; Utility windows cannot be docked like the Scene and Game view windows.
        var window = (SceneSwitchWindow)GetWindow(typeof(SceneSwitchWindow), false, "Scene Switch Window");
        window.position = new Rect(window.position.xMin + 100f, window.position.yMin + 100f, 200f, 400f);
    }

    /// <summary>
    /// Called on GUI events.
    /// </summary>
    internal void OnGUI()
    {
        EditorGUILayout.BeginVertical();
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);


        GUILayout.Label("Scenes", EditorStyles.boldLabel);
        for (var i = 0; i < EditorBuildSettings.scenes.Length; i++)
        {
            var scene = EditorBuildSettings.scenes[i];
            var sceneName = Path.GetFileNameWithoutExtension(scene.path);
            var chooseScene = GUILayout.Button(i + ": " + sceneName);
            if (chooseScene)
            {
                if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                {
                    EditorSceneManager.OpenScene(scene.path);
                }
            }
        }


        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndVertical();
    }
}
