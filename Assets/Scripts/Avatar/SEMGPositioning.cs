using MixedReality.Toolkit;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

public class SEMGPositioning : MonoBehaviour
{
    public GameObject HumanMale;
    public GameObject arrowPrefab;
    private GameObject intermediateLeg;
    private GameObject lowerLeg;
    private GameObject upperLeg;
    private GameObject restBody;

    private Transform leftUpLeg;
    private Transform leftKnee;

    private Transform CylinderFibula;
    private Transform CylinderMalleoulus;
    private Transform CylinderSpinaIliaca;
    private Transform CylinderPatella;

    private GameObject electrodePeroneusLSENIAM;
    private GameObject electrodePeroneusLATLAS;
    private GameObject electrodeVastusLateralisSENIAM;
    private GameObject electrodeVastusLateralisATLAS;
    private GameObject muscleBellyPos;
    private GameObject unrotatedPos;

    private Vector3 initialFibulaPosition;
    private Vector3 initialMalleoulusPosition;
    private Vector3 initialSpinaIliacaPosition;
    private Vector3 initialPatellaPosition;

    public string currentUserName;
    public string muscle;
    float rot = 20f;
    public float muscleBellyDistance = 0.04f;
    float lastMuscleBellyDistance;
    float i = 0;
    
    //Landmarks displacements 

    //PERONEUS LONGUS
    //CylinderFibula (Fibula)
    [Range(-0.07f, 0.07f)]
    [SerializeField] private float _fibulaLateralDisplacement;
    [Range(-0.13f, 0.4f)]
    [SerializeField] private float _fibulaVerticalDisplacement;

    //CylinderMalleoulus (LatMalleolus)
    [Range(-0.05f, 0.05f)]
    [SerializeField] private float _malleoulusLateralDisplacement;
    [Range(-0.55f, 0.03f)]
    [SerializeField] private float _malleoulusVerticalDisplacement;

    //VASTUS LATERALIS
    //CylinderSpinaIliaca (Anterior spina iliaca superior)
    [Range(-0.07f, 0.07f)]
    [SerializeField] private float _spinaIliacaLateralDisplacement;
    [Range(-0.13f, 0.4f)]
    [SerializeField] private float _spinaIliacaVerticalDisplacement;

    //CylinderPatella (Lateral side of the patella)
    [Range(-0.05f, 0.05f)]
    [SerializeField] private float _patellaLateralDisplacement;
    [Range(-0.55f, 0.03f)]
    [SerializeField] private float _patellaVerticalDisplacement;
    [Range(-0.55f, 0.03f)]
    [SerializeField] private float _patellaFrontalLateralDisplacement;
    // Properties to get/set displacement values

    //PERONEUS LONGUS
    public float fibulaLateralDisplacement
    {
        get { return _fibulaLateralDisplacement; }
        set
        {
            _fibulaLateralDisplacement = value;
            Debug.Log("Lat displ fibula: " + _fibulaLateralDisplacement);

        }
    }

    float lastFibulaLateralDisplacement;
    //float lastFibulaLateralDisplacement = 0;

    public float fibulaVerticalDisplacement
    {
        get { return _fibulaVerticalDisplacement; }
        set
        {
            _fibulaVerticalDisplacement = value;
            Debug.Log("Vertical displ fibula: " + _fibulaVerticalDisplacement);

        }
    }

    float lastFibulaVerticalDisplacement;
    //float lastFibulaVerticalDisplacement = 0;


    // Properties to get/set displacement values LATERAL MALLEOULUS

    public float malleoulusLateralDisplacement
    {
        get { return _malleoulusLateralDisplacement; }
        set
        {
            _malleoulusLateralDisplacement = value;
            Debug.Log("Lat displ malleoulus: " + _malleoulusLateralDisplacement);

        }
    }

    float lastMalleoulusLateralDisplacement;
    //float lastMalleoulusLateralDisplacement = 0;

