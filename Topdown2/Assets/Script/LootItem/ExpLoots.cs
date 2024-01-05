using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpLoots : LootItem
{
    [SerializeField] float exp = 15f;
    protected override void PickUp()
    {
        SetTypeEnemy();
        PartyController.AddExperience(exp);
        base.PickUp();
    }
    private void SetTypeEnemy()
    {
        if (enemy.type == TypeEnemy.Bat)
        {
            exp = exp * 1.2f;
        }
        if (enemy.type == TypeEnemy.Boar)
        {
            exp = exp * 1.3f;
        }
        if (enemy.type == TypeEnemy.FireTotem)
        {
            exp = exp * 1.4f;
        }
        if (enemy.type == TypeEnemy.FlyingMelee)
        {
            exp = exp * 1.5f;
        }
        if (enemy.type == TypeEnemy.LittleMelee)
        {
            exp = exp * 1.6f;
        }
        if (enemy.type == TypeEnemy.LittleRange)
        {
            exp = exp * 1.7f;
        }
        if (enemy.type == TypeEnemy.Skeleton)
        {
            exp = exp * 1.8f;
        }
    }
}
