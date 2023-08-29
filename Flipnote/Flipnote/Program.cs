using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.Window;
using SFML.System;

using System.Drawing;
using System.Drawing.Imaging;
using System.Text;

namespace flipnote
{
    class Program
    {
        static List<RenderTexture> frames = new List<RenderTexture>();
        static int currentFrame = 0;
        static int brushSize = 10;
        static bool keyPressed = false;

        static bool isPlaying = false; // Flag to indicate if animation is playing
        static int playbackFrame = 0;  // Frame index for playback


        static void Main(string[] args)
        {
            



            var window = new RenderWindow(new VideoMode(800, 600), "Flipnote Clone");
            window.Closed += (_, __) => window.Close();
            window.MouseWheelScrolled += (sender, e) => AdjustBrushSize(e.Delta);

            // Initialize the frames list with one white frame
            AddNewFrame();

            while (window.IsOpen)
            {
                window.DispatchEvents();

                HandleDrawing(window);

                HandleNavigation(window);

                if (isPlaying)
                {
                    HandlePlayback();
                }

                window.Clear(SFML.Graphics.Color.White);

                // Draw the current frame on top
                window.Draw(new Sprite(frames[currentFrame].Texture));

                DrawPreviousFramePreview(window);

                window.Display();

                if(Keyboard.IsKeyPressed(Keyboard.Key.S))
                {
                    GifSaver.CreateGif(frames, "animation.gif");
                }

            }
        }

        static void AddNewFrame()
        {
            var newFrame = new RenderTexture(800, 600);
            newFrame.Clear(new SFML.Graphics.Color(0, 0, 0, 0));  // Transparent background
            newFrame.Display();
            frames.Add(newFrame);
        }

        static void AdjustBrushSize(float delta)
        {
            brushSize += (int)delta * 2;
            if (brushSize < 2) brushSize = 2;
        }

        static void HandleDrawing(RenderWindow window)
        {
            if (Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                var mousePosition = Mouse.GetPosition(window);
                var brush = new CircleShape(brushSize);
                brush.Position = new Vector2f(mousePosition.X - brushSize / 2, mousePosition.Y - brushSize / 2);
                brush.FillColor = SFML.Graphics.Color.Black;

                frames[currentFrame].Draw(brush);
                frames[currentFrame].Display();
            }
        }

        static void HandleNavigation(RenderWindow window)
        {
            if (!Keyboard.IsKeyPressed(Keyboard.Key.Right) && !Keyboard.IsKeyPressed(Keyboard.Key.Left) && !Keyboard.IsKeyPressed(Keyboard.Key.Space))
            {
                keyPressed = false;
            }

            if (!keyPressed)
            {
                if (Keyboard.IsKeyPressed(Keyboard.Key.Right))
                {
                    keyPressed = true;

                    // Go to the next frame if it exists, otherwise add a new frame and then go to it
                    if (currentFrame < frames.Count - 1)
                    {
                        currentFrame++;
                    }
                    else
                    {
                        AddNewFrame();
                        currentFrame++;
                    }
                }
                else if (Keyboard.IsKeyPressed(Keyboard.Key.Left))
                {
                    keyPressed = true;

                    if (currentFrame > 0)
                    {
                        currentFrame--;
                    }
                }
                else if (Keyboard.IsKeyPressed(Keyboard.Key.Space))
                {
                    keyPressed = true;
                    isPlaying = !isPlaying;
                    playbackFrame = currentFrame; // Set the playback starting frame
                }
            }
        }



        static void DrawPreviousFramePreview(RenderWindow window)
        {
            if (isPlaying)
            {
                return; // Stop playback if isPlaying is toggled off
            }

            if (currentFrame > 0)
            {
                var spritePrev = new Sprite(frames[currentFrame - 1].Texture);
                spritePrev.Color = new SFML.Graphics.Color(255, 255, 255, 128);  // Semi-transparent
                window.Draw(spritePrev);
            }
        }

        static void HandlePlayback()
        {
            if (!isPlaying)
            {
                return; // Stop playback if isPlaying is toggled off
            }

            playbackFrame++;
            if (playbackFrame >= frames.Count)
            {
                playbackFrame = 0;
            }
            currentFrame = playbackFrame;
            System.Threading.Thread.Sleep(100); // Add a small delay between frames
        }
    }
}
