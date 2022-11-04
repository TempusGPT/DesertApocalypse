using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct RoomInfo {
    public int roomID;
    public int[] enemyID;
}

[CreateAssetMenu(menuName = "ScriptableObjects/LevelTemplate")]
public class SOLevelInfo : ScriptableObject {
    public int level = 0;
    public RoomInfo[] roomInformation = null;
}
