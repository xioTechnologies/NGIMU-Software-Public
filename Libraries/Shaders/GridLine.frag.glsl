#version 330

uniform vec4 color;

in float colorScale;

out vec4 colorFrag;

void main()
{
#section Mode_Vertical
	if ((int(gl_FragCoord.y / 2) % 2) == 1) discard;
#end
#section Mode_Horizontal 
	if ((int(gl_FragCoord.x / 2) % 2) == 1) discard;
#end

    colorFrag = color * colorScale;
}