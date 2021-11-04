using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public StampSheetController sheet;
    private static LevelManager self = null;

    void Awake() {
        self = GetComponent<LevelManager>();
    }

    public static LevelManager GetInstance() {
        return self;
    }

    public StampSheetController GetSheetController() {
        return sheet;
    }
}
