using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierSpawn : MonoBehaviour
{
    public GameObject soldierPrefab;
    private int soldierCount = 1;
    private int MAX_Count = 5;
    private HashSet<Tuple<int, int>> soldierLocation = new HashSet<Tuple<int, int>>();
    private HashSet<Tuple<int, int>> foodLocation = new HashSet<Tuple<int, int>>();
    private float CellSize = 1f;
    // Start is called before the first frame update

    public void InitSoldiers(int count, int width, int height, float CellSize)
    {
        if(count > 0 && count < MAX_Count){
            soldierCount = count;
        }
        this.CellSize = CellSize;
        for(int i = 0; i < soldierCount; i++){
            // find a random value between 1 and 5 (6 exclusive)
            int x = UnityEngine.Random.Range(1,width);
            int y = UnityEngine.Random.Range(1,height);
            Tuple<int,int> newLoc = new Tuple<int,int>(x,y);
            if(foodLocation.Contains(newLoc) || soldierLocation.Contains(newLoc)){
                i--;
            }else{
                spawnSoldier(x, y); 
                soldierLocation.Add(newLoc);
            } 
        }
    }
    public void spawnSoldier(int x, int y){
        Vector3 pos = new Vector3((float)x*CellSize, 0f, (float)y*CellSize);
        Instantiate (soldierPrefab, pos, Quaternion.identity);
    }
}
