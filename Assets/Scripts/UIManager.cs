using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Dash Ability")]
    public AbilityHolder abilityHolder;
    public AbilityBase ability;
    public Image dashImage;
    float dashCooldown;
    bool isCooldown = false;
    KeyCode dashKey;

    [Header("Music Ability")]
    public MusicAbilityHolder musicAbilityHolder;
    public MusicAbility ability2;
    public Image musicImage;
    float musicCooldown;
    bool isCooldown2 = false;
    KeyCode musicKey;

    [Header("Music Ability 2")]
    public MusicAbility2Holder musicAbilityHolder2;
    public MusicAbility ability3;
    public Image musicImage2;
    float musicCooldown2;
    bool isCooldown3 = false;
    KeyCode musicKey2;

    void Start()
    {
        //Dash Ability
        dashKey = ability.key;
        dashImage.fillAmount = 0;

        //Music Ability
        musicKey = ability2.key;
        musicImage.fillAmount = 0;
        
        //Music 2 Ability
        musicKey2 = ability3.key;
        musicImage2.fillAmount = 0;

    }


    void Update()
    {
        DashAbility();
        MusicAbility();
        MusicAbility2();
    }

    
    void DashAbility()
    {
        if(abilityHolder.state != AbilityHolder.AbilityState.ready) //ability active or cd
        {
            if(isCooldown == false) //if dash pressed and not on cd
            {
                //set to cd and fill amount
                dashCooldown = ability.cooldownTime + ability.activeTime;
                isCooldown = true;
                dashImage.fillAmount = 1;
            }
            if(isCooldown)
            {
                dashImage.fillAmount -= 1 / dashCooldown * Time.deltaTime; //start reducing fill amount

                if(dashImage.fillAmount <= 0) //if fillamount is finished (cd finished)
                {
                    dashImage.fillAmount = 0;
                    isCooldown = false;
                }
            }
        }
        else //ability ready
        {
            dashImage.fillAmount = 0;
        }
    }

    void MusicAbility()
    {
        if(musicAbilityHolder.state != MusicAbilityHolder.AbilityState.ready)
        {
            if(isCooldown2 == false)
            {
                musicCooldown = ability2.cooldownTime;
                isCooldown2 = true;
                musicImage.fillAmount = 1;
            }
            if(isCooldown2)
            {
                if(ability2.spawningReady)
                {
                    musicImage.fillAmount -= 1 / musicCooldown * Time.deltaTime;
                }
                if(musicImage.fillAmount <= 0) //if fillamount is finished (cd finished)
                {
                    musicImage.fillAmount = 0;
                    isCooldown2 = false;
                }
            }
        }
        else //ability ready
        {
            musicImage.fillAmount = 0;
        }
    }

    void MusicAbility2()
    {
        if(musicAbilityHolder2.state != MusicAbility2Holder.AbilityState.ready)
        {
            if(isCooldown3 == false)
            {
                musicCooldown2 = ability3.cooldownTime;
                isCooldown3 = true;
                musicImage2.fillAmount = 1;
            }
            if(isCooldown3)
            {
                if(ability3.spawningReady)
                {
                    musicImage2.fillAmount -= 1 / musicCooldown2 * Time.deltaTime;
                }
                if(musicImage2.fillAmount <= 0) //if fillamount is finished (cd finished)
                {
                    musicImage2.fillAmount = 0;
                    isCooldown3 = false;
                }
            }
        }
        else //ability ready
        {
            musicImage2.fillAmount = 0;
        }
    }
}
