using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public Text scoreRedTeam;
    public Text scoreBlueTeam;
    public GameObject winScreen;

    private int scoreBlueTeamInt;
    private int scoreRedTeamInt;

    public void setScore(string team, int pointToAdd)
    {
        if (team == "blue")
            scoreBlueTeamInt += pointToAdd;
        else if (team == "red")
            scoreRedTeamInt += pointToAdd;


    }

	// Use this for initialization
	void Start () {
        scoreBlueTeamInt = 0;
        scoreRedTeamInt = 0;
    }
	
	// Update is called once per frame
	void Update () {
        scoreBlueTeam.text = scoreBlueTeamInt.ToString();
        scoreRedTeam.text = scoreRedTeamInt.ToString();
    }
}
