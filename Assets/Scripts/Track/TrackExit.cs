using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackExit : MonoBehaviour
{
     private float delay = 10;

     public static Action onTrackExit;

     private void OnTriggerExit(Collider other)
     {
          if (other.CompareTag(Constants.PLAYER_TAG))
          {
               print("Track ended");
               onTrackExit?.Invoke();
               StartCoroutine(ReturnTrackToPool());
          }
     }
     IEnumerator ReturnTrackToPool()
     {
          yield return new WaitForSeconds(delay);

          transform.parent.gameObject.SetActive(false);
     }
}
