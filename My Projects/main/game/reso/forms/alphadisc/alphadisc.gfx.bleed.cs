// Copyright information can be found in the file named COPYING
// located in the root directory of this distribution.

datablock DecalData(FrmAlphaDiscBleedEffect_Decal)
{
   size = "2";
   material = xa_notc_core_shapes_standardcat_blood_p1_decalmat;
   textureCoordCount = "0";
   lifeSpan = "250";
   fadeTime = "2500";
   paletteSlot = 0;
};

datablock DebrisData(FrmAlphaDiscBleedEffect_Debris)
{
	// shape...
	shapeFile = "content/xa/notc/core/shapes/standardcat/blood/p1/shape.dae";
 
   // decal...
   decal = FrmAlphaDiscBleedEffect_Decal;

	// bounce...
	staticOnMaxBounce = "0";
	numBounces = "0";

	// physics...
	gravModifier = 4.0;
	elasticity = 0.6;
	friction = 0.1;

	// spin...
	minSpinSpeed = 60;
	maxSpinSpeed = 600;

	// lifetime...
	lifetime = 4.0;
	lifetimeVariance = 1.0;
   explodeOnMaxBounce = "1";
};

datablock ParticleData(FrmAlphaDiscBleedEffect_Particles : DefaultParticle)
{
   sizes[0] = "0.5";
   sizes[1] = "0";
   sizes[2] = "0";
   sizes[3] = "0";
   times[1] = "1";
   times[2] = "1";
   inheritedVelFactor = "0";
   lifetimeMS = "2000";
   lifetimeVarianceMS = "0";
   dragCoefficient = "0";
   spinSpeed = "0";
   textureName = "content/xa/notc/core/textures/white.128.png";
   animTexName = "content/xa/notc/core/textures/white.128.png";
   colors[1] = "0.980392 0.996078 0.345098 0";
   colors[2] = "1 1 1 0.330709";
   colors[3] = "1 1 1 0";
   ejectionPeriodMS = "2";
   ejectionVelocity = "0";
   softnessDistance = "1";
   ejectionOffset = "0";
   gravityCoefficient = "3";
   colors[0] = "0.980392 0.996078 0.32549 1";
   useInvAlpha = "0";
};

//------------------------------------------------------------------------------

datablock ExplosionData(FrmAlphaDiscBleedEffect0)
{
	// shape...
	explosionShape = "content/xa/rotc_hack/shapes/explosion_white.dts";
	faceViewer	  = true;
	playSpeed = 4.0;
	sizes[0] = "0.0 0.0 0.0";
	sizes[1] = "0.1 0.1 0.1";
	times[0] = 0.0;
	times[1] = 1.0;

	// dynamic light...
	lightStartRadius = 4;
	lightEndRadius = 0;
	lightStartColor = "1.0 1.0 1.0";
	lightEndColor = "0.0 0.0 0.0";
};

//------------------------------------------------------------------------------

datablock ParticleEmitterData(FrmAlphaDiscBleedEffect10Emitter : DefaultEmitter)
{
   particles = "FrmAlphaDiscBleedEffect_Particles";
   ejectionPeriodMS = "1";
   periodVarianceMS = "0";
   softnessDistance = "1";
   ejectionVelocity = "15";
   ejectionOffset = "0";
   thetaMin = "0";
   thetaMax = "180";
   orientParticles = "1";
   blendStyle = "ADDITIVE";
   soundProfile = "FrmAlphaDiscBleedEffectSound";
   particleDensity = "2";
   particleRadius = "0.1";
   emitter[0] = "FrmAlphaDiscBleedEffect_Sting_Emitter";
   lifetimeMS = "96";
   lightStartRadius = "4.94118";
   lightStartColor = "1 0 0 1";
   lightEndColor = "0.992126 0 0 1";
   lightStartBrightness = "0.784314";
   lightEndBrightness = "1.80392";
   paletteSlot = -1;
   emitter0 = "FrmAlphaDiscBleedEffect_Sting_Emitter";
   glow = "1";
};

datablock ExplosionData(FrmAlphaDiscBleedEffect10)
{
   //soundProfile = FrmAlphaDiscBleedEffectSound;
   
   explosionShape = "content/xa/notc/core/shapes/mgl1/impactdmg/p1/shape.dae";
	faceViewer	  = true;
	playSpeed = 8.0;
	sizes[0] = "0.1 0.1 0.1";
	sizes[1] = "1.0 1.0 1.0";
	times[0] = 0.0;
	times[1] = 1.0;
   
   lifetimeMS = "64";
   lightStartRadius = "4.94118";
   lightStartColor = "1 0 0 1";
   lightEndColor = "0.992126 0 0 1";
   lightStartBrightness = "0.784314";
   lightEndBrightness = "1.80392";
   particleRadius = "0.1";
   particleDensity = "2";
   //emitter[0] = "FrmAlphaDiscBleedEffect10Emitter";
   //Debris = "FrmAlphaDiscBleedEffect_Debris";
   debrisThetaMax = "60";
   debrisNum = "2";
   debrisVelocity = 10;
};

//------------------------------------------------------------------------------

datablock ExplosionData(FrmAlphaDiscBleedEffect20 : FrmAlphaDiscBleedEffect10)
{
   lifetimeMS = "96";
   debrisNum = "6";
	sizes[1] = "4 4 4";
};

datablock ExplosionData(FrmAlphaDiscBleedEffect30 : FrmAlphaDiscBleedEffect10)
{
   lifetimeMS = "128";
   debrisNum = "6";
	sizes[1] = "6 6 6";
};

datablock ExplosionData(FrmAlphaDiscBleedEffect40 : FrmAlphaDiscBleedEffect10)
{
   lifetimeMS = "160";
   debrisNum = "8";
	sizes[1] = "8 8 8";
};

datablock ExplosionData(FrmAlphaDiscBleedEffect50 : FrmAlphaDiscBleedEffect10)
{
   lifetimeMS = "192";
   debrisNum = "10";
	sizes[1] = "10 10 10";
};

datablock ExplosionData(FrmAlphaDiscBleedEffect60 : FrmAlphaDiscBleedEffect10)
{
   lifetimeMS = "224";
   debrisNum = "12";
	sizes[1] = "12 12 12";
};

datablock ExplosionData(FrmAlphaDiscBleedEffect70 : FrmAlphaDiscBleedEffect10)
{
   lifetimeMS = "256";
   debrisNum = "14";
	sizes[1] = "14 14 14";
};

datablock ExplosionData(FrmAlphaDiscBleedEffect80 : FrmAlphaDiscBleedEffect10)
{
   lifetimeMS = "288";
   debrisNum = "16";
	sizes[1] = "16 16 16";
};

datablock ExplosionData(FrmAlphaDiscBleedEffect90 : FrmAlphaDiscBleedEffect10)
{
   lifetimeMS = "320";
   debrisNum = "18";
	sizes[1] = "18 18 18";
};

datablock ExplosionData(FrmAlphaDiscBleedEffect100 : FrmAlphaDiscBleedEffect10)
{
   lifetimeMS = "352";
   debrisNum = "20";
	sizes[1] = "20 20 20";
};

