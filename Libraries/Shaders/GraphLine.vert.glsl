#version 330

uniform vec4 window;
uniform vec4 offsetAndScale;

layout(location = 0) in vec2 pos;

void main()
{
	vec2 finalPosition = (window.xy + ((offsetAndScale.xy + pos) * offsetAndScale.zw)) * window.zw;

	gl_Position = vec4(finalPosition.x, finalPosition.y, 0.5, 1.0);
}