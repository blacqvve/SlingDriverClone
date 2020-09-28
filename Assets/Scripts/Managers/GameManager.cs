using Enums;
using Managers;
using System.Collections;
using System.Collections.Generic;
using Toolbox;
using UnityEngine;

public class GameManager : MonoBehaviour
{

     private int levelIndex=0;
     public int LevelIndex
     {
          get { return levelIndex; }
          set { levelIndex = value; }
     }

     private GameStates gameState;

     public GameStates GameState
     {
          get { return gameState; }
     }


     List<Track> tracks;
     private void OnEnable()
     {
          LevelManager.Instance.onLevelCreate += () => levelIndex++;
         
     }
     private void OnDisable()
     {
          LevelManager.Instance.onLevelCreate -= () => levelIndex++;
     }
     private void Start()
     {
          tracks = LevelManager.Instance.GenerateLevel();
          gameState = GameStates.Countdown;
     }

     private void Update()
     {
          if (Input.GetKeyDown(KeyCode.T))
          {
               tracks.AddRange(LevelManager.Instance.GenerateLevel(1));
          }
          if (Input.GetKeyDown(KeyCode.Space))
          {
               SetGameState(GameStates.Play);
          }
     }

     public void SetGameState(GameStates state)
     {
          gameState = state;
     }
}
