#version 330

uniform sampler2D sourceTexture; 

#section Mode_TexturedColor
	uniform vec4 effect_color; 
	uniform vec2 effect_color_mod; 
#end

in vec4 output_col;
in vec2 output_tex;

out vec4 colorFrag;

void main()
{
#section Mode_Color
    colorFrag = output_col;
#end

#section Mode_Textured
    colorFrag = texture(sourceTexture, output_tex) * output_col;
#end

#section Mode_TexturedColor
    colorFrag = texture(sourceTexture, output_tex);

	colorFrag.rgb *= effect_color_mod.x; 
	colorFrag.rgb += effect_color_mod.y; 

	colorFrag *= effect_color; 

	colorFrag *= output_col;
#end
}