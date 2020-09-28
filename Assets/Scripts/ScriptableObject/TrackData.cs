using Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TrackData")]

public class TrackData : ScriptableObject
{
     public enum Direction
     {
          North, East, South, West
     }

     public Vector3 chunkSize = new Vector3(10f, 0,10f);

     public GameObject[] levelChunks;
     public Direction entryDirection;
     public Direction exitDirection;
     public TrackTypes trackType;
}
