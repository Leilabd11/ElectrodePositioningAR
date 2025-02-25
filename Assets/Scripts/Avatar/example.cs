using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.AffordanceSystem.Receiver.Primitives;

public class example : MonoBehaviour
{
    // Declaration of variables for bones and body parts
    private Transform head, headEnd, leftToe, rightToe, leftScapula, rightScapula;
    private Transform chest, leftShoulder, rightShoulder, leftElbow, rightElbow, leftWrist, rightWrist;
    private Transform leftTip, rightTip, hips, leftHip, rightHip, leftHipBorder, rightHipBorder;
    private Transform leftKnee, rightKnee, leftAnkle, rightAnkle;

    // Main GameObject of the avatar where the bones are located
    public GameObject HumanMale;

    // Scaling vectors
    private Vector3 scaleHip, scaleKnee, scaleAnkle, scaleScapulas, widthScaleShoulders, heightScaleShoulders, scaleShoulders, scaleElbow, scaleWrist, scaleHead;

    // Subject’s body measurements in cm
    public float bodyHeightSubject, shoulderHeightSubject, shoulderWidthSubject, elbowSpanSubject;
    public float wristSpanSubject, armSpanSubject, hipHeightSubject, hipWidhtSubject, kneeHeightSubject, ankleHeightSubject;

    private float metersToCentimeters = 100f;

    void Start()
    {
        LoadBones();
        ScaleAvatar();
        HumanMale.transform.rotation = Quaternion.Euler(new Vector3(90, 0, 0)); // To pass from Euler angles to quaternion
    }

    // Loads all required bones for the avatar
    void LoadBones()
    {
        // Head
        head = FindBone("mixamorig1:Hips/mixamorig1:Spine/mixamorig1:Spine1/mixamorig1:Spine2/mixamorig1:Neck/mixamorig1:Head");
        headEnd = FindBone("mixamorig1:Hips/mixamorig1:Spine/mixamorig1:Spine1/mixamorig1:Spine2/mixamorig1:Neck/mixamorig1:Head/mixamorig1:HeadTop_End");

        // Toes
        leftToe = FindBone("mixamorig1:Hips/mixamorig1:LeftUpLeg/mixamorig1:LeftLeg/mixamorig1:LeftFoot/mixamorig1:LeftToeBase");
        rightToe = FindBone("mixamorig1:Hips/mixamorig1:RightUpLeg/mixamorig1:RightLeg/mixamorig1:RightFoot/mixamorig1:RightToeBase");

        // Chest
        chest = FindBone("mixamorig1:Hips/mixamorig1:Spine/mixamorig1:Spine1/mixamorig1:Spine2");

        // Scapulas
        leftScapula = FindBone("mixamorig1:Hips/mixamorig1:Spine/mixamorig1:Spine1/mixamorig1:Spine2/mixamorig1:LeftShoulder");
        rightScapula = FindBone("mixamorig1:Hips/mixamorig1:Spine/mixamorig1:Spine1/mixamorig1:Spine2/mixamorig1:RightShoulder");

        // Shoulders
        leftShoulder = FindBone("mixamorig1:Hips/mixamorig1:Spine/mixamorig1:Spine1/mixamorig1:Spine2/mixamorig1:LeftShoulder/mixamorig1:LeftArm");
        rightShoulder = FindBone("mixamorig1:Hips/mixamorig1:Spine/mixamorig1:Spine1/mixamorig1:Spine2/mixamorig1:RightShoulder/mixamorig1:RightArm");

        // Elbows
        leftElbow = FindBone("mixamorig1:Hips/mixamorig1:Spine/mixamorig1:Spine1/mixamorig1:Spine2/mixamorig1:LeftShoulder/mixamorig1:LeftArm/mixamorig1:LeftForeArm");
        rightElbow = FindBone("mixamorig1:Hips/mixamorig1:Spine/mixamorig1:Spine1/mixamorig1:Spine2/mixamorig1:RightShoulder/mixamorig1:RightArm/mixamorig1:RightForeArm");

        // Wrists (Carpus)
        leftWrist = FindBone("mixamorig1:Hips/mixamorig1:Spine/mixamorig1:Spine1/mixamorig1:Spine2/mixamorig1:LeftShoulder/mixamorig1:LeftArm/mixamorig1:LeftForeArm/mixamorig1:LeftHand");
        rightWrist = FindBone("mixamorig1:Hips/mixamorig1:Spine/mixamorig1:Spine1/mixamorig1:Spine2/mixamorig1:RightShoulder/mixamorig1:RightArm/mixamorig1:RightForeArm/mixamorig1:RightHand");

        // Finger tips
        leftTip = FindBone("mixamorig1:Hips/mixamorig1:Spine/mixamorig1:Spine1/mixamorig1:Spine2/mixamorig1:LeftShoulder/mixamorig1:LeftArm/mixamorig1:LeftForeArm/mixamorig1:LeftHand/mixamorig1:LeftHandMiddle1/mixamorig1:LeftHandMiddle2/mixamorig1:LeftHandMiddle3/mixamorig1:LeftHandMiddle4");
        rightTip = FindBone("mixamorig1:Hips/mixamorig1:Spine/mixamorig1:Spine1/mixamorig1:Spine2/mixamorig1:RightShoulder/mixamorig1:RightArm/mixamorig1:RightForeArm/mixamorig1:RightHand/mixamorig1:RightHandMiddle1/mixamorig1:RightHandMiddle2/mixamorig1:RightHandMiddle3/mixamorig1:RightHandMiddle4");

        // Hips
        hips = FindBone("mixamorig1:Hips");

        // Hip bones
        leftHip = FindBone("mixamorig1:Hips/mixamorig1:LeftUpLeg");
        rightHip = FindBone("mixamorig1:Hips/mixamorig1:RightUpLeg");

        //Hip borders
        leftHipBorder = FindBone("mixamorig1:Hips/mixamorig1:LeftUpLeg/mixamorig1:LeftHipBorder");
        rightHipBorder = FindBone("mixamorig1:Hips/mixamorig1:RightUpLeg/mixamorig1:RightHipBorder");

        // Knees
        leftKnee = FindBone("mixamorig1:Hips/mixamorig1:LeftUpLeg/mixamorig1:LeftLeg");
        rightKnee = FindBone("mixamorig1:Hips/mixamorig1:RightUpLeg/mixamorig1:RightLeg");

        // Ankles
        leftAnkle = FindBone("mixamorig1:Hips/mixamorig1:LeftUpLeg/mixamorig1:LeftLeg/mixamorig1:LeftFoot");
        rightAnkle = FindBone("mixamorig1:Hips/mixamorig1:RightUpLeg/mixamorig1:RightLeg/mixamorig1:RightFoot");
        

        // Check for nulls after loading
        CheckBones();
    }

