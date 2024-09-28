using UnityEngine;

namespace SeamothHover
{
    public class HoverMonoBehaviour : MonoBehaviour
    {
        public SeaMoth seaMoth;
        public float energy;
        public bool passedLimit;
        public bool HoverEnabled;
        public bool shouldFly;
        public bool test;

        public void Start()
        {
            seaMoth = gameObject.GetComponentInChildren<SeaMoth>();
            energy = 0.066667f;
            passedLimit = true;
            HoverEnabled = false;
        }
        public void Update()
        {
            int count = seaMoth.modules.GetCount(SeaMothHoverModule.Info.TechType);
            float height = seaMoth.transform.position.y;
            if (seaMoth.playerFullyEntered || Main.config.keepFlying)
            {
                shouldFly = true;
            }
            else shouldFly = false;
            if (count > 0 && (HoverEnabled || (Main.config.Mode == SMLConfig.FlyMode.Flight && height >= 0)))
            {
                seaMoth.enginePowerConsumption = 0.066667f * Main.config.powerconsumption;
            }
            if (Main.config.Mode == SMLConfig.FlyMode.Hover && count > 0)
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
                    if (passedLimit == true)
                    {
                        ErrorMessage.AddWarning("Max Hover Height Reached");
                        ErrorMessage.AddWarning("Thrusters losing power!");
                        passedLimit = false;
                    }
                    return;
                }
                if(HoverEnabled)
                {
                    if (height >= Main.config.hoverheight)
                    {
                        Debug();
                        seaMoth.worldForces.aboveWaterGravity = 9.81f + height - Main.config.hoverheight;
                        passedLimit = false;
                    }
                    if (height < Main.config.hoverheight - 0.001f)
                    {
                        seaMoth.worldForces.aboveWaterGravity = 0;
                        passedLimit = true;
                    }
                    if(!shouldFly)
                    {
                        HoverEnabled = false;
                        Hovering();
                    }
                }
                if (Input.GetKeyDown(Main.config.hovertoggle))
                {
                    HoverEnabled = !HoverEnabled;
                    Hovering();
                }
            }
            void Flight()
            {
                if (count > 0 && shouldFly)
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

