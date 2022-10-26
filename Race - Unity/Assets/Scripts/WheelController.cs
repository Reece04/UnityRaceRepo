using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelController : MonoBehaviour
{
    [SerializeField] WheelCollider frontRight;
    [SerializeField] WheelCollider frontLeft;
    [SerializeField] WheelCollider backRight;
    [SerializeField] WheelCollider backLeft;

    [SerializeField] Transform frontRightTransform;
    [SerializeField] Transform frontleftTransform;
    [SerializeField] Transform backRightTransform;
    [SerializeField] Transform backLeftTransform;

    public float acceleration = 450f;
    public float breakingForce = 350f;
    public float maxTurnAngle = 30f;


    private float currentAcceleration = 0f;
    private float currentBreakforce = 0f;
    private float currentTurnAngle = 0f;

    private void FixedUpdate()
    {
    currentAcceleration = acceleration * Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.Space))
        {
            currentBreakforce = breakingForce;
        }
        else
        {
            currentBreakforce = 0f;
        }

        //Drive from front wheels
        frontRight.motorTorque = currentAcceleration;
        frontLeft.motorTorque = currentAcceleration;

        frontRight.brakeTorque = currentBreakforce;
        frontLeft.brakeTorque = currentBreakforce;
        backRight.brakeTorque = currentBreakforce;
        backLeft.brakeTorque = currentBreakforce;

        //Steering
        currentTurnAngle = maxTurnAngle * Input.GetAxis("Horizontal");
        frontLeft.steerAngle = currentTurnAngle;
        frontRight.steerAngle = currentTurnAngle;

        UpdateWheel(frontRight, frontRightTransform);
        UpdateWheel(frontLeft, frontleftTransform);
        UpdateWheel(backRight, backRightTransform);
        UpdateWheel(backLeft, backLeftTransform);

    }

    void UpdateWheel(WheelCollider col, Transform trans)
    {
        //wheel Collider
        Vector3 postion;
        Quaternion rotation;
        col.GetWorldPose(out postion, out rotation);

        //Setting wheel transform
        trans.position = postion;
        trans.rotation = rotation;

    }

}
    
