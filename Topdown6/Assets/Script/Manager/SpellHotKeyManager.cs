using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class SpellHotKeyManager : Singleton<SpellHotKeyManager> , IHotKey
{
    public int NumOfHotKeySpell = 4;
    public Image[] hotkeySpellIcons;
    public Image[] hotkeySpellCD;
    public SpellBook[] hotkeySpell;
    public Dictionary<SpellBookType, float> spellbookCDcounter;
    public Dictionary<SpellBookType, bool> isSpellCDcounter;
    public Image castDelay_img;
    public GameObject skillbarDisplay;
    float delayTime;
    private void Start()
    {
        if (hotkeySpell.Length == 0)
            hotkeySpell = new SpellBook[NumOfHotKeySpell];

        if (spellbookCDcounter == null)
        {
            spellbookCDcounter = new Dictionary<SpellBookType, float>();
            foreach (var type in (SpellBookType[])Enum.GetValues(typeof(SpellBookType)))
                spellbookCDcounter.Add(type, 0f);
        }
        if (isSpellCDcounter == null)
        {
            isSpellCDcounter = new Dictionary<SpellBookType, bool>();
            foreach (var type in (SpellBookType[])Enum.GetValues(typeof(SpellBookType)))
                isSpellCDcounter.Add(type, false);
        }
    }
    private void Update()
    {
        UpdateCoolDown();
        UpdateHoyKeySpellIcon();

        foreach (var spell in hotkeySpell)
        {
            if (spell != null)
            {
                if (skillbarDisplay != null)
                {
                    delayTime += Time.deltaTime;
                    castDelay_img.fillAmount = delayTime / spell.spell.baseCastDelay;
                }
                else
                    delayTime = 0;
            }
        }

    }
    public void SetHotKeySpell(int numKey, SpellBook spellItem)
    {
        for (int i = 0; i < NumOfHotKeySpell; i++)
        {
            if (hotkeySpell[i] == spellItem)
            {
                hotkeySpell[i] = null;
                break;
            }
        }
        hotkeySpell[numKey] = spellItem;
    }
    private void UpdateHoyKeySpellIcon()
    {
        for (int i = 0; i < NumOfHotKeySpell; i++)
        {
            if (hotkeySpell[i] == null)
            {
                hotkeySpellIcons[i].enabled = false;
                hotkeySpellIcons[i].fillAmount = 1;
            }
            else if (hotkeySpell[i] != null)
            {
                hotkeySpellIcons[i].enabled = true;
                hotkeySpellIcons[i].sprite = hotkeySpell[i].icon;
                SpellBookType type = hotkeySpell[i].type;
                hotkeySpellCD[i].fillAmount = spellbookCDcounter[type] / SpellBook.GetSpellCooldown(type);
            }
        }
    }
    public void UseSpell(SpellBook spellbook)
    {
        if (spellbook != null)
            StartCoroutine(DelayCast(spellbook));
    }
    public void UpdateCoolDown()
    {
        foreach (var type in (SpellBookType[])Enum.GetValues(typeof(SpellBookType)))
        {
            spellbookCDcounter[type] -= Time.deltaTime;
            if (spellbookCDcounter[type] < 0)
            {
                spellbookCDcounter[type] = 0;
                isSpellCDcounter[type] = false;
            }
        }
    }

    public bool IsHotKeyCoolDown(int numkey)
    {
        if (hotkeySpell[numkey] == null)
            return true;
        else
            return isSpellCDcounter[hotkeySpell[numkey].type];
    }
    public void UseHotKey(int numkey)
    {
        UseSpell(hotkeySpell[numkey]);
    }
    IEnumerator DelayCast(SpellBook spellbook)
    {
        skillbarDisplay.gameObject.SetActive(true);
        delayTime = 0;
        yield return new WaitForSeconds(spellbook.spell.baseCastDelay);
        spellbook.Use();
        SpellBookType type = spellbook.type;
        isSpellCDcounter[type] = true;
        spellbookCDcounter[type] = SpellBook.GetSpellCooldown(type);
        skillbarDisplay.gameObject.SetActive(false);
        UpdateHoyKeySpellIcon();
    }
}
