using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CreatureController : MonoBehaviour
{
    public int hp;
    private int defense;
    // Start is called before the first frame update
    void Start()
    {
        defense = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncreaseDefense(int val) {
        defense += val;
        Debug.Log(string.Format("Defense increased by {0} to {1}", val, defense));
    }

    public void ResetDefense() {
        Debug.Log("Reset defense");
        defense = 0;
    }

    public void Damage(int dmg) {
        if(defense >= dmg) return;
        dmg -= defense;
        Debug.Log(string.Format("Oww! Hit for {0} damage", dmg));
        hp -= dmg;
        if(hp <= 0) Die();
    }

    protected void Die() {
        Debug.Log("Fly, you fools!");
        Destroy(gameObject);
    }
}
