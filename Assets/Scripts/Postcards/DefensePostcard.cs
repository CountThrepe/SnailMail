using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefensePostcard : PostcardController
{
    public int baseDefense;

    protected override CreatureController GetTarget() {
        Collider2D other = Physics2D.OverlapBox(transform.position, Vector2.Scale(GetComponent<BoxCollider2D>().size / 2, transform.localScale), 0, LayerMask.GetMask("Creatures"));
        if(other == null || other.tag != "Player") return null;
        return other.gameObject.GetComponent<CreatureController>();
    }

    protected override void ApplyEffect(CreatureController target)
    {
        int defense = baseDefense;
        foreach(StampController stamp in stamps) {
            defense = stamp.ApplyEffect(target, defense);
        }
        target.IncreaseDefense(defense);
    }
}
