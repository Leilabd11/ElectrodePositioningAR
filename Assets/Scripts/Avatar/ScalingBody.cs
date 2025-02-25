using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.AffordanceSystem.Receiver.Primitives;

public class ScalingBody : MonoBehaviour
{
    // Declaration of variables for bones and body parts
    private Transform head, headEnd, leftToe, rightToe, leftScapula, rightScapula;
    private Transform chest, leftShoulder, rightShoulder, leftElbow, rightElbow, leftWrist, rightWrist;
    private Transform leftTip, rightTip, hips, leftHip, rightHip, leftHipBorder, rightHipBorder;
    private Transform leftKnee, rightKnee, leftAnkle, rightAnkle;

    // Main GameObject of the avatar where the bones are located
    public GameObject HumanMale;

    // Scaling vectors
    private Vector3 scaleHip, scaleKnee, scaleAnkle, scaleScapulas, scaleShoulders, scaleElbow, scaleWrist, scaleHead;

    // Subject’s body measurements in cm
    public float bodyHeightSubject, shoulderHeightSubject, shoulderWidthSubject, elbowSpanSubject;
    public float wristSpanSubject, armSpanSubject, hipHeightSubject, hipWidhtSubject, kneeHeightSubject, ankleHeightSubject;

    //bodyHeigh: Stijn = 170 / Menthy = 170
    //ShoulderHeigh: Stijn = 149 / Menthy = 140.4
    //ShoulderWidth: Stijn = 41 / Menthy = 46.1
    //elbowSpan: Stijn = 90 / Menthy = 82.4
    //wristSpan: Stijn = 133 / Menthy = 140
    //armSpan: Stijn = 171 / Menthy = 170
    //hipHeight: Stijn=98 / Menthy = 93.5
    //hipWidth: Stijn = 32 / Menthy = 35.8
    //kneeHeight: Stijn = 47 / Menthy = 52.6
    //ankleHeighStijn = 12 / Menthy = 11

    private float metersToCentimeters = 100f;

