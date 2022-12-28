using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostChase : GhostBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Node node = collision.GetComponent<Node>();

        if (node != null && this.enabled && !this.ghost.escape.enabled)
        {
            Vector2 direction = Vector2.zero;
            float minDistance = float.MaxValue;

            foreach(Vector2 avaibleDirection in node.avaibleDirection)
            {
                Vector3 newPos = transform.position + new Vector3(avaibleDirection.x, avaibleDirection.y, 0f);
                float distance = (ghost.target.position - newPos).sqrMagnitude;
                if(distance<minDistance)
                {
                    direction = avaibleDirection;
                    minDistance = distance;
                }
            }
            ghost.movement.SetDirection(direction);
        }

    }

    private void OnDisable()
    {
        ghost.scatter.Enable();
    }
}
