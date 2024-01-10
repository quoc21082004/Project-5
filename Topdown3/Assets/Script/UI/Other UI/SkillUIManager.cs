using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class SkillUIManager : Singleton<SkillUIManager>
{
    /*Player player;
    PlayerAttack playerAttack;
    SkillSet skillSet;
    [SerializeField] Image ultimateSkill_Img, ultimateSkillCD_Img;
    [SerializeField] Image dashSkill_Img, dashSkillCD_Img;
    [SerializeField] Image attack_Img, attackCD_Img;
    [HideInInspector] public float skillCDcounter, dashCD, attackCD;
    [HideInInspector] public bool isUltimateSkillCD, isDashCD, isAttackCD;
    private void Update()
    {
        if (PartyController.player == null)
            return;
        else
        {
            playerAttack = PartyController.player.gameObject.GetComponent<PlayerAttack>();
            player = PartyController.player.gameObject.GetComponent<Player>();
            skillSet = PartyController.player.playerSkill;
        }
        UpdateSkillCD();
        UpdateSKillCD_Image();
        UpdateDashCD();
        UpdateDashCD_Image();
        UpdateAttackCD();
        UpdateAttackCD_Img();
    }
    #region Update Skill CD
    public void UseUltimateSkill()
    {
        isUltimateSkillCD = true;
        skillCDcounter = skillSet.ultimateskill.skillCD;
    }
    public void UseDashSkill()
    {
        isDashCD = true;
        dashCD = player.dashCD;
    }
    public void UseAttackNormal()
    {
        isAttackCD = true;
        attackCD = playerAttack.attackCD;
    }
    public void UpdateSkillCD()
    {
        if (isUltimateSkillCD)
        {
            skillCDcounter -= Time.deltaTime;
            if (skillCDcounter <= 0)
            {
                skillCDcounter = 0;
                isUltimateSkillCD = false;
            }
            UpdateSKillCD_Image();
        }
    }
    public void UpdateDashCD()
    {
        if (isDashCD)
        {
            dashCD -= Time.deltaTime;
            if (dashCD <= 0)
            {
                dashCD = 0;
                isDashCD = false;
            }
            UpdateDashCD_Image();
        }
    }
    public void UpdateAttackCD()
    {
        if (isAttackCD)
        {
            attackCD -= Time.deltaTime;
            if (attackCD <= 0)
            {
                attackCD = 0;
                isAttackCD = false;
            }
            UpdateAttackCD_Img();
        }
    }
    public void UpdateSKillCD_Image()
    {
        ultimateSkill_Img.sprite = skillSet.ultimateskill.skillIcon;
        ultimateSkillCD_Img.sprite = skillSet.ultimateskill.skillIcon;
        ultimateSkillCD_Img.fillAmount = skillCDcounter / skillSet.ultimateskill.skillCD;
    }
    public void UpdateDashCD_Image()
    {
        dashSkillCD_Img.fillAmount = dashCD / player.dashCD;
    }
    public void UpdateAttackCD_Img()
    {
        attackCD_Img.fillAmount = attackCD / playerAttack.attackCD;
    }
    #endregion
    */
}
