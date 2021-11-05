using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PostcardController : DragDrop
{
    protected List<StampController> stamps;

    void Start() {
        stamps = new List<StampController>();
    }

    public void AddStamp(GameObject stamp) {
        stamp.transform.parent = transform;
        stamps.Add(stamp.GetComponent<StampController>());
    }

    protected override bool OnDrop() {
        CreatureController target = GetTarget();
        if(target == null) return false;

        ApplyEffect(target);
        LevelManager.GetInstance().GetPostcardDeck().DiscardItem(index);
        return true;
    }

    protected abstract CreatureController GetTarget();

    protected abstract void ApplyEffect(CreatureController target);
}
