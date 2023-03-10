using UnityEngine;

public class Pacman : MonoBehaviour
{
    [HideInInspector]
    public GameObject deathSound;
    public Movement movement { get; private set; }
    [SerializeField]
    private GameManager gameManager;
    private void Awake()
    {
        movement = GetComponent<Movement>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            movement.SetDirection(Vector2.up);
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            movement.SetDirection(Vector2.down);
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            movement.SetDirection(Vector2.left);
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            movement.SetDirection(Vector2.right);
        }

        float angle = Mathf.Atan2(movement.direction.y, movement.direction.x);
        transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);
    }

    public void ResetState()
    {

        movement.ResetState();
        gameObject.SetActive(true);
    }

    public void PlayDeath()
    {
        Instantiate(deathSound);
    }
    public void DisablePacman()
    {
        gameObject.SetActive(false);
        if (gameManager.GetComponent<GameManager>().lives <= 0)
        {
            gameManager.GetComponent<GameManager>().GameOver();

        }
    }
}
