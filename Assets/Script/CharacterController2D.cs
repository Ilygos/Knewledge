using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2D : MonoBehaviour {
    public int playerId;
    public int type;
    public Transform ballPosition;
    public int team = 0;
    public float dashDelay = 0.5f;
    public bool isDashing;
    public bool isDead;
    public bool canTake = true;
    public bool isImortal;
    public GameObject damageZone;

    public Transform spawn;

    public Transform[] spawnPositions;
	public GameObject Explosion;
	public GameObject Pickup_Collectible;
	public GameObject Pickup_Bonus;
	public GameObject Respawn;
	public GameObject Transmission;
	public GameObject Player_Collision;
	public GameObject Bonus_Bouclieru;


    public Sprite normalSprite;
    public Sprite stack1Sprite;
    public Sprite stack2Sprite;
    public Sprite stack3Sprite;

    public int stacks;

    [SerializeField]
    GameObject ballReference;


    [SerializeField]
    float speed = 5;
    [SerializeField]
    float friction = 0.8f;
    [SerializeField]
    float dashValue = 50;

    float timeOfDeath;
    [SerializeField]
    float timeBeingDead = 5;

    float timeBefore;
    float currentSpeed;
    float currentDashImpulse;
    Rigidbody2D _rgbg2D;
    bool shielded;
    SpriteRenderer _sprtiRenderer;


    // Use this for initialization
    void Start () {
        _rgbg2D = GetComponent<Rigidbody2D>();
        stacks = 0;
        currentSpeed = speed;
        currentDashImpulse = dashValue;
        _sprtiRenderer = GetComponent<SpriteRenderer>();
        spawn = transform;
        timeBefore = Time.fixedTime;
    }


    // Update is called once per frame
    void Update() {
        if (!isDead)
        { float inputHorizontal = Input.GetAxis("Horizontal" + playerId);
            float inputVertical = Input.GetAxis("Vertical" + playerId);
            Vector2 lVelocityDelta = new Vector2(currentSpeed * inputHorizontal, currentSpeed * inputVertical);
            transform.rotation = inputHorizontal != 0 || inputVertical != 0 ? Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Atan2(inputVertical, inputHorizontal)) : transform.rotation;

            if (Input.GetButtonDown("Attack" + playerId) && Time.fixedTime > timeBefore + dashDelay)
            {
                timeBefore = Time.fixedTime;
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
        if (Time.fixedTime > timeBeingDead + timeOfDeath)
        {
            isImortal = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "DashZone")
        {
            CharacterController2D lCharaCtrl2D = other.GetComponentInParent<CharacterController2D>();
            if ((lCharaCtrl2D.isDashing && isDashing) && (lCharaCtrl2D.team != team))
            {
                _rgbg2D.velocity = new Vector3(-250, 0);
                other.GetComponentInParent<Rigidbody2D>().velocity =new Vector3(250,0);
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
        if (canTake && collision.tag == "Collectible" && !collision.GetComponent<Ball>().firstValidation && !collision.GetComponent<Ball>().taken)
        {
            Debug.Log(name + collision.GetComponent<Ball>().type);
            if (type == collision.GetComponent<Ball>().type)
            {
                if (ballReference == null)
                {
                    grabBall(collision.gameObject);
                    collision.GetComponent<Ball>().firstValidation = true;
                }
            }
        }
        else if(canTake && collision.tag == "Collectible" && collision.GetComponent<Ball>().firstValidation && collision.GetComponent<Ball>().team == team )
        {
            if (type != collision.GetComponent<Ball>().type && stacks < 3)
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
        _rgbg2D.velocity = Vector3.zero;
        if(shielded)
        {
            shielded = false;
            return;
        }
        if (isImortal) return;
        isImortal = true;
        isDead = true;
        timeOfDeath = Time.fixedTime;
        if (ballReference != null) drop(ballReference);
        canTake = false;
        if (stacks > 0)stacks--;
        StartCoroutine(explosion());
    }

    public void transfertBall()
    {
        if (ballReference.GetComponent<Ball>().type == 1)
            FindObjectOfType<UIManager>().spawnBall(0);
        else
            FindObjectOfType<UIManager>().spawnBall(1);

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
        ballReference = null;
        ball.GetComponent<Ball>().taken = false;
        ball.GetComponent<Ball>().firstValidation = false;
        ball.GetComponent<Ball>().secondValidation = false;
    }

    IEnumerator explosion()
    {
        GameObject lPart = Instantiate(Explosion, transform.position, Quaternion.identity);
        lPart.GetComponent<ParticleSystem>().Play();
        _sprtiRenderer.enabled = !_sprtiRenderer.enabled;
        yield return new WaitForSeconds(0.8f);
        respawed();
    }

    public void respawed()
    {
        StartCoroutine(respawn());
    }

    IEnumerator respawn()
    {
        transform.position = spawnPositions[Random.Range(0, spawnPositions.Length - 1)].position;
        GameObject lPart = Instantiate(Respawn, transform.position, Quaternion.identity);
        lPart.GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(0.8f);
        isDead = false;
        canTake = true;
        _sprtiRenderer.enabled = !_sprtiRenderer.enabled;
    }

}
