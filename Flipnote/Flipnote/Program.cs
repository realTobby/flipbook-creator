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
        static void Main(string[] args)
        {
            var app = new FlipnoteApp();
            app.Run();
        }
    }

    class FlipnoteApp
    {
        private RenderWindow window;
        private FrameManager frameManager;
        private Brush brush;
        private ShortcutManager shortcutManager;

        static Dictionary<Keyboard.Key, bool> keyStates = new Dictionary<Keyboard.Key, bool>();


        public void Run()
        {
            Initialize();

            while (window.IsOpen)
            {
                window.DispatchEvents();

                shortcutManager.HandleShortcuts(frameManager, brush, window);


                frameManager.HandleDrawing(brush, window);

                if (frameManager.IsPlaying)
                {
                    frameManager.HandlePlayback();
                }

                window.Clear(SFML.Graphics.Color.White);

                frameManager.DrawCurrentFrame(window);
                frameManager.DrawPreviousFramePreview(window);
                brush.DrawPreview(window);

                window.Display();

                if (Keyboard.IsKeyPressed(Keyboard.Key.S))
                {
                    GifSaver.ConvertToGif(frameManager.Frames, "animation.gif");
                }
            }
        }

        private void Initialize()
        {

            brush = new Brush();

            window = new RenderWindow(new VideoMode(800, 600), "Flipnote Clone");
            window.Closed += (_, __) => window.Close();
            window.MouseWheelScrolled += (_, e) => brush.AdjustSize(e.Delta);

            frameManager = new FrameManager();
            frameManager.AddNewFrame();

            shortcutManager = new ShortcutManager();
            shortcutManager.AddShortcut(Keyboard.Key.Right, frameManager.NextFrame);
            shortcutManager.AddShortcut(Keyboard.Key.Left, frameManager.PreviousFrame);
            shortcutManager.AddShortcut(Keyboard.Key.Space, frameManager.TogglePlayback);
            shortcutManager.AddShortcut(Keyboard.Key.S, () => GifSaver.ConvertToGif(frameManager.Frames, "animation.gif"));
            shortcutManager.AddShortcut(Keyboard.Key.C, frameManager.ClearCurrentFrame);


        }
    }
}
