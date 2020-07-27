using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner instance = null;

    [SerializeField]
    List<EnemyPack> packs = new List<EnemyPack>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void SpawnAllEnemies(List<Vector2> positions)
    {
        for (int i = 0; i < positions.Count; i++)
        {
            instance.SpawnEnemyPack(positions[i]);
        }
    }

    private void SpawnEnemyPack(Vector2 position)
    {
        Instantiate(instance.packs[0].enemies[0], position, Quaternion.identity);
    }
}

[System.Serializable]
public class EnemyPack
{
    public enum PackType { Easy, Medium, Hard }
    public PackType packType;
    public Enemy[] enemies;
}