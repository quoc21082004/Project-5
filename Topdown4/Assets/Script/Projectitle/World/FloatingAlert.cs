using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingAlert: MonoBehaviour
{
    private SpriteRenderer mySR;
    public float moveSpeed;
    public float alertTime;
    private float alertTimeCounter;

    private void Awake()
    {
        mySR = GetComponent<SpriteRenderer>();
        alertTimeCounter = alertTime;
    }
    private void Update()
    {
        transform.Translate(new Vector3(0f, moveSpeed * Time.deltaTime, 0f));
        mySR.color = new Color(1f, 1f, 1f, alertTimeCounter / alertTime);
        alertTimeCounter -= Time.deltaTime;
    }
}
