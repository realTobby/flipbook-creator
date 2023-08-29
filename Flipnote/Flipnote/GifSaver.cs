using ImageMagick;
using SFML.Graphics;
using System.Collections.Generic;

namespace flipnote
{
    public static class GifSaver
    {
        public static void CreateGif(List<RenderTexture> frames, string outputPath)
        {
            using (MagickImageCollection collection = new MagickImageCollection())
            {
                foreach (var frame in frames)
                {
                    // Convert the RenderTexture to a MagickImage
                    using (var stream = new System.IO.MemoryStream())
                    {
                        frame.Texture.CopyToImage().SaveToStream(SFML.Graphics.ImageFormat.Png, stream);
                        stream.Position = 0;

                        var image = new MagickImage(stream);

                        // Set the "dispose" setting to "None" for the first frame, and "Previous" for subsequent frames
                        if (frame == frames[0])
                        {
                            image.AnimationDisposeMethod = GifDisposeMethod.None;
                        }
                        else
                        {
                            image.AnimationDisposeMethod = GifDisposeMethod.Previous;
                        }

                        collection.Add(image);
                    }
                }

                // Save the GIF
                collection.Write(outputPath);
            }
        }
    }
}
