using System;
using UdonSharp;
using UnityEngine;

namespace net.shiftall.pfsignal.Udon
{
    public enum PFThermalRequest
    {
        CoolFastHigh = 0,
        CoolHigh = 1,
        CoolMid = 2,
        CoolLow = 3,
        HotHigh = 4,
        HotMid = 5,
        HotLow = 6,
        Off = 7
    }

    public class PFSignal : UdonSharpBehaviour
    {
        [SerializeField] 
        public bool debug;
        
        [SerializeField]
        public PFSignalCollider[] colliders;
        
        private readonly GameObject[] signalTextures = new GameObject[Enum.GetNames(typeof(PFThermalRequest)).Length];

        void Start()
        {
            signalTextures[(int)PFThermalRequest.CoolFastHigh] = GameObject.Find("PFSignalPixelsCoolFastHigh");
            signalTextures[(int)PFThermalRequest.CoolHigh] = GameObject.Find("PFSignalPixelsCoolHigh");
            signalTextures[(int)PFThermalRequest.CoolMid] = GameObject.Find("PFSignalPixelsCoolMid");
            signalTextures[(int)PFThermalRequest.CoolLow] = GameObject.Find("PFSignalPixelsCoolLow");
            signalTextures[(int)PFThermalRequest.HotHigh] = GameObject.Find("PFSignalPixelsHotHigh");
            signalTextures[(int)PFThermalRequest.HotMid] = GameObject.Find("PFSignalPixelsHotMid");
            signalTextures[(int)PFThermalRequest.HotLow] = GameObject.Find("PFSignalPixelsHotLow");
            signalTextures[(int)PFThermalRequest.Off] = GameObject.Find("PFSignalPixelsOff");
            foreach (var c in colliders)
            {
                c.Register(this);
            }
            foreach (var t in signalTextures)
            {
                t.SetActive(false);   
            }
            signalTextures[(int)PFThermalRequest.Off].SetActive(true);
        }

        private void OnDestroy()
        {
            foreach (var c in colliders)
            {
                c.Unregister(this);
            }
        }
        
        public void OnStay(PFThermalRequest m)
        {
            // Log($"on stay: {m.ToString()}");
            signalTextures[(int)m].SetActive(true);
            signalTextures[(int)PFThermalRequest.Off].SetActive(false);
        }
        
        public void OnExit(PFThermalRequest m)
        {
            Log($"on exit : {m.ToString()}");
            signalTextures[(int)m].SetActive(false);
            signalTextures[(int)PFThermalRequest.Off].SetActive(true);
        }
        
        private void Log(string msg)
        {
            if (debug)
            {
                Debug.Log("[<color=#58ACFA>PebbleFeel</color><color=#b44c97>#PFSignal</color>]" + $" {msg}");   
            }
        }
    }
    
}
