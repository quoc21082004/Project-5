using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] GameObject closeDoor;
    [SerializeField] GameObject openDoor;
    private bool isOpening;
    private void Awake()
    {
        isOpening = openDoor.gameObject.activeSelf;
    }
    public void SwitchState()
    {
        if (isOpening)
            OpenDoor();
        else
            CloseDoor();
        // audio
    }
    public void OpenDoor()
    {
        openDoor.gameObject.SetActive(true);
        closeDoor.gameObject.SetActive(false);
        isOpening = true;
    }
    public void CloseDoor()
    {
        closeDoor.gameObject.SetActive(true);
        openDoor.gameObject.SetActive(false);
        isOpening = false;
    }
}
