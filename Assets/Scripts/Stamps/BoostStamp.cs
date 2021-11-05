using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostStamp : StampController
{
    public int boostAmount = 2;
    public override int ApplyEffect(CreatureController target, int effectVal) {
        return effectVal + boostAmount;
    }
}
