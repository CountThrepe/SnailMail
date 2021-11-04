using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public int nDraw = 2;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("space")) {
            LevelManager.GetInstance().GetSheetController().Draw(nDraw);
        }
    }
}
