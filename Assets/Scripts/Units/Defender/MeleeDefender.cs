using UnityEngine;

public class MeleeDefender : CombatDefender
{
    protected override float AttackRange => 0.75f;

    protected override void Attack()
    {
        //TODO: Implement Attack logic
        Debug.Log("melee attack");
    }
}