#version 330

uniform vec4 window;
uniform vec4 offsetAndScale;

layout(location = 0) in vec4 pos;

out float colorScale;

void main()
{
	vec2 finalPosition = (window.xy + ((offsetAndScale.xy + pos.xy) * offsetAndScale.zw)) * window.zw;

	gl_Position = vec4(finalPosition.x, finalPosition.y, pos.z, 1.0);
	
	colorScale = pos.w;
}