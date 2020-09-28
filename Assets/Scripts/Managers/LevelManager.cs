using Enums;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Toolbox;
using UnityEngine;

namespace Managers
{
     public class LevelManager : Singleton<LevelManager>
     {
          private int levelIndex = 0;
          Transform trackParent;

          public TrackData[] trackDataArray;
          public TrackData firstTrack;
          public TrackData finishTrack;

          private TrackData previousTrack;

          public Vector3 spawnOrigin;

          private Vector3 spawnPosition;

          public System.Action onLevelCreate;

          private void Start()
          {
               trackParent = GameObject.Find(Constants.TRACK_PARENT).transform;
          }
          public List<Track> GenerateLevel(int levelCount = 2)
          {
               List<Track> levelTracks = new List<Track>();

               if (previousTrack == null)
               {
                    previousTrack = firstTrack;
               }

               for (int j = 0; j < levelCount; j++)
               {
                    for (int i = 0; i < Constants.LEVEL_LENGHT; i++)
                    {
                         levelTracks.Add(PickAndSpawnChunk());
                    }
                    while (previousTrack.exitDirection != TrackData.Direction.North)
                    {
                         levelTracks.Add(PickAndSpawnChunk());
                    }
                    levelTracks.Add(PickAndSpawnChunk(true));

                    onLevelCreate?.Invoke();
               }

               return levelTracks;
          }
          TrackData PickNextChunk()
          {
               List<TrackData> allowedChunkList = new List<TrackData>();
               TrackData nextChunk = null;

               TrackData.Direction nextRequiredDirection = TrackData.Direction.North;

               switch (previousTrack.exitDirection)
               {
                    case TrackData.Direction.North:
                         nextRequiredDirection = TrackData.Direction.South;
                         spawnPosition = spawnPosition + new Vector3(0f, 0, previousTrack.chunkSize.z);

                         break;
                    case TrackData.Direction.East:
                         nextRequiredDirection = TrackData.Direction.West;
                         spawnPosition = spawnPosition + new Vector3(previousTrack.chunkSize.x, 0, 0);
                         if (previousTrack.trackType == TrackTypes.U)
                         {
                              spawnPosition += new Vector3(0, 0, 20);
                         }
                         break;
                    case TrackData.Direction.South:
                         nextRequiredDirection = TrackData.Direction.North;
                         spawnPosition = spawnPosition + new Vector3(0, 0, -previousTrack.chunkSize.z);
                         break;
                    case TrackData.Direction.West:
                         nextRequiredDirection = TrackData.Direction.East;
                         spawnPosition = spawnPosition + new Vector3(-previousTrack.chunkSize.x, 0, 0);
                         if (previousTrack.trackType == TrackTypes.U)
                         {
                              spawnPosition += new Vector3(0, 0, 20);
                         }

                         break;
                    default:
                         break;
               }

               for (int i = 0; i < trackDataArray.Length; i++)
               {
                    if (trackDataArray[i].entryDirection == nextRequiredDirection)
                    {
                         allowedChunkList.Add(trackDataArray[i]);
                    }
               }


               if (previousTrack.trackType != TrackTypes.U && Random.Range(0, 6) >= 4 && (nextRequiredDirection == TrackData.Direction.East || nextRequiredDirection == TrackData.Direction.West))
               {
                    nextChunk = allowedChunkList.FirstOrDefault(x => x.trackType.Equals(TrackTypes.U));
                    spawnPosition += new Vector3(0, 0, 20);

               }
               else
               {
                    allowedChunkList = allowedChunkList.Where(x => x.trackType != TrackTypes.U).ToList();
                    nextChunk = allowedChunkList[Random.Range(0, allowedChunkList.Count)];
               }

           
               return nextChunk;

          }

          Track PickAndSpawnChunk(bool last = false)
          {
               TrackData chunkToSpawn = PickNextChunk();
               if (last)
               {
                    chunkToSpawn = finishTrack;
               }
               GameObject objectFromChunk = chunkToSpawn.levelChunks[Random.Range(0, chunkToSpawn.levelChunks.Length)];
               previousTrack = chunkToSpawn;
               Track go = TrackPool.Instance.GetTrackFromPool(objectFromChunk);
               SetPosition(ref go);
               Debug.Log($"spawning chunk at {spawnPosition} type of {chunkToSpawn.trackType} previous {previousTrack.trackType} ");
               return go;
          }

          private void SetPosition(ref Track track)
          {
               track.gameObject.transform.parent = trackParent;
               track.gameObject.transform.position = spawnPosition + spawnOrigin;

          }

          public void UpdateSpawnOrigin(Vector3 originDelta)
          {
               spawnOrigin = spawnOrigin + originDelta;
          }


     }


}