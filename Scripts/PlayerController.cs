using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform airMeter;
    public Camera camera;
    public PolygonCollider2D collider;

    public float thrust = 1f;
    public float speed = 1f;
    public float maxSpeed = 1f;
    public bool isDead = false;

    [SerializeField]
    private Animator animator;
    [SerializeField]
    private Animator playerAnim;

    private bool isMovingUp = false;
    
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10;
        mousePos = camera.ScreenToWorldPoint(mousePos);

        //Causes character to face correct direction
        if (mousePos.x - transform.position.x >= 0 && transform.localScale.x >= 0)
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        else if (mousePos.x - transform.position.x < 0 && transform.localScale.x < 0)
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);

        if (Input.GetKeyDown("space"))
            isMovingUp = true;
    }

    private void FixedUpdate()
    {
        if (!isDead)
            movePlayer();
    }

    void movePlayer()
    {
        if (isMovingUp)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(transform.up * thrust);

            isMovingUp = false;
        }

        if (Input.GetKey("a"))
        {
            rb.AddForce(-transform.right * speed);
        }
        else if (Input.GetKey("d"))
        {
            rb.AddForce(transform.right * speed);
        }

        if (rb.velocity.x > maxSpeed)
        {
            rb.velocity = new Vector2(maxSpeed, rb.velocity.y);
        }
        else if (rb.velocity.x < -maxSpeed)
        {
            rb.velocity = new Vector2(-maxSpeed, rb.velocity.y);
        }
    }

    public void takeDamage()
    {
        playerAnim.SetTrigger("playerHurt");
        FindObjectOfType<AudioManager>().Play("Player Hurt");
    }

    public IEnumerator death()
    {
        Destroy(collider);

        isDead = true;
        playerAnim.SetBool("playerDead", true);
        FindObjectOfType<AudioManager>().Play("Player Death");

        transform.GetComponent<PolygonCollider2D>().isTrigger = true;

        animator.SetBool("playerIsDead", true);

        yield return new WaitForSecondsRealtime(5);

        //Load Title Screen
        Destroy(GameObject.Find("Main Camera"));
        Destroy(GameObject.Find("Player"));
        Destroy(GameObject.Find("UI"));
        Destroy(GameObject.Find("Audio Manager"));

        playerAnim.SetBool("playerDead", false);

        SceneManager.LoadScene(0);
    }
}