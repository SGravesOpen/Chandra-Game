using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QR_Pointer : MonoBehaviour
{
   private void Awake()
   {
       PlayerEvents.OnControllerSource += UpdateOrigin;
       PlayerEvents.OnTouchPadDown += ProcessTouchPadDown;
    }

   private void OnDestroy()
   {
       PlayerEvents.OnControllerSource -= UpdateOrigin;
       PlayerEvents.OnTouchPadDown -= ProcessTouchPadDown;
   }

   private void UpdateOrigin(OVRInput.Controller controller, GameObject controllerObject)
   {

   }

   private void ProcessTouchPadDown()
   {

   }
}
