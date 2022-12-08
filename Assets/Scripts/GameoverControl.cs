using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameoverControl : MonoBehaviour {
    void Update() {
        if (Input.anyKey) {
            SceneSwitcher.ChangeScene(1);
        }
    }
}
