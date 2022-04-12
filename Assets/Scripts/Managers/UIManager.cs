using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviourPunCallbacks
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

    [Header("Barrage Ability")]
    public BarrageAbilityHolder barrageAbilityHolder;
    public BarrageAbility ability4;
    public Image barrageImage;
    float barrageCooldown;
    bool isCooldown4 = false;
    KeyCode barrageKey;

    [Header("Boss")]
    public BossController bossController;
    public Slider bossUIHealthBar;

    [Header("Player")]
    public PlayerMovement playerMovement;
    public Slider playerUIHealthBar;

    [Header("Respawning")]
    public GameObject RespawningPanel;
    public Text respawnTimerText;
    private float currentRespawnTimer;

    [Header("Menu")]
    public GameObject menuPanel;

    private void Awake() 
    {
        abilityHolder = FindObjectOfType<AbilityHolder>();
        musicAbilityHolder = FindObjectOfType<MusicAbilityHolder>();
        musicAbilityHolder2 = FindObjectOfType<MusicAbility2Holder>();
        barrageAbilityHolder = FindObjectOfType<BarrageAbilityHolder>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        bossController = FindObjectOfType<BossController>();
    }

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

        //Barrage Ability
        barrageKey = ability4.key;
        barrageImage.fillAmount = 0;

        //Set boss health bar max value to boss health
        bossUIHealthBar.maxValue = bossController.health;

        //Set player health bar max value to player health
        playerUIHealthBar.maxValue = playerMovement.health;
    }


    void Update()
    {
        DashAbility();
        MusicAbility();
        MusicAbility2();
        BarrageAbility();
        UpdateBossHealth();
        UpdatePlayerHealth();
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

    void BarrageAbility()
    {
        if(barrageAbilityHolder.state == BarrageAbilityHolder.AbilityState.cooldown) //ability active or cd
        {
            if(isCooldown4 == false)
            {
                isCooldown4 = true;
                barrageImage.fillAmount = 1;
            }
            if(isCooldown4)
            {
                barrageImage.fillAmount -= 1 / ability4.cooldownTime * Time.deltaTime; //start reducing fill amount

                if(barrageImage.fillAmount <= 0) //if fillamount is finished (cd finished)
                {
                    barrageImage.fillAmount = 0;
                    isCooldown4 = false;
                }
            }
        }
        else //ability ready
        {
            barrageImage.fillAmount = 0;
        }
    }

    void UpdateBossHealth()
    {
        bossUIHealthBar.value = bossController.health;
    }

    void UpdatePlayerHealth()
    {
        playerUIHealthBar.value = playerMovement.health;
    }

    public void RestartLevel()
    {
        RespawningPanel.SetActive(true);
        StartCoroutine(StartCountdown(5f));
    }

    IEnumerator StartCountdown(float respawnTimer)
    {
        currentRespawnTimer = respawnTimer;
        while(currentRespawnTimer > 0)
        {
            respawnTimerText.text = currentRespawnTimer.ToString();
            yield return new WaitForSeconds(1f);
            currentRespawnTimer--;
        }
        if(currentRespawnTimer <= 0)
        {
            PhotonNetwork.LoadLevel(2);
        }
    }

    public void OpenMenu()
    {
        if(menuPanel.activeSelf)
        {
            menuPanel.SetActive(false);
        }
        else
            menuPanel.SetActive(true);
    }

    public void OnClickResume()
    {
        menuPanel.SetActive(false);
    }
    
    public void OnClickLeave()
    {
        //Leave room
        menuPanel.SetActive(false);
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(0);

        base.OnLeftRoom();
    }
}