    void bonesLoading()
    {

        // Head
        head = HumanMale.transform.Find("mixamorig1:Hips/mixamorig1:Spine/mixamorig1:Spine1/mixamorig1:Spine2/mixamorig1:Neck/mixamorig1:Head");

        headEnd = HumanMale.transform.Find("mixamorig1:Hips/mixamorig1:Spine/mixamorig1:Spine1/mixamorig1:Spine2/mixamorig1:Neck/mixamorig1:Head/mixamorig1:HeadTop_End");

        // Toes
        leftToe = HumanMale.transform.Find("mixamorig1:Hips/mixamorig1:LeftUpLeg/mixamorig1:LeftLeg/mixamorig1:LeftFoot/mixamorig1:LeftToeBase");
        rightToe = HumanMale.transform.Find("mixamorig1:Hips/mixamorig1:RightUpLeg/mixamorig1:RightLeg/mixamorig1:RightFoot/mixamorig1:RightToeBase");

        // Chest
        chest = HumanMale.transform.Find("mixamorig1:Hips/mixamorig1:Spine/mixamorig1:Spine1/mixamorig1:Spine2");

        // Scapulas
        leftScapula = HumanMale.transform.Find("mixamorig1:Hips/mixamorig1:Spine/mixamorig1:Spine1/mixamorig1:Spine2/mixamorig1:LeftShoulder");
        rightScapula = HumanMale.transform.Find("mixamorig1:Hips/mixamorig1:Spine/mixamorig1:Spine1/mixamorig1:Spine2/mixamorig1:RightShoulder");

        // Shoulders
        leftShoulder = HumanMale.transform.Find("mixamorig1:Hips/mixamorig1:Spine/mixamorig1:Spine1/mixamorig1:Spine2/mixamorig1:LeftShoulder/mixamorig1:LeftArm");
        rightShoulder = HumanMale.transform.Find("mixamorig1:Hips/mixamorig1:Spine/mixamorig1:Spine1/mixamorig1:Spine2/mixamorig1:RightShoulder/mixamorig1:RightArm");

        // Elbows
        leftElbow = HumanMale.transform.Find("mixamorig1:Hips/mixamorig1:Spine/mixamorig1:Spine1/mixamorig1:Spine2/mixamorig1:LeftShoulder/mixamorig1:LeftArm/mixamorig1:LeftForeArm");
        rightElbow = HumanMale.transform.Find("mixamorig1:Hips/mixamorig1:Spine/mixamorig1:Spine1/mixamorig1:Spine2/mixamorig1:RightShoulder/mixamorig1:RightArm/mixamorig1:RightForeArm");

        // Wrists (Carpus)
        leftWrist = HumanMale.transform.Find("mixamorig1:Hips/mixamorig1:Spine/mixamorig1:Spine1/mixamorig1:Spine2/mixamorig1:LeftShoulder/mixamorig1:LeftArm/mixamorig1:LeftForeArm/mixamorig1:LeftHand");
        rightWrist = HumanMale.transform.Find("mixamorig1:Hips/mixamorig1:Spine/mixamorig1:Spine1/mixamorig1:Spine2/mixamorig1:RightShoulder/mixamorig1:RightArm/mixamorig1:RightForeArm/mixamorig1:RightHand");

        // Finger tips
        leftTip = HumanMale.transform.Find("mixamorig1:Hips/mixamorig1:Spine/mixamorig1:Spine1/mixamorig1:Spine2/mixamorig1:LeftShoulder/mixamorig1:LeftArm/mixamorig1:LeftForeArm/mixamorig1:LeftHand/mixamorig1:LeftHandMiddle1/mixamorig1:LeftHandMiddle2/mixamorig1:LeftHandMiddle3/mixamorig1:LeftHandMiddle4");
        rightTip = HumanMale.transform.Find("mixamorig1:Hips/mixamorig1:Spine/mixamorig1:Spine1/mixamorig1:Spine2/mixamorig1:RightShoulder/mixamorig1:RightArm/mixamorig1:RightForeArm/mixamorig1:RightHand/mixamorig1:RightHandMiddle1/mixamorig1:RightHandMiddle2/mixamorig1:RightHandMiddle3/mixamorig1:RightHandMiddle4");

        // Hips
        hips = HumanMale.transform.Find("mixamorig1:Hips");

        // Hip bones
        leftHip = HumanMale.transform.Find("mixamorig1:Hips/mixamorig1:LeftUpLeg");
        rightHip = HumanMale.transform.Find("mixamorig1:Hips/mixamorig1:RightUpLeg");

        //Hip borders
        leftHipBorder = HumanMale.transform.Find("mixamorig1:Hips/mixamorig1:LeftUpLeg/mixamorig1:LeftHipBorder");
        rightHipBorder = HumanMale.transform.Find("mixamorig1:Hips/mixamorig1:RightUpLeg/mixamorig1:RightHipBorder");

        // Knees
        leftKnee = HumanMale.transform.Find("mixamorig1:Hips/mixamorig1:LeftUpLeg/mixamorig1:LeftLeg");
        rightKnee = HumanMale.transform.Find("mixamorig1:Hips/mixamorig1:RightUpLeg/mixamorig1:RightLeg");

        // Ankles
        leftAnkle = HumanMale.transform.Find("mixamorig1:Hips/mixamorig1:LeftUpLeg/mixamorig1:LeftLeg/mixamorig1:LeftFoot");
        rightAnkle = HumanMale.transform.Find("mixamorig1:Hips/mixamorig1:RightUpLeg/mixamorig1:RightLeg/mixamorig1:RightFoot");



        //NOT FOUND BONES (just to look for errors). It is hide after checking that it was correct, to clean the script

        // Check if the bones have been found
        if (head == null) Debug.LogError("The bone 'Head' was not found.");
        if (leftToe == null) Debug.LogError("The bone 'LeftToe' was not found.");
        if (rightToe == null) Debug.LogError("The bone 'RightToe' was not found.");
        if (leftShoulder == null) Debug.LogError("The bone 'LeftShoulder' was not found.");
        if (rightShoulder == null) Debug.LogError("The bone 'RightShoulder' was not found.");
        if (leftElbow == null) Debug.LogError("The bone 'LeftElbow' was not found.");
        if (rightElbow == null) Debug.LogError("The bone 'RightElbow' was not found.");
        if (leftWrist == null) Debug.LogError("The bone 'leftWrist' was not found.");
        if (rightWrist == null) Debug.LogError("The bone 'rightWrist' was not found.");
        if (hips == null) Debug.LogError("The bone 'Hips' was not found.");
        if (leftHip == null) Debug.LogError("The bone 'leftHip' was not found.");
        if (rightHip == null) Debug.LogError("The bone 'rightHip' was not found.");
        if (leftKnee == null) Debug.LogError("The bone 'LeftKnee' was not found.");
        if (rightKnee == null) Debug.LogError("The bone 'RightKnee' was not found.");
        if (leftAnkle == null) Debug.LogError("The bone 'LeftAnkle' was not found.");
        if (rightAnkle == null) Debug.LogError("The bone 'RightAnkle' was not found.");
        if (leftTip == null) Debug.LogError("The bone 'leftTip' was not found.");
        if (rightTip == null) Debug.LogError("The bone 'rightTip' was not found.");
        if (leftHipBorder == null) Debug.LogError("The bone 'leftHipBorder' was not found.");
        if (rightHipBorder == null) Debug.LogError("The bone 'rightHipBorder' was not found.");

        if (head == null || leftToe == null || rightToe == null || leftShoulder == null || rightShoulder == null ||
            leftElbow == null || rightElbow == null || leftWrist == null || rightWrist == null ||
            hips == null || leftKnee == null || rightKnee == null || leftAnkle == null || rightAnkle == null || leftTip == null ||
            rightTip == null || leftHip == null || rightHip == null || leftHipBorder == null || rightHipBorder == null)
        {
            Debug.LogError("Some bones were not found. Check the names in the hierarchy.");
            return;
        }

    }

