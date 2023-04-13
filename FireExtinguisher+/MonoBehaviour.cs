using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FireExtinguisherPlus
{
    internal class FireExtPlusMonoBehaviour : MonoBehaviour
    {
        public FireExtinguisher fireExt;
        public float multiplier;
        
        public void Start()
        {
            fireExt = gameObject.GetComponentInChildren<FireExtinguisher>();
            multiplier = 1.0f;
        }
        public void Update()
        {
            if(Main.config.Overkill == true)
            {
                multiplier = 100;
            }
            else
            {
                multiplier = Main.config.FireExtRate / 10;
            }
            fireExt.expendFuelPerSecond = Main.config.newFuelPerSec;
            fireExt.fireDousePerSecond = Main.config.FireExtRate;
            if(Main.config.MaxFuelEnabled == true)
            {
                fireExt.fuel = 99999f;
            }
            else
            {
                fireExt.fuel = Main.config.newFuel;
            }
            if (fireExt.usedThisFrame && fireExt.fuel > 0f)
            {
                if (Player.main.IsUnderwater())
                {
                    Player.main.GetComponent<UnderwaterMotor>().rb.AddForce(-MainCamera.camera.transform.forward * multiplier, ForceMode.Impulse);
                }
            }
        }
    }
    
}
