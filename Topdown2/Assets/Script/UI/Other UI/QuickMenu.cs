using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickMenu : MonoBehaviour
{
    [SerializeField] GameObject mainbtn;
    [SerializeField] GameObject upgradebtn,marketbtn, settingbtn;
    [Space]
    [SerializeField] GameObject upgrade_obj, setting_obj, ScrollViewObj;
    private bool isOpen = false;

    public void QuickMenu_Click()
    {
        AudioManager.instance.PlaySfx("Click");
        if (!isOpen)
        {
            isOpen = true;
            StartCoroutine(menuAnimOpen_button());
        }
        else if (isOpen)
        {
            isOpen = false;
            StartCoroutine(menuAnimClose_button());
        }
    }
    public void upgrade_open()
    {
        AudioManager.instance.PlaySfx("Click");
        upgrade_obj.gameObject.SetActive(true);
    }
    IEnumerator menuAnimOpen_button()
    {
        ScrollViewObj.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.02f);
        upgradebtn.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.02f);
        marketbtn.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.02f);
        settingbtn.gameObject.SetActive(true);
    }
    IEnumerator menuAnimClose_button()
    {
        ScrollViewObj.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.02f);
        settingbtn.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.02f);
        marketbtn.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.02f);
        upgradebtn.gameObject.SetActive(false);
    }
    
}
