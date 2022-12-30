using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Ghost[] ghosts;

    public Pacman pacman;

    public Transform pellets;

    public int score { get; private set; }
    public int lives { get; private set; }

    public int ghostMultiplayer { get; private set; }

    private void Start()
    {
        NewGame();
    }

    private void NewGame()
    {
        SetScore(0);
        SetLives(3);
        NewRound();

    }

    private void NewRound()
    {
        foreach(Transform pellet in this.pellets)
        {
            pellet.gameObject.SetActive(true);
        }
        ResetState();
        
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

    private void GameOver()
    {
        for (int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].gameObject.SetActive(false);
        }

        pacman.gameObject.SetActive(false);
    }
    private void SetScore(int score)
    {
        this.score = score;
    }

    private void SetLives(int lives)
    {
        this.lives = lives;
    }

    public void GhostEaten(Ghost ghost)
    {
        int points = ghost.points * ghostMultiplayer;
        SetScore(score + ghost.points);
        ghostMultiplayer++;
    }

    public void PacmanEaten()
    {
        pacman.gameObject.SetActive(false);

        SetLives(lives - 1);

        if(lives > 0)
        {
            Invoke(nameof(ResetState), 3.0f);

        }
        else
        {
            GameOver();
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
        for (int i = 0; i < ghosts.Length; i++)
        {
            ghosts[i].escape.Enable(pellet.duration);
        }
        PelletEaten(pellet);
        CancelInvoke();
        Invoke(nameof(ResetGhostMultiplier), pellet.duration);

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
}
