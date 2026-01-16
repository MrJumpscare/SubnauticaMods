using UnityEngine;

namespace SeamothHover
{
    public class HoverMonoBehaviour : MonoBehaviour
    {
        private SeaMoth seaMoth;
        private bool passedLimit;
        private bool HoverEnabled;
        private bool shouldFly;

        public void Start()
        {
            seaMoth = gameObject.GetComponentInChildren<SeaMoth>();
            passedLimit = true;
            HoverEnabled = false;
        }
        public void Update()
        {
            int count = seaMoth.modules.GetCount(SeaMothHoverModule.Info.TechType);
            float height = seaMoth.transform.position.y;
            shouldFly = seaMoth.playerFullyEntered || Main.config.keepFlying;

            if (count <= 0) return;
            if (HoverEnabled || (Main.config.Mode == SMLConfig.FlyMode.Flight && height >= 0))
            {
                seaMoth.enginePowerConsumption = 0.066667f * Main.config.powerconsumption;
            }

            if (Main.config.Mode == SMLConfig.FlyMode.Hover)
            {
                HoverCheck(height);
            }
            else
            {
                ToggleFlight(count);
            }

        }

        private void HoverCheck(float height)
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
            if (HoverEnabled)
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
                if (!shouldFly) { 
                    HoverEnabled = false;
                    ToggleHover();
                }
            }
            if (GameInput.GetButtonDown(Main.HoverButton))
            {
                HoverEnabled = !HoverEnabled;
                ToggleHover();
            }
        }

        private void ToggleFlight(int count)
        {
            if (shouldFly)
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

        private void ToggleHover()
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