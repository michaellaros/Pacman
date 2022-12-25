
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    public float speed = 8.0f;
    public float speedMultiplier = 1.0f;

    public Vector2 initialDirection;

    public LayerMask obstacleLayer;
    public Rigidbody2D rigidBody { get; private set; }

    public Vector2 direction { get; private set; }

    public Vector2 nextDirection { get; private set; }

    public Vector3 startingPosition { get; private set; }
    private void Awake()
    {
        this.rigidBody = GetComponent<Rigidbody2D>();
        this.startingPosition = this.transform.position;
    }

    private void Start()
    {
        ResetState();
    }

    public void ResetState()
    {
        this.speedMultiplier = 1.0f;
        this.direction = this.initialDirection;
        this.nextDirection = Vector2.zero;
        this.transform.position = startingPosition;
        this.rigidBody.isKinematic = false;
        this.enabled = true;
    }

    private void FixedUpdate()
    {
        Vector2 position = rigidBody.position;
        Vector2 translation = direction * speed * speedMultiplier * Time.fixedDeltaTime;
        rigidBody.MovePosition(position + translation);
    }

    private void Update()
    {
        if(this.nextDirection != Vector2.zero)
        {
            SetDirection(nextDirection);
        }
    }
    public void SetDirection(Vector2 direction, bool forced = false)
    {
        if( forced || !Occupied(direction))
        {
            this.direction = direction;
            nextDirection = Vector2.zero;
        }
        else
        {
            nextDirection = direction;
        }
    }

    public bool Occupied(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, Vector2.one * 0.75f, 0.0f, direction, 1.5f,obstacleLayer);
        return hit.collider != null;

    }


}
