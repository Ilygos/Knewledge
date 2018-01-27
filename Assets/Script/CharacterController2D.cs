using System.Collections;
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

    public Transform spawn;

	public GameObject Explosion_Bleu;
	public GameObject Explosion_Orange;
	public GameObject Pickup_Collectible;
	public GameObject Pickup_Bonus;
	public GameObject Respawn_Bleu;
	public GameObject Respawn_Orange;
	public GameObject Transmission_Bleu;
	public GameObject Transmission_Orange;
	public GameObject Player_Collision;
	public GameObject Bonus_Bouclier_Bleu;
	public GameObject Bonus_Bouclier_Orange;

    public Sprite normalSprite;
    public Sprite stack1Sprite;
    public Sprite stack2Sprite;
    public Sprite stack3Sprite;
    private SpriteRenderer _sprtiRenderer;

    public int stacks;

    [SerializeField]
    private GameObject ballReference;


    [SerializeField]
    float speed = 5;
    [SerializeField]
    float friction = 0.8f;
    [SerializeField]
    float dashValue = 50;


    float currentSpeed;
    float currentDashImpulse;
    Rigidbody2D _rgbg2D;
    private bool shielded;


    // Use this for initialization
    void Start () {
        _rgbg2D = GetComponent<Rigidbody2D>();
        stacks = 0;
        currentSpeed = speed;
        currentDashImpulse = dashValue;
        _sprtiRenderer = GetComponent<SpriteRenderer>();
        spawn = transform;
    }
	
	// Update is called once per frame
	void Update () {
        float inputHorizontal = Input.GetAxis("Horizontal" + playerId);
        float inputVertical = Input.GetAxis("Vertical" + playerId);
        Vector2 lVelocityDelta = new Vector2(currentSpeed * inputHorizontal, currentSpeed * inputVertical);
        transform.rotation = inputHorizontal != 0 || inputVertical != 0 ? Quaternion.Euler(0, 0,Mathf.Rad2Deg*Mathf.Atan2(inputVertical, inputHorizontal)) : transform.rotation;
        if (Input.GetButtonDown("Attack" + playerId))
        {
            lVelocityDelta.x += currentDashImpulse * Input.GetAxisRaw("Horizontal" + playerId);
            lVelocityDelta.y += currentDashImpulse * Input.GetAxisRaw("Vertical" + playerId);
            isDashing = true;
            damageZone.SetActive(true);
        }
        _rgbg2D.velocity += lVelocityDelta;
        _rgbg2D.velocity *= friction;
        if (ballReference != null) ballReference.transform.position = ballPosition.position;
        if (stacks == 1)
            _sprtiRenderer.sprite = stack1Sprite;
        else if (stacks == 2)
            _sprtiRenderer.sprite = stack2Sprite;
        else if (stacks == 3)
            _sprtiRenderer.sprite = stack3Sprite;
        else
            _sprtiRenderer.sprite = normalSprite;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "DashZone")
        {
            CharacterController2D lCharaCtrl2D = other.GetComponentInParent<CharacterController2D>();
            if ((lCharaCtrl2D.isDashing && isDashing) && (lCharaCtrl2D.team != team))
            {
                _rgbg2D.velocity *= -1;
                other.GetComponentInParent<Rigidbody2D>().velocity = _rgbg2D.velocity;
            }
            else if ((lCharaCtrl2D.isDashing && !isDashing) && (lCharaCtrl2D.team != team))
            {
                killed();
            }

        }

        if (other.tag == "Bonus")
        {
            if (other.tag == "Speed")
            {
                currentSpeed *= 2;
            }

            if (other.tag == "Shield")
            {
                shielded = true;
            }

            if (other.tag == "Dash")
            {
                currentDashImpulse *= 2;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Collectible" && !collision.GetComponent<Ball>().firstValidation && !collision.GetComponent<Ball>().taken && Input.GetButtonDown("Interaction"+playerId))
        {
            Debug.Log(name + collision.GetComponent<Ball>().type);
            if (type != collision.GetComponent<Ball>().type)
            {
                if (ballReference == null)
                {
                    grabBall(collision.gameObject);
                    collision.GetComponent<Ball>().firstValidation = true;
                }
            }
        }
        else if(collision.tag == "Collectible" && collision.GetComponent<Ball>().firstValidation && collision.GetComponent<Ball>().team == team && Input.GetButtonDown("Interaction" + playerId))
        {
            if (type == collision.GetComponent<Ball>().type)
            {
                CharacterController2D[] lCharactersArray = FindObjectsOfType<CharacterController2D>();
                foreach (CharacterController2D character in lCharactersArray)
                {
                    if (character.ballReference == collision.gameObject/* && character.team == team*/)
                        character.transfertBall();
                }

                if (!collision.GetComponent<Ball>().secondValidation)
                {
                    collision.GetComponent<Ball>().secondValidation = true;
                    DestroyObject(collision.gameObject);
                    stacks++;
                }
            }
        }
       
    }

    public void resetList()
    {
        stacks = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       
    }

    private void killed()
    {
        if(shielded)
        {
            shielded = false;
            return;
        }
        isDead = true;
        stacks--;
    }

    public void transfertBall()
    {
        ballReference = null; 
    }

    private void grabBall(GameObject ball)
    {
        ballReference = ball;
        ball.GetComponent<Ball>().taken = true;
        ball.GetComponent<Ball>().team = team;
        ballReference.transform.position = ballPosition.position;
    }

    private void drop(GameObject ball)
    {
        ball.GetComponent<Ball>().taken = false;
        ball.GetComponent<Ball>().firstValidation = false;
        ball.GetComponent<Ball>().secondValidation = false;
        ballReference = null;
    }

}
