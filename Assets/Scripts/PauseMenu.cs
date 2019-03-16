using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

    public GameObject pauseMenuUI;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (GameManager.instance.gameState != GameState.Paused) {
                Pause();
            }
            else {
                Resume();
            }
        }
    }

    public void Pause() {
        Time.timeScale = 0f;
        GameManager.instance.SetState(GameState.Paused);
        pauseMenuUI.SetActive(true);
    }


    public void Resume() {
        Time.timeScale = 1f;
        GameManager.instance.SetState(GameState.Playing);
        pauseMenuUI.SetActive(false);
    }
}
