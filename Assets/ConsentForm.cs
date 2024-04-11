using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsentForm : MonoBehaviour
{
    public static bool dataConsent = false;
    // Start is called before the first frame update
    public void consentGiven()
    {
        dataConsent = true;
        Debug.Log($"Consent has been provided.");

    }

    public void consentRevoked()
    {
        dataConsent = false;
        Debug.Log($"Consent is no longer given.");
    }
}
