using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour {

    public string type;

    // Use this for initialization
    void Start () {
        
	}
	
    public void changeType(string newType)
    {
        type = newType;
    }

	// Update is called once per frame
	void Upddate () {

    }

}
