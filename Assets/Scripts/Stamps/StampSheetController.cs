using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StampSheetController : DeckController
{
    public GameObject[] tempStamps;

    protected override void Init() {
        RandomInit();
    }

    private void RandomInit() {
        for(int i = 0; i < nItems; i++) {
            int newStampNum = Random.Range(0, tempStamps.Length);
            GameObject newStamp = Instantiate(tempStamps[newStampNum], offscreenPoint, Quaternion.identity);
            newStamp.GetComponent<StampController>().SetIndex(i);
            newStamp.SetActive(false);
            items[i] = newStamp;
            deck.Add(i);
        }
        deck = Shuffle(deck);
    }
}
