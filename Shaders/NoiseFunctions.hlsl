// Hash without Sine functions - David Hoskins
// https://www.shadertoy.com/view/4djSRW
// 	The MIT License
// 	Copyright © 2014 David Hoskins

float hash11(float p)
{
	p = frac(p * .1031);
	p *= p + 33.33;
	p *= p + p;

	return -1.0 + 2.0 * frac(p);
}

float2 hash22(float2 p)
{
	float3 p3 = frac(float3(p.xyx) * float3(.1031, .1030, .0973));
	p3 += dot(p3, p3.yzx+33.33);
	
	return -1.0 + 2.0 * frac((p3.xx+p3.yz)*p3.zy);
}

float3 hash33(float3 p3)
{
	p3 = frac(p3 * float3(.1031, .1030, .0973));
	p3 += dot(p3, p3.yxz+33.33);
	
	return -1.0 + 2.0 * frac((p3.xxy + p3.yxx)*p3.zyx);
}

// Gradient Noise 2D Deriv - Inigo Quilez
// https://www.shadertoy.com/view/XdXBRH
//	The MIT License
//	Copyright © 2013 Inigo Quilez

float3 gradientNoise2DDeriv(float2 x)
{
	float2 i = floor(x);
	float2 f = frac(x);
	
	float2 u = f*f*f*(f*(f*6.0-15.0)+10.0);
	float2 du = 30.0*f*f*(f*(f-2.0)+1.0);
    
	float2 ga = hash22(i + float2(0.0,0.0));
	float2 gb = hash22(i + float2(1.0,0.0));
	float2 gc = hash22(i + float2(0.0,1.0));
	float2 gd = hash22(i + float2(1.0,1.0));
    
	float va = dot(ga, f - float2(0.0,0.0));
	float vb = dot(gb, f - float2(1.0,0.0));
	float vc = dot(gc, f - float2(0.0,1.0));
	float vd = dot(gd, f - float2(1.0,1.0));

	return float3( va + u.x*(vb-va) + u.y*(vc-va) + u.x*u.y*(va-vb-vc+vd),   // value
				 ga + u.x*(gb-ga) + u.y*(gc-ga) + u.x*u.y*(ga-gb-gc+gd) +  // derivatives
				 du * (u.yx*(va-vb-vc+vd) + float2(vb,vc) - va));
}

// Gradient Noise 2D - Inigo Quilez
// https://www.shadertoy.com/view/XdXGW8
//	The MIT License
//	Copyright © 2013 Inigo Quilez

float gradientNoise2D(float2 p)
{
	float2 i = floor(p);
	float2 f = frac(p);
	
	float2 u = f*f*(3.0-2.0*f);

	return lerp(lerp(dot(hash22(i + float2(0.0,0.0)), f - float2(0.0, 0.0)), 
					 dot(hash22(i + float2(1.0,0.0)), f - float2(1.0, 0.0)), u.x),
				lerp(dot(hash22(i + float2(0.0,1.0)), f - float2(0.0, 1.0)), 
					 dot(hash22(i + float2(1.0,1.0)), f - float2(1.0, 1.0)), u.x), u.y);
}

// Gradient Noise 3D Deriv - Inigo Quilez
// https://www.shadertoy.com/view/4dffRH
//	The MIT License
//	Copyright © 2013 Inigo Quilez

float4 gradientNoise3DDeriv(float3 x)
{
	float3 i = floor(x);
    float3 f = frac(x);
    
    // quintic interpolant
    float3 u = f*f*f*(f*(f*6.0-15.0)+10.0);
    float3 du = 30.0*f*f*(f*(f-2.0)+1.0);

	// gradients
    float3 ga = hash33(i + float3(0.0,0.0,0.0));
    float3 gb = hash33(i + float3(1.0,0.0,0.0));
    float3 gc = hash33(i + float3(0.0,1.0,0.0));
    float3 gd = hash33(i + float3(1.0,1.0,0.0));
    float3 ge = hash33(i + float3(0.0,0.0,1.0));
	float3 gf = hash33(i + float3(1.0,0.0,1.0));
    float3 gg = hash33(i + float3(0.0,1.0,1.0));
    float3 gh = hash33(i + float3(1.0,1.0,1.0));
    
    // projections
    float va = dot(ga, f - float3(0.0,0.0,0.0));
    float vb = dot(gb, f - float3(1.0,0.0,0.0));
    float vc = dot(gc, f - float3(0.0,1.0,0.0));
    float vd = dot(gd, f - float3(1.0,1.0,0.0));
    float ve = dot(ge, f - float3(0.0,0.0,1.0));
    float vf = dot(gf, f - float3(1.0,0.0,1.0));
    float vg = dot(gg, f - float3(0.0,1.0,1.0));
    float vh = dot(gh, f - float3(1.0,1.0,1.0));
	
    // interpolations
    float k0 = va-vb-vc+vd;
    float3 g0 = ga-gb-gc+gd;
    float k1 = va-vc-ve+vg;
    float3 g1 = ga-gc-ge+gg;
    float k2 = va-vb-ve+vf;
    float3 g2 = ga-gb-ge+gf;
    float k3 = -va+vb+vc-vd+ve-vf-vg+vh;
    float3 g3 = -ga+gb+gc-gd+ge-gf-gg+gh;
    float k4 = vb-va;
    float3 g4 = gb-ga;
    float k5 = vc-va;
    float3 g5 = gc-ga;
    float k6 = ve-va;
    float3 g6 = ge-ga;
    
    return float4( va + k4*u.x + k5*u.y + k6*u.z + k0*u.x*u.y + k1*u.y*u.z + k2*u.z*u.x + k3*u.x*u.y*u.z,    // value
                 ga + g4*u.x + g5*u.y + g6*u.z + g0*u.x*u.y + g1*u.y*u.z + g2*u.z*u.x + g3*u.x*u.y*u.z +   // derivatives
                 du * (float3(k4,k5,k6) + 
                       float3(k0,k1,k2)*u.yzx +
                       float3(k2,k0,k1)*u.zxy +
                       k3*u.yzx*u.zxy ));
}

// Gradient Noise 3D - Inigo Quilez
// https://www.shadertoy.com/view/Xsl3Dl
//	The MIT License
//	Copyright © 2013 Inigo Quilez

float gradientNoise3D(float3 p)
{
	float3 i = floor( p );
	float3 f = frac( p );

	// quintic interpolant
	float3 u = f*f*f*(f*(f*6.0-15.0)+10.0);
	
	return lerp(lerp(lerp(dot(hash33(i + float3(0.0,0.0,0.0) ), f - float3(0.0,0.0,0.0)), 
						  dot(hash33(i + float3(1.0,0.0,0.0) ), f - float3(1.0,0.0,0.0)), u.x),
					lerp(dot(hash33(i + float3(0.0,1.0,0.0) ), f - float3(0.0,1.0,0.0)), 
						  dot(hash33(i + float3(1.0,1.0,0.0) ), f - float3(1.0,1.0,0.0)), u.x), u.y),
				lerp(lerp(dot(hash33(i + float3(0.0,0.0,1.0) ), f - float3(0.0,0.0,1.0)), 
						  dot(hash33(i + float3(1.0,0.0,1.0) ), f - float3(1.0,0.0,1.0)), u.x),
					lerp(dot(hash33(i + float3(0.0,1.0,1.0) ), f - float3(0.0,1.0,1.0)), 
						  dot(hash33(i + float3(1.0,1.0,1.0) ), f - float3(1.0,1.0,1.0)), u.x), u.y), u.z );
}