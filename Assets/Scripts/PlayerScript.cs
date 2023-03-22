using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;
    public float speed;
    public Text score;
    private int scoreValue = 0;
    public Text lives;
    private int livesValue = 3;
    private int levelNum;

    public AudioClip musicOne;
    public AudioClip winSound;
    public AudioSource musicSource;

    public Text winMessage;
    public Text loseMessage;

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        SetCountText();
        levelNum = 1;

        winMessage.text = " ";
        loseMessage.text = " ";

        musicSource.clip = musicOne;
        musicSource.Play();

        anim = GetComponent<Animator>();
    }

    void SetCountText()
    {
        score.text = "Score: " + scoreValue.ToString();
        lives.text = "Lives: " + livesValue.ToString();

        // Teleports to Level 2
        if ((scoreValue == 4) && (levelNum == 1))
        {
            transform.position = new Vector2(96.0f, 0.0f);
            livesValue = 3;
            lives.text = "Lives: " + livesValue.ToString();
            levelNum = 2;
        }

        if (scoreValue == 8)
        {
            winMessage.text = "You Win! Game created by Michael Kocis!";
            musicSource.clip = winSound;
            musicSource.loop = false;
            musicSource.Play();
            Destroy(this);
        }

        if (livesValue <= 0)
        {
            loseMessage.text = "You Lose! Try again!";
            Destroy(this);
        }
    }

    void Update()
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

        if (Input.GetKeyDown(KeyCode.W))
        {
            anim.SetInteger("State", 2);
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            anim.SetInteger("State", 0);
        }
    }

    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");

        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            SetCountText();
            Destroy(collision.collider.gameObject);
        }

        else if (collision.collider.tag == "Enemy")
        {
            livesValue -= 1;
            SetCountText();
            Destroy(collision.collider.gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 4), ForceMode2D.Impulse);
            }
        }
    }
}