using UnityEngine;
using Pathfinding;

public class EnemyGFX : MonoBehaviour
{
    public AIPath aiPath;

    void Update()
    {
        if (aiPath.desiredVelocity.x >= .01f)
        {
            transform.localScale = new Vector3(3, 3, 3);
        }
        else if (aiPath.desiredVelocity.x <= -.01f)
        {
            transform.localScale = new Vector3(-3, 3, 3);
        }
    }
}
