using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPill : MonoBehaviour {

    public int healthBonus;
    public float fallingSpeed;
    // Use this for initialization

    private void Start()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector3(0, -fallingSpeed, 0);
    }
}
