using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class CharacterInput : MonoBehaviour
{
    public SteamVR_Action_Boolean testTriggerAction;
    public Hand right;
    public GameObject finger;

	void Update ()
    {
        string s = "";
        if (testTriggerAction != null)
            s+= "working";
        if (Test())
            s += "UseSecond";
        if (IsEligible(right))
            s += "TRUE";

        print(s);
        finger.SetActive(Test());
	}

    public bool IsEligible(Hand hand)
    {
        if (hand == null)
            return false;

        if (!hand.gameObject.activeInHierarchy)
            return false;

        if (hand.hoveringInteractable != null)
            return false;

        if (hand.noSteamVRFallbackCamera == null)
        {
            if (hand.isActive == false)
            {
                return false;
            }
        }
        return true;
    }

    public bool Test()
    {
        return testTriggerAction.GetState(right.handType);
    }
}
