using UnityEngine;

public class MeleeDefender : Defender
{
    protected override float AttackRange => 0.75f;
    protected override void Attack()
    {
        Debug.Log("melee attack");
    }
}