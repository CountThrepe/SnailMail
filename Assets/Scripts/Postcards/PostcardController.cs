using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostcardController : DragDrop
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
    }

    protected override bool OnDrop() {
        return true;
    }
}
