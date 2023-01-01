using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Ghost[] ghosts;
    public GameObject[] fruits;
    public Pacman pacman;
    

    public Transform pellets;
    
    public int score { get; private set; }
    public int lives { get; private set; }

    public int ghostMultiplayer { get; private set; }
    private Animator pacmanAnim;
    public GameObject[] audioClips;
    [SerializeField]
    private TextMeshProUGUI scoreUI;
    [SerializeField]
    private GameObject gameOver;
    [SerializeField]
    private GameObject[] livesUI;
    private void Start()
    {
        NewGame();
        pacmanAnim = pacman.GetComponent<Animator>();
        

    }

    private void NewGame()
    {
        SetScore(0);
        SetLives(3);
        NewRound();
        gameOver.gameObject.SetActive(false);
        livesUI[0].gameObject.SetActive(true);
        livesUI[1].gameObject.SetActive(true);
        livesUI[2].gameObject.SetActive(true);
    }

    private void NewRound()
    {
        foreach(Transform pellet in pellets)
        {
            pellet.gameObject.SetActive(true);
        }
        ResetState();
        Invoke(nameof(FruitSpawn),1f);
        
    }

    private void ResetState()
    {
        ResetGhostMultiplier();
        for (int i = 0; i < ghosts.Length; i++)
        {
            ghosts[i].ResetState();
        }

        pacman.ResetState();
        
    }

    public void GameOver()
    {
        for (int i = 0; i < ghosts.Length; i++)
        {
            ghosts[i].gameObject.SetActive(false);
        }
        Pellet[] pelletsInScene = GameObject.FindObjectsOfType<Pellet>();
        for (int i = 0; i < pelletsInScene.Length ; i++)
        {
            pelletsInScene[i].gameObject.SetActive(false);
        }

        pacman.gameObject.SetActive(false);
        gameOver.gameObject.SetActive(true);
    }
    private void SetScore(int score)
    {
        this.score = score;
        scoreUI.text = score.ToString();
    }

    private void SetLives(int lives)
    {
        this.lives = lives;
    }

    public void GhostEaten(Ghost ghost)
    {
        Instantiate(audioClips[1]);
        int points = ghost.points * ghostMultiplayer;
        SetScore(score + ghost.points);
        ghostMultiplayer++;
    }

    public void PacmanEaten()
    {
        pacmanAnim.Play("PacmanDeath");
        SetLives(lives - 1);
        if(lives == 2)
        {
            livesUI[2].gameObject.SetActive(false);
        }
        if (lives == 1)
        {
            livesUI[1].gameObject.SetActive(false);
        }
        if (lives == 0)
        {
            livesUI[0].gameObject.SetActive(false);
        }
        if (lives > 0)
        {
            Invoke(nameof(ResetState), 3.0f);

        }
        
    }

    private void Update()
    {
        if (lives <= 0 && Input.anyKeyDown)
        {
            NewGame();
        }
    }

    public void PelletEaten(Pellet pellet)
    {
        pellet.gameObject.SetActive(false);
        Instantiate(audioClips[0]);
        SetScore(score + pellet.scorePoints);
        if(!HasRemainingPellets())
        {
            pacman.gameObject.SetActive(false);
            foreach (Ghost item in ghosts)
            {
                item.gameObject.SetActive(false);
            }
            Invoke(nameof(NewRound), 3.0f);
        }
    }

    public void PowerPelletEaten(PowerPellet pellet)
    {
        audioClips[2].SetActive(true);
        Instantiate(audioClips[0]);
        for (int i = 0; i < ghosts.Length; i++)
        {
            ghosts[i].escape.Enable(pellet.duration);
        }
        PelletEaten(pellet);
        CancelInvoke();
        Invoke(nameof(ResetGhostMultiplier), pellet.duration);
        StartCoroutine(DisableAudio(audioClips[2], pellet.duration));


    }
    private bool HasRemainingPellets()
    {
        foreach(Transform pellet in pellets)
        {
            if (pellet.gameObject.activeSelf)
            {
                return true;
            }
        }
        return false;
    }

    private void ResetGhostMultiplier()
    {
        ghostMultiplayer = 1;
    }

    private void DisableAudio(GameObject go)
    {
        go.SetActive(false);
    }
    
    IEnumerator DisableAudio(GameObject gO,float duration)
    {
        yield return new WaitForSeconds(duration);
        gO.SetActive(false);
    }

    public void FruitSpawn()
    {
        Pellet[] pelletsInScene = GameObject.FindObjectsOfType<Pellet>();
        int pelletIndex = Random.Range(0, pelletsInScene.Length);
        pelletsInScene[pelletIndex].gameObject.SetActive(false);
        Instantiate(fruits[0], pelletsInScene[pelletIndex].transform.position,Quaternion.identity);
    }
}
