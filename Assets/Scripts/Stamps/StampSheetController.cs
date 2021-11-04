using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StampSheetController : MonoBehaviour
{
    public GameObject[] tempStamps;
    public int nStamps;
    public Vector2 drawStart, drawStop;

    private GameObject[] stamps;
    private List<int> deck, active, discard;
    private float stampsOnSheet;
    private Vector2 spawnPoint, discardPoint;


    void Awake() {
        stamps = new GameObject[nStamps];
        deck = new List<int>();
        active = new List<int>();
        discard = new List<int>();

        spawnPoint = new Vector2(10, 6);
        discardPoint = new Vector3(10, -6);

        RandomInit();
    }

    private void RandomInit() {
        for(int i = 0; i < nStamps; i++) {
            int newStampNum = Random.Range(0, tempStamps.Length);
            GameObject newStamp = Instantiate(tempStamps[newStampNum], spawnPoint, Quaternion.identity);
            newStamp.GetComponent<StampController>().SetIndex(i);
            newStamp.SetActive(false);
            stamps[i] = newStamp;
            deck.Add(i);
        }
        deck = Shuffle(deck);
        deck.ToString();
    }

    public void Draw(int n = -1) {
        // Discard stamps still active (unstamped)
        Discard();

        if(n == -1) n = deck.Count;
        if(n > deck.Count) {
            RefillDeck();
            if(n > deck.Count) n = deck.Count;
        }

        Debug.Log(string.Format("Drawing {0} stamps from the deck! {1} cards remaining!", n, deck.Count - n));

        Vector2[] spawnPoints = GetSpacing(n);
        foreach(Vector2 point in spawnPoints) {
            int stampIndex = deck[0];
            stamps[stampIndex].transform.position = point;
            stamps[stampIndex].SetActive(true);
            active.Add(stampIndex);
            deck.RemoveAt(0);
        }
    }

    public void Discard() {
        if(active.Count == 0) return;

        Debug.Log(string.Format("Discarding {0} stamps...", active.Count));
        foreach(int stampIndex in active) {
            stamps[stampIndex].transform.position = discardPoint;
            stamps[stampIndex].SetActive(false);
        }

        discard.AddRange(active);
        active.Clear();
    }

    public void RefillDeck() {
        Debug.Log(string.Format("Reshuffling {0} stamps from discard to deck...", discard.Count));
        discard = Shuffle(discard);
        deck.AddRange(discard);
        discard.Clear();
    }

    public void UntrackStamp(int index) {
        active.Remove(index);
        Debug.Log(active.Count);
    }

    private Vector2[] GetSpacing(int n) {
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

    private List<int> Shuffle(List<int> l) {
        for (int i = l.Count - 1; i > 0; i--) {
            int swapIndex = Random.Range(0, i + 1);
            int tmp = l[i];
            l[i] = l[swapIndex];
            l[swapIndex] = tmp;
        }
        return l;
    }
}
