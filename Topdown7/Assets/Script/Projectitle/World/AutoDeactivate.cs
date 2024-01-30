using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDeactivate : MonoBehaviour
{
    [SerializeField] bool destroyGameObject;
    [SerializeField] float lifeTime;
    WaitForSeconds waitTime;
    private void Awake()
    {
        waitTime = new WaitForSeconds(lifeTime);
    }
    private void OnEnable()
    {
        StartCoroutine(DeactivateCourtine());
    }
    private IEnumerator DeactivateCourtine()
    {
        yield return waitTime;
        gameObject.SetActive(false);
    }
}
