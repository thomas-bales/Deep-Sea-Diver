using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour
{
    public Animator animator;
    public Animator textAnimator;

    public Camera camera;
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Instructions()
    {
        if (animator.GetBool("instructionsIsClicked") == false)
            textAnimator.SetBool("instructionsIsClicked", true);
        else
            textAnimator.SetBool("instructionsIsClicked", false);

        animator.SetBool("instructionsIsClicked", !animator.GetBool("instructionsIsClicked"));
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
