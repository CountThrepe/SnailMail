using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public CreatureController player, enemy;
    public int nStamps = 4;
    public int nPostcards = 2;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("space")) {
            LevelManager.GetInstance().GetSheetController().Draw(nStamps);
            LevelManager.GetInstance().GetPostcardDeck().Draw(nPostcards);
        }

        if(Input.GetKeyDown("a")) {
            player.Damage(2);
        }

        if(Input.GetKeyDown("d")) {
            player.IncreaseDefense(1);
        }

        if(Input.GetKeyDown("r")) {
            player.ResetDefense();
        }
    }
}
