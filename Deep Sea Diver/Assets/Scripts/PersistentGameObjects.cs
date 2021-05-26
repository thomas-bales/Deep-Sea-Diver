using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentGameObjects : MonoBehaviour
{
    public GameObject airMeter;
    public GameObject player;
    public GameObject audio;
    public GameObject camera;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(airMeter);
        DontDestroyOnLoad(player); 
        DontDestroyOnLoad(audio);
        DontDestroyOnLoad(camera);
    }
}
