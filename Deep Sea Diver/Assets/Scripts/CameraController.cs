using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float smoothing;
    public float minPosition, maxPosition;

    public Transform focus;

    private Vector3 targetPosition;
    private LevelLoader lvLoader;

    private void Awake()
    {
        focus = GameObject.Find("Player").transform;
        lvLoader = GameObject.FindObjectOfType<LevelLoader>();
        minPosition = lvLoader.transform.position.y + 10.6f;
    }
    void Update()
    {
        lvLoader = GameObject.FindObjectOfType<LevelLoader>();
        minPosition = lvLoader.transform.position.y + 10.6f;

        if (transform.position != focus.position && !focus.GetComponent<PlayerController>().isDead)
        {
            targetPosition = new Vector3(transform.position.x, focus.position.y, transform.position.z);

            targetPosition.y = Mathf.Clamp(targetPosition.y, minPosition, maxPosition);

            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);
        }

        
    }

}