    public float malleoulusVerticalDisplacement
    {
        get { return _malleoulusVerticalDisplacement; }
        set
        {
            _malleoulusVerticalDisplacement = value;
            Debug.Log("Vertical displ malleoulus: " + _malleoulusVerticalDisplacement);

        }
    }

    float lastMalleoulusVerticalDisplacement;
    //float lastMalleoulusVerticalDisplacement = 0;

    //VASTUS LATERALIS
    public float spinaIliacaLateralDisplacement
    {
        get { return _spinaIliacaLateralDisplacement; }
        set
        {
            _spinaIliacaLateralDisplacement = value;
            Debug.Log("Lat displ spina iliaca: " + _spinaIliacaLateralDisplacement);

        }
    }

    float lastSpinaIliacaLateralDisplacement;
    //float lastFibulaLateralDisplacement = 0;

    public float spinaIliacaVerticalDisplacement
    {
        get { return _spinaIliacaVerticalDisplacement; }
        set
        {
            _spinaIliacaVerticalDisplacement = value;
            Debug.Log("Vertical displ spina iliaca: " + _fibulaVerticalDisplacement);

        }
    }

    float lastSpinaIliacaVerticalDisplacement;
    //float lastFibulaVerticalDisplacement = 0;


    // Properties to get/set displacement values LATERAL MALLEOULUS

    public float patellaLateralDisplacement
    {
        get { return _patellaLateralDisplacement; }
        set
        {
            _patellaLateralDisplacement = value;
            Debug.Log("Lat displ patella: " + _patellaLateralDisplacement);

        }
    }

    float lastPatellaLateralDisplacement;
    //float lastMalleoulusLateralDisplacement = 0;

    public float patellaVerticalDisplacement
    {
        get { return _patellaVerticalDisplacement; }
        set
        {
            _patellaVerticalDisplacement = value;
            Debug.Log("Vertical displ patella: " + _patellaVerticalDisplacement);

        }
    }

    float lastPatellaVerticalDisplacement;
    //float lastMalleoulusVerticalDisplacement = 0;

    public float patellaFrontalLateralDisplacement
    {
        get { return _patellaFrontalLateralDisplacement; }
        set
        {
            _patellaVerticalDisplacement = value;
            Debug.Log("Frontal Vertical displ patella: " + _patellaFrontalLateralDisplacement);

        }
    }

    float lastPatellaFrontalLateralDisplacement;
    //float lastMalleoulusVerticalDisplacement = 0;

