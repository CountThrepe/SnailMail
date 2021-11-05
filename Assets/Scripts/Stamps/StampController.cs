using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StampController : DragDrop
{
    private bool stamped;

    void Start()
    {
        stamped = false;
    }

    new void Update()
    {
        base.Update();
    }

    // If stamped, reroute click events to postcard parent
    new void OnMouseDown() {
        if(!stamped) base.OnMouseDown();
        else transform.parent.GetComponent<PostcardController>().OnMouseDown();
    }

    new void OnMouseUp() {
        if(!stamped) base.OnMouseUp();
        else transform.parent.GetComponent<PostcardController>().OnMouseUp();
    }

    protected override bool OnDrop() {
        Collider2D other = Physics2D.OverlapBox(transform.position, Vector2.Scale(GetComponent<BoxCollider2D>().size, transform.localScale), 0, LayerMask.GetMask("Postcards"));
        if(other == null) return false;

        Debug.Log("Stamped!");
        stamped = true;
        SetDragEnabled(false);
        LevelManager.GetInstance().GetSheetController().UntrackItem(index);
        other.gameObject.GetComponent<PostcardController>().AddStamp(gameObject);
        return true;
    }

    public abstract int ApplyEffect(CreatureController target, int effectVal);
}
