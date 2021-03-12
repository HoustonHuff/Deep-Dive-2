using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.UI;

public class HorizonScript : MonoBehaviour
{
    public static HorizonScript current;
    //Config Variables
    public bool isActive = false;

    public Transform player;
    

    [Space(5)]
    [Header("Roll")]
    public bool useRoll = true;
    public float rollAmplitude = 1, rollOffSet = 0;
    [Range(0, 1)] public float rollFilterFactor = 0.25f;
    public RectTransform horizonRoll;
    public Text horizonRollTxt;

    [Space(5)]
    [Header("Pitch")]
    public bool usePitch = true;
    public float pitchAmplitude = 1, pitchOffSet = 0, pitchXOffSet = 0, pitchYOffSet = 0;
    [Range(0, 1)] public float pitchFilterFactor = 0.125f;
    public RectTransform horizonPitch;
    public Text horizonPitchTxt;

    //All Flight Variables
    [Space(10)]
    [Header("Flight Variables - ReadOnly!")]
    public float speed;
    public float altitude, pitch, roll, heading, turnRate, gForce, maxGForce, minGForce, alpha, beta, vv, hv, engine, fuel, fuelFlow;



    //Internal Calculation Variables
    Vector3 currentPosition, lastPosition, relativeSpeed, absoluteSpeed, lastSpeed, relativeAccel;

    Vector3 angularSpeed;
    Quaternion currentRotation, lastRotation, deltaTemp;
    float angleTemp = 0.0f;
    Vector3 axisTemp = Vector3.zero;




    void Awake()
    {
        if (player == null) player = Camera.main.transform; //if no player object is set
    }
    void Update()
    {
        //////////////////////////////////////////// Frame Calculations
        lastPosition = currentPosition;
        lastSpeed = relativeSpeed;
        lastRotation = currentRotation;

        if (player != null) //Mode Transform
        {
            currentPosition = player.transform.position;
            absoluteSpeed = (currentPosition - lastPosition) / Time.fixedDeltaTime;
            relativeSpeed = player.transform.InverseTransformDirection((currentPosition - lastPosition) / Time.fixedDeltaTime);
            relativeAccel = (relativeSpeed - lastSpeed) / Time.fixedDeltaTime;
            currentRotation = player.transform.rotation;

            //angular speed
            deltaTemp = currentRotation * Quaternion.Inverse(lastRotation);
            angleTemp = 0.0f;
            axisTemp = Vector3.zero;
            deltaTemp.ToAngleAxis(out angleTemp, out axisTemp);
            //
            angularSpeed = player.InverseTransformDirection(angleTemp * axisTemp) * Mathf.Deg2Rad / Time.fixedDeltaTime;
            //
        }
        else //Zero all values
        {
            currentPosition = Vector3.zero;
            relativeSpeed = Vector3.zero;
            relativeAccel = Vector3.zero;
            angularSpeed = Vector3.zero;

            lastPosition = currentPosition;
            lastSpeed = relativeSpeed;
            lastRotation = currentRotation;
        }
        //

        //////////////////////////////////////////// Roll
        if (useRoll)
        {
            roll = Mathf.LerpAngle(roll, player.rotation.eulerAngles.z + rollOffSet, rollFilterFactor) % 360;

            //Send values to Gui and Instruments
            if (horizonRoll != null) horizonRoll.localRotation = Quaternion.Euler(0, 0, rollAmplitude * roll);
            if (horizonRollTxt != null)
            {
                //horizonRollTxt.text = roll.ToString("##");
                if (roll > 180) horizonRollTxt.text = (roll - 360).ToString("00");
                else if (roll < -180) horizonRollTxt.text = (roll + 360).ToString("00");
                else horizonRollTxt.text = roll.ToString("00");
            }
            //
        }
        //////////////////////////////////////////// Roll


        //////////////////////////////////////////// Pitch
        if (usePitch)
        {
            pitch = Mathf.LerpAngle(pitch, -player.eulerAngles.x + pitchOffSet, pitchFilterFactor);

            //Send values to Gui and Instruments
            if (horizonPitch != null) horizonPitch.localPosition = new Vector3(-pitchAmplitude * pitch * Mathf.Sin(horizonPitch.transform.localEulerAngles.z * Mathf.Deg2Rad) + pitchXOffSet, pitchAmplitude * pitch * Mathf.Cos(horizonPitch.transform.localEulerAngles.z * Mathf.Deg2Rad) + pitchYOffSet, 0);
            if (horizonPitchTxt != null) horizonPitchTxt.text = pitch.ToString("0");
        }
        //////////////////////////////////////////// Pitch
    }
}