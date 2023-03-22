using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;
    public float speed;
    public Text score;
    public Text life;
    public GameObject winTextObject;
    public GameObject loseTextObject;
    public AudioSource musicSource;
    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;
    private int scoreValue = 0;
    private int lifeValue = 3;
    Animator anim;
    private bool facingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = scoreValue.ToString();
        life.text = lifeValue.ToString();
        winTextObject.SetActive(false);
        loseTextObject.SetActive(false);
        musicSource.clip = musicClipOne;
        musicSource.Play();
        musicSource.loop = true;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
    
    if (facingRight == false && hozMovement > 0)
   {
     Flip();
   }
    else if (facingRight == true && hozMovement < 0)
   {
     Flip();
   }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = scoreValue.ToString();
            Destroy(collision.collider.gameObject);
            SetCountText();
        }

        if (collision.collider.tag == "Enemy")
        {
            lifeValue -= 1;
            life.text = lifeValue.ToString();
            Destroy(collision.collider.gameObject);
            SetCountText();
        }

    }
    void SetCountText()
    {
        if (scoreValue == 4)
        {
            transform.position = new Vector3(42.0f, 0.0f, 0.0f); 
        }

        if (scoreValue >= 8)
        {
            winTextObject.SetActive(true);
            musicSource.clip = musicClipTwo;
            musicSource.Play();
        } 

        if (lifeValue <= 0)
        {
            loseTextObject.SetActive(true);
            Destroy(rd2d.gameObject);
        }    
    }
       
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse); //the 3 in this line of code is the player's "jumpforce," and you change that number to get different jump behaviors.  You can also create a public variable for it and then edit it in the inspector.
            }
        }
    }
    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
          anim.SetInteger("State", 1);
         }

     if (Input.GetKeyUp(KeyCode.D))
        {
          anim.SetInteger("State", 0);
         }

     if (Input.GetKeyDown(KeyCode.A))
        {
          anim.SetInteger("State", 1);
         }

     if (Input.GetKeyUp(KeyCode.A))
        {
          anim.SetInteger("State", 0);
         }
    }
    
    void Flip()
   {
     facingRight = !facingRight;
     Vector2 Scaler = transform.localScale;
     Scaler.x = Scaler.x * -1;
     transform.localScale = Scaler;
   }
}
