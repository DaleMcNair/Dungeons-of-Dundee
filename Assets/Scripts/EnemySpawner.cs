using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
public class EnemySpawner : MonoBehaviour {
    public static EnemySpawner instance = null;
    public int minMediumPacks, minHardPakcs, minBossPacks;
    int mediumPacksCount, hardPacksCount, bossPacksCount; // Track how many of each pack we've spawned

    [SerializeField]
    List<EnemyPack> easyPacks = new List<EnemyPack> ();
    [SerializeField]
    List<EnemyPack> mediumPacks = new List<EnemyPack> ();
    [SerializeField]
    List<EnemyPack> hardPacks = new List<EnemyPack> ();
    [SerializeField]
    List<EnemyPack> bossPacks = new List<EnemyPack> ();

    private Tilemap groundTilemap, wallTilemap, pitTilemap;
    private Grid grid;

    private void Awake () {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy (gameObject);
        }

        this.grid = GameObject.FindGameObjectWithTag ("Grid").GetComponent<Grid> ();

        Tilemap[] maps = FindObjectsOfType<Tilemap> ();
        for (int i = 0; i < maps.Length; i++) {
            if (maps[i].name == "Ground") {
                groundTilemap = maps[i];
            } else if (maps[i].name == "Pit") {
                pitTilemap = maps[i];
            } else if (maps[i].name == "Wall") {
                wallTilemap = maps[i];
            }
        }

        if (groundTilemap == null || wallTilemap == null || pitTilemap == null) {
            Debug.Log ("Tilemap not found...");
        }
    }

    public void SpawnAllEnemies (List<Vector2> positions) {
        for (int i = 0; i < positions.Count; i++) {
            instance.SpawnEnemyPack (positions[i]);
        }
    }

    private void SpawnEnemyPack (Vector2 position) {
        EnemyPack pack = GetPackType ();

        for (int i = 0; i < pack.enemies.Length; i++) {
            Vector2 randomSpawnPosition = GetRandomPosition (position);
            Vector2 spawnPosition = GetCleanSpawnPosition (randomSpawnPosition);

            Instantiate (pack.enemies[i], spawnPosition, Quaternion.identity);
        }
    }

    private EnemyPack GetPackType () {
        int random = Random.Range (0, 100);
        if (random < 100) {
            return easyPacks[Random.Range (0, easyPacks.Count)];
        } else if (random < 67) {
            return mediumPacks[Random.Range (0, mediumPacks.Count)];
        } else if (random < 89) {
            return hardPacks[Random.Range (0, hardPacks.Count)];
        } else {
            return bossPacks[Random.Range (0, bossPacks.Count)];
        }
    }

    private Vector2 GetRandomPosition (Vector2 position) {
        Vector2 random = position + Random.insideUnitCircle * 5;

        return random;
    }

    private Vector2 GetCleanSpawnPosition (Vector2 position) {
        int iteration = 1;

        while (true) {
            Vector3Int pos = grid.WorldToCell (position);
            Vector3Int posBelow = new Vector3Int (pos.x, pos.y - iteration, 0);
            Vector3Int posBelowLeft = new Vector3Int (pos.x - iteration, pos.y - iteration, 0);
            Vector3Int posBelowRight = new Vector3Int (pos.x + iteration, pos.y - iteration, 0);
            Vector3Int posAbove = new Vector3Int (pos.x, pos.y + iteration, 0);
            Vector3Int posAboveLeft = new Vector3Int (pos.x - iteration, pos.y + iteration, 0);
            Vector3Int posAboveRight = new Vector3Int (pos.x + iteration, pos.y + iteration, 0);
            Vector3Int posLeft = new Vector3Int (pos.x - iteration, pos.y, 0);
            Vector3Int posRight = new Vector3Int (pos.x + iteration, pos.y, 0);

            List<Vector3Int> positions = new List<Vector3Int> { pos, posBelow, posBelowLeft, posBelowRight, posAbove, posAboveLeft, posAboveRight, posLeft, posRight };

            for (int i = 0; i < positions.Count; i++) {
                if (groundTilemap.GetTile (positions[i]) != null) {
                    return grid.CellToWorld (positions[i]);
                }
            }

            iteration++;
        }
    }
}

[System.Serializable]
public class EnemyPack {
    public Enemy[] enemies;
}