using UnityEngine;

public class Pause : MonoBehaviour
{
    public ShipController shipController;

    #if UNITY_ANDROID
        private bool paused;
    #endif

    private AnimationController animationController;

    void Start()
    {
        animationController = this.GetComponent<AnimationController>();
    }

    // When player pressed pause or back/esc button.
    public void PauseGame()
    {
        // Pause player controller.
        shipController.Pause();
        // Open pause winodw.
        animationController.OpenWindow();
        // Used to check if game is paused/resumed.
        #if UNITY_ANDROID
            paused = true;
        #endif
    }

    // When player pressed resume or back/esc(when paused) button.
    public void ResumeGame()
    {
        
        // Resume player controller.
        shipController.Resume();
        // Close pause window.
        animationController.CloseWindow();
        // Used to check if game is paused/resumed.
        #if UNITY_ANDROID
            paused = false;
        #endif
    }

    void Update()
    {
#if UNITY_ANDROID
        // Check if back/esc button was pressed.
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            // Check if player hasn't already crashed. In this case pause will not work.
            if(!GameOver.instance.crashed)
            {
                // Check if game is paused.
                if(paused)
                {
                    ResumeGame();
                }
                else
                {
                    PauseGame();
                }
            }
        }    
#endif

    }
}
