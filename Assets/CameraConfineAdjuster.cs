using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CameraConfineAdjuster : MonoBehaviour
{
    // Start is called before the first frame update
    
    
    void Start()
    {
        float z = 30;
        if (GameManager.Instance.GameData.cameraMode == CameraMode.AD)
        {
            z = 60;
        }
        transform.localScale = new Vector3(100, 100, z);

    }


}
