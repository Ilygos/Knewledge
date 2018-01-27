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

    public int randomIndex;
    public int waitingTime;
    public int COUNTER_FRAME = 60;

    public GameObject collectiblePrefabs;

    public void setScore(int team, int pointToAdd)
    {
        if (team == 2)
            scoreBlueTeamInt += pointToAdd;
        else if (team == 1)
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
        spawnCollectible();

    }

    void spawnCollectible()
    {
        GameObject lCollectibleType;

        if (waitingTime++ <= COUNTER_FRAME)
            return;

        waitingTime = 0;

        randomIndex = Random.Range(1, 3);
        Vector3 pos = new Vector3(Random.Range(Screen.width/6, Screen.width - Screen.width / 6), Random.Range(Screen.height / 6, Screen.height - Screen.height / 6));
        lCollectibleType = Instantiate(collectiblePrefabs, pos, Quaternion.identity);
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
