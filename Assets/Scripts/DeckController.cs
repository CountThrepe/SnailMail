using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DeckController : MonoBehaviour
{
    public Vector2 offscreenPoint, drawStart, drawStop;
    public int nItems;

    protected GameObject[] items;
    protected List<int> deck, active, discard;

    void Awake() {
        items = new GameObject[nItems];
        deck = new List<int>();
        active = new List<int>();
        discard = new List<int>();

        Init();
    }

    protected abstract void Init();

    public void Draw(int n = -1) {
        // Discard items still active
        Discard();

        if(n == -1) n = deck.Count;
        if(n > deck.Count) {
            RefillDeck();
            if(n > deck.Count) n = deck.Count;
        }

        Debug.Log(string.Format("Drawing {0} items from the deck! {1} items remaining!", n, deck.Count - n));

        Vector2[] spawnPoints = GetSpacing(n);
        foreach(Vector2 point in spawnPoints) {
            int itemIndex = deck[0];
            items[itemIndex].transform.position = point;
            items[itemIndex].SetActive(true);
            active.Add(itemIndex);
            deck.RemoveAt(0);
        }
    }
    public void Discard() {
        if(active.Count == 0) return;

        Debug.Log(string.Format("Discarding {0} items...", active.Count));
        foreach(int i in active) {
            items[i].transform.position = offscreenPoint;
            items[i].SetActive(false);
        }

        discard.AddRange(active);
        active.Clear();
    }

    public void RefillDeck() {
        Debug.Log(string.Format("Reshuffling {0} items from discard to deck...", discard.Count));
        discard = Shuffle(discard);
        deck.AddRange(discard);
        discard.Clear();
    }

    public void UntrackItem(int index) {
        active.Remove(index);
        Debug.Log(active.Count);
    }

    public void DiscardItem(int index) {
        if(active.Remove(index)) {
            discard.Add(index);
            items[index].transform.position = offscreenPoint;
            items[index].SetActive(false);
        } else Debug.Log("Attempted to discard item but item was not in active list.");
    }

    protected Vector2[] GetSpacing(int n) {
        if(n <= 0) return new Vector2[0];

        List<Vector2> points = new List<Vector2>();
        if(n == 1) points.Add(drawStart);
        else {
            float spacing = 1f / (n - 1);
            for(int i = 0; i < n; i++) {
                points.Add(Vector2.Lerp(drawStart, drawStop, i * spacing));
            }
        }

        return points.ToArray();
    }

    protected List<int> Shuffle(List<int> l) {
        for (int i = l.Count - 1; i > 0; i--) {
            int swapIndex = Random.Range(0, i + 1);
            int tmp = l[i];
            l[i] = l[swapIndex];
            l[swapIndex] = tmp;
        }
        return l;
    }

}
