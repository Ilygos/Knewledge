﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2D : MonoBehaviour {
    public int playerId;
    public int type;
    public Transform ballPosition;
    public int team = 0;
    public bool isDashing;
    public bool isDead;
    public GameObject damageZone;

    [SerializeField]
    private GameObject ballReference;

    [SerializeField]
    float speed = 5;
    [SerializeField]
    float friction = 0.8f;
    [SerializeField]
    float dashValue = 50;

    Rigidbody2D _rgbg2D;


	// Use this for initialization
	void Start () {
        _rgbg2D = GetComponent<Rigidbody2D>();	
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 lVelocityDelta = new Vector2(speed * Input.GetAxis("Horizontal" + playerId), speed * Input.GetAxis("Vertical" + playerId));
        transform.rotation = Quaternion.Euler(0, 0,Mathf.Rad2Deg*Mathf.Atan2(Input.GetAxis("Horizontal" + playerId), Input.GetAxis("Vertical" + playerId)));
        if (Input.GetButtonDown("Attack" + playerId))
        {
            lVelocityDelta.x += dashValue * Input.GetAxisRaw("Horizontal" + playerId);
            lVelocityDelta.y += dashValue * Input.GetAxisRaw("Vertical" + playerId);
            isDashing = true;
            damageZone.SetActive(true);
        }
        _rgbg2D.velocity += lVelocityDelta;
        _rgbg2D.velocity *= friction;
        if (ballReference != null) ballReference.transform.position = ballPosition.position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "DashZone")
        {
            CharacterController2D lCharaCtrl2D = other.GetComponentInParent<CharacterController2D>();
            if ((lCharaCtrl2D.isDashing && isDashing) && (lCharaCtrl2D.team != team))
            {
                _rgbg2D.velocity *= -1;
                other.GetComponentInParent<Rigidbody2D>().velocity *= -1;
            }
            else if ((lCharaCtrl2D.isDashing && !isDashing) && (lCharaCtrl2D.team != team))
            {
                killed();
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Collectible" && !collision.GetComponent<Ball>().firstValidation && !collision.GetComponent<Ball>().taken && Input.GetButtonDown("Interaction"+playerId))
        {
            Debug.Log(collision.GetComponent<Ball>().type);
            if (type != collision.GetComponent<Ball>().type)
            {
                if (ballReference == null) grabBall(collision.gameObject);
            }
        }
        else if(collision.tag == "Collectible" && collision.GetComponent<Ball>().firstValidation && !collision.GetComponent<Ball>().taken && Input.GetButtonDown("Interaction" + playerId))
        {
            if (type == collision.GetComponent<Ball>().type)
            {
                grabBall(collision.gameObject);
            }
        }
       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       
    }

    private void killed()
    {
        isDead = true;
        Debug.Log("Killed");
    }

    private void grabBall(GameObject ball)
    {
        ballReference = ball;
        ball.GetComponent<Ball>().taken = true;
        ballReference.transform.position = ballPosition.position;
    }

    private void drop(GameObject ball)
    {
        ball.GetComponent<Ball>().taken = false;
        ballReference = null;
    }

}
