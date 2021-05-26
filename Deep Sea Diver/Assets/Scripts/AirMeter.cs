using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirMeter : MonoBehaviour
{
    public Transform player;

    private Animator playerAnim;

    public float depletionRate = 0.1f;
    public float maxAirAmount = 100f;
    public float airAmount;

    private void Start()
    {
        airAmount = maxAirAmount;
        playerAnim = player.GetComponent<Animator>();
    }
    void Update()
    {
        if (transform.localScale.x > 0)
        {
            airAmount -= (depletionRate * Time.deltaTime);
            transform.localScale = new Vector3((airAmount / maxAirAmount), transform.localScale.y, transform.localScale.z);
        }

        if (transform.localScale.x < 0)
            transform.localScale = new Vector3(0, transform.localScale.y, transform.localScale.z);

        if (airAmount <= 0 && !player.GetComponent<PlayerController>().isDead)
        {
            airAmount = 0;

            if (!player.GetComponent<PlayerController>().isDead)
                StartCoroutine(player.GetComponent<PlayerController>().death());
        }
    }

    public void fillAir()
    {
        playerAnim.SetTrigger("playerBubble");
        FindObjectOfType<AudioManager>().Play("Bubble");
    }
}
