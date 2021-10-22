using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StampController : DragDrop
{
    private bool stamped;
    private BoxCollider2D col;

    void Start()
    {
        stamped = false;
        col = GetComponent<BoxCollider2D>();
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
        Collider2D other = Physics2D.OverlapBox(transform.position, Vector2.Scale(col.size, transform.localScale), 0, LayerMask.GetMask("Postcards"));
        if(other == null) return false;

        Debug.Log("Stamped!");
        stamped = true;
        SetDragEnabled(false);
        transform.parent = other.transform;
        return true;
    }
}
