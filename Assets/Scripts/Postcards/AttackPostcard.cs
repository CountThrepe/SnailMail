using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPostcard : PostcardController
{
    public int baseDamage;

    protected override CreatureController GetTarget() {
        Collider2D other = Physics2D.OverlapBox(transform.position, Vector2.Scale(GetComponent<BoxCollider2D>().size / 2, transform.localScale), 0, LayerMask.GetMask("Creatures"));
        Debug.Log(other.tag);
        if(other == null || other.tag != "Enemy") return null;
        return other.gameObject.GetComponent<CreatureController>();
    }

    protected override void ApplyEffect(CreatureController target)
    {
        int damage = baseDamage;
        foreach(StampController stamp in stamps) {
            damage = stamp.ApplyEffect(target, damage);
        }
        target.Damage(damage);
    }
}
