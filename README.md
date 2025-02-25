# **Electrode Positioning AR Application**  

This repository is part of an AR-based project designed to visualize optimal electrode placement, ensuring good-quality muscle activation signals and a streamlined process. Placement is determined using methods such as the **SENIAM guidelines** and the **Atlas of Muscle Innervation Zones**.  

## **Project Overview**  

This project has been developed using **Unity 2022.3.9f1**. Its main goal is to **overlay an avatar onto a real subject**, marking the proper electrode positions with cylinders based on the aforementioned methods.  

- The avatar model was downloaded from **Mixamo** and modified in **Blender** to make certain parts transparent, focusing on relevant body areas.  
- The avatar file used is **SlicedAvatar.fbx** (`Assets/Character/SlicedAvatar.fbx`).  

The subject is wearing **IMUs (XSens)**, which are connected to **MVN Analyze/Animate software**. This allows the real-time tracking of the subject’s movements, which are then sent to Unity through the **MVN Unity Live Plugin** (located in `packages/com.movella.xsens`). This integration ensures a more accurate overlay of the avatar by tracking the subject's movement.

To ensure proper alignment between the avatar and the real person, both need to stand in the same position. A **QR Code-based positioning system** is used to place the avatar at a known and suitable position for the subject to stand, resolving the alignment challenge.

## **QR Code Positioning**  

To implement QR-based positioning, this project integrates code from the following repository:  

  **GitHub Repository:** [FireDragonGameStudio/Unity-ZXing-BarQrCodeHandling](https://github.com/FireDragonGameStudio/Unity-ZXing-BarQrCodeHandling)  
  **Tutorial Video:** [Barcode and QRCode in Unity with ZXing.NET](https://www.youtube.com/watch?v=tJXvynhbmpg)  

This application allows scanning a **QR code** and placing a **GameObject** at the scanned position. The script **QRCode.cs** (`Assets/Scripts/Hololens/MicrosoftSample`) was adapted to apply **offsets** that properly position the avatar for the subject.  

## **Key Features & Scripts**  

### **1. Avatar Scaling**  
- To obtain the correct electrode position, the **avatar must be scaled** according to the real subject’s body measurements.  
- This is manually adjusted using **ScalingBody.cs** (`Assets/Scripts/Avatar/ScalingBody.cs`).  

### **2. Electrode Positioning Methodology**  
- The main script that applies the **SENIAM and Atlas of Muscle Innervation Zones** methodologies is **SEMGPositioning.cs** (`Assets/Scripts/Avatar/SEMGPositioning.cs`).  
- This script computes and places **cylinders** onto the avatar to mark electrode positions.  

### **3. Landmark Fine-Tuning & Data Management**  
- To refine the placement process, **a fine adjustment of landmarks** is necessary.  
- This adjustment is done **using sliders**, allowing precise control over landmark positions.  
- A **JSON dictionary** (`Assets/Scripts/Avatar/database.json`) stores displacement values based on the studied subject.  
- The script **DataManager.cs** (`Assets/Scripts/Avatar/DataManager.cs`) manages these settings.  
- If a subject has used the application before, their previous settings are loaded, making electrode positioning more accurate from the start.  

## **Technology Stack**  
- **Game Engine:** Unity (2022.3.9f1)  
- **Programming Language:** C#  
- **3D Modeling Tools:** Blender, Mixamo  
- **QR Code Handling:** ZXing.NET  
- **Motion Capture Integration:** MVN Unity Live Plugin (Packages/com.movella.xsens)  
- **Hardware:** XSens IMUs (worn by the subject), HoloLens 2  
