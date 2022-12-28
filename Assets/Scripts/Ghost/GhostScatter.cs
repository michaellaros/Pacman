using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostScatter : GhostBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Node node = collision.GetComponent<Node>();

        if(node != null && this.enabled && !this.ghost.escape.enabled)
        {
            int index = Random.Range(0,node.avaibleDirection.Count);

            if(node.avaibleDirection[index] == -this.ghost.movement.direction && node.avaibleDirection.Count > 1)
            {
                index++;

                if(index >= node.avaibleDirection.Count)
                {
                    index = 0;
                }
            }

            this.ghost.movement.SetDirection(node.avaibleDirection[index]);
        }
    }

    private void OnDisable()
    {
        this.ghost.chase.Enable();
    }
}
