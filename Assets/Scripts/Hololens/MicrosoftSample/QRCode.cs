// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.
using TMPro;
using Unity.Mathematics;
using UnityEngine;

namespace Microsoft.MixedReality.SampleQRCodes
{
    [RequireComponent(typeof(SpatialGraphNodeTracker))]
    public class QRCode : MonoBehaviour
    {
        
#if UNITY_WSA
        public Microsoft.MixedReality.QR.QRCode qrCode;

        //private GameObject qrCodeCube;
        public GameObject qrCodeCube;

        Vector3 lastqrCodeCubePosition;
        quaternion lastqrCodeCubeRotation;


        private TextMeshProUGUI resultText;
        [Range(-0.5f, 0.5f)]
        [SerializeField] private float _lateralDisplacement;
        [Range(-0.5f, 0.5f)]
        [SerializeField] private float _depthDisplacement;
        [Range(-0.5f, 0.5f)]
        [SerializeField] private float _heightDisplacement;

        // Properties to get/set displacement values

        public float lateralDisplacement
        {
            get { return _lateralDisplacement; }
            set
            {
                _lateralDisplacement = value;
                Debug.Log("Lat displ: " + _lateralDisplacement);
                
            }
        }

        float lastLateralDisplacement = 0;

        public float depthDisplacement
        {
            get { return _depthDisplacement; }
            set
            {
                _depthDisplacement = value;
                Debug.Log("Depth displ: " + _depthDisplacement);
                
            }
        }

        float lastDepthDisplacement = 0;

        public float heightDisplacement
        {
            get { return _heightDisplacement; }
            set
            {
                _heightDisplacement = value;
                Debug.Log("Depth displ: " + _heightDisplacement);

            }
        }

        float lastHeightDisplacement = 0;

        // Other fields
        public float PhysicalSize { get; private set; }
        private float LastPhysicalSize;
        private long lastTimeStamp = 0;

        // Use this for initialization
        void Start()
        {
            PhysicalSize = 1f;

            if (qrCode == null)
            {
                throw new System.Exception("QR Code Empty");
            }

            PhysicalSize = qrCode.PhysicalSideLength;

            resultText = GameObject.Find("ResultText").GetComponent<TextMeshProUGUI>();

            //Debug.Log("Id= " + qrCode.Id + "NodeId= " + qrCode.SpatialGraphNodeId + " PhysicalSize = " + PhysicalSize + " TimeStamp = " + qrCode.SystemRelativeLastDetectedTime.Ticks + " QRVersion = " + qrCode.Version + " QRData = " + qrCode.Data);
        }

        void UpdatePropertiesDisplay()
        {

            // Only update when qrCode is detected, and it's the latest detection
            if ((qrCode != null && lastTimeStamp != qrCode.SystemRelativeLastDetectedTime.Ticks) || (lastLateralDisplacement != _lateralDisplacement || lastDepthDisplacement != _depthDisplacement)) //To update the position when either the variables or QR code's position are changed 
            {
                PhysicalSize = qrCode.PhysicalSideLength; // Update physical size based on qrCode
                LastPhysicalSize = qrCode.PhysicalSideLength;
                // Update position with the new displacement values
                //qrCodeCube.transform.localPosition = new Vector3(
                  //  (PhysicalSize / 2.0f) + 0.8f + _lateralDisplacement, // X-axis
                    //(PhysicalSize / 2.0f) + 1.7f + _depthDisplacement,  // Y-axis
                    //-0.905f + _heightDisplacement // Z-axis
                //)
                qrCodeCube.transform.localPosition = new Vector3(
                   (PhysicalSize / 2.0f) + _lateralDisplacement, // X-axis
                    (PhysicalSize / 2.0f) + 1f + _depthDisplacement,  // Y-axis
                    -0.75f + _heightDisplacement // Z-axis
                )
                    ;

                lastTimeStamp = qrCode.SystemRelativeLastDetectedTime.Ticks;

                lastLateralDisplacement = _lateralDisplacement;
                lastDepthDisplacement = _depthDisplacement;
                lastHeightDisplacement = _heightDisplacement;
                Debug.Log("Lat displ: " + _lateralDisplacement + "Depth displ" + _depthDisplacement + "height displ: " + _heightDisplacement);
                
            }
            
            
            lastTimeStamp = qrCode.SystemRelativeLastDetectedTime.Ticks;
           
        }

    
        // Update is called once per frame
        void Update()
            {

                // You can call UpdatePropertiesDisplay here if you want to keep checking

                // qrCode's position based on qrCode's changes. However, with OnValidate,
                // manual updates are handled in real-time by changing the variables in the Inspector.
                UpdatePropertiesDisplay();

            }
#endif
        }
    }

