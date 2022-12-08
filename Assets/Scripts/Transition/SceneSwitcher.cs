using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prime31.TransitionKit;
using UnityEngine.SceneManagement;

public static class SceneSwitcher {
    public static void ChangeScene() {
        int sceneNumber = GetSceneNumber(SceneManager.GetActiveScene().buildIndex);
        ChangeScene(sceneNumber);
    }

    public static void ChangeScene(int sceneNumber) {
        var transition = new FishEyeTransition() {
            nextScene = sceneNumber,
            duration = 2.0f,
            size = 0.2f,
            zoom = 100.0f,
            colorSeparation = 0.1f
        };
        TransitionKit.instance.transitionWithDelegate( transition );
    }

    public static void ChangeGameoverScene() {
        var transition = new FadeTransition() {
            nextScene = 4,
            fadedDelay = 0.2f,
            fadeToColor = Color.black
        };
        TransitionKit.instance.transitionWithDelegate( transition );
    }

    private static int GetSceneNumber(int sceneNumber) {
        if (sceneNumber < 3) {
            return sceneNumber + 1;
        }
        return sceneNumber - 1;
    }
}
