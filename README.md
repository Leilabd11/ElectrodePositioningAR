# Electrode Positioning AR application

This repository is part of an AR-based project designed to visualize optimal electrode placement, ensuring high-quality muscle activation signals and a streamlined process. Placement is determined using methods such as the SENIAM guidelines and the Atlas of Muscle Innervation Zones.

This project has been developed in Unity editor version 2022.3.9f1.

The goal of this project is to overlap an avatar which will contain some cylinders on it marking the proper electrode's location based on the aforementioned methods with a real subject. Therefore, both the avatar and the person should stand at the same position. In order to achieve this, a QR Code position will be used to position the avatar in a suitable position for a person to stand.

After some research, the corresponding github repository (https://github.com/FireDragonGameStudio/Unity-ZXing-BarQrCodeHandling.git) for the video tutorial series about "Barcode and QRCode in Unity with ZXing.NET - A guide for Standalone, Android, WebGL and Hololens" was used. 
Tutorial Video - https://www.youtube.com/watch?v=tJXvynhbmpg

This application allows us to scan a QR code position and place a GameObject at that location. As was explained before, this is the idea that we wanted to follow. Therefore, the "QRCode.cs" script (Assets/Scripts/Hololens/MicrosoftSample) was adapted to our application by applying some offsets to stablish the desired avatar position at a known and suitable position for a person to stand.


-Another key point to obtain the proper electrode position is that the avatar should be properly scaled based on the real subject body measurements. This is achieved manually by the "ScalingBody.cs" script (Assets/Scripts/Avatar/ScalingBody).

-The main script that applies the methodology (SENIAM and Atlas of IZs) to compute and create marks (cylinders) onto the avatar is "SEMGPositioning" (Assets/Scripts/Avatar/SEMGPositioning.cs)

-To refine the application and make it easier to use, a finner adjustment of the landmarks needed to compute the proper electrode's positions is needed. Therefore, a .json dictionary (Assets/Scripts/Avatar/database.json) has been created to store all those displacement values of the landmarks based on the studied subject thanks to the "DataManager" script (Assets/Scripts/Avatar/DataManager.cs) that was also coded to manage all this. Like so, in case the subject has already tried the application, those settings are loaded so the position is almost the proper one directly.

All the scripts are coded in C#


