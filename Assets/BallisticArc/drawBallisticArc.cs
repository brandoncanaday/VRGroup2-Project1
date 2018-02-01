﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drawBallisticArc : MonoBehaviour {

    // member attributes
    public float initV = 20.0f; // initial velocity
    public float timeStep = 0.02f;  // default time step (In seconds I think)
    public float timeMaximum = 10.0f;
    public LayerMask layerMask = -1;
    public GameObject impactCircle;

    private GameObject impactCircleInstance;
    private LineRenderer lr;


	// Use this for initialization
	void Start () {
        lr = GetComponent<LineRenderer>();  
	}
	
	// Update is called once per frame
	void Update () {

        Vector3 velocityVect = transform.forward * initV;
        lr.SetVertexCount((int)(timeMaximum / timeStep));
        
        Vector3 currPos = transform.position;

        int i = 0; // index


        for (float t = 0.0f ; t < timeMaximum; t += timeStep )
        {
            lr.SetPosition(i, currPos);


            // project target impact point
            RaycastHit impact;

            if(Physics.Raycast(currPos, velocityVect, out impact, velocityVect.magnitude, layerMask))
            {

                lr.SetVertexCount(i + 2);
                lr.SetPosition(i + 1, impact.point);

                if (impactCircle != null)
                {
                    if (impactCircleInstance != null)
                    {
                        impactCircleInstance.transform.position = impact.point;
                    }
                    else
                    {
                        impactCircleInstance = Instantiate(impactCircle, impact.point, Quaternion.identity) as GameObject;
                    }
                }
                break;

            }
            // update position
            currPos = currPos + (velocityVect * timeStep);

            // account for gravity on arc
            velocityVect += (Physics.gravity * timeStep); 
            i++;
        }
	}
}
