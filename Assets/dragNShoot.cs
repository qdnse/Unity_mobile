using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class dragNShoot : MonoBehaviour
{
    public float power = 1f;
    public Rigidbody rb;
    public LineRenderer line;

    public Vector2 minPower;
    public Vector2 maxPower;

    public Camera cam;
    public Camera lineCam;
    Vector3 force;
    Vector3 startPoint;
    Vector3 endPoint;
    bool isClicked = false;
    
    private void Start()
    {
    }

    private void Update()
    {
        line.SetPosition(0, rb.position);
        if (Input.GetMouseButtonDown(0))
        {
            isClicked = true;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero; 
            startPoint = Input.mousePosition;
            startPoint.z = 10;
            startPoint = cam.ScreenToWorldPoint(startPoint);
        }
        if (isClicked) {
            endPoint = Input.mousePosition;
            endPoint.z = 10;
            endPoint = lineCam.ScreenToWorldPoint(endPoint);
            var point = new Vector3(endPoint.x, rb.position.y, endPoint.z);
            line.SetPosition(1, point);
        } else {
            line.SetPosition(1, rb.position);
        }

        if (Input.GetMouseButtonUp(0))
        {
            isClicked = false;
            endPoint = Input.mousePosition;
            endPoint.z = 10;
            endPoint = cam.ScreenToWorldPoint(endPoint);

            force = new Vector3(Mathf.Clamp(startPoint.x - endPoint.x, minPower.x, maxPower.x), 0f, Mathf.Clamp(startPoint.z - endPoint.z, minPower.y, maxPower.y));
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero; 
            rb.AddForce(force * power, ForceMode.Impulse);
            line.SetPosition(1, rb.position);
        }
    }
}
