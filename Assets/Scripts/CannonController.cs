using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    RaycastHit hit;
    Ray ray;
    public int spped;
    public float friction;
    public float lerpSpeed;
    float xDeg;
    float yDeg;
    Quaternion fromRotation;
    Quaternion toRotation;
    private Camera camera;

    [SerializeField] float minFirePower;
    [SerializeField] float maxFirePower;

    [SerializeField] float xLowerLimit = -30f;
    [SerializeField] float xUpperLimit = 0;
    [SerializeField] float yLowerLimit = -35f;
    [SerializeField] float yUpperLimit = 35f;


    public Transform aimPoint;
    private LineRenderer lineRenderer;

    [SerializeField] GameObject cannonBall;
    [SerializeField] GameObject explosion;
    public float firePower;
    Rigidbody ballRB;
    [SerializeField] GameObject hitPoint;


    private Vector3 mousePressDownPos;
    private Vector3 mouseRealesePos;
    private bool isShoot;

    public PowerBar powerBar;

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
        lineRenderer = GetComponent<LineRenderer>();
        isShoot = false;
        firePower = minFirePower;
    }

    // Update is called once per frame
    void Update()
    {
        if (!PasueMenu.isPaused || !UIManagement.isGameOver)
        {
            ray = camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {

                xDeg -= Input.GetAxis("Mouse Y") * spped * friction;
                yDeg += Input.GetAxis("Mouse X") * spped * friction;
                if (xDeg > xUpperLimit)
                {
                    xDeg = xUpperLimit;
                }

                if (xDeg < xLowerLimit)
                {
                    xDeg = xLowerLimit;
                }

                if (yDeg > yUpperLimit)
                {
                    yDeg = yUpperLimit;
                }

                if (yDeg < yLowerLimit)
                {
                    yDeg = yLowerLimit;
                }

                fromRotation = transform.rotation;
                toRotation = Quaternion.Euler(xDeg, yDeg, 0);
                aimPoint.transform.rotation = Quaternion.Lerp(fromRotation, toRotation, Time.deltaTime * lerpSpeed);
                transform.rotation = aimPoint.transform.rotation;


                if (transform.eulerAngles.z != 0)
                {
                    transform.rotation = Quaternion.Euler(xDeg, yDeg, 0);
                }



                if (Input.GetMouseButtonDown(0))
                {
                    isShoot = true;
                    Debug.Log("Shoot");
                    fireCannon();
                }

                if(Input.GetKeyDown(KeyCode.UpArrow))
                {
                    increasePower();
                }
                
                if(Input.GetKeyDown(KeyCode.DownArrow))
                {
                    decresePower();
                }
            }
        }

        updatePowerBar();
    }

    public void fireCannon()
    {
        if (isShoot)
        {
            Vector3 finalPos = camera.WorldToScreenPoint(Input.mousePosition);
            finalPos.z = 0;
            GameObject ballCopy = Instantiate(cannonBall, aimPoint.position, aimPoint.rotation) as GameObject;
            ballRB = ballCopy.GetComponent<Rigidbody>();
            ballRB.AddForce(transform.forward * firePower);
            //ballRB.velocity = aimPoint.transform.up * firePower;
            Destroy(Instantiate(explosion, aimPoint.position, aimPoint.rotation), 2f);
            //ballCopy.transform.position = hit.transform.position;
        }
    }

    void increasePower()
    {
        isShoot = false;
        if (firePower < maxFirePower)
        {
            firePower += 50f;
        }

        if(firePower>maxFirePower)
        {
            firePower = maxFirePower;
        }
        updatePowerBar();
    }

    void decresePower()
    {
        isShoot = false;
        if (firePower > minFirePower)
        {
            firePower -= 50f;
        }

        if(firePower<minFirePower)
        {
            firePower = minFirePower;
        }

        updatePowerBar();
    }

    void updatePowerBar()
    {
        powerBar.setPower(firePower/maxFirePower);
    }
}
