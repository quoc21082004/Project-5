using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackGroundCtl : MonoBehaviour
{
    public float speed = 5f;
    Vector2 startPos;
    public float dis;
    private void Awake()
    {
        startPos = transform.position;
    }
    private void Update()
    {
        transform.Translate(Vector2.right * Time.deltaTime * speed);
        if (transform.position.x > dis)
            transform.position = startPos;
    }
}
