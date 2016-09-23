#version 330
//#extension GL_EXT_gpu_shader4 : enable

in vec4 viewspace_var;
in vec3 normal_var;

layout(location = 0) out vec4 colorFrag;
in vec4 color_var;

flat in vec3 instance_color_var;
flat in vec2 instance_surface_var; 

in vec4 lighting_points;

void main()
{	
	vec4 diffuseColor = vec4(color_var.rgb, 1);// color_var;
	
	float emissiveness = instance_surface_var.x;
	float final_alpha = instance_surface_var.y;

	vec3 lighting_ambient = vec3(0.1, 0.1, 0.1); 
	vec4 lightAccum = vec4(0.0); 		

	float depthValue = gl_FragCoord.z / gl_FragCoord.w; 

	lightAccum.rgb = mix(diffuseColor.rgb, (lightAccum.rgb + lighting_points.rgb) * diffuseColor.rgb, emissiveness);			
	lightAccum.a += lighting_points.a;

	lightAccum.rgb = max(lightAccum.rgb, lighting_ambient * diffuseColor.rgb);

	colorFrag = vec4(lightAccum.rgb + vec3(lightAccum.a), diffuseColor.a);

	colorFrag.a *= final_alpha; 

	//gl_FragColor = colorFrag;
	//colorFrag.rgb = applyFog(colorFrag.rgb, length(viewspace_var.z));
}
