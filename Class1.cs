using System;
using System.Drawing;
using GooseShared;

namespace TurboDrifterGoose
{
    public class ModMain : IMod
    {
        private Vector2 driftVelocity = new Vector2(0, 0);
        private Random rand = new Random();

        public void Init(GooseAPI api)
        {
            api.OnUpdate += ApplyDriftPhysics;
        }

        private void ApplyDriftPhysics(GooseAPI api)
        {
            if (api.currentTask == GooseAPI.Task.Walking || api.currentTask == GooseAPI.Task.Charging)
            {
                api.currentSpeed = 350; 

                driftVelocity.X = (driftVelocity.X * 0.92f) + (api.targetDirection.X * 0.08f);
                driftVelocity.Y = (driftVelocity.Y * 0.92f) + (api.targetDirection.Y * 0.08f);

                api.position.X += driftVelocity.X * api.currentSpeed * 0.016f;
                api.position.Y += driftVelocity.Y * api.currentSpeed * 0.016f;

                if (rand.Next(0, 100) < 25) 
                {
                    Color neonColor = Color.FromArgb(rand.Next(0, 256), rand.Next(100, 256), rand.Next(200, 256));
                    api.SpawnParticle(api.position, 1.5f, neonColor);
                }
            }
            else
            {
                driftVelocity.X = 0;
                driftVelocity.Y = 0;
            }
        }
    }
}