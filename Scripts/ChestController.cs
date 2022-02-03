using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChestController : MonoBehaviour
{
    public Animator Win;
    public Animator Chest;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            StartCoroutine("PlayerWon");
            Debug.Log("You Won");
        }
        
    }

    IEnumerator PlayerWon()
    {
        Win.SetBool("playerHasWon", true);
        Chest.SetBool("playerHasWon", true);

        yield return new WaitForSecondsRealtime(5);

        //Load Title Screen
        Destroy(GameObject.Find("Main Camera"));
        Destroy(GameObject.Find("Player"));
        Destroy(GameObject.Find("UI"));
        Destroy(GameObject.Find("Audio Manager"));

        SceneManager.LoadScene(0);
    }
}
