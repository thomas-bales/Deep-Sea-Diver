using UnityEngine;

public class BubbleController : MonoBehaviour
{
    public float speed = 1f;
    public float air = 5f;

    private Transform airMeter;
    private float maxAirAmount;

    private void Start()
    {
        airMeter = GameObject.Find("Air Meter").transform;
    }

    private void Update()
    {
        //Gradually moves bubble upwards
        transform.position = new Vector3(transform.position.x, transform.position.y + speed * Time.deltaTime, transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
            fillAir();
    }

    void fillAir()
    { 
        maxAirAmount = airMeter.GetComponent<AirMeter>().maxAirAmount;

        airMeter.GetComponent<AirMeter>().airAmount += air;
        if (airMeter.GetComponent<AirMeter>().airAmount > maxAirAmount)
            airMeter.GetComponent<AirMeter>().airAmount = maxAirAmount;

        airMeter.GetComponent<AirMeter>().fillAir();

        Destroy(gameObject);
    }
}
