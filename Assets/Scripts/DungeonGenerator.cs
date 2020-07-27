using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;
using System.ComponentModel;
using UnityEditorInternal;

[RequireComponent(typeof(EnemySpawner))]
public class DungeonGenerator : MonoBehaviour
{
    [Header("Ground")]
    [SerializeField]
    private Tile[] groundTiles;
    [SerializeField]
    
    [Header("Pit")]
    private Tile pitTile;

    [Header("Top Walls")]
    [SerializeField]
    private Tile[] topWallTiles;
    [SerializeField]
    private Tile[] topWallTilesAbove;
    [SerializeField]
    private Tile topLeftWallTile;
    [SerializeField]
    private Tile topRightWallTile;

    [Header("Bottom Walls")]
    [SerializeField]
    private Tile bottomLeftWallTile;
    [SerializeField]
    private Tile bottomRightWallTile;
    [SerializeField]
    private Tile bottomLeftCornerWallTile;
    [SerializeField]
    private Tile bottomRightCornerWallTile;
    [SerializeField]
    private Tile bottomWallTile;

    [Header("Side Walls")]
    [SerializeField]
    private Tile topLeftCornerWallTile;
    [SerializeField]
    private Tile topRightCornerWallTile;
    [SerializeField]
    private Tile sideWallLeftMid;
    [SerializeField]
    private Tile sideWallRightMid;
    
    [SerializeField]
    private Tile botLeftWallTile;
    [SerializeField]
    private Tile botRightWallTile;
    [SerializeField]
    private Tilemap groundMap;
    [SerializeField]
    private Tilemap pitMap;
    [SerializeField]
    private Tilemap wallMap;

    [SerializeField]
    private GameObject portal;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private Tile redTile;
    public GameObject waypointInactive;
    public GameObject waypointActive;
    public GameObject redboi;
    private int roomSpawns = 0;

    [Header("Map Settings")]
    [SerializeField]
    private int deviationRate = 10;
    [SerializeField]
    private int roomRate = 15;
    [SerializeField]
    private int maxRouteLength;
    [SerializeField]
    private int maxRoutes = 20;


    private int routeCount = 0;
    Dictionary<Vector2, float> portalPositions = new Dictionary<Vector2, float>();
    List<Vector2> spawnPoints = new List<Vector2>();
    List<Vector2> waypointPositions = new List<Vector2>();
    List<Vector2> enemyPositions = new List<Vector2>();

    private void Start()
    {
        GenerateDungeon();
    }

    public void GenerateDungeon()
    {
        ClearDungeon();

        int x = 0;
        int y = 0;
        int routeLength = 0;
        //AddWaypoint(new Vector2(0, 0));

        GenerateSquare(x, y, 2);

        Vector2Int previousPos = new Vector2Int(x, y);
        y += 3;

        GenerateSquare(x, y, 1);
        NewRoute(x, y, routeLength, previousPos);

        SpawnPortal();
        SpawnWaypoints();
        SpawnEnemyHubPositions();
        EnemySpawner.instance.SpawnAllEnemies(enemyPositions);

        FillGaps();

        FillWalls();
    }

    private void ClearDungeon()
    {
        groundMap.ClearAllTiles();
        groundMap.ResizeBounds();
        groundMap.RefreshAllTiles();
        groundMap.ClearAllEditorPreviewTiles();

        pitMap.ClearAllTiles();
        pitMap.ResizeBounds();
        pitMap.RefreshAllTiles();
        pitMap.ClearAllEditorPreviewTiles();

        wallMap.ClearAllTiles();
        wallMap.ResizeBounds();
        wallMap.RefreshAllTiles();
        wallMap.ClearAllEditorPreviewTiles();

    }

