#version 330 core

// Input vertex coordinates
layout (location = 0) in vec2 inPosition;
layout (location = 1) in vec2 inTexCoords;

// Output texture coordinates
out vec2 TexCoords;

void main()
{
    // Output vertex coordinates
    gl_Position = vec4(inPosition.x, inPosition.y, 0.0, 1.0);

    // Output texture coordinates
    TexCoords = inTexCoords;
}
