using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MazeRenderer : MonoBehaviour
{
    [SerializeField] MazeGenerator mazeGenerator;
    [SerializeField] GameObject MazeCellPrefab;
    private List<NavMeshSurface> surfaces = new List<NavMeshSurface>();


    public float CellSize = 1f;

    public int foodcount = 10;
    public int soldiercount = 1;

    private void Start(){
        MazeCell[,]maze = mazeGenerator.GetMaze();

        for(int x= 0; x<mazeGenerator.mazeWidth; x++){
            for(int y = 0; y<mazeGenerator.mazeHeight; y++){
                GameObject newCell = Instantiate(MazeCellPrefab, new Vector3((float)x*CellSize, -0.5f, (float)y*CellSize), Quaternion.identity, transform);
                MazeCellObject mazeCell = newCell.GetComponent<MazeCellObject>();
                surfaces.Add(newCell.transform.Find("Floor").GetComponent<NavMeshSurface>());
                // Determine walls active state
                bool top = maze[x,y].topWall;
                bool left = maze[x,y].leftWall;

                bool right = false;
                bool bottom = false;

                if(x == mazeGenerator.mazeWidth-1){
                    right = true;
                }
                if(y == 0){
                    bottom = true;
                }
                mazeCell.Init(top, bottom, right, left);
            }
        }

        FoodSpawn foodspawner = GetComponent<FoodSpawn>();
        foodspawner.InitFoods(foodcount, mazeGenerator.mazeWidth, mazeGenerator.mazeHeight, CellSize);
        SoldierSpawn soldierspawner = GetComponent<SoldierSpawn>();
        soldierspawner.InitSoldiers(soldiercount, mazeGenerator.mazeWidth, mazeGenerator.mazeHeight, CellSize);

        for (int i = 0; i < surfaces.Count; i++) 
        {
            Debug.Log("cell added");
            surfaces[i].BuildNavMesh();    
        }    
    }

}
