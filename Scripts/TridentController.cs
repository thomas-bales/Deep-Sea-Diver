using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TridentController : MonoBehaviour
{

    public Transform player;
    public Camera camera;

    public float throwSpeed = 1f;
    public float throwDistance = 5;
    public bool isThrown = false;

    float initX;
    float initY;
    public float timePassed = 0f;

    private void Start()
    {
        initX = transform.localPosition.x;
        initY = transform.localPosition.y;
    }
    void Update()
    {
        if (!isThrown && !player.GetComponent<PlayerController>().isDead)
            faceMouse();

        if (Input.GetMouseButtonDown(0) && !isThrown)
            throwTrident();

        if (isThrown)
        {
            moveTrident();
        }
    }

    void faceMouse()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10;
        mousePos = camera.ScreenToWorldPoint(mousePos);

        Vector2 direction = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);

        transform.up = direction;
    }

    void throwTrident()
    {
        isThrown = true;

        timePassed = 0;
        FindObjectOfType<AudioManager>().Play("Trident");
    }

    void moveTrident()
    {
        transform.Translate(0, throwSpeed * Time.deltaTime, 0);
        
        timePassed += Time.deltaTime;

        if (timePassed >= throwDistance)
        {
            resetPos();
        }
    }

    public void resetPos()
    {
        isThrown = false;
        transform.localPosition = new Vector3(initX, initY, 0);
    }
}
