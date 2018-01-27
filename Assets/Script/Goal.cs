using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour {

    public int team;

	// Use this for initialization
	void Start () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && team == collision.GetComponent<CharacterController2D>().team)
        {
            Debug.Log("Melchior is SuperGay");
            if (collision.GetComponent<CharacterController2D>().stacks == 3)
            {
                collision.GetComponent<CharacterController2D>().resetList();
                FindObjectOfType<UIManager>().setScore(team, 1);
            }
        }
    }
}