    private void FillWalls()
    {
        BoundsInt bounds = groundMap.cellBounds;

        for (int xMap = bounds.xMin - 10; xMap <= bounds.xMax + 10; xMap++)
        {
            for (int yMap = bounds.yMin - 10; yMap <= bounds.yMax + 10; yMap++)
            {
                Vector3Int pos = new Vector3Int(xMap, yMap, 0);
                Vector3Int posBelow = new Vector3Int(xMap, yMap - 1, 0);
                Vector3Int posBelowLeft = new Vector3Int(xMap - 1, yMap - 1, 0);
                Vector3Int posBelowRight = new Vector3Int(xMap + 1, yMap - 1, 0);
                Vector3Int posAbove = new Vector3Int(xMap, yMap + 1, 0);
                Vector3Int posAboveLeft = new Vector3Int(xMap - 1, yMap + 1, 0);
                Vector3Int posAboveRight = new Vector3Int(xMap + 1, yMap + 1, 0);

                Vector3Int posLeft = new Vector3Int(xMap - 1, yMap, 0);
                Vector3Int posRight = new Vector3Int(xMap + 1, yMap, 0);

                Vector3Int posAboveAbove = new Vector3Int(xMap, yMap + 2, 0);
                Vector3Int posBelowBelow = new Vector3Int(xMap, yMap - 2, 0);

                TileBase tile = groundMap.GetTile(pos);

                TileBase tileBelow = groundMap.GetTile(posBelow);
                TileBase tileBelowLeft = groundMap.GetTile(posBelowLeft);
                TileBase tileBelowRight = groundMap.GetTile(posBelowRight);
                TileBase tileBelowBelow = groundMap.GetTile(posBelowBelow);

                TileBase tileAbove = groundMap.GetTile(posAbove);
                TileBase tileAboveLeft = groundMap.GetTile(posAboveLeft);
                TileBase tileAboveRight = groundMap.GetTile(posAboveRight);
                TileBase tileAboveAbove = groundMap.GetTile(posAboveAbove);


                TileBase tileLeft = groundMap.GetTile(posLeft);
                TileBase tileRight = groundMap.GetTile(posRight);

                if (tile == null)
                {
                    pitMap.SetTile(pos, pitTile);

                    if (tileBelow != null)
                    {
                        wallMap.SetTile(pos, GetTopWallTile());
                        wallMap.SetTile(posAbove, topWallTilesAbove[0]);

                        if (tileAboveAbove)
                        {
                            if (tileRight != null) {
                                wallMap.SetTile(new Vector3Int(posAbove.x, posAbove.y, 1), topRightCornerWallTile);
                                wallMap.SetTile(new Vector3Int(posAbove.x, posAbove.y, 2), topWallTilesAbove[0]);
                                wallMap.SetTile(new Vector3Int(posAboveLeft.x, posAboveLeft.y, 1), topLeftWallTile);
                                wallMap.SetTile(new Vector3Int(posAboveLeft.x, posAboveLeft.y, 2), bottomLeftWallTile);
                                wallMap.SetTile(new Vector3Int(posLeft.x, posLeft.y, 2), sideWallLeftMid);
                            }
                            else if (tileLeft != null)
                            {
                                wallMap.SetTile(new Vector3Int(posAbove.x, posAbove.y, 1), topLeftCornerWallTile);
                                wallMap.SetTile(new Vector3Int(posAbove.x, posAbove.y, 2), topWallTilesAbove[0]);
                                wallMap.SetTile(new Vector3Int(posAboveRight.x, posAboveRight.y, 1), topRightWallTile);
                                wallMap.SetTile(new Vector3Int(posAboveRight.x, posAboveRight.y, 2), bottomRightWallTile);
                                wallMap.SetTile(new Vector3Int(posRight.x, posRight.y, 2), sideWallRightMid);

                            } else if (tileLeft == null && tileRight == null)
                            {
                                if (tileBelowLeft == null)
                                {
                                    wallMap.SetTile(new Vector3Int(posLeft.x, posLeft.y, 1), sideWallLeftMid);
                                    wallMap.SetTile(new Vector3Int(posAboveLeft.x, posAboveLeft.y, 1), topLeftWallTile);
                                } else if (tileBelowRight == null)
                                {
                                    wallMap.SetTile(new Vector3Int(posRight.x, posRight.y, 1), sideWallRightMid);
                                    wallMap.SetTile(new Vector3Int(posAboveRight.x, posAboveRight.y, 1), topRightWallTile);
                                }
                                wallMap.SetTile(new Vector3Int(posAbove.x, posAbove.y, 1), topWallTilesAbove[0]);
                                wallMap.SetTile(new Vector3Int(posAbove.x, posAbove.y, 2), bottomWallTile);
                            }
                        }

                        else if (tileBelowLeft == null)
                        {
                            wallMap.SetTile(posAboveLeft, topLeftWallTile);
                            wallMap.SetTile(new Vector3Int(posLeft.x, posLeft.y, 2), sideWallLeftMid);
                        }

                        else if (tileBelowRight == null)
                        {
                            wallMap.SetTile(posAboveRight, topRightWallTile);
                            wallMap.SetTile(new Vector3Int(posRight.x, posRight.y, 2), sideWallRightMid);
                        }

                        else if (tileRight != null && tileAboveRight == null)
                        {
                            wallMap.SetTile(new Vector3Int(posAbove.x, posAbove.y, 1), bottomRightCornerWallTile);
                            wallMap.SetTile(posAboveAbove, topLeftWallTile);
                        }

                        else if (tileLeft != null && tileAboveLeft == null)
                        {
                            wallMap.SetTile(new Vector3Int(posAbove.x, posAbove.y, 1), bottomLeftCornerWallTile);
                            wallMap.SetTile(posAboveAbove, topRightWallTile);
                        }

                        else if (tileRight != null && tileLeft == null)
                        {
                            wallMap.SetTile(pos, GetTopWallTile());
                            wallMap.SetTile(posAbove, bottomRightCornerWallTile);
                        }

                        else if (tileRight == null && tileLeft != null)
                        {
                            wallMap.SetTile(pos, GetTopWallTile());
                            wallMap.SetTile(posAbove, bottomLeftCornerWallTile);
                        }

                        else if (tileRight != null && tileLeft == null)
                        {
                            wallMap.SetTile(new Vector3Int(posAbove.x, posAbove.y, 2), topRightCornerWallTile);
                        }
                    }

                    else if (tileAbove != null)
                    {
                        if (tileRight != null && tileLeft == null)
                        {
                            wallMap.SetTile(pos, topRightCornerWallTile);

                            if (tileAboveLeft == null)
                            {
                                wallMap.SetTile(posLeft, bottomLeftWallTile);
                            }
                        }

                        else if (tileRight == null && tileLeft != null)
                        {
                            wallMap.SetTile(pos, topLeftCornerWallTile);

                            if (tileAboveRight == null)
                            {
                                wallMap.SetTile(posRight, bottomRightWallTile);
                            }
                        }

                        else if (tileAboveLeft == null && tileAboveRight == null)
                        {
                            wallMap.SetTile(pos, bottomWallTile);
                        }
                        else if (tileAboveLeft == null)
                        {
                            wallMap.SetTile(pos, bottomWallTile);
                            wallMap.SetTile(posLeft, bottomLeftWallTile);
                        }
                        else if (tileAboveRight == null)
                        {
                            wallMap.SetTile(pos, bottomWallTile);
                            wallMap.SetTile(posRight, bottomRightWallTile);
                        }
                        else
                        {
                            wallMap.SetTile(pos, bottomWallTile);
                        }
                        //

                    }

                    else if (tileLeft != null)
                    {
                        if (tileBelowBelow != null)
                        {
                            wallMap.SetTile(pos, bottomLeftCornerWallTile);
                        }
                        else if (tileAboveLeft == null)
                        {
                            wallMap.SetTile(pos, sideWallRightMid);
                            wallMap.SetTile(posAbove, sideWallRightMid);
                            wallMap.SetTile(posAboveAbove, topRightWallTile);
                        }
                        else
                        {
                            wallMap.SetTile(pos, sideWallRightMid);
                        }
                    }

                    else if (tileRight != null)
                    {
                        if (tileBelowBelow != null)
                        {
                            wallMap.SetTile(pos, bottomRightCornerWallTile);
                        }
                        else if (tileAboveRight == null)
                        {
                            wallMap.SetTile(pos, sideWallLeftMid);
                            wallMap.SetTile(posAbove, sideWallLeftMid);
                            wallMap.SetTile(posAboveAbove, topLeftWallTile);
                        }
                        else
                        {
                            wallMap.SetTile(pos, sideWallLeftMid);
                        }
                    }
                }
            }
        }
    }