    // Start is called before the first frame update
    void Start()
    {
        
        leftKnee = HumanMale.transform.Find("mixamorig1:Hips/mixamorig1:LeftUpLeg/mixamorig1:LeftLeg");
        leftUpLeg = HumanMale.transform.Find("mixamorig1:Hips/mixamorig1:LeftUpLeg");

        intermediateLeg = GameObject.Find("IntermediateLeg");
        lowerLeg = GameObject.Find("LowerLeg");
        upperLeg = GameObject.Find("UpperLeg");
        restBody = GameObject.Find("RestBody");

        if (intermediateLeg == null) Debug.LogError("The cylinder 'IntermediateLeg' was not found.");
        if (lowerLeg == null) Debug.LogError("The cylinder 'LowerLeg' was not found.");
        if (upperLeg == null) Debug.LogError("The cylinder 'UpperLeg' was not found.");
        if (restBody == null) Debug.LogError("The cylinder 'RestBody' was not found.");

        if(muscle == "Peroneus Longus") { 
            HumanMale.transform.Rotate(-8.0f, 0.0f, 0.0f, Space.Self); 
        }
        //PERONEUS LONGUS
        CylinderFibula = HumanMale.transform.Find("mixamorig1:Hips/mixamorig1:LeftUpLeg/CylinderFibula");
        CylinderMalleoulus = HumanMale.transform.Find("mixamorig1:Hips/mixamorig1:LeftUpLeg/mixamorig1:LeftLeg/CylinderMalleoulus");

        if (CylinderFibula == null) Debug.LogError("The cylinder 'CylinderFibula' was not found.");
        if (CylinderMalleoulus == null) Debug.LogError("The cylinder 'CylinderMalleoulus' was not found.");

        //VASTUS LATERALIS
        CylinderSpinaIliaca = HumanMale.transform.Find("mixamorig1:Hips/mixamorig1:LeftUpLeg/CylinderSpinaIliaca");
        CylinderPatella = HumanMale.transform.Find("mixamorig1:Hips/mixamorig1:LeftUpLeg/CylinderPatella");

        if (CylinderSpinaIliaca == null) Debug.LogError("The cylinder 'CylinderSpinaIliaca' was not found.");
        if (CylinderPatella == null) Debug.LogError("The cylinder 'CylinderPatella' was not found.");
        //PERONEUS LONGUS
        if (muscle == "Peroneus Longus") {

            CylinderPatella.gameObject.SetActive(false);
            CylinderSpinaIliaca.gameObject.SetActive(false);
            restBody.gameObject.SetActive(false);
            upperLeg.gameObject.SetActive(false);

            Vector3 lowerPointPeroneusL = CylinderMalleoulus.transform.position;        
            Vector3 upperPointPeroneusL = CylinderFibula.transform.position;

            //SENIAM
            Vector3 intermediatePointPeroneusLSENIAM = Vector3.Lerp(upperPointPeroneusL, lowerPointPeroneusL, 1f/3f); //World position

            //Create a cylinder and place it at the intermediate point
            electrodePeroneusLSENIAM = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            electrodePeroneusLSENIAM.name = "ElectrodePeroneusLSENIAM"; // Assign an specific name
        
            electrodePeroneusLSENIAM.transform.position = intermediatePointPeroneusLSENIAM;
            electrodePeroneusLSENIAM.transform.rotation = Quaternion.Euler(2.4420011f, 0.289002538f, 270f);

            // Adjust the cylinder's size. Ensure it has a small radius and appropriate height.
            electrodePeroneusLSENIAM.transform.localScale = new Vector3(0.02f, 0.02f, 0.02f); // Adjust Y value to control height
            electrodePeroneusLSENIAM.GetComponent<Renderer>().material.color = Color.red;
            electrodePeroneusLSENIAM.transform.parent = leftKnee.transform;

            //ATLAS
            Vector3 intermediatePointPeroneusLATLAS = Vector3.Lerp(upperPointPeroneusL, lowerPointPeroneusL, 0.38f);
            electrodePeroneusLATLAS = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            electrodePeroneusLATLAS.name = "ElectrodePeroneusLATLAS";

            electrodePeroneusLATLAS.transform.position = intermediatePointPeroneusLATLAS;
            electrodePeroneusLATLAS.transform.rotation = Quaternion.Euler(2.4420011f, 0.289002538f, 270f);

            // Adjust the cylinder's size. Ensure it has a small radius and appropriate height.
            electrodePeroneusLATLAS.transform.localScale = new Vector3(0.02f, 0.02f, 0.02f); // Adjust Y value to control height
            electrodePeroneusLATLAS.GetComponent<Renderer>().material.color = Color.blue;
            electrodePeroneusLATLAS.transform.parent = leftKnee.transform;

            initialFibulaPosition = CylinderFibula.transform.localPosition;
            initialMalleoulusPosition = CylinderMalleoulus.transform.localPosition;
        }

        //VASTUS LATERALIS (VL)
        if (muscle == "Vastus Lateralis") {

            CylinderFibula.gameObject.SetActive(false);
            CylinderMalleoulus.gameObject.SetActive(false);
            restBody.gameObject.SetActive(false);
            lowerLeg.gameObject.SetActive(false);

            Vector3 patellaVL = CylinderPatella.transform.position;        
            Vector3 spinaIliacaVL = CylinderSpinaIliaca.transform.position;        

            //SENIAM
            Vector3 intermediatePointVastusLateralisSENIAM = Vector3.Lerp(patellaVL, spinaIliacaVL, 2f / 3f);
            //Create a cylinder and place it at the intermediate point
            electrodeVastusLateralisSENIAM = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            electrodeVastusLateralisSENIAM.name = "ElectrodeVastusLateralisSENIAM"; // Assign an specific name
            electrodeVastusLateralisSENIAM.transform.position = intermediatePointVastusLateralisSENIAM;
            electrodeVastusLateralisSENIAM.transform.rotation = Quaternion.Euler(0f, 0f, -45f);
            // Adjust the cylinder's size. Ensure it has a small radius and appropriate height.
            electrodeVastusLateralisSENIAM.transform.localScale = new Vector3(0.02f, 0.06f, 0.02f); // Adjust Y value to control height
            electrodeVastusLateralisSENIAM.GetComponent<Renderer>().material.color = Color.red;
            electrodeVastusLateralisSENIAM.transform.parent = leftUpLeg.transform;

            //ATLAS           
            // Compute the reference vector (from SIAS to Patella)        
            Vector3 referenceVectorVL = (spinaIliacaVL - patellaVL).normalized;
            Vector3 muscleBelly = patellaVL + referenceVectorVL * muscleBellyDistance;                      

            muscleBellyPos = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            muscleBellyPos.name = "muscleBellyPos";
            muscleBellyPos.transform.position = muscleBelly;
            muscleBellyPos.transform.rotation = Quaternion.Euler(0f, 0f, -45f);
            muscleBellyPos.transform.localScale = new Vector3(0.01f, 0.06f, 0.01f);
            muscleBellyPos.transform.parent = leftUpLeg.transform;
            muscleBellyPos.GetComponent<Renderer>().material.color = Color.green;
            
            //20º rotated direction computation
            Vector3 customDirection = new Vector3 (86.3481064f, 293.947998f, 344.041931f).normalized;
            Vector3 rotationAxis = Vector3.Cross(referenceVectorVL, customDirection).normalized;
            Quaternion rotation = Quaternion.AngleAxis(-rot, rotationAxis); //20º rotation
            Vector3 newDirection = rotation * referenceVectorVL;
            Vector3 rotatedPosition = muscleBelly + newDirection.normalized * 0.165f; //Electrode's position            
            Vector3 intermediatePointVastusLateralisATLAS = rotatedPosition;
            electrodeVastusLateralisATLAS = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            electrodeVastusLateralisATLAS.name = "ElectrodeVastusLateralisATLAS";
            electrodeVastusLateralisATLAS.transform.position = intermediatePointVastusLateralisATLAS;           
            electrodeVastusLateralisATLAS.transform.rotation = Quaternion.Euler(0f, 0f, -45f);
            // Adjust the cylinder's size. Ensure it has a small radius and appropriate height.
            electrodeVastusLateralisATLAS.transform.localScale = new Vector3(0.02f, 0.06f, 0.02f); // Adjust Y value to control height
            electrodeVastusLateralisATLAS.GetComponent<Renderer>().material.color = Color.blue;
            electrodeVastusLateralisATLAS.transform.parent = leftUpLeg.transform;

            initialSpinaIliacaPosition = CylinderSpinaIliaca.transform.localPosition;
            initialPatellaPosition = CylinderPatella.transform.localPosition;
        }


        //LOAD OF USERNAME PARAMETERS (instead of 0, we use personal configuration)
        var dataManager = DataManager.Instance;
        if (dataManager == null) return;

        var userData = dataManager.LoadOrInitializeUserData(currentUserName); //If name does not exists in database, all variables will be equal to zero. Thus, no error should occur

        //PERONEUS LONGUS
        if (muscle == "Peroneus Longus")
        {
            lastFibulaLateralDisplacement = userData.fibulaLateralDisplacement;
            lastFibulaVerticalDisplacement = userData.fibulaVerticalDisplacement;

            lastMalleoulusLateralDisplacement = userData.malleoulusLateralDisplacement;
            lastMalleoulusVerticalDisplacement = userData.malleoulusVerticalDisplacement;
        }
        //VASTUS LATERALIS
        if (muscle == "Vastus Lateralis")
        {
            lastSpinaIliacaLateralDisplacement = userData.spinaIliacaLateralDisplacement;
            lastSpinaIliacaVerticalDisplacement = userData.spinaIliacaVerticalDisplacement;

            lastPatellaLateralDisplacement = userData.patellaLateralDisplacement;
            lastPatellaVerticalDisplacement = userData.patellaVerticalDisplacement;
            lastPatellaFrontalLateralDisplacement = userData.patellaFrontalLateralDisplacement;

            lastMuscleBellyDistance = muscleBellyDistance;
        }
    }