    // Helper function to find a bone and log an error if not found
    Transform FindBone(string path)
    {
        Transform bone = HumanMale.transform.Find(path);
        if (bone == null) Debug.LogError($"The bone '{path}' was not found.");
        return bone;
    }

    // Checks if any essential bone was not found
    void CheckBones()
    {
        if (head == null || leftToe == null || rightToe == null || leftShoulder == null || rightShoulder == null ||
            leftElbow == null || rightElbow == null || leftWrist == null || rightWrist == null ||
            hips == null || leftKnee == null || rightKnee == null || leftAnkle == null || rightAnkle == null || leftTip == null ||
            rightTip == null || leftHip == null || rightHip == null || leftHipBorder == null || rightHipBorder == null)
        {
            Debug.LogError("Some bones were not found. Check the names in the hierarchy.");
            return;
        }
    }

    // Controls the overall scaling of the avatar
    void ScaleAvatar()
    {
        ScaleLegs();
        ScaleUpperBody();
        ScaleHead();
    }

    // Scales the legs of the avatar
    void ScaleLegs()
    {
        ScaleHip();
        ScaleKnee();
        ScaleAnkle();
    }

    void ScaleHip()
    {
        //Height
        float hipHeight = CalculateHeight(leftHip, leftToe);
        float scaleFactor = hipHeightSubject / hipHeight;
        scaleHip = new Vector3(1, scaleFactor, 1);
        ApplyScale(leftHip, rightHip, scaleHip);

        //Width
        float hipWidth = CalculateWidth(leftHipBorder, rightHipBorder);
        float varHipWidth = hipWidhtSubject / metersToCentimeters - hipWidth;
        AdjustPosition(leftHip, rightHip, varHipWidth, "Hip");
    }

    void ScaleKnee()
    {
        // Scaling
        float toeHeight1 = leftToe.position.y;
        
        float kneeHeight = CalculateHeight(leftKnee, leftToe);
        float scaleFactor = kneeHeightSubject / kneeHeight;
        scaleKnee = new Vector3(1, scaleFactor, 1);
        ApplyScale(leftKnee, rightKnee, scaleKnee);

        //Repositioning
        float toeHeight2 = leftToe.position.y;
        float kneeDisplacement = toeHeight2 - toeHeight1;

        AdjustPosition(leftKnee, rightKnee, kneeDisplacement, "Knee");
    }

    void ScaleAnkle()
    {
        float toeHeight1 = leftToe.position.y;

        float ankleHeight = CalculateHeight(leftAnkle, leftToe);
        float scaleFactor = ankleHeightSubject / ankleHeight;
        scaleAnkle = new Vector3(scaleFactor, scaleFactor, scaleFactor);
        ApplyScale(leftAnkle, rightAnkle, scaleAnkle);

        float toeHeight2 = leftToe.position.y;
        float ankleDisplacement = toeHeight2 - toeHeight1;

        AdjustPosition(leftAnkle, rightAnkle, ankleDisplacement, "Ankle");
    }

