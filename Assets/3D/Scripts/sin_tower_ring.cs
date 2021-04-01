using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sin_tower_ring : MonoBehaviour
{
    float min_val;
    float max_val;

    float speed;

    private void Awake()
    {
        min_val = Random.Range(-0.25f, -0.5f);
        max_val = Random.Range(0.5f, 1f);
        speed = Random.Range(0.5f, 2f);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        var oscilationRange = (max_val - min_val) / 2;
        var oscilationOffset = oscilationRange + min_val;

        float g = oscilationOffset + Mathf.Sin(Time.time * speed) * oscilationRange;
        var res = transform.localPosition;
        //transform.localPosition = new Vector3(res.x, res.y, g);
        transform.localPosition = new Vector3(res.x, g, res.z);


    }
}
