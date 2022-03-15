using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeMechanic : MonoBehaviour
{
    public GameObject player;
    public GameObject circleStroke;

    private Rigidbody2D playerRb;
    

    private void Awake() {
        playerRb = player.GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            //freeze player
            //turn off scripts
            //turn on skillcheck mechanic
            MechanicStart();
        }
    }

    void MechanicStart()
    {
        playerRb.constraints = RigidbodyConstraints2D.FreezeAll;
        player.GetComponent<AbilityHolder>().enabled = false;
        player.GetComponent<MusicAbilityHolder>().enabled = false;

        circleStroke.SetActive(true);
    }

    public void MechanicOff()
    {
        playerRb.constraints = RigidbodyConstraints2D.None;
        player.GetComponent<AbilityHolder>().enabled = true;
        player.GetComponent<MusicAbilityHolder>().enabled = true;

        circleStroke.SetActive(false);
    }
}
