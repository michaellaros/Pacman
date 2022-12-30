using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public List<Vector2> avaibleDirection { get; private set; }
    public LayerMask obstacleLayer;

    public void Start()
    {
        this.avaibleDirection = new List<Vector2>();
        CheckAvaibleDirection(Vector2.up);
        CheckAvaibleDirection(Vector2.down);
        CheckAvaibleDirection(Vector2.right);
        CheckAvaibleDirection(Vector2.left);
    }

    private void CheckAvaibleDirection(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, Vector2.one * 0.5f, 0f, direction, 1.0f, obstacleLayer);
        if (hit.collider == null)
        {
            this.avaibleDirection.Add(direction);
        }
    }
}
