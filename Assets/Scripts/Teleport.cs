using System.Collections;
using System.Collections.Generic;
using UnityEditor.MemoryProfiler;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField]
    private Transform teleportPosition;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Vector3 position = collision.transform.position;    
        position.x = teleportPosition.position.x;
        position.y = teleportPosition.position.y;

        collision.transform.position = position;

    }
}
