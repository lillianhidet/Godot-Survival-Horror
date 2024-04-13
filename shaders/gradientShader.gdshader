shader_type spatial;
render_mode blend_mix, depth_draw_opaque, cull_disabled, diffuse_lambert, specular_disabled, unshaded;

uniform vec4 Color : source_color = vec4(1.000000, 1.000000, 0.000000, 1.000000);
uniform sampler2D Gradient : source_color;
uniform float Opacity = 0.5;
uniform float Fresnel = 0.5;



void fragment() {
// ColorParameter:3
	vec4 n_out3p0 = Color;


	vec4 n_out10p0;
// Texture2D:10
	n_out10p0 = texture(Gradient, UV);
	float n_out10p4 = n_out10p0.a;


// FloatParameter:2
	float n_out2p0 = Opacity;


// FloatParameter:8
	float n_out8p0 = Fresnel;


// Fresnel:4
	float n_out4p0 = pow(1.0 - clamp(dot(NORMAL, VIEW), 0.0, 1.0), n_out8p0);


// Clamp:6
	float n_in6p1 = 0.00000;
	float n_in6p2 = 1.00000;
	float n_out6p0 = clamp(n_out4p0, n_in6p1, n_in6p2);


// Mix:7
	float n_in7p1 = 0.00000;
	float n_out7p0 = mix(n_out2p0, n_in7p1, n_out6p0);


// FloatOp:11
	float n_out11p0 = n_out10p4 * n_out7p0;


// Output:0
	ALBEDO = vec3(n_out3p0.xyz);
	ALPHA = n_out11p0;


}
