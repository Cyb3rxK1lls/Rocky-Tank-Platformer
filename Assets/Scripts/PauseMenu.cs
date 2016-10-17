using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {

    public static bool gamePaused = false;
    public float transitionSpeed = 16;
    private RectTransform rt;
    private int offScreen = 1000;
    private bool escPressed = false;
    private float initialVolume = 1f;

	// Use this for initialization
	void Start () {
        rt = GetComponent<RectTransform>();
        offScreen = Screen.height*2;
        rt.position = new Vector2(Screen.width/2, offScreen);
        initialVolume = Camera.main.GetComponent<AudioSource>().volume;
    }

    // Update is called once per frame
    void Update () {
        rt.position = Vector2.Lerp(rt.position, new Vector2(Screen.width / 2, gamePaused? Screen.height/2 : offScreen), Time.unscaledDeltaTime * transitionSpeed);
        if ((Input.GetAxisRaw("Cancel") > 0 || Input.GetKeyDown(KeyCode.P)) && escPressed == false) {
            // Pause/unpause the game
            // Esc is held
            escPressed = true;
            if(gamePaused) {
                // Unpause
                UnpauseGame();
            } else {
                // Pause
                PauseGame();
            }
        } else if(Input.GetAxisRaw("Cancel") == 0) {
            // Esc is no longer held
            escPressed = false;
        }
    }

    public void PauseGame() {
        gamePaused = true;
        Time.timeScale = 0;
    }

    public void UnpauseGame() {
        gamePaused = false;
        Time.timeScale = 1;
    }

    public void ExitGame() {
        Application.Quit();
    }
}
