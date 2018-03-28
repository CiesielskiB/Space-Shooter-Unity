using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{

    public static int score = 0;
    public int health;
    public Text scoreText;
    public Text healthText;
    private PlayerController playerController;
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        health = playerController.health;
        scoreText.text = "score: " + score.ToString();
        healthText.text = "health: " + health + "/" + health;
    }

    public void AddScore(int points)
    {
        score += points;
        scoreText.text = "score: " + score.ToString();
    }

    public void AdjustHealth(int hp, int maxHp)
    {
        health = hp;
        healthText.text = "health: " + health + "/" + maxHp;
    }
    public static void ResetPoints()
    {
        score = 0;
        
    }


}