    void UpdateLandmarksPositionPL()
    {
        var dataManager = DataManager.Instance;
        if (dataManager == null) return;

        var userData = dataManager.LoadOrInitializeUserData(currentUserName);
                
        if (i == 0)
        {

            Vector3 lowerPointPeroneusL = initialMalleoulusPosition + new Vector3(0, userData.malleoulusVerticalDisplacement, userData.malleoulusLateralDisplacement); //World position

            CylinderMalleoulus.transform.localPosition = lowerPointPeroneusL;
            malleoulusLateralDisplacement = userData.malleoulusLateralDisplacement;
            malleoulusVerticalDisplacement = userData.malleoulusVerticalDisplacement;
            

            Vector3 upperPointPeroneusL = initialFibulaPosition + new Vector3(0, userData.fibulaVerticalDisplacement, userData.fibulaLateralDisplacement);
            CylinderFibula.transform.localPosition = upperPointPeroneusL;
            fibulaLateralDisplacement = userData.fibulaLateralDisplacement;
            fibulaVerticalDisplacement = userData.fibulaVerticalDisplacement;

            //SENIAM
            //
            Vector3 intermediatePointPeroneusLSENIAM = Vector3.Lerp(CylinderFibula.transform.position, CylinderMalleoulus.transform.position, 1f / 3f); //This should be done in world position, that is why it was not working well before (I was using local positions)!!                        
            electrodePeroneusLSENIAM.transform.position = intermediatePointPeroneusLSENIAM;

            //ATLAS            
            Vector3 intermediatePointPeroneusLATLAS = Vector3.Lerp(CylinderFibula.transform.position, CylinderMalleoulus.transform.position, 0.38f);
            electrodePeroneusLATLAS.transform.position = intermediatePointPeroneusLATLAS;            

            i++;
            //Debug.Log("Initial position");
        }

        if ((lastFibulaLateralDisplacement != _fibulaLateralDisplacement || lastFibulaVerticalDisplacement != _fibulaVerticalDisplacement || lastMalleoulusLateralDisplacement != _malleoulusLateralDisplacement || lastMalleoulusVerticalDisplacement != _malleoulusVerticalDisplacement)) //To update the position when either the variables or QR code's position are changed 
        {

            Vector3 lowerPointPeroneusL = initialMalleoulusPosition + new Vector3(0, malleoulusVerticalDisplacement, malleoulusLateralDisplacement); //World position. I think it is local as initial pos is local
            CylinderMalleoulus.transform.localPosition = lowerPointPeroneusL;

            Vector3 upperPointPeroneusL = initialFibulaPosition + new Vector3(0, fibulaVerticalDisplacement, fibulaLateralDisplacement);
            CylinderFibula.transform.localPosition = upperPointPeroneusL;

            //SENIAM            
            Vector3 intermediatePointPeroneusLSENIAM = Vector3.Lerp(CylinderFibula.transform.position, CylinderMalleoulus.transform.position, 1f / 3f);            
            electrodePeroneusLSENIAM.transform.position = intermediatePointPeroneusLSENIAM;

            //ATLAS
            Vector3 intermediatePointPeroneusLATLAS = Vector3.Lerp(CylinderFibula.transform.position, CylinderMalleoulus.transform.position, 0.38f);
            electrodePeroneusLATLAS.transform.position = intermediatePointPeroneusLATLAS;


            lastFibulaLateralDisplacement = _fibulaLateralDisplacement;
            userData.fibulaLateralDisplacement = _fibulaLateralDisplacement;
            lastFibulaVerticalDisplacement = _fibulaVerticalDisplacement;
            userData.fibulaVerticalDisplacement = _fibulaVerticalDisplacement;

            lastMalleoulusLateralDisplacement = _malleoulusLateralDisplacement;
            userData.malleoulusLateralDisplacement = lastMalleoulusLateralDisplacement;
            lastMalleoulusVerticalDisplacement = _malleoulusVerticalDisplacement;
            userData.malleoulusVerticalDisplacement = lastMalleoulusVerticalDisplacement;            
                      
            dataManager.SaveData(currentUserName, userData);
            //Debug.Log("data saved");
        }
    }

