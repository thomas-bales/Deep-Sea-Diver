using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyFishAI : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;
    public float pauseLength = 5f;
    public float damage = 1;
    public int health = 1;
    public int bubbleNumber = 1;
    public float knockback;

    public Animator fishAnim;

    public GameObject bubble;

    private Transform player;
    private Transform airMeter;
    private Transform trident;
    

    private Vector3 targetPosition;
    private Rigidbody2D rb;
    private float initSpeed;

    private bool isDead = false;
    private bool isHit = false;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();

        airMeter = GameObject.Find("Air Meter").GetComponent<Transform>();
        trident = GameObject.Find("Trident").GetComponent<Transform>();

        rb = gameObject.GetComponent<Rigidbody2D>();

        initSpeed = speed;
    }
    void Update()
    {
        facePlayer();
    }

    private void FixedUpdate()
    {
        followPlayer();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player" && !isDead)
            StartCoroutine("hitPlayer");

        if (collision.name == "Trident" && trident.GetComponent<TridentController>().isThrown == true && !isDead)
            StartCoroutine("hitTrident");
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name == "Trident" && trident.GetComponent<TridentController>().isThrown == true && !isDead && !isHit)
            StartCoroutine("hitTrident");
    }

    void facePlayer()
    {
        Vector2 direction = new Vector2(player.position.x - transform.position.x, player.position.y - transform.position.y);
        transform.right = -direction;

        if (player.position.x - transform.position.x >= 0 && transform.localScale.y >= 0)
            transform.localScale = new Vector3(transform.localScale.x, -transform.localScale.y, transform.localScale.z);
        else if (player.position.x - transform.position.x < 0 && transform.localScale.y < 0)
            transform.localScale = new Vector3(transform.localScale.x, -transform.localScale.y, transform.localScale.z);
    }

    void followPlayer()
    {
        rb.AddForce(-transform.right * speed);
        rb.AddForce(transform.up * rotationSpeed * speed);
    }

    IEnumerator hitPlayer()
    {
        rb.velocity = Vector2.zero;
        rb.AddForce(transform.right * knockback * 100);

        speed = 0;
        dealDamage();
        yield return new WaitForSecondsRealtime(pauseLength);

        speed = initSpeed;
    }

    IEnumerator hitTrident()
    {
        isHit = true;
        health -= 1;
        if (health <= 0)
        {
            isDead = true;
            FindObjectOfType<AudioManager>().Play("Enemy Death");

            fishAnim.SetTrigger("isDead");

            speed = 0;
            

            StartCoroutine("death");
        }
        else
        {
            fishAnim.SetTrigger("isHurt");
            FindObjectOfType<AudioManager>().Play("Enemy Hurt");
        }


        trident.GetComponent<TridentController>().resetPos();

        rb.velocity = Vector2.zero;
        rb.AddForce(transform.right * knockback * 100);

        speed = 0;
        isHit = false;

        yield return new WaitForSecondsRealtime(pauseLength);

        speed = initSpeed;
    }

    void dealDamage()
    {
        if (!player.GetComponent<PlayerController>().isDead)
        {
            airMeter.GetComponent<AirMeter>().airAmount -= damage;
            player.GetComponent<PlayerController>().takeDamage();
        }

    }

    IEnumerator death()
    {
        // Creates bubble where fish died, sets random attributes
        for (int i = 1; i <= bubbleNumber; i++)
        {
            var position = new Vector3(Random.Range(transform.position.x - 0.5f, transform.position.x + 0.5f), Random.Range(transform.position.y - 0.5f, transform.position.y + 0.5f), 0);
            GameObject newBubble = Instantiate(bubble, position, Quaternion.identity);

            newBubble.GetComponent<BubbleController>().speed = Random.Range(0.5f, 2);

        }

        yield return new WaitForSecondsRealtime(0.5f);

        Destroy(gameObject);
    }

}
