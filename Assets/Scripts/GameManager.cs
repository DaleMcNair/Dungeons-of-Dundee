using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState {
    Playing,
    Paused
}

public class GameManager : MonoBehaviour {
    public static GameManager instance; // Singleton
    public GameState gameState = new GameState();

    private void Awake() {
        if (instance != null) {
            Destroy(gameObject);
        }
        else {
            instance = this; // Set singleton
            DontDestroyOnLoad(gameObject); // Persist this object even through scene changes
        }
    }

    void Start() {
        gameState = GameState.Playing;
    }

    public void SetState(GameState state) {
        gameState = state;
    }
}
