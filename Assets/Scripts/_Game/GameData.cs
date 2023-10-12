using System;
using UnityEngine;
using UnityEngine.Serialization;


[Serializable] 
    public class GameData
    {
        public float dogChaseRadius;
        // public float dogChasePower;
        public float sheepMaxSpeed;
        public float sheepMinSpeed;
        
        
        public float playerSpeed;
        public float levelTime;
        public CameraMode cameraMode;
        public int sheepPrice; 
        public int goldenSheepPrice;
        public Vector3 cameraOffset;
        public float cameraXRotation;
     
        public GameData()
        {
            cameraXRotation = 61.31f;
            cameraOffset = new Vector3(0, 38, -49.72f + 37.8f);
            
            // dogChasePower = 1;
            sheepMaxSpeed = 25;
            sheepMinSpeed = 0;
            levelTime = 30;
            dogChaseRadius = 25;
            playerSpeed = 10;
            sheepPrice = 5;
            goldenSheepPrice = 20;
        }
    }



public enum CameraMode
{
    Normal,
    AD
}
