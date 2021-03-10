using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbiantLight : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<Renderer>().material.name != "BG (Instance)") {
            GetComponent<Light>().color = GetComponent<Renderer>().material.color;
        } else {
            Destroy(GetComponent<Light>());
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
