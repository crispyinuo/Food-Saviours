using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawn : MonoBehaviour
{

    public GameObject prefab1, prefab2, prefab3, prefab4, prefab5, prefab6, prefab7, prefab8, prefab9, prefab10;
    private int foodCount = 10;

    private float CellSize = 1f;
    // Start is called before the first frame update
    public void InitFoods(int count, int width, int height, float CellSize)
    {
        if(count > 0){
            foodCount = count;
        }
        this.CellSize = CellSize;
        for(int i = 0; i < foodCount; i++){
            // find a random value between 1 and 5 (6 exclusive)
            int x = Random.Range(0,width);
            int y = Random.Range(0,height);
            int whatToSpawn = Random.Range(1,6);
            spawn(whatToSpawn, x, y);  
        }
    }

    public void spawn(int whatToSpawn, int x, int y){
        Vector3 pos = new Vector3((float)x*CellSize, 0f, (float)y*CellSize);
        switch(whatToSpawn){
            case 1:
                Instantiate (prefab1, pos, Quaternion.identity);
                break;
            case 2:
                Instantiate (prefab2, pos, Quaternion.identity);
                break;
            case 3:
                Instantiate (prefab3, pos, Quaternion.identity);
                break;
            case 4:
                Instantiate (prefab4, pos, Quaternion.identity);
                break;
            case 5:
                Instantiate (prefab5, pos, Quaternion.identity);
                break;
            case 6:
                Instantiate (prefab6, pos, Quaternion.identity);
                break;
            case 7:
                Instantiate (prefab7, pos, Quaternion.identity);
                break;
            case 8:
                Instantiate (prefab8, pos, Quaternion.identity);
                break;
            case 9:
                Instantiate (prefab9, pos, Quaternion.identity);
                break;
            case 10:
                Instantiate (prefab10, pos, Quaternion.identity);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
