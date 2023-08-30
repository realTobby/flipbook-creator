using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace flipnote
{
    public class Brush
    {
        private CircleShape brushPreview = new CircleShape();
        public int Size = 10;

        public Brush()
        {
            brushPreview.OutlineThickness = 1f;
            brushPreview.OutlineColor = SFML.Graphics.Color.Black;
            brushPreview.FillColor = new SFML.Graphics.Color(0, 0, 0, 50);
        }

        public void AdjustSize(float delta)
        {
            Size += (int)delta * 2;
            if (Size < 2) Size = 2;
        }

        public void DrawPreview(RenderWindow window)
        {
            var mousePosition = window.MapPixelToCoords(Mouse.GetPosition(window));
            // Draw brush preview logic
            brushPreview.Position = new Vector2f(mousePosition.X - Size / 2, mousePosition.Y - Size / 2);
            brushPreview.Radius = Size;
            window.Draw(brushPreview);
        }
    }
}
