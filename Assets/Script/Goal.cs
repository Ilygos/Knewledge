using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour {

    public int team;

    private Animator _anim;
	// Use this for initialization
	void Start () {
        _anim = GetComponent<Animator>();
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && team == collision.GetComponent<CharacterController2D>().team)
        {
            Debug.Log("Melchior is a SuperGayFags");
            if (collision.GetComponent<CharacterController2D>().stacks == 3)
            {
                collision.GetComponent<CharacterController2D>().resetList();
                FindObjectOfType<UIManager>().setScore(team, 1);
                GetComponentInChildren<ParticleSystem>().Play();
                _anim.SetTrigger("Goal");
            }
        }
    }
}