    // Scales the upper body parts of the avatar
    void ScaleUpperBody()
    {
        ScaleShoulders();
        ScaleArms();
        ScaleElbows();
        ScaleWrists();
    }

    void ScaleShoulders()
    {
        //Width (scapulas)
        float shoulderWidth = CalculateWidth(leftShoulder, rightShoulder);
        float widthScaleFactor = shoulderWidthSubject / shoulderWidth;
        widthScaleShoulders = new Vector3(widthScaleFactor, widthScaleFactor, widthScaleFactor);
        ApplyScale(leftShoulder, rightShoulder, widthScaleShoulders);

        //Height 
        float shoulderHeight = CalculateHeight(leftShoulder, leftToe);
        float chestDisplacement = (shoulderHeightSubject - shoulderHeight) /metersToCentimeters;

        //I cannot use AdjustPosition function because it is just for one bone, not two
        Vector3 chestPosition = chest.transform.localPosition;

        AdjustPosition(chest, chest, chestDisplacement, "Chest");
    }

    void ScaleArms()
    {
        float shoulderWidth = CalculateWidth(leftShoulder, rightShoulder);
        float armSpan = CalculateWidth (leftTip, rightTip);

        float scaleFactorShoulders = (armSpanSubject - shoulderWidth) / (armSpan-shoulderWidth);
        scaleShoulders = new Vector3(1, scaleFactorShoulders, 1);
        ApplyScale(leftShoulder,rightShoulder, scaleShoulders); 

    }

    void ScaleElbows()
    {
        //Repositioning
        float elbowSpan = CalculateWidth(leftElbow, rightElbow);
        float elbowDisplacement = ((elbowSpanSubject - elbowSpan) / 2) /metersToCentimeters;

        AdjustPosition(leftElbow, rightElbow, elbowDisplacement, "Elbow");

        //Scaling (to have the proper arm span)
        elbowSpan = CalculateWidth(leftElbow, rightElbow); //Recalculate bcs it has changed
        float armSpan = CalculateWidth(leftTip, rightTip);

        float scaleFactor = (armSpanSubject - elbowSpan) / (armSpan - elbowSpan);
        scaleElbow = new Vector3(1, scaleFactor, 1);
        ApplyScale(leftElbow, rightElbow, scaleElbow);
    }

    void ScaleWrists()
    {
        //Repositioning
        float wristSpan = CalculateWidth(leftWrist, rightWrist);
        float wristDisplacement = ((wristSpanSubject - wristSpan) / 2) / metersToCentimeters;

        AdjustPosition(leftWrist, rightWrist, wristDisplacement, "Wrist");

        //Scaling (to have the proper arm span)
        wristSpan = CalculateWidth(leftWrist, rightWrist); //Recalculate bcs it has changed
        float armSpan = CalculateWidth(leftTip, rightTip);

        float scaleFactor = (armSpanSubject - wristSpan) / (armSpan - wristSpan);
        scaleWrist = new Vector3(1, scaleFactor, 1);
        ApplyScale(leftWrist, rightWrist, scaleWrist);
    }

    void ScaleHead()
    {
        float bodyHeight = Vector3.Distance(headEnd.position, (leftToe.position + rightToe.position) / 2) * metersToCentimeters;
        float scaleFactor = bodyHeightSubject / bodyHeight;
        scaleHead = new Vector3(1, scaleFactor, 1);
        head.transform.localScale = scaleHead;
    }

    // Helper function to apply the same scale to two bones
    void ApplyScale(Transform bone1, Transform bone2, Vector3 scale)
    {
        bone1.transform.localScale = scale;
        bone2.transform.localScale = scale;
    }

    // Calculates height difference between two points
    float CalculateHeight(Transform top, Transform bottom)
    {
        return (top.position.y - bottom.position.y) * metersToCentimeters;
    }

    // Calculates width between two points
    float CalculateWidth(Transform left, Transform right)
    {
        return Vector3.Distance(left.position, right.position) * metersToCentimeters;
    }

    // Adjusts position of a bone pair based on scaling factor
    void AdjustPosition(Transform bone1, Transform bone2, float displacement,string bodyPart)
    {
        Vector3 bone1Position = bone1.transform.localPosition;
        Vector3 bone2Position = bone2.transform.localPosition;

        if(bodyPart == "Hip") {
            bone1Position.x -= displacement / 2;
            bone2Position.x += displacement / 2;
        }

        if(bodyPart == "Knee" || bodyPart == "Ankle" || bodyPart == "Elbow" || bodyPart =="Wrist") { 
        bone1Position.y += displacement;
        bone2Position.y += displacement;
        }

        if(bodyPart == "Chest")
        {
            bone1Position.y += displacement;
        }
        bone1.transform.localPosition = bone1Position;
        //For chest, only one bone
        if(bodyPart != "Chest") { 
            bone2.transform.localPosition= bone2Position;
        }
    }
}
