using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {


    public float shipSpeed;
    public float padding;
    public GameObject laserPrefab;
    public float laserSpeed;
    public float shootingSpeed;
    public int health;
    

    private bool canShot = true;
    private float xmin;
    private float xmax;
    private AudioClip laserSound;
    private ScoreKeeper healthText;
    private int maxHealth;


    private void Start()
    {
        WorldBounds();
        laserSound = gameObject.GetComponent<AudioSource>().clip;
        healthText = GameObject.Find("ScoreKeeper").GetComponent<ScoreKeeper>();
        maxHealth = health;
    }
    // Update is called once per frame
    void Update () {
        InputManager();
    }
    

    void InputManager()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            this.transform.position += Vector3.left * (shipSpeed * Time.deltaTime);

           

        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.position += Vector3.right * (shipSpeed * Time.deltaTime);
            
        }
        float currentx = Mathf.Clamp(transform.position.x, xmin, xmax);
        transform.position = new Vector3(currentx, transform.position.y, transform.position.z);

        if (Input.GetKeyDown(KeyCode.Space) && canShot)
        {
            Vector3 startPosition = transform.position + new Vector3(0, 0.6f, 0);
            GameObject shotLaser = Instantiate(laserPrefab, startPosition, Quaternion.identity) as GameObject;
            shotLaser.GetComponent<Rigidbody2D>().velocity = new Vector3(0, laserSpeed, 0);
            AudioSource.PlayClipAtPoint(laserSound, transform.position);
            canShot = false;
            Invoke("Delay", shootingSpeed);
        }
        
    }

    void WorldBounds()
    {
        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftpos = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 rightpos = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
        xmin = leftpos.x + padding;
        xmax = rightpos.x - padding;

    }

    void Delay()
    {
            canShot = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.GetComponent<Laser>())
        {
            Laser bullet = collision.gameObject.GetComponent<Laser>();
            bullet.Hit();
            Damaged(bullet);
        } else if (collision.gameObject.GetComponent<HealthPill>())
        {
            HealthPill bonus = collision.gameObject.GetComponent<HealthPill>();
            if (health < maxHealth)
            {
                health += bonus.healthBonus;
                if(health > maxHealth)
                {
                    health = maxHealth;
                }
                healthText.AdjustHealth(health, maxHealth);
                
            }
            Destroy(collision.gameObject);
        }
    }

    void Damaged(Laser bullet)
    {
        health -= bullet.GetDamage();
        healthText.AdjustHealth(health, maxHealth);
        if (health <= 0)
        {
            LevelManager level = FindObjectOfType<LevelManager>().GetComponent<LevelManager>();
            level.LoadLevel("Lose Screen");
            Destroy(gameObject);
        }
    }
}
