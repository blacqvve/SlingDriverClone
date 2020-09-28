using Enums;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Toolbox;
using UnityEngine;

public class TrackPool : Singleton<TrackPool>
{
     public const string PATH = "TrackPrefabs/";
     private List<Track> objectList;

     public List<Track> trackPool;

     private new void Awake()
     {
          trackPool = new List<Track>();
          objectList = Resources.LoadAll<Track>(PATH).ToList();
          ExpandPool();


     }
     private void ExpandPool()
     {
          foreach (var item in objectList)
          {
               int createAmount = 0;
               switch (item.TrackType)
               {
                    case TrackTypes.Normal:
                         createAmount = Constants.POOL_NORMAL_COUNT;
                         break;
                    case TrackTypes.U:
                         createAmount = Constants.POOL_U_COUNT;
                         break;
                    case TrackTypes.Finish:
                         createAmount = Constants.POOL_FINISH_COUNT;
                         break;
                    case TrackTypes.Start:
                         createAmount = Constants.POOL_START_COUNT;
                         break;

               }
               for (int i = 0; i < createAmount; i++)
               {
                    InstantiateAndAddPool(item);
               }
          }
     }
     public Track GetTrackFromPool(GameObject type)
     {
          var track = trackPool.FirstOrDefault(x => !x.gameObject.activeInHierarchy&&x.gameObject.name==type.name);
          if (track != null)
          {
               track.gameObject.SetActive(true);
               return track;
          }
          else
          {
               ExpandPool();
               return GetTrackFromPool(type);
          }
     }
     Track InstantiateAndAddPool(Track go)
     {
          Track track = Instantiate(go,Vector3.zero,Quaternion.identity,this.transform);
          track.gameObject.name = go.name;
          track.gameObject.SetActive(false);
          trackPool.Add(track);
          return track;
     }
}

