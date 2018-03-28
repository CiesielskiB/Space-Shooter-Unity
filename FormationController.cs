using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormationController : MonoBehaviour {

    public GameObject enemyPrefab;
    public float witdh, height;
    public float speed;
    public float spawnDelay;

    private float xmin, xmax;
    private bool movingRight = true;
    private Vector3 startPosition;
    private int enemyQuantity;



    // Use this for initialization
    void Start() {
        enemyQuantity = transform.childCount;
        WorldBounds();
        SpawnEnemies();
        startPosition = transform.position;

    }

       // Update is called once per frame
    void Update () {
         MovingEnemies();
  
        
        if (AllMembersDead())
        {
            transform.position = startPosition;
            SpawnEnemies();
            Debug.Log("ded");
        }

        

    }

    // Deciding world bounds
    void WorldBounds()
    {
        float padding = 0.5f;
        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftpos = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 rightpos = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
        xmin = leftpos.x + padding;
        xmax = rightpos.x - padding;

    }

    // Moving enemies left-right
    void MovingEnemies()
    {
        if (movingRight)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        else
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        float rightEdge = transform.position.x + (0.5f * witdh);
        float leftEdge = transform.position.x - (0.5f * witdh);
        if (leftEdge < xmin)
        {
            movingRight = true;
        } else if (rightEdge > xmax)
        {
            movingRight = false;
        }
    }

    // Checking if enemies died;
    bool AllMembersDead()
    {
        foreach(Transform childPositionGameObject in transform){
            if (childPositionGameObject.childCount > 0) return false;
        }
        enemyQuantity = transform.childCount;
        return true;
    }

    Transform  NextFreePosition()
    {
        foreach (Transform childPositionGameObject in transform)
        {
            if (childPositionGameObject.childCount <= 0) return childPositionGameObject;
        }
        
        return null;
    }

    // Enemy spawner
    void SpawnEnemies()
    {
        Transform freePosition = NextFreePosition();
        if (freePosition )
        {
            GameObject enemy = Instantiate(enemyPrefab, freePosition.position, Quaternion.identity) as GameObject;
            enemy.transform.parent = freePosition;
            enemyQuantity--;
            
        }
        if (NextFreePosition() && enemyQuantity >= 0)
        {
            Invoke("SpawnEnemies", spawnDelay);
        }

    }

    // drawing gizmos
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(witdh, height));
    }

}
