/*#pragma disable_preprocessor

shader_type canvas_item;

// 0: bypass
uniform float color_depth: hint_range(0, 64, 8) = 24;

uniform sampler2D dithering_pattern_texture:
	hint_default_white, repeat_enable, filter_nearest;

void fragment()
{
	vec3 base_color = texture(TEXTURE, UV).rgb;

	if (color_depth > 0.0)
	{
		vec2 dpt_size = vec2(textureSize(dithering_pattern_texture, 0));
		vec2 texture_size = vec2(textureSize(TEXTURE, 0));

		vec3 dithering = texture(
			dithering_pattern_texture, UV * (texture_size / dpt_size)).rgb - 0.5;

		COLOR.rgb = round(
			base_color.rgb * color_depth + dithering) / color_depth;
	}
	else
	{
		COLOR.rgb = base_color;
	}
}


This shader is under MIT license. Feel free to use, improve and 
change this shader according to your needs and consider sharing 
the modified result to godotshaders.com.
*/


shader_type canvas_item;

render_mode unshaded;

#define MAXCOLORS 64

uniform bool enabled = true;
uniform bool dithering = true;
uniform int colors : hint_range(1, MAXCOLORS) = 32;
uniform int dither_size: hint_range(1, 8) = 1;

float dithering_pattern(ivec2 fragcoord) {
	const float pattern[] = {
		0.00, 0.50, 0.10, 0.65, 
		0.75, 0.25, 0.90, 0.35, 
		0.20, 0.70, 0.05, 0.50, 
		0.95, 0.40, 0.80, 0.30
	};
	
	int x = fragcoord.x % 4;
	int y = fragcoord.y % 4;
	
	return pattern[y * 4 + x];
}

float reduce_color(float raw, float dither, int depth) {
	float div = 1.0 / float(depth);
	float val = 0.0;
	int i = 0;

	while (i <= MAXCOLORS)
	{
		if (raw > div * (float(i + 1))) {
			i = i + 1;
			continue;
		}

		if (raw * float(depth) - float(i) <= dither * 0.999)
		{
			val = div * float(i);
		} 
		else
		{
			val = div * float(i + 1);
		}
		return val;

		i = i+1;
	}

	return val;
}

void fragment() {
	vec4 raw = texture(TEXTURE, SCREEN_UV);
	ivec2 uv = ivec2(FRAGCOORD.xy / float(dither_size));

	if (enabled == true){
		float dithering_value = 1.0;
		if (dithering)
		{
			dithering_value = dithering_pattern(uv);
		}
		
		COLOR.r = reduce_color(raw.r, (dithering_value - 0.5) * dithering_value + 0.5, colors - 1);
		COLOR.g = reduce_color(raw.g, (dithering_value - 0.5) * dithering_value + 0.5, colors - 1);
		COLOR.b = reduce_color(raw.b, (dithering_value - 0.5) * dithering_value + 0.5, colors - 1);

	} else {
		COLOR.rgb = raw.rgb;
	}
}


