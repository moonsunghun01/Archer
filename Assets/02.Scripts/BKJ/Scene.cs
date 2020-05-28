using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene : MonoBehaviour
{
    public void ChangeScene()
    {
        SceneManager.LoadScene(2);
    }

    public Animator playerAnimator;

    private void Update()
    {
        if(playerAnimator.GetBool("DIE") == true)
        {
            ChangeScene();
        }
    }
}
