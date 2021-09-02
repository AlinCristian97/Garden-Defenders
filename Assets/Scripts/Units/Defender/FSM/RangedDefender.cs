using UnityEngine;

public class RangedDefender : Defender
{
    protected override float AttackRange
    {
        get
        {
            if (Camera.main != null)
            {
                return Camera.main.orthographicSize * 2 - Tile.transform.position.x - Tile.transform.localScale.x;
            }

            Debug.LogWarning("There is no camera tagged as 'main'");
            return 0f;
        }
    }


    public override void Attack()
    {
        Instantiate(Projectile, GetColliderSideBoundCenterPoint(), Quaternion.identity);
        
        Debug.Log("ranged attack");
    }
}