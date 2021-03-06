using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public bool deathzone;
    public static CameraFollow instance;

    private void Awake()
    {
        if (instance != null)
        {
            return;
        }
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        deathzone = false;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void getPosition(Vector3 pos)
    {
        pos.z = -10;
        gameObject.transform.position = pos;
    }
}
