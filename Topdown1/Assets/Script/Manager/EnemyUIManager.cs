using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUIManager : MonoBehaviour
{
    private Enemy theEnemy;
    public List<Enemy> alertEnemies;
    [SerializeField] Image maxhp_img;
    [SerializeField] Image hp_img;
    private void Start()
    {
        theEnemy = GetComponentInParent<Enemy>();
    }
    private void Update()
    {
        hp_img.fillAmount = theEnemy.heath / theEnemy.maxhealth;
    }
    public void addAlertEnemy(Enemy enemy)
    {
        alertEnemies.Add(enemy);
    }
}
