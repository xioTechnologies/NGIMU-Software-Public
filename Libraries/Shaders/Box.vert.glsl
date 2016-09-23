#version 330

layout(location = 0) in vec3 pos;
layout(location = 1) in vec2 tex;
layout(location = 2) in vec4 col;

out vec4 output_col;
out vec2 output_tex;

void main()
{
	gl_Position = vec4(pos, 1.0);
	output_col = col;
	output_tex = tex;
}