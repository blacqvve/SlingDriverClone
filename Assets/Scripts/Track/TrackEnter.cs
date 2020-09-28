using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackEnter : MonoBehaviour
{

     public static Action<Track> onTrackEnter;

     private void OnTriggerExit(Collider other)
     {
          if (other.CompareTag(Constants.PLAYER_TAG))
          {
               print("track enter");
               onTrackEnter?.Invoke(this.transform.parent.GetComponent<Track>());
          }
     }
}
