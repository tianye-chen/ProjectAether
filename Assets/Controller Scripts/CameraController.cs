using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    public float baseZoom = 8;

    private void Awake()
    {
      SetCameraSize(baseZoom);
    }

    void Start()
    {
      if (player == null)
        player = GameObject.FindGameObjectWithTag("Player").transform;
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
