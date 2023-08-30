using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace flipnote
{
    public class ShortcutManager
    {
        private Dictionary<Keyboard.Key, Action> shortcuts = new Dictionary<Keyboard.Key, Action>();
        private HashSet<Keyboard.Key> keysPressed = new HashSet<Keyboard.Key>();

        public void AddShortcut(Keyboard.Key key, Action action)
        {
            shortcuts[key] = action;
        }

        public void HandleShortcuts(FrameManager frameManager, Brush brush, RenderWindow window)
        {
            foreach (var kvp in shortcuts)
            {
                if (Keyboard.IsKeyPressed(kvp.Key))
                {
                    if (!keysPressed.Contains(kvp.Key))
                    {
                        keysPressed.Add(kvp.Key);
                        kvp.Value.Invoke();
                    }
                }
                else
                {
                    keysPressed.Remove(kvp.Key);
                }
            }
        }
    }

}
