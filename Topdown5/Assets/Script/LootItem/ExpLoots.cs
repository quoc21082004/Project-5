using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpLoots : LootItem
{
    [SerializeField] float exp = 15f;
    protected override void PickUp()
    {
        PartyController.AddExperience(exp);
        base.PickUp();
    }
}
