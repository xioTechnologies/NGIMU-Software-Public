#version 330
#extension GL_ARB_explicit_attrib_location : require
#extension GL_ARB_explicit_attrib_location : enable

struct ModelLight
{
	vec3 position;
	float intensity;
	vec4 color;  
	vec4 attenuation;  
};

vec4 GetLighting_Directional_DiffuseSpecular(in vec4 pos, in vec3 normal, 
	in vec4 light_color, 
	in vec3 light_position, 
	in float light_intensity,
	in float specularFactor, in float specularAmount) 
{
	vec4 lightColor = light_color;

	// get the direction from the light to the face
	vec3 L = normalize(light_position);

	// get the direction from the camera to the face
	vec3 E = normalize(pos.xyz);

	// get the reflection vector of the light off the normal 
	vec3 R = normalize(reflect(-L, normal)); 

	float intensity = max(dot(normal, L), 0.0);
	float specScale = smoothstep(0.0, 0.01, intensity);

	float specular = lightColor.a; 
	float spec = pow(max(dot(-R, E), 0.0), specularFactor) * specScale * specular * specularAmount;

	// add the specular term 
	return vec4(intensity * lightColor.xyz, spec) * light_intensity;
}

uniform samplerCube ambientCubeMap;

uniform mat4 worldMatrix;
uniform mat4 normalWorldMatrix;
uniform mat4 perspectiveMatrix;
uniform vec3 cameraCenter = vec3(0, 1, 0);

uniform mat4 objectMatrix;
uniform mat4 normalMatrix;

uniform vec3 instance_color;
uniform vec2 instance_surface;

uniform vec4 light_color;
uniform vec3 light_position; 
uniform float light_intensity;

//layout(location = 0) in vec3 position;
//layout(location = 1) in vec3 normal;
//layout(location = 2) in vec4 color;
//layout(location = 3) in vec4 face;

layout(location = 0) in vec3 position;
layout(location = 1) in vec3 normal;
layout(location = 2) in vec2 textureCoords;
layout(location = 3) in vec4 color;

out vec4 viewspace_var;
out vec3 normal_var;
out vec4 color_var;
out vec2 texture_uv_var;
flat out vec3 instance_color_var;
flat out vec2 instance_surface_var;
out vec4 lighting_points;

layout(std140) uniform modelLightBlock
{
	ModelLight ModelLights[1];
};

void main()
{
	vec3 vert = position; 
	vec3 face_vert = position; //face.xyz; 

    vec4 localSpace = objectMatrix * vec4(vert, 1);
	vec4 worldSpace = worldMatrix * localSpace;
	viewspace_var = worldSpace; 
	gl_Position = perspectiveMatrix * worldSpace;  

	texture_uv_var = textureCoords; 

	vec3 world_normal_var;	

	// lighting is done in camera space so we need to transform the normal into the camera frame
	world_normal_var = (normalMatrix * vec4(normal, 0.0)).xyz; 	

	normal_var = (normalWorldMatrix * vec4(world_normal_var, 0.0)).xyz; 	

	instance_color_var = instance_color; 
	instance_surface_var = instance_surface; 

	//vec2 temp_emmisive_alpha = unpackHalf2x16(floatBitsToUint(instance_color_var.a));	
	//float emmisive_value = temp_emmisive_alpha.x; 

	// get the vert color	
	color_var = vec4(color.rgb * instance_color, 1);

	// only need to transform the face by the camera space
	vec4 face_pos = objectMatrix * vec4(face_vert, 1);  
				
	//vec3 reflection_face_var = reflect(-world_normal_var.xyz, normalize(face_pos.xyz - (-cameraCenter))); 
	//vec3 reflection_face_var = reflect(world_normal_var.xyz, normalize(localSpace.xyz - (-cameraCenter))); 

	face_pos = worldMatrix * face_pos;	
    	
	// extract the specular factor from the alpha channel of the face color
	float specularFactor = 0; //  color.a;

	// extract the specular amount from the w channel of the unmodified face position 
	float specularAmount = 1; //  face.w;

	// extract and multiply up the specular factor
	specularFactor = 2.0 + (248.0 * specularFactor); 

	vec4 lightAccum = vec4(0); 		
	vec4 shadowlightTemp = vec4(0); 

	shadowlightTemp = GetLighting_Directional_DiffuseSpecular(face_pos, normal_var.xyz,
																light_color, light_position, light_intensity,
									specularFactor, specularAmount);

	vec4 lighting_shadow_0 = vec4(shadowlightTemp.rgb * color_var.rgb, shadowlightTemp.w);

	// do final sum
	lighting_points = lightAccum + lighting_shadow_0;
}
