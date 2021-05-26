using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    private Transform player;

    public int levelToLoad;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
            if (!collision.GetComponent<PlayerController>().isDead)
                loadLevel();
    }

    void loadLevel()
    {
        SceneManager.LoadScene(levelToLoad);
        player.position = new Vector3(player.position.x, 9, player.position.z);
        player.GetComponent<Rigidbody2D>().velocity = Vector3.zero; 
    }
}
