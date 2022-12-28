using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostSpawn : GhostBehaviour
{
    public Transform insideSpawn;
    public Transform outsiteSpawn;

    private void OnEnable()
    {
        StopAllCoroutines();
    }
    private void OnDisable()
    {
        if (gameObject.activeInHierarchy)
        {
            StartCoroutine(ExitTransition());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (enabled && collision.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            ghost.movement.SetDirection(-ghost.movement.direction);
        }
    }
    private IEnumerator ExitTransition()
    {
        ghost.movement.SetDirection(Vector2.up, true);
        ghost.movement.rigidbody.isKinematic = true;
        ghost.movement.enabled = false;

        Vector3 position = transform.position;

        float duration = 0.5f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            Vector3 newPos = Vector3.Lerp(position,insideSpawn.position,elapsed/duration);
            newPos.z = position.z;
            this.ghost.transform.position = newPos;
            elapsed += Time.deltaTime;
            yield return null;
        }

        elapsed = 0.0f;

        while (elapsed < duration)
        {
            Vector3 newPos = Vector3.Lerp(insideSpawn.position,outsiteSpawn.position, elapsed / duration);
            newPos.z = position.z;
            ghost.transform.position = newPos;
            elapsed += Time.deltaTime;
            yield return null;
        }
        ghost.movement.SetDirection(new Vector2(Random.value < 0.5f ? -1.0f : 1.0f, 0.0f), true);
        ghost.movement.rigidbody.isKinematic = false;
        ghost.movement.enabled = true;
    }

    
}
