using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] public bool deathzone;
    [SerializeField] public GameObject SpotLight;

    public static CameraFollow instance;
    public CameraShaking CameraShaking;

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

    public void getPosition_twoD(Vector3 pos)
    {
        pos.z = -10;
        gameObject.transform.position = pos;
    }
    public void getPosition_threeD(Vector3 pos)
    {
        Vector3 tmp = pos;
        pos.y += 12;
        pos.z += -9;
        gameObject.transform.position = pos;
        gameObject.transform.localRotation = Quaternion.Euler(60, 0, 0);
        tmp.y += 11;
        SpotLight.transform.position = tmp;
    }
}
