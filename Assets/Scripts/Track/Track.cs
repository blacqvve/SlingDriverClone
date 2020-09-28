using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using System.Linq;

public class Track : MonoBehaviour
{
     [SerializeField]
     private TrackTypes trackType;

     public TrackTypes TrackType
     {
          get { return trackType; }
     }

     private float speedMultiplier;

     public float SpeedMultiplier
     {
          get { return speedMultiplier; }
          set { speedMultiplier = value; }
     }
     
     private int trackDifficulty;

     public int TrackDifficulty
     {
          get { return trackDifficulty; }
          set { trackDifficulty = value; }
     }

     public GameObject tower;

     private void Awake()
     {
          speedMultiplier = 1f;
     }

     public static void ChangePositionWithChild(Transform thisTransform, string childname)
     {
          var childs = thisTransform.Cast<Transform>().ToList();
          var changedChild = childs.FirstOrDefault(x => x.name == childname);
          if (changedChild == null)
               return;

          childs.ForEach(x => x.SetParent(null));
          var tempPos = changedChild.position;
          changedChild.position = thisTransform.position;
          thisTransform.position = tempPos;

          childs.ForEach(x => x.SetParent(thisTransform));
     }

     private void OnDisable()
     {
          transform.position = Vector3.zero;
          transform.rotation = Quaternion.identity;
     }
}

