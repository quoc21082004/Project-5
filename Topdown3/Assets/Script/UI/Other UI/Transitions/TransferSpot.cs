using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransferSpot : MonoBehaviour
{
    public string spotName;
    private void Update()
    {
        if (TransferPlayer.nextTransferSpot == spotName)
        {
            Vector3 newPos = transform.position;
            TransferPlayer.Teleport(newPos, new Vector2(0, 0));
            TransferPlayer.nextTransferSpot = "";
            TransferPlayer.nextDirection = Vector2.zero;
        }
    }
}
