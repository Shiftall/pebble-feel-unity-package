using UdonSharp;
using UnityEngine;
using VRC.SDKBase;

namespace net.shiftall.pfsignal.Udon
{
    public class PFSignalCollider : UdonSharpBehaviour
    {
        private PFSignal _pfSignal;

        [Header("Enable Debug Logging")]
        [SerializeField] 
        private bool debug;

        [Header("PebbleFeel Mode Setting")]
        [SerializeField] 
        private  PFThermalRequest pfThermalRequest;

        public void Register(PFSignal listener)
        {
            if (_pfSignal != null)
            {
                // Udon does not support exception
                Debug.LogWarning("Listener has set up already. Multiple listeners cannot be set.");
                return;
            }
            _pfSignal = listener;
            Log(" Register " + $"{listener.name}");
        }
    
        public void Unregister(PFSignal listener)
        {
            if (ReferenceEquals(listener, _pfSignal) == false) return;
            _pfSignal = null;
            Log("Unregister " + $"{listener.name}");
        }
    
        public override void OnPlayerTriggerStay(VRCPlayerApi player)
        {
            if (player != Networking.LocalPlayer) return;
            if (_pfSignal == null) return;
            _pfSignal.OnStay(pfThermalRequest);
            // Log("OnPlayerTriggerStay() : " + player.displayName);
        }

        public override void OnPlayerTriggerExit(VRCPlayerApi player)
        {
            if (player != Networking.LocalPlayer) return;
            if (_pfSignal == null) return;
            _pfSignal.OnExit(pfThermalRequest);
            Log("OnPlayerTriggerExit() : " + player.displayName);
        }

        private void Log(string msg)
        {
            if (debug)
            {
                Debug.Log("[<color=#58ACFA>PebbleFeel</color><color=#ffd700>#PFSignalCollider</color>]" + $" {msg}");   
            }
        }
        
    }
}
