using Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
     public Camera camera;

     private bool isMoving = false;
     private bool isDrifting = false;

     private Track currentTrack;

     private GameManager gameManager;

     private Transform rope;

     Vector3 cameraOffset;
     private void OnEnable()
     {
          TrackEnter.onTrackEnter += SetCurrentTrack;
     }

     void SetCurrentTrack(Track obj)
     {
          currentTrack = obj;
     }
     private void Start()
     {
          gameManager = GameObject.FindObjectOfType(typeof(GameManager)) as GameManager;
          cameraOffset = camera.transform.position - transform.position;
          rope = transform.GetChild(0).transform;
     }

     private void FixedUpdate()
     {
          if (gameManager.GameState.Equals(GameStates.Play))
          {
               isMoving = true;
               if (isMoving)
               {
                    transform.Translate(transform.forward * (Time.deltaTime * 30), Space.World);

                    if (Input.GetKey(KeyCode.A))
                    {
                         transform.Rotate( new Vector3(0, -90f * Time.deltaTime, 0));
                    }
                    if (Input.GetKey(KeyCode.D))
                    {
                         transform.Rotate(new Vector3(0, 90f * Time.deltaTime, 0));
                    }
               }
               HandleInput();
          }
          else
          {
               isMoving = false;
          }

     }
     void HandleInput()
     {
          if (currentTrack != null && currentTrack.tower != null)
          {

               var distance = GetDistanceToTower();

               if (Input.GetMouseButton(0))
               {
                    if (distance < 20f)
                    {
                         isDrifting = true;
                         //enter drift state
                         DriftState();
                    }
                    else
                    {
                         isDrifting = false;
                    }
               }
                

          }
     }
     public void DriftState()
     {
          if (isDrifting)
          {
               transform.RotateAround(currentTrack.tower.transform.position, currentTrack.tower.transform.up * 90f,
                     Time.deltaTime * 90f);
               transform.Rotate(0, 90f*3.5f  * Time.deltaTime / 4, 0);
          }
     }
     float GetDistanceToTower()
     {
          var towerTransform = currentTrack.tower.transform;
          var towerOffset = Vector3.Distance(towerTransform.position, transform.position);
          return towerOffset; 
     }
     private void LateUpdate()
     {
          camera.transform.position = transform.position + cameraOffset;
     }

}
