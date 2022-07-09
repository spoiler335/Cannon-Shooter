using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    RaycastHit hit;

    public int spped;
    public float friction;
    public float lerpSpeed;
    float xDeg;
    float yDeg;
    Quaternion fromRotation;
    Quaternion toRotation;
    private Camera camera;

    [SerializeField] float xLowerLimit = -30f;
    [SerializeField] float xUpperLimit = 0;
    [SerializeField] float yLowerLimit = -35f;
    [SerializeField] float yUpperLimit = 35f;


    [SerializeField] Transform aimPoint;
    private LineRenderer lineRenderer;

    [SerializeField] GameObject cannonBall;
    [SerializeField] GameObject explosion;
    [SerializeField] float firePower;
    Rigidbody ballRB;

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
        lineRenderer = GetComponent<LineRenderer>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
        
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray,out hit))
        {
            if (Input.GetMouseButton(0))
            {
                lineRenderer.enabled = true;
                xDeg -= Input.GetAxis("Mouse Y") * spped * friction;
                yDeg += Input.GetAxis("Mouse X") * spped * friction;
                if(xDeg>xUpperLimit)
                {
                    xDeg = xUpperLimit;
                }

                if(xDeg < xLowerLimit)
                {
                    xDeg = xLowerLimit;
                }

                if(yDeg > yUpperLimit)
                {
                    yDeg = yUpperLimit;
                }

                if(yDeg < yLowerLimit)
                {
                    yDeg = yLowerLimit;
                }

                fromRotation = transform.rotation;
                toRotation = Quaternion.Euler(xDeg, yDeg, 0);
                aimPoint.transform.rotation = Quaternion.Lerp(fromRotation, toRotation, Time.deltaTime * lerpSpeed);
                transform.rotation = aimPoint.transform.rotation;
                lineRenderer.SetPosition(0, aimPoint.transform.position);
                lineRenderer.SetPosition(1, hit.point);

                if(transform.eulerAngles.z != 0)
                {
                    transform.rotation = Quaternion.Euler(xDeg,yDeg,0);
                }

            }

            if(Input.GetMouseButtonUp(0))
            {
                lineRenderer.enabled = false;
                Debug.Log("Shoot");
                fireCannon();
            }
        }
        
        else
        {
            lineRenderer.enabled = false;
        }
    }

    public void fireCannon()
    {
        GameObject ballCopy = Instantiate(cannonBall, aimPoint.position, aimPoint.rotation) as GameObject;
        ballRB = ballCopy.GetComponent<Rigidbody>();
        ballRB.AddForce(transform.forward * firePower);
        Instantiate(explosion, aimPoint.position, aimPoint.rotation);
        ballCopy.transform.position = hit.transform.position;
    }
}
