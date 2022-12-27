
using System.Runtime.CompilerServices;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public int points = 200;

    public Movement movement { get; private set; }
    public GhostSpawn spawn { get; private set; }
    public GhostChase chase { get; private set; }
    public GhostScatter scatter { get; private set; }
    public GhostEscape escape { get; private set; }
    public GhostBehaviour initialBehaviour;
    public Transform target;
    private void Awake()
    {
        movement = GetComponent<Movement>();
        spawn = GetComponent<GhostSpawn>();
        chase = GetComponent<GhostChase>();
        scatter = GetComponent<GhostScatter>();
        escape = GetComponent<GhostEscape>();
    }

    private void Start()
    {
        ResetState();
    }

    public void ResetState()
    {
        gameObject.SetActive(true);
        movement.ResetState();

        escape.Disable();
        chase.Disable();
        scatter.Enable();

        if (spawn != initialBehaviour)
        {
            spawn.Disable();
        }
        if (initialBehaviour != null)
        {
            initialBehaviour.Enable();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            if(escape.enabled)
            {
                FindObjectOfType<GameManager>().GhostEaten(this);

            }
            else
            {
                FindObjectOfType<GameManager>().PacmanEaten();
            }
        }
    }
}



