using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Builet : MonoBehaviour
{
    private Rigidbody2D myrigd;
    [SerializeField] public float realspeed;
    float speed;
    private void Awake()
    {
        myrigd = GetComponent<Rigidbody2D>();
        speed = PlayerPrefs.GetFloat("builetSpeed");
    }
    private void FixedUpdate()
    {
        myrigd.AddForce(transform.right * speed * realspeed, ForceMode2D.Impulse);
        myrigd.velocity = Vector2.ClampMagnitude(myrigd.velocity, speed * realspeed);
    }
}
