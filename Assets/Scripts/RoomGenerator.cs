﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator : MonoBehaviour {
    public Map map;

    public enum RoomTheme { Normal, Royal, Etc }
    public RoomTheme roomTheme;

    public GameObject[] floorPrefabs, wallPrefabsTop, wallPrefabsBottom, wallPrefabsSide, wallPrefabsTopCorner, outerTopCornerWall, wallPrefabsBottomCorner, outerTopWallPrefabs, doorPrefabsTop, doorPrefabsBottom, doorPrefabsSide, obstacles;
    int[] rotations = { 0, 90, 180, 270 };

    List<Coord> allTileCoords;
    Transform[,] tileMap;
    float tileSize = 1f;

    public void GenerateRoom() {
        tileMap = new Transform[map.mapSize.x, map.mapSize.y];


        // Generating Coords
        allTileCoords = new List<Coord>();
        for (int x = 0; x < map.mapSize.x; x++) {
            for (int y = 0; y < map.mapSize.y; y++) {
                allTileCoords.Add(new Coord(x, y));
            }
        }

        // Create map holder object
        string holderName = "Generated Room";
        if (transform.Find(holderName)) {
            DestroyImmediate(transform.Find(holderName).gameObject);
        }

        Transform mapHolder = new GameObject(holderName).transform;
        mapHolder.parent = transform;

        // Spawn Floor Tiles
        for (int x = 0; x < map.mapSize.x; x++) {
            for (int y = 0; y < map.mapSize.y; y++) {
                Vector2 tilePosition = CoordToPosition(x, y);
                GameObject newTile = Instantiate(floorPrefabs[Random.Range(0, floorPrefabs.Length)], tilePosition, Quaternion.Euler(Vector3.forward * rotations[Random.Range(0, rotations.Length - 1)]));
                newTile.transform.parent = mapHolder;
                tileMap[x, y] = newTile.transform;
            }
        }

        // Spawn Top Walls
        for (int x = (-map.mapSize.x / 2) + (int)tileSize; x < map.mapSize.x / 2 + tileSize; x = x + 2) {
            float yPos = (map.mapSize.y / 2) + tileSize;
            float topYPos = (map.mapSize.y / 2) + tileSize + tileSize * 2;

            // Spawn Corners
            if (x == (-map.mapSize.x / 2) + (int)tileSize) {
                GameObject leftCornerWall = Instantiate(wallPrefabsTopCorner[Random.Range(0, wallPrefabsTopCorner.Length)], new Vector2(x - tileSize * 2, yPos), Quaternion.identity);
                GameObject rightCornerWall = Instantiate(wallPrefabsTopCorner[Random.Range(0, wallPrefabsTopCorner.Length)], new Vector2(map.mapSize.x / 2 + tileSize, yPos), Quaternion.identity);
                rightCornerWall.GetComponent<SpriteRenderer>().flipX = true;
                leftCornerWall.transform.parent = mapHolder;
                rightCornerWall.transform.parent = mapHolder;
                Debug.Log("parsing");
            }

            GameObject newTopLeftCornerWall = Instantiate(outerTopCornerWall[Random.Range(0, outerTopCornerWall.Length)], new Vector2(x - tileSize * 2, topYPos), Quaternion.identity);
            GameObject newTopRightCornerWall = Instantiate(outerTopCornerWall[Random.Range(0, outerTopCornerWall.Length)], new Vector2(map.mapSize.x / 2 + tileSize, topYPos), Quaternion.identity);
            newTopRightCornerWall.GetComponent<SpriteRenderer>().flipX = true;

            GameObject newWall = Instantiate(wallPrefabsTop[Random.Range(0, wallPrefabsTop.Length)], new Vector2(x, yPos), Quaternion.identity);
            GameObject newTopWall = Instantiate(outerTopWallPrefabs[Random.Range(0, outerTopWallPrefabs.Length)], new Vector2(x, topYPos), Quaternion.identity);
            newWall.transform.parent = mapHolder;
            newTopWall.transform.parent = mapHolder;
            newTopLeftCornerWall.transform.parent = mapHolder;
            newTopRightCornerWall.transform.parent = mapHolder;
        }

        // Spawn Bottom Walls
        for (int x = (-map.mapSize.x / 2) + (int)tileSize; x < map.mapSize.x / 2 + tileSize; x = x + 2) {
            float yPos = (-map.mapSize.y / 2) - tileSize;

            // Spawn Corners
            if (x == (-map.mapSize.y / 2) + tileSize) {
                GameObject leftCornerWall = Instantiate(wallPrefabsBottomCorner[0], new Vector2(-map.mapSize.x / 2 - tileSize, yPos), Quaternion.identity);
                GameObject rightCornerWall = Instantiate(wallPrefabsBottomCorner[0], new Vector2(map.mapSize.x / 2 + tileSize, yPos), Quaternion.identity);
                leftCornerWall.GetComponent<SpriteRenderer>().flipX = true;
                leftCornerWall.transform.parent = mapHolder;
                rightCornerWall.transform.parent = mapHolder;
            }

            GameObject newWall = Instantiate(wallPrefabsBottom[Random.Range(0, wallPrefabsBottom.Length)], new Vector2(x, yPos), Quaternion.identity);
            newWall.transform.parent = mapHolder;
        }

        // Spawn Left Walls
        for (int y = (-map.mapSize.y / 2) + (int)tileSize; y < (map.mapSize.y / 2) + tileSize; y = y + 2) {
            float xPos = (-map.mapSize.x / 2) - tileSize;
            GameObject newWall = Instantiate(wallPrefabsSide[Random.Range(0, wallPrefabsSide.Length)], new Vector2(xPos, y), Quaternion.identity);
            newWall.GetComponent<BoxCollider2D>().offset = new Vector2(tileSize * .75f, 0);
            newWall.transform.parent = mapHolder;
        }

        // Spawn Right Walls
        for (int y = (-map.mapSize.y / 2) + (int)tileSize; y < (map.mapSize.y / 2); y = y + 2) {
            float xPos = (map.mapSize.x / 2) + tileSize;
            GameObject newWall = Instantiate(wallPrefabsSide[Random.Range(0, wallPrefabsSide.Length)], new Vector2(xPos, y), Quaternion.identity);
            newWall.GetComponent<SpriteRenderer>().flipX = true;
            newWall.transform.parent = mapHolder;
        }
    }

    Vector3 CoordToPosition(int x, int y) {
        return new Vector2(-map.mapSize.x / 2f + 0.5f + x, -map.mapSize.y / 2f + 0.5f + y) * tileSize;
    }

    [System.Serializable]
    public struct Coord {
        public int x;
        public int y;

        public Coord(int _x, int _y) {
            x = _x;
            y = _y;
        }

        public static bool operator ==(Coord c1, Coord c2) {
            return c1.x == c2.x && c1.y == c2.y;
        }
        public static bool operator !=(Coord c1, Coord c2) {
            return !(c1 == c2);
        }
    }

    [System.Serializable]
    public class Map {

        public Coord mapSize;

        public Coord mapCentre {
            get {
                return new Coord(mapSize.x / 2, mapSize.y / 2);
            }
        }
    }
}
