using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState {
    Playing,
    Paused
}

public class GameManager : MonoBehaviour {
    public GameState gameState = new GameState();

    // Start is called before the first frame update
    void Start() {
        gameState = GameState.Playing;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            SetState(gameState == GameState.Paused ? GameState.Playing : GameState.Paused);
            Debug.Log(gameState);
        }
    }

    void SetState(GameState state) {
        if (state == GameState.Paused) {
            gameState = GameState.Paused;
            Time.timeScale = 0f;
        }

        if (state == GameState.Playing) {
            gameState = GameState.Playing;
            Time.timeScale = 1f;
        }
    }
}
