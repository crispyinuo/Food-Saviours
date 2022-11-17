using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    [Range(5,200)]
    public int mazeWidth = 5, mazeHeight = 5;
    public int startX, startY;
    MazeCell[,] maze;

    Vector2Int currentCell;

    // Start is called before the first frame update
    public MazeCell[,]GetMaze()
    {
        maze = new MazeCell[mazeWidth, mazeHeight];
        for(int x = 0; x < mazeWidth; x++){
            for(int y = 0; y<mazeHeight; y++){
                maze[x,y] = new MazeCell(x,y);
            }
        }
        // Start carving
        CarvePath(startX, startY);
        return maze;
    }

    List<Direction> directions = new List<Direction>{
        Direction.Up, Direction.Down, Direction.Left, Direction.Right
    };

    List<Direction> GetRandomDirections(){

        // make a copy of the directions list
        List<Direction> dir = new List<Direction>(directions);

        // make a directions list to store the randomized directions
        List<Direction> rndDir = new List<Direction>();

        while(dir.Count >0){
            int rnd = Random.Range(0,dir.Count);
            rndDir.Add(dir[rnd]);
            dir.RemoveAt(rnd);
        }

        return rndDir;
    }

    bool IsCellValid(int x, int y){
        if( x<0 || y<0 || x> mazeWidth -1 || y > mazeHeight -1 || maze[x,y].visited){
            return false;
        }else{
            return true;
        }
    }

    Vector2Int CheckNeighbours(){
        List<Direction>rndDir = GetRandomDirections();

        for (int i =0; i< rndDir.Count; i++){
            // Set neighbour coordinates to current cell for now.
            Vector2Int neighbour = currentCell;

            //Modify neighbour coordinates
            switch(rndDir[i]){
                case Direction.Up:
                    neighbour.y++;
                    break;
                case Direction.Down:
                    neighbour.y--;
                    break;
                case Direction.Right:
                    neighbour.x++;
                    break;
                case Direction.Left:
                    neighbour.x--;
                    break;
            }

            // If the neighbour we just tried is valid, we can return that neighbour
            if(IsCellValid(neighbour.x, neighbour.y)) 
                return neighbour;
        }
        // If we tried all directions and no valid neighbour, just return currentCell
        return currentCell;
    }


    // Takes in two maze positions and sets the cells accordingly
    void BreakWalls(Vector2Int firstCell, Vector2Int secondCell){
        if(firstCell.x > secondCell.x){ // first Cell' left wall
            maze[firstCell.x, firstCell.y].leftWall = false;
        }else if(firstCell.x < secondCell.x){ // second Cell' left wall
            maze[secondCell.x, secondCell.y].leftWall = false;
        }else if(firstCell.y < secondCell.y){ // first Cell' top wall
            maze[firstCell.x, firstCell.y].topWall = false;
        }else if(firstCell.y > secondCell.y){ // second Cell' top wall
            maze[secondCell.x, secondCell.y].topWall = false;
        }
    }

    // Starting at x,y, carves a path throught the maze until hitting a dead end (no valid neighbours)
    void CarvePath(int x, int y){

        if( x<0 || y<0 || x> mazeWidth -1 || y > mazeHeight -1 ){
            x = y = 0;
            Debug.LogWarning("Starting position out of bounds. Defaulting to (0,0)");
        }

        // Set the current cell to the starting position we were passed
        currentCell = new Vector2Int(x,y);

        List<Vector2Int> path = new List<Vector2Int>();

        // Loop until we encounter a dead end.
        bool deadEnd = false;
        while(!deadEnd){

            //Get the next cell we're going to try.
            Vector2Int nextCell = CheckNeighbours();

            // if the cell has no valid neighbours, set deadend to true
            if(nextCell == currentCell){

                // try to backtrack
                for(int i = path.Count -1; i>=0; i--){
                    currentCell = path[i];
                    path.RemoveAt(i);
                    nextCell = CheckNeighbours();
                    if(nextCell != currentCell) break;
                }
                if(nextCell == currentCell)
                    deadEnd = true;
            }else{
                BreakWalls(currentCell, nextCell); // set Walls
                maze[currentCell.x, currentCell.y].visited = true;
                currentCell = nextCell; 
                path.Add(currentCell); // Add to path
            }
        }



    }

}

public enum Direction{
    Up,
    Down, 
    Left,
    Right
}
public class MazeCell{
    public bool visited;
    public int x, y;

    public bool topWall;
    public bool leftWall;


    // Return the position of the cell
    public Vector2Int position{
        get{
            return new Vector2Int(x,y);
        }
    }

    public MazeCell(int x, int y){

        // The coordinates of the cell
        this.x = x;
        this.y = y;

        // Whether the cell has been visited - false to start
        visited = false;

        // All walls are present until the algorithm removes them
        topWall = leftWall = true;
    }
}