    private void NewRoute(int x, int y, int routeLength, Vector2Int previousPos)
    {
        if (routeCount < maxRoutes)
        {
            routeCount++;
            while (++routeLength < maxRouteLength)
            {
                //Initialize
                bool routeUsed = false;
                int xOffset = x - previousPos.x; //0
                int yOffset = y - previousPos.y; //3
                int roomSize = 3; //Hallway size
                if (Random.Range(1, 100) <= roomRate)
                    roomSize = Random.Range(3, 6);
                previousPos = new Vector2Int(x, y);

                //Go Straight
                if (Random.Range(1, 100) <= deviationRate)
                {
                    if (routeUsed)
                    {
                        GenerateSquare(previousPos.x + xOffset, previousPos.y + yOffset, roomSize);
                        NewRoute(previousPos.x + xOffset, previousPos.y + yOffset, Random.Range(routeLength, maxRouteLength), previousPos);
                    }
                    else
                    {
                        AddSpawnPoint(new Vector2(x, y));

                        x = previousPos.x + xOffset;
                        y = previousPos.y + yOffset;
                        GenerateSquare(x, y, roomSize);
                        routeUsed = true;
                    }
                }

                //Go left
                if (Random.Range(1, 100) <= deviationRate)
                {
                    if (routeUsed)
                    {
                        GenerateSquare(previousPos.x - yOffset, previousPos.y + xOffset, roomSize);
                        NewRoute(previousPos.x - yOffset, previousPos.y + xOffset, Random.Range(routeLength, maxRouteLength), previousPos);
                    }
                    else
                    {
                        AddSpawnPoint(new Vector2(x, y));

                        y = previousPos.y + xOffset;
                        x = previousPos.x - yOffset;
                        GenerateSquare(x, y, roomSize);
                        routeUsed = true;
                    }
                }
                //Go right
                if (Random.Range(1, 100) <= deviationRate)
                {
                    if (routeUsed)
                    {
                        GenerateSquare(previousPos.x + yOffset, previousPos.y - xOffset, roomSize);
                        NewRoute(previousPos.x + yOffset, previousPos.y - xOffset, Random.Range(routeLength, maxRouteLength), previousPos);
                    }
                    else
                    {
                        AddSpawnPoint(new Vector2(x, y));

                        y = previousPos.y - xOffset;
                        x = previousPos.x + yOffset;
                        GenerateSquare(x, y, roomSize);
                        routeUsed = true;
                    }
                }

                if (!routeUsed)
                {
                    AddSpawnPoint(new Vector2(x, y));

                    x = previousPos.x + xOffset;
                    y = previousPos.y + yOffset;
                    GenerateSquare(x, y, roomSize);
                }
            }
        }

        try { portalPositions.Add(new Vector2(x, y), Vector2.Distance(new Vector2(0, 0), new Vector2(x, y))); }catch(System.Exception ex)
        {
            Debug.Log("Error adding Portal spawn location to portalPositions Dict: " + ex.Message);
        }
    }

