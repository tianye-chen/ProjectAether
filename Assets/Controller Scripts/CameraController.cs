using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    public float baseZoom = 5;
    // Start is called before the first frame updates

    void Start()
    {
      SetCameraSize(baseZoom);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3 (player.position.x + offset.x, player.position.y + offset.y, offset.z);    
    }

    public void SetCameraSize(float value)
    {
      GetComponent<Camera>().orthographicSize = value;
    }
}
