using System;
using System.Collections.Generic;
using System.IO;
using MagickNet = ImageMagick;
using SFML.Graphics;
using ImageMagick;

namespace flipnote
{
    public class GifSaver
    {
        public static void ConvertToGif(List<RenderTexture> renderTextures, string outputPath)
        {
            var magickImages = new List<MagickImage>();

            foreach (var renderTexture in renderTextures)
            {
                var magickImage = ConvertRenderTextureToMagickImage(renderTexture);
                magickImages.Add(magickImage);
            }

            CreateGifFromMagickImages(magickImages, outputPath);
        }

        private static MagickImage ConvertRenderTextureToMagickImage(RenderTexture renderTexture)
        {
            var sfmlImage = renderTexture.Texture.CopyToImage();
            var width = (int)renderTexture.Size.X;
            var height = (int)renderTexture.Size.Y;

            // Get the pixel bytes from the SFML image and prepare it for MagickImage
            byte[] pixels = sfmlImage.Pixels;
            var magickImage = new MagickImage(pixels, new PixelReadSettings(width, height, StorageType.Char, PixelMapping.RGBA));

            magickImage.BackgroundColor = MagickColors.White;
            magickImage.Alpha(AlphaOption.Remove);

            return magickImage;
        }

        private static void CreateGifFromMagickImages(List<MagickImage> magickImages, string outputPath)
        {
            using (var magickImageCollection = new MagickImageCollection())
            {
                foreach (var image in magickImages)
                {
                    magickImageCollection.Add(image);
                    image.AnimationDelay = 10;
                }
                magickImageCollection.Write(outputPath);
            }
        }
    }
}