    private void FillGaps()
    {
        BoundsInt bounds = groundMap.cellBounds;
        bool filledGaps = false;

        for (int xMap = bounds.xMin - 10; xMap <= bounds.xMax + 10; xMap++)
        {
            for (int yMap = bounds.yMin - 10; yMap <= bounds.yMax + 10; yMap++)
            {
                if (groundMap.GetTile(new Vector3Int(xMap, yMap, 0)) == null)
                {
                    int count = 0; // used to track adjoining squares

                    Dictionary<int, Vector3Int> tiles = new Dictionary<int, Vector3Int>();
                    tiles.Add(1, new Vector3Int(xMap - 1, yMap + 1, 0));
                    tiles.Add(2, new Vector3Int(xMap, yMap + 1, 0));
                    tiles.Add(3, new Vector3Int(xMap + 1, yMap + 1, 0));

                    tiles.Add(4, new Vector3Int(xMap - 1, yMap, 0));
                    tiles.Add(5, new Vector3Int(xMap, yMap, 0));
                    tiles.Add(6, new Vector3Int(xMap + 1, yMap, 0));

                    tiles.Add(7, new Vector3Int(xMap - 1, yMap - 1, 0));
                    tiles.Add(8, new Vector3Int(xMap, yMap - 1, 0));
                    tiles.Add(9, new Vector3Int(xMap + 1, yMap - 1, 0));

                    foreach (KeyValuePair<int, Vector3Int> entry in tiles)
                    {
                        if (groundMap.GetTile(entry.Value) != null) count++;
                    }

                    if (count >= 5)
                    {
                        //groundMap.SetTile(new Vector3Int(xMap, yMap, 0), GetGroundTile());
                        filledGaps = true;
                        groundMap.SetTile(new Vector3Int(xMap, yMap, 0), GetGroundTile());

                    }
                }
            }
        }

        // Run recursively til we're sweet to go
        if (filledGaps)
        {
            FillGaps();
        }
    }

