using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    // Variable para activar el trigger
    public bool triggerActivated = false;

    // M�todo que ser� llamado al presionar el bot�n
    public void OnButtonPress()
    {
        triggerActivated = true;
        Debug.Log("Trigger activado por el bot�n.");
    }
}

