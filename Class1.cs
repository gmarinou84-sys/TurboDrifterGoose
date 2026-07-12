using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using GooseShared;
using SamEngine;

namespace TurboDrifterGoose
{
    public class ModMain : IMod
    {
        public void Init()
        {
            InjectionPoints.PostRenderEvent += DrawGlasses;
            InjectionPoints.PreTickEvent += ApplyDriftAndSpeed;
        }

        private void ApplyDriftAndSpeed(GooseEntity goose)
        {
            goose.parameters.WalkSpeed = 300f;
            goose.parameters.RunSpeed = 500f;
            goose.parameters.ChargeSpeed = 900f;
            goose.parameters.AccelerationCharged = 6000f;

            if (goose.currentSpeed > 400f)
            {
                goose.trackMudEndTime = 5.0f;
            }
        }

        private void DrawGlasses(GooseEntity goose, Graphics g)
        {
            Vector2 headPos = goose.rig.head1EndPoint;
            float angle = goose.direction;

            GraphicsState state = g.Save();
            
            g.TranslateTransform(headPos.x, headPos.y);
            g.RotateTransform(angle);

            g.SmoothingMode = SmoothingMode.None;

            using (SolidBrush blackBrush = new SolidBrush(Color.Black))
            using (SolidBrush whiteBrush = new SolidBrush(Color.White))
            {
                int width = 16;
                int height = 5;
                int offsetX = -8;
                int offsetY = -3;

                Point[] leftLens = new Point[]
                {
                    new Point(offsetX, offsetY),
                    new Point(offsetX + 7, offsetY),
                    new Point(offsetX + 5, offsetY + height),
                    new Point(offsetX, offsetY + height)
                };
                g.FillPolygon(blackBrush, leftLens);

                Point[] rightLens = new Point[]
                {
                    new Point(offsetX + 9, offsetY),
                    new Point(offsetX + width, offsetY),
                    new Point(offsetX + width, offsetY + height),
                    new Point(offsetX + 11, offsetY + height)
                };
                g.FillPolygon(blackBrush, rightLens);

                g.FillRectangle(blackBrush, offsetX + 7, offsetY, 2, 2);

                g.FillRectangle(blackBrush, offsetX - 6, offsetY, 6, 2);

                g.FillRectangle(whiteBrush, offsetX + 1, offsetY + 1, 2, 2);
                g.FillRectangle(whiteBrush, offsetX + 3, offsetY + 3, 2, 1);

                g.FillRectangle(whiteBrush, offsetX + 10, offsetY + 1, 2, 2);
                g.FillRectangle(whiteBrush, offsetX + 12, offsetY + 3, 2, 1);
            }

            g.Restore(state);
        }
    }
}