    void avatarScaling()
    {
        //LEG SCALING

        //HIP SCALE
        float hipHeight = (leftHip.position.y - leftToe.position.y) * metersToCentimeters;
        float scaleFactorHip = hipHeightSubject / hipHeight;
        scaleHip = new Vector3(1, scaleFactorHip, 1); //In this new model the leg is in the y direction, not x

        leftHip.transform.localScale = scaleHip;
        rightHip.transform.localScale = scaleHip;

        float toeHeight1 = leftToe.position.y;


        //KNEE SCALE
        float KneeHeight = (leftKnee.position.y - leftToe.position.y) * metersToCentimeters;
        float scaleFactorKnee = kneeHeightSubject / KneeHeight;

        scaleKnee = new Vector3(1, scaleFactorKnee, 1); //As I am computing the new knee height, I will scale based on that and not in the original measurements. Thus, I do not need to multiple by the inverse of the parent's scale (what I though for solving parent-children problem)
        leftKnee.transform.localScale = scaleKnee;
        rightKnee.transform.localScale = scaleKnee;

        float toeHeight2 = leftToe.position.y;

        //KNEE DISPLACEMENT

        float kneeDisplacement;

        kneeDisplacement = toeHeight2 - toeHeight1; //Explanation in my notes

        Vector3 LKnee = leftKnee.transform.localPosition;
        Vector3 RKnee = rightKnee.transform.localPosition;

        LKnee.y = LKnee.y + kneeDisplacement; //kneeDisplacement is in meters, so no conversion is needed
        RKnee.y = RKnee.y + kneeDisplacement; //In this model axes are not inverted

        leftKnee.transform.localPosition = LKnee;
        rightKnee.transform.localPosition = RKnee;


        //ANKLE SCALE
        float toeHeight3 = leftToe.position.y;

        float ankleHeight = (leftAnkle.position.y - leftToe.position.y) * metersToCentimeters;
        float scaleFactorAnkle = ankleHeightSubject / ankleHeight;
        scaleAnkle = new Vector3(scaleFactorAnkle, scaleFactorAnkle, scaleFactorAnkle);
        leftAnkle.transform.localScale = scaleAnkle;
        rightAnkle.transform.localScale = scaleAnkle;



        //ANKLE DISPLACEMENT

        float toeHeight4 = leftToe.position.y;
        float ankleDisplacement = toeHeight4 - toeHeight3;
        //Debug.Log("Ankle displ: " + ankleDisplacement);

        Vector3 LAnkle = leftAnkle.transform.localPosition;
        Vector3 RAnkle = rightAnkle.transform.localPosition;

        LAnkle.y = LAnkle.y + ankleDisplacement;
        RAnkle.y = RAnkle.y + ankleDisplacement;

        leftAnkle.transform.localPosition = LAnkle;
        rightAnkle.transform.localPosition = RAnkle;

        //HIP WIDTH 

        float hipWidth = Vector3.Distance(leftHipBorder.position, rightHipBorder.position);
        float varHipWidth = hipWidhtSubject / metersToCentimeters - hipWidth;

        Vector3 LHip = leftHip.transform.localPosition;
        Vector3 RHip = rightHip.transform.localPosition;

        LHip.x = LHip.x - varHipWidth / 2; //Is in the negative part of the x axis
        RHip.x = RHip.x + varHipWidth / 2; //Is in the positive part of the x axis

        leftHip.transform.localPosition = LHip;
        rightHip.transform.localPosition = RHip;



        //UPPER BODY SCALING

        //SHOULDERS 
        //Width (scaling scapulas, not working really well xd, axes problem) Check with Hololens
        float shoulderWidth = Vector3.Distance(leftShoulder.position, rightShoulder.position) * metersToCentimeters;
        float scaleFactorShouldersWidth = shoulderWidthSubject / shoulderWidth;

        scaleScapulas = new Vector3(scaleFactorShouldersWidth, scaleFactorShouldersWidth, scaleFactorShouldersWidth);
        leftScapula.transform.localScale = scaleScapulas;
        rightScapula.transform.localScale = scaleScapulas;

        //Height 

        float shoulderHeight = (leftShoulder.position.y - leftToe.position.y) * metersToCentimeters;
        float chestDisplacement = shoulderHeightSubject - shoulderHeight;

        Vector3 Chest = chest.transform.localPosition;

        Chest.y = Chest.y + chestDisplacement / metersToCentimeters;

        chest.transform.localPosition = Chest;

        //ARM SPAN (scaling shoulders)

        shoulderWidth = Vector3.Distance(leftShoulder.position, rightShoulder.position) * metersToCentimeters;
        float armSpan = Vector3.Distance(leftTip.position, rightTip.position) * metersToCentimeters; //Explanation in my notes

        float scaleFactorShoulders = (armSpanSubject - shoulderWidth) / (armSpan - shoulderWidth);
        scaleShoulders = new Vector3(1, scaleFactorShoulders, 1);
        leftShoulder.transform.localScale = scaleShoulders;
        rightShoulder.transform.localScale = scaleShoulders;

        //ELBOWS

        //Reposition

        Vector3 LElbow = leftElbow.transform.localPosition;
        Vector3 RElbow = rightElbow.transform.localPosition;

        float elbowSpan = Vector3.Distance(leftElbow.position, rightElbow.position) * metersToCentimeters;
        float elbowDisplacement = (elbowSpanSubject - elbowSpan) / 2;

        LElbow.y = LElbow.y + elbowDisplacement / metersToCentimeters; //In meters
        RElbow.y = RElbow.y + elbowDisplacement / metersToCentimeters; //The opposite sign bcs both x axes goes in the same direction

        leftElbow.transform.localPosition = LElbow;
        rightElbow.transform.localPosition = RElbow;

        //Scaling (to have the proper arm span)
        elbowSpan = Vector3.Distance(leftElbow.position, rightElbow.position) * metersToCentimeters;
        armSpan = Vector3.Distance(leftTip.position, rightTip.position) * metersToCentimeters;

        float scaleFactorElbow = (armSpanSubject - elbowSpan) / (armSpan - elbowSpan);

        scaleElbow = new Vector3(1, scaleFactorElbow, 1);
        leftElbow.transform.localScale = scaleElbow;
        rightElbow.transform.localScale = scaleElbow;

        //WRISTS

        //Reposition
        Vector3 LWrist = leftWrist.transform.localPosition;
        Vector3 RWrist = rightWrist.transform.localPosition;

        float wristSpan = Vector3.Distance(leftWrist.position, rightWrist.position) * metersToCentimeters;

        float wristDisplacement = (wristSpanSubject - wristSpan) / 2;


        LWrist.y = LWrist.y + wristDisplacement / metersToCentimeters; //In meters
        RWrist.y = RWrist.y + wristDisplacement / metersToCentimeters;

        leftWrist.transform.localPosition = LWrist;
        rightWrist.transform.localPosition = RWrist;

        //Scaling
        wristSpan = Vector3.Distance(leftWrist.position, rightWrist.position) * metersToCentimeters;
        armSpan = Vector3.Distance(leftTip.position, rightTip.position) * metersToCentimeters;

        float scaleFactorWrist = (armSpanSubject - wristSpan) / (armSpan - wristSpan);

        scaleWrist = new Vector3(1, scaleFactorWrist, 1);
        leftWrist.transform.localScale = scaleWrist;
        rightWrist.transform.localScale = scaleWrist;


        //Head (it does not scalate propertly
        float bodyHeight = Vector3.Distance(headEnd.position, (leftToe.position + rightToe.position) / 2) * metersToCentimeters;

        float scaleFactorHead = bodyHeightSubject / bodyHeight;



        scaleHead = new Vector3(1, scaleFactorHead, 1);
        head.transform.localScale = scaleHead;

        HumanMale.transform.rotation = Quaternion.Euler(new Vector3(90, 0, 0)); // To pass from Euler angles to quaternion

        // Body height
        bodyHeight = Vector3.Distance(headEnd.position, (leftToe.position + rightToe.position) / 2) * metersToCentimeters;
        Debug.Log("Body height: " + bodyHeight + " cm");


        // Shoulder height
        shoulderHeight = leftShoulder.position.y * metersToCentimeters; //Global variables. Height is in y direction
        Debug.Log("Shoulder height: " + shoulderHeight + " cm");

        // Shoulder width
        shoulderWidth = Vector3.Distance(leftShoulder.position, rightShoulder.position) * metersToCentimeters;
        Debug.Log("Shoulder width: " + shoulderWidth + " cm");

        // Elbow span
        elbowSpan = Vector3.Distance(leftElbow.position, rightElbow.position) * metersToCentimeters;
        Debug.Log("Elbow span: " + elbowSpan + " cm");

        //Carpus span 
        wristSpan = Vector3.Distance(leftWrist.position, rightWrist.position) * metersToCentimeters;
        Debug.Log("Wrist span: " + wristSpan + " cm");

        // Arm span
        armSpan = Vector3.Distance(leftTip.position, rightTip.position) * metersToCentimeters;
        Debug.Log("Arm span: " + armSpan + " cm");

        // Hip height 
        hipHeight = (leftHip.position.y - leftToe.position.y) * metersToCentimeters;
        Debug.Log("Hip height: " + hipHeight + " cm");


        //Hip width
        hipWidth = Vector3.Distance(leftHipBorder.position, rightHipBorder.position) * metersToCentimeters; //It takes global variables not local         
        Debug.Log("Hip width: " + hipWidth + " cm");

        // Knee height 
        KneeHeight = (leftKnee.position.y - leftToe.position.y) * metersToCentimeters;
        Debug.Log(" Knee height: " + KneeHeight + " cm");


        // Ankle height 
        ankleHeight = (leftAnkle.position.y - leftToe.position.y) * metersToCentimeters;
        Debug.Log("Ankle height: " + ankleHeight + " cm");
    }

    // Start is called before the first frame update
    void Start()
    {
        //BONES LOADING
        bonesLoading();

        avatarScaling();

    }

    // Update is called once per frame
    void Update()
    {

    }
}
