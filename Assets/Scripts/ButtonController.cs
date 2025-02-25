using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    // Variable para activar el trigger
    public bool triggerActivated = false;

    // Método que será llamado al presionar el botón
    public void OnButtonPress()
    {
        triggerActivated = true;
        Debug.Log("Trigger activado por el botón.");
    }
}

