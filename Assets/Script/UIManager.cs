﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public Text scoreRedTeam;
    public Text scoreBlueTeam;
    public GameObject winScreen;
    public GameObject[] balls;
    public Transform[] spawnPoint;
    private int scoreBlueTeamInt;
    private int scoreRedTeamInt;

    public int waitingTime;
    private int randomIndex;
    public int COUNTER_FRAME = 60;

    public GameObject collectiblePrefabs;

    public void setScore(int team, int pointToAdd)
    {
        if (team == 2)
            scoreBlueTeamInt += pointToAdd;
        else if (team == 1)
            scoreRedTeamInt += pointToAdd;


    }

    public void spawnBall(int leType)
    {
        int rng = UnityEngine.Random.Range(0, spawnPoint.Length);
        GameObject lCollectibleType = Instantiate(balls[leType], spawnPoint[rng].position, Quaternion.identity);
    }

	// Use this for initialization
	void Start () {
        scoreBlueTeamInt = 0;
        scoreRedTeamInt = 0;
        spawnBall(0);
        spawnBall(1);
        Time.timeScale = 1;
    }
	
	// Update is called once per frame
	void Update () {
        scoreBlueTeam.text = scoreBlueTeamInt.ToString();
        scoreRedTeam.text = scoreRedTeamInt.ToString();
        //spawnCollectible();
        if (scoreBlueTeamInt >= 5 || scoreRedTeamInt >= 5)
            win();
    }

    private void win()
    {
        Time.timeScale = 0;
        winScreen.SetActive(true);
    }

    void spawnCollectible()
    {
        GameObject lCollectibleType;

        if (waitingTime++ <= COUNTER_FRAME)
            return;

        waitingTime = 0;

        randomIndex = UnityEngine.Random.Range(1, 3);
        int rng = UnityEngine.Random.Range(0, spawnPoint.Length);
        lCollectibleType = Instantiate(collectiblePrefabs, spawnPoint[rng].position, Quaternion.identity);
        if (randomIndex == 1)
        {
            lCollectibleType.GetComponent<Collectible>().changeType("shield");
        }
        else if (randomIndex == 2)
        {
            lCollectibleType.GetComponent<Collectible>().changeType("Speed");
        }
        else if (randomIndex == 3)
        {
            lCollectibleType.GetComponent<Collectible>().changeType("Dash");
        }
    }
}
