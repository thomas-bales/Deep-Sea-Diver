using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EelAI : MonoBehaviour
{
    public float initSpeed;
    public float damage = 1;
    public int health = 1;
    public int bubbleNumber = 2;
    public float pauseLength = 3;
    public float pathLength = 10f;
    public bool faceRight = true;
    public PolygonCollider2D collider;

    public GameObject bubble;

    private Transform player;
    private Transform airMeter;
    private Transform trident;
    private Animator fishAnim;

    private Rigidbody2D rb;
    private float deltaDistance = 0;
    private float speed;

    private bool isDead = false;
    private bool isHit = false;


    private void Awake()
    {
        fishAnim = gameObject.GetComponent<Animator>();
        speed = initSpeed;
    }


    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();

        airMeter = GameObject.Find("Air Meter").GetComponent<Transform>();
        trident = GameObject.Find("Trident").GetComponent<Transform>();

        rb = gameObject.GetComponent<Rigidbody2D>();
    }


    private void FixedUpdate()
    {
        if (!isDead)
            followPath();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player" && !isDead)
            hitPlayer();

        if (collision.name == "Trident" && trident.GetComponent<TridentController>().isThrown == true && !isDead)
        {
            health -= 1;
            if (health <= 0)
            {
                Destroy(collider);
                StartCoroutine("death");
            }
            else
                StartCoroutine("hitTrident");
        }  
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name == "Trident" && trident.GetComponent<TridentController>().isThrown == true && !isDead && !isHit)
        {
            isHit = true;
            health -= 1;
            if (health <= 0)
            {
                Destroy(collider);
                StartCoroutine("death");
            }
            else
                StartCoroutine("hitTrident");
        }
    }

    void followPath()
    {
        transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
        deltaDistance += Mathf.Abs(speed * Time.deltaTime);

        if (deltaDistance >= pathLength)
        {
            deltaDistance = 0;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            speed = -speed;
        }
    }

    void hitPlayer()
    {
        dealDamage();
    }

    IEnumerator hitTrident()
    {
         speed = 0;
         FindObjectOfType<AudioManager>().Play("Enemy Hurt");
         fishAnim.SetTrigger("isHurt");

         trident.GetComponent<TridentController>().resetPos();

         if (transform.localScale.x < 0)
             speed *= 0.5f;
         yield return new WaitForSecondsRealtime(0.05f);
         speed = 0;
         yield return new WaitForSecondsRealtime(0.1f);
         if (transform.localScale.x < 0)
             speed = initSpeed * 0.5f;
         else
             speed = initSpeed * 0.5f;
         yield return new WaitForSecondsRealtime(0.05f);
         if (transform.localScale.x < 0)
             speed = -initSpeed;
         else
             speed = initSpeed;

        isHit = false;
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
        speed = 0;
        isDead = true;
        FindObjectOfType<AudioManager>().Play("Enemy Death");
        fishAnim.SetTrigger("isDead");

        trident.GetComponent<TridentController>().resetPos();
        
        // Creates bubble where fish died, sets random attributes
        for (int i = 1; i <= bubbleNumber; i++)
        {
            var position = new Vector3(Random.Range(transform.position.x - 0.5f, transform.position.x + 0.5f), Random.Range(transform.position.y - 0.5f, transform.position.y + 0.5f), 0);
            GameObject newBubble = Instantiate(bubble, position, Quaternion.identity);

            newBubble.GetComponent<BubbleController>().speed = Random.Range(0.5f, 2);
        }

        

        yield return new WaitForSecondsRealtime(0.5f);
        Destroy(gameObject);
        Debug.Log("Eel Destroyed");
    }
}
