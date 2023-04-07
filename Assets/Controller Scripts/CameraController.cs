using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    public float baseZoom = 8;
    // Start is called before the first frame updates

    private void Awake()
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
