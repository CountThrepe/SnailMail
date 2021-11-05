using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostcardDeckController : DeckController
{
    public GameObject[] tempCards;

    protected override void Init()
    {
        BalancedInit();
    }

    private void BalancedInit() {
        for(int i = 0; i < nItems; i++) {
            int newCardNum = i % tempCards.Length;
            GameObject newPostcard = Instantiate(tempCards[newCardNum], offscreenPoint, Quaternion.identity);
            newPostcard.GetComponent<PostcardController>().SetIndex(i);
            newPostcard.SetActive(false);
            items[i] = newPostcard;
            deck.Add(i);
        }
        deck = Shuffle(deck);
    }
}
