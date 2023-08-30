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
    public class FrameManager
    {
        public List<RenderTexture> Frames { get; private set; }
        public bool IsPlaying { get; private set; } = false;

        private int currentFrame = 0;
        private bool keyPressed = false;
        private int playbackFrame = 0;

        public FrameManager()
        {
            Frames = new List<RenderTexture>();
        }

        public void AddNewFrame()
        {
            var newFrame = new RenderTexture(800, 600);
            newFrame.Clear(new SFML.Graphics.Color(0, 0, 0, 0));  // Transparent background
            newFrame.Display();
            Frames.Add(newFrame);
        }

        public void NextFrame()
        {
            Console.WriteLine("NextFrame!");
            // Go to the next frame if it exists, otherwise add a new frame and then go to it
            if (currentFrame < Frames.Count - 1)
            {
                currentFrame++;
            }
            else
            {
                AddNewFrame();
                currentFrame++;
            }
        }

        public void PreviousFrame()
        {
            if (currentFrame > 0)
            {
                currentFrame--;
            }
        }

        public void TogglePlayback()
        {
            // Logic to toggle playback
            IsPlaying = !IsPlaying;
            playbackFrame = currentFrame; // Set the playback starting frame
        }

        public void HandlePlayback()
        {
            if (!IsPlaying)
            {
                return; // Stop playback if isPlaying is toggled off
            }

            playbackFrame++;
            if (playbackFrame >= Frames.Count)
            {
                playbackFrame = 0;
            }
            currentFrame = playbackFrame;
            System.Threading.Thread.Sleep(50); // Add a small delay between frames
        }

        public void HandleDrawing(Brush brush, RenderWindow target)
        {
            // Get the mouse position in world coordinates
            var mousePosition = target.MapPixelToCoords(Mouse.GetPosition(target));

            if (Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                var paintBrush = new CircleShape(brush.Size);
                paintBrush.Position = new Vector2f(mousePosition.X - brush.Size / 2, mousePosition.Y - brush.Size / 2);
                paintBrush.FillColor = SFML.Graphics.Color.Black;

                Frames[currentFrame].Draw(paintBrush);
                Frames[currentFrame].Display();
            }
        }

        public void DrawCurrentFrame(RenderWindow window)
        {
            // Draw the current frame on top
            window.Draw(new Sprite(Frames[currentFrame].Texture));
        }

        public void DrawPreviousFramePreview(RenderWindow window)
        {
            if (IsPlaying)
            {
                return; // Stop playback if isPlaying is toggled off
            }

            if (currentFrame > 0)
            {
                var spritePrev = new Sprite(Frames[currentFrame - 1].Texture);
                spritePrev.Color = new SFML.Graphics.Color(255, 255, 255, 128);  // Semi-transparent
                window.Draw(spritePrev);
            }
        }

        public void ClearCurrentFrame()
        {
            if (currentFrame >= 0 && currentFrame < Frames.Count)
            {
                Frames[currentFrame].Clear(new SFML.Graphics.Color(0, 0, 0, 0));
                Frames[currentFrame].Display();
            }
        }
    }
}
