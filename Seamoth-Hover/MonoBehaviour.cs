using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static VFXParticlesPool;

namespace SeamothHover
{
    public class HoverMonoBehaviour : MonoBehaviour
    {
        public SeaMoth seaMoth;
        public Vehicle vehicle;
        public float energy;
        public bool passedlimit;
        public bool HoverEnabled;
        
        public void Start()
        {
            seaMoth = gameObject.GetComponentInChildren<SeaMoth>();
            vehicle = gameObject.GetComponentInChildren<Vehicle>();
            energy = 0.24f;
            passedlimit = true;
            HoverEnabled = false;
        }
        public void Update()
        {
            int count = seaMoth.modules.GetCount(SeaMothHoverModule.TechTypeID);
            float height = seaMoth.transform.position.y;
            if (!seaMoth.playerFullyEntered) return;
            if (Main.config.powerconsumption == 1){ energy = 0.066667f; }
            if (count > 0 && HoverEnabled || Main.config.Mode == SMLConfig.FlyMode.Flight && height >= 0)
            {
                seaMoth.enginePowerConsumption = energy * Main.config.powerconsumption;
            }
            if (Main.config.Mode == SMLConfig.FlyMode.Hover)
            {
                Hover();
            }
            else
            {
                Flight();
            }
            void Hover()
            {
                void Debug()
                {
                    if (passedlimit == true)
                    {
                        ErrorMessage.AddDebug("Max Height Reached");
                        ErrorMessage.AddDebug("Disengaging Thrusters");
                        passedlimit = false;
                    }
                    return;
                }
                if (height >= Main.config.hoverheight && HoverEnabled)
                {
                    Debug();
                    seaMoth.worldForces.aboveWaterGravity = 9.81f + height - Main.config.hoverheight;
                    passedlimit = false;
                }
                if (height < Main.config.hoverheight - 0.001f && HoverEnabled)
                {
                    seaMoth.worldForces.aboveWaterGravity = 0;
                    passedlimit = true;
                }
                if (count > 0 && Input.GetKeyDown(Main.config.hovertoggle))
                {
                    HoverEnabled = !HoverEnabled;
                    Hovering();
                }
            }
            void Flight()
            {
                if (count > 0)
                {
                    seaMoth.worldForces.aboveWaterDrag = seaMoth.worldForces.underwaterDrag;
                    seaMoth.moveOnLand = true;
                    seaMoth.worldForces.aboveWaterGravity = 0;
                }
                else
                {
                    seaMoth.worldForces.aboveWaterGravity = 9.81f;
                    seaMoth.worldForces.aboveWaterDrag = 0f;
                    seaMoth.moveOnLand = false;
                }
            }
        }
        public void Hovering()
        {
            if (HoverEnabled)
            {
                seaMoth.worldForces.underwaterGravity = -9.81f;
                seaMoth.worldForces.aboveWaterDrag = seaMoth.worldForces.underwaterDrag;
                seaMoth.moveOnLand = true;
                seaMoth.worldForces.aboveWaterGravity = 0;
                ErrorMessage.AddDebug("Hovering ON");
                ErrorMessage.AddDebug("Maneuver Jets now configured to over water flight.");
            }
            else
            {
                seaMoth.worldForces.underwaterGravity = 0f;
                seaMoth.worldForces.aboveWaterGravity = 9.81f;
                seaMoth.worldForces.aboveWaterDrag = 0f;
                seaMoth.moveOnLand = false;
                ErrorMessage.AddDebug("Hovering OFF");
            }
        }
    }
}