    private void GenerateSquare(int x, int y, int radius)
    {
        for (int tileX = x - radius; tileX <= x + radius; tileX++)
        {
            for (int tileY = y - radius; tileY <= y + radius; tileY++)
            {
                Vector3Int tilePos = new Vector3Int(tileX, tileY, 0);
                groundMap.SetTile(tilePos, GetGroundTile());
            }
        }
    }

    private void SpawnPortal()
    {
        Vector2 furthestPortal = new Vector2();
        float distance = -100f;

        foreach (KeyValuePair<Vector2, float> item in portalPositions)
        {
            if (item.Value > distance)
            {
                distance = item.Value;
                furthestPortal = item.Key;
            }
        }

        Instantiate(portal, furthestPortal, Quaternion.identity);
    }

    private Tile GetGroundTile()
    {
        if (Random.Range(0, 100) < 96)
        {
            return groundTiles[0];
        }
        else
        {
            return groundTiles[Random.Range(1, groundTiles.Length)];
        }
    }

    private void AddSpawnPoint(Vector2 position) {
        spawnPoints.Add(position);
    }
    
    private void SpawnWaypoints()
    {
        Vector2 offset = new Vector2(-1.4f, -1.6f);

        for (int i = 0; i < spawnPoints.Count; i++) {
            if (i == 0)
            {
                Instantiate(waypointActive, spawnPoints[i] + offset, Quaternion.identity);
                waypointPositions.Add(spawnPoints[i]);
            } else {
                bool spawnWaypoint = true;

                for (int j = 0; j < waypointPositions.Count; j++)
                {
                    if (Vector2.Distance(spawnPoints[i], waypointPositions[j]) < 50)
                    {
                        spawnWaypoint = false;
                    }
                }
                
                if (spawnWaypoint) {
                    Instantiate(waypointActive, spawnPoints[i] + offset, Quaternion.identity);
                    waypointPositions.Add(spawnPoints[i]);
                }
            }
        }
    }

    private void SpawnEnemyHubPositions()
    {
        int minSpawnDistance = 12;
        int maxSpawnDistance = 30;
        int nextSpawnDistance = 20;

        for (int i = 0; i < spawnPoints.Count; i++)
        {
            bool spawnEnemy = true;
            for (int j = 0; j < enemyPositions.Count; j++)
            {
                if (Vector2.Distance(spawnPoints[i], enemyPositions[j]) < nextSpawnDistance)
                {
                    spawnEnemy = false;
                }
            }

            if (spawnEnemy)
            {
                for (int x = 0; x < waypointPositions.Count; x++)
                {
                    if (Vector2.Distance(spawnPoints[i], waypointPositions[x]) < 10)
                    {
                        spawnEnemy = false;
                    }
                }
            }

            if (spawnEnemy)
            {
                enemyPositions.Add(spawnPoints[i]);
                nextSpawnDistance = Random.Range(minSpawnDistance, maxSpawnDistance);
            }
        }
    }

    private Tile GetTopWallTile()
    {
        if (Random.Range(0, 100) < 90) {
            return topWallTiles[0];
        } else
        {
            return topWallTiles[Random.Range(1, topWallTiles.Length)];
        }
    }
}