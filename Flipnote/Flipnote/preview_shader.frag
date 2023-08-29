#version 330 core

// Output color of the fragment
out vec4 FragColor;

// Input texture
uniform sampler2D texture;

// Input texture coordinates
in vec2 TexCoords;

void main()
{
    // Sample the original color from the texture
    vec4 originalColor = texture2D(texture, TexCoords);

    // Add transparency by changing the alpha value
    originalColor.a *= 0.5;

    // Output the color
    FragColor = originalColor;
}
