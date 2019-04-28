using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Tilemaps;

public class SpawnObstaclesOnTilemap : MonoBehaviour
{

    
    public Tilemap tilemap;
    public int layer = 1;
    public GameObject obstacle; 
    private void Start()
    {
        foreach (var item in tilemap.cellBounds.allPositionsWithin)
        {
            Vector3Int localPlace = new Vector3Int(item.x, item.y, item.z);
            Vector3 place = tilemap.CellToWorld(localPlace);
            //place = new Vector3(place.x, place.z, place.y);
            
            if (tilemap.HasTile(localPlace))
            {
                if (localPlace.z == layer)
                {
                    //Debug.Log("tile at " + place);
                    GameObject tmpOnj = GameObject.Instantiate(obstacle);
                    tmpOnj.transform.position = place + new Vector3(0.5f,0,0.5f);
                    
                    
                }
            }
        }
        
    }

}
