using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmExit : MonoBehaviour
{

    private bool exitGameOpened;
    private AnimationController animationController;

    // Start is called before the first frame update
    void Start()
    {
        animationController = this.GetComponent<AnimationController>();
    }


    public void OpenWindow()
    {
     
         // Open exit game window.
         animationController.OpenWindow();
         exitGameOpened = true;
     
    }

    public void CloseWindow()
    {
        // Close window animation.
        animationController.CloseWindow();
        exitGameOpened = false;
    }

    public void Exit()
    {
        Application.Quit();
    }
}
