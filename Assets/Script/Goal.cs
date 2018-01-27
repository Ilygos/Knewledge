using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (collision.GetComponent<CharacterController2D>().stacks.Count == 3)
            {
                foreach(GameObject stack in collision.GetComponent<CharacterController2D>().stacks)
                {
                    collision.GetComponent<CharacterController2D>().stacks.Remove(stack);

                }

            }
        }
    }
}
