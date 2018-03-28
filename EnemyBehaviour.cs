using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {

    public int health;
    public float ShotsPerSeconds;
    public float projectileSpeed;
    public GameObject projectilePrefab;
    public int pointsWorth;
    public GameObject healthPillPrefab;
    public float chanceToDrop;

    private float interval;
    private AudioClip laserSound;
    private ScoreKeeper scoreText;


    void Start () {
        scoreText = GameObject.Find("ScoreKeeper").GetComponent<ScoreKeeper>();
        laserSound = gameObject.GetComponent<AudioSource>().clip;
        
    }
	
	// Update is called once per frame
	void Update () {
        WillShoot();

	}

    // Checking if enemy will shoot
    void WillShoot()
    {
        float probability = Time.deltaTime * ShotsPerSeconds;
        if (Random.value < probability)
        {
            Shot();
        }
    }

    // Shooting
    void Shot()
    {
        
        Vector3 startPosition = transform.position;
        AudioSource.PlayClipAtPoint(laserSound,startPosition);
        GameObject shotLaser = Instantiate(projectilePrefab, startPosition, Quaternion.identity) as GameObject;
        shotLaser.GetComponent<Rigidbody2D>().velocity = new Vector3(0, -projectileSpeed, 0);
    }

    // Checking triggers
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Laser bullet = collision.gameObject.GetComponent<Laser>();
        if (bullet)
        {
            bullet.Hit();
         Damaged(bullet);
            
        }
    }

    // Damaging enemies
    void Damaged(Laser bullet)
    {
        health -= bullet.GetDamage();
        if(health<= 0)
        {
            
            if (Random.value < chanceToDrop)
            {
                SpawnHealthPill();
            }
            Destroy(gameObject);
            scoreText.AddScore(pointsWorth);
            
        }
    }

    void SpawnHealthPill()
    {
        Instantiate(healthPillPrefab, transform.position, Quaternion.identity);
    }


}
