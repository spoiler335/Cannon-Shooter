using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawTragetory : MonoBehaviour
{
    private LineRenderer lineRenderer;
    CannonController cannonController;

    [SerializeField] int numpoints = 50;
    [SerializeField] float timeBetweenPoints;

    public LayerMask collidableLayers;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        cannonController = GetComponent<CannonController>();
    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer.positionCount = (int)numpoints;
        List<Vector3> points = new List<Vector3>();
        Vector3 startingPosition = cannonController.aimPoint.position;
        Vector3 startingVelocity = cannonController.aimPoint.up * cannonController.firePower;

        for(float t=0;t<numpoints;t+=timeBetweenPoints)
        {
            Vector3 newPoint = startingPosition + t * startingVelocity;
            newPoint.y = startingPosition.y + startingVelocity.y * t + Physics.gravity.y / 2f * t * t;
            points.Add(newPoint);

            //if(Physics.OverlapSphere(newPoint,2,collidableLayers).Length>0)
            //{
            //    lineRenderer.positionCount = points.Count;
            //    break;
            //}
            lineRenderer.positionCount = points.Count;

        }

        lineRenderer.SetPositions(points.ToArray());
    }

    

}