    void UpdateLandmarksPositionVL()
    {
        var dataManager = DataManager.Instance;
        if (dataManager == null) return;

        var userData = dataManager.LoadOrInitializeUserData(currentUserName);

        if (i == 0)
        {

            Vector3 patellaVL = initialPatellaPosition + new Vector3(userData.patellaFrontalLateralDisplacement, userData.patellaVerticalDisplacement, userData.patellaLateralDisplacement); //World position            
            CylinderPatella.transform.localPosition = patellaVL;
            patellaLateralDisplacement = userData.patellaLateralDisplacement;
            patellaVerticalDisplacement = userData.patellaVerticalDisplacement;
            patellaFrontalLateralDisplacement = userData.patellaFrontalLateralDisplacement;

            Vector3 spinaIliacaVL = initialSpinaIliacaPosition + new Vector3(userData.spinaIliacaLateralDisplacement, userData.spinaIliacaVerticalDisplacement, 0);
            CylinderSpinaIliaca.transform.localPosition = spinaIliacaVL;
            spinaIliacaLateralDisplacement = userData.spinaIliacaLateralDisplacement;
            spinaIliacaVerticalDisplacement = userData.spinaIliacaVerticalDisplacement;

            //SENIAM
            
            Vector3 intermediatePointVastusLateralisSENIAM = Vector3.Lerp(CylinderSpinaIliaca.transform.position, CylinderPatella.transform.position, 2f / 3f); //This should be done in world position, that is why it was not working well before (I was using local positions)!!
            electrodeVastusLateralisSENIAM.transform.position = intermediatePointVastusLateralisSENIAM;

            //ATLAS                      
            Vector3 referenceVectorVL = (CylinderSpinaIliaca.transform.position - CylinderPatella.transform.position).normalized;
            Vector3 muscleBelly = CylinderPatella.transform.position + referenceVectorVL * muscleBellyDistance;                                                           
            Vector3 customDirection = new Vector3(86.3481064f, 293.947998f, 344.041931f).normalized;
            Vector3 rotationAxis = Vector3.Cross(referenceVectorVL, customDirection).normalized;
            Quaternion rotation = Quaternion.AngleAxis(-rot, rotationAxis);            
            Vector3 newDirection = rotation * referenceVectorVL;
            Vector3 rotatedPosition = muscleBelly + newDirection.normalized * 0.165f;

            muscleBellyPos.transform.position = muscleBelly;

            Vector3 intermediatePointVastusLateralisATLAS = rotatedPosition;
            electrodeVastusLateralisATLAS.transform.position = intermediatePointVastusLateralisATLAS;                        

            i++;
            //Debug.Log("Initial position");
        }

        if ((lastPatellaLateralDisplacement != _patellaLateralDisplacement || lastPatellaVerticalDisplacement != _patellaVerticalDisplacement || lastPatellaFrontalLateralDisplacement != _patellaFrontalLateralDisplacement|| lastSpinaIliacaLateralDisplacement != _spinaIliacaLateralDisplacement || lastSpinaIliacaVerticalDisplacement != _spinaIliacaVerticalDisplacement || lastMuscleBellyDistance != muscleBellyDistance)) //To update the position when either the variables or QR code's position are changed 
        {

            Vector3 patellaVL = initialPatellaPosition + new Vector3(patellaFrontalLateralDisplacement, patellaVerticalDisplacement, patellaLateralDisplacement); //World position. I think it is local as initial pos is local            
            CylinderPatella.transform.localPosition = patellaVL;
            
            Vector3 spinaIliacaVL = initialSpinaIliacaPosition + new Vector3(spinaIliacaLateralDisplacement, spinaIliacaVerticalDisplacement, 0);
            CylinderSpinaIliaca.transform.localPosition = spinaIliacaVL;

            //SENIAM
            Vector3 intermediatePointVastusLateralisSENIAM = Vector3.Lerp(CylinderSpinaIliaca.transform.position, CylinderPatella.transform.position, 2f / 3f); //This should be done in world position, that is why it was not working well before (I was using local positions)!!
            electrodeVastusLateralisSENIAM.transform.position = intermediatePointVastusLateralisSENIAM;

            //ATLAS            
            Vector3 referenceVectorVL = (CylinderSpinaIliaca.transform.position - CylinderPatella.transform.position).normalized;
            Vector3 muscleBelly = CylinderPatella.transform.position + referenceVectorVL * muscleBellyDistance;                        
            Vector3 customDirection = new Vector3(86.3481064f, 293.947998f, 344.041931f).normalized;
            Vector3 rotationAxis = Vector3.Cross(referenceVectorVL, customDirection).normalized;
            Quaternion rotation = Quaternion.AngleAxis(-rot, rotationAxis);            
            Vector3 newDirection = rotation * referenceVectorVL;
            Vector3 rotatedPosition = muscleBelly + newDirection.normalized * 0.165f;

            muscleBellyPos.transform.position = muscleBelly;

            Vector3 intermediatePointVastusLateralisATLAS = rotatedPosition;
            electrodeVastusLateralisATLAS.transform.position = intermediatePointVastusLateralisATLAS;           


            lastPatellaLateralDisplacement = _patellaLateralDisplacement;
            userData.patellaLateralDisplacement = _patellaLateralDisplacement;
            lastPatellaVerticalDisplacement = _patellaVerticalDisplacement;
            userData.patellaVerticalDisplacement = _patellaVerticalDisplacement;
            lastPatellaFrontalLateralDisplacement = _patellaFrontalLateralDisplacement;
            userData.patellaFrontalLateralDisplacement = _patellaFrontalLateralDisplacement;

            lastSpinaIliacaLateralDisplacement = _spinaIliacaLateralDisplacement;
            userData.spinaIliacaLateralDisplacement = lastSpinaIliacaLateralDisplacement;
            lastSpinaIliacaVerticalDisplacement = _spinaIliacaVerticalDisplacement;
            userData.spinaIliacaVerticalDisplacement = lastSpinaIliacaVerticalDisplacement;

            lastMuscleBellyDistance = muscleBellyDistance;

            dataManager.SaveData(currentUserName, userData);
            //Debug.Log("data saved");
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (muscle == "Peroneus Longus")
        {
            UpdateLandmarksPositionPL();
        }
        else if (muscle == "Vastus Lateralis") 
        { 
            UpdateLandmarksPositionVL();
        }


    }
}
