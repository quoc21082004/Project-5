using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonTrap : MonoBehaviour
{
    public float damage;
    private float damagespeed = 0.2f;
    private float timer = 0;
    private bool isPoison = false;
    float saveMovementSpeed;
    Player player;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        damage = (player.maxhealth * 1.5f) / 100;
    }
    private void Start()
    {
        saveMovementSpeed = PlayerPrefs.GetFloat("speed");
        timer = damagespeed;
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > damagespeed)
        {
            timer = damagespeed;
        }
        if (isPoison)
        {
            if (timer >= damagespeed)
            {
                player.gameObject.GetComponent<PlayerHurt>().TakeDamage(null,damage);
                timer = 0;
            }
        }
        else
        {
            ChangeColorExit();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ChangeColorEnter();
            if (PlayerPrefs.GetString("immueSlow") == "false")
            {
                PlayerPrefs.SetFloat("speed", PlayerPrefs.GetFloat("speed") / 2);
            }
            else if (PlayerPrefs.GetString("immueSlow") == "true")
            {
                PlayerPrefs.SetFloat("speed", PlayerPrefs.GetFloat("speed"));
            }
            PlayerPrefs.Save(); 
            isPoison = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ChangeColorExit();
            PlayerPrefs.SetFloat("speed", saveMovementSpeed);
            PlayerPrefs.Save();
            isPoison = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ChangeColorEnter();
            if (PlayerPrefs.GetString("immueSlow") == "false")
            {
                PlayerPrefs.SetFloat("speed", PlayerPrefs.GetFloat("speed") / 2);
            }
            else if (PlayerPrefs.GetString("immueSlow") == "true")
            {
                PlayerPrefs.SetFloat("speed", PlayerPrefs.GetFloat("speed"));
            }
            PlayerPrefs.Save();
            isPoison = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ChangeColorExit();
            PlayerPrefs.SetFloat("speed", saveMovementSpeed);
            PlayerPrefs.Save();
            isPoison = false;
        }
    }
    private void OnDisable()
    {
        PlayerPrefs.SetFloat("speed", saveMovementSpeed);
        PlayerPrefs.Save();
    }
    private void OnDestroy()
    {
        PlayerPrefs.SetFloat("speed", saveMovementSpeed);
        PlayerPrefs.Save();
    }
    private void OnApplicationQuit()
    {
        PlayerPrefs.SetFloat("speed", saveMovementSpeed);
        PlayerPrefs.Save();
    }
    private void ChangeColorEnter()
    {
        //player.gameObject.GetComponent<Player>().mySR.color = Color.green;
    }
    private void ChangeColorExit()
    {
       // player.gameObject.GetComponent<Player>().mySR.color = Color.white;
    }
}
