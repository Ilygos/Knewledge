using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Ball : MonoBehaviour {
    public static List<Ball> list = new List<Ball>();

    public int type;
    public int team = 0;

    public bool firstValidation;
    public bool secondValidation;
    public bool taken;


    private SpriteRenderer _spriteRdr;

    // Use this for initialization
    void Start () {
		

    }

    // Update is called once per frame
    void Update () {
	}
}
