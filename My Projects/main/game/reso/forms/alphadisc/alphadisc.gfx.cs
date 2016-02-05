// Copyright information can be found in the file named COPYING
// located in the root directory of this distribution.

//----------------------------------------------------------------------------
// Slide decal
//----------------------------------------------------------------------------

datablock DecalData(FrmAlphaDiscSlideDecal)
{
   size = "2";
   material = xa_notc_core_shapes_standardcat_slidedecalmat;
   textureCoordCount = "0";
   lifeSpan = "250";
   fadeTime = "1500";
   paletteSlot = 0;
};

//----------------------------------------------------------------------------
// Skid decal
//----------------------------------------------------------------------------

datablock DecalData(FrmAlphaDiscSkidDecal)
{
   Material = xa_notc_core_shapes_standardcat_skiddecalmat;
   size = "3";
   lifeSpan = "3000";
   randomize = "1";
   texRows = "4";
   texCols = "2";
   textureCoordCount = "7";
   textureCoords[0] = "0 0 0.25 0.5";
   textureCoords[1] = "0.25 0 0.25 0.5";
   textureCoords[2] = "0.5 0 0.25 0.5";
   textureCoords[3] = "0.75 0 0.25 0.5";
   textureCoords[4] = "0 0.5 0.25 0.5";
   textureCoords[5] = "0.25 0.5 0.25 0.5";
   textureCoords[6] = "0.5 0.5 0.25 0.5";
   textureCoords[7] = "0.75 0.5 0.25 0.5";
   textureCoords[8] = "0 0.5 0.25 0.25";
   textureCoords[9] = "0.25 0.5 0.25 0.25";
   textureCoords[10] = "0.5 0.5 0.25 0.25";
   textureCoords[11] = "0.75 0.5 0.25 0.25";
   textureCoords[12] = "0 0.75 0.25 0.25";
   textureCoords[13] = "0.25 0.75 0.25 0.25";
   textureCoords[14] = "0.5 0.75 0.25 0.25";
   textureCoords[15] = "0.75 0.75 0.25 0.25";
   fadeTime = "3000";
   paletteSlot = 0;   
};

//----------------------------------------------------------------------------
// Foot prints
//----------------------------------------------------------------------------

datablock DecalData(FrmAlphaDiscFootprint)
{
   size = "1";
   material = xa_notc_core_shapes_standardcat_footprint;
   textureCoordCount = "0";
   paletteSlot = 0;
};

//----------------------------------------------------------------------------
// Foot puffs
//----------------------------------------------------------------------------

datablock ParticleData(FrmAlphaDiscFootPuffParticle)
{
	dragCoefficient		= "0";
	gravityCoefficient	= "1.5";
	inheritedVelFactor	= 0.0;
	constantAcceleration = 0.0;
	lifetimeMS			  = "600";
	lifetimeVarianceMS	= 0;
	useInvAlpha			 = false;
	spinRandomMin		  = "-100";
	spinRandomMax		  = "100";
	colors[0]	  = "1.0 1.0 1.0 1.0";
	colors[1]	  = "1 1 1 0.496063";
	colors[2]	  = "1.0 1.0 1.0 0.0";
	sizes[0]		= "1";
	sizes[1]		= "0.5";
	sizes[2]		= 0.0;
	times[0]		= 0.0;
	times[1]		= "0.494118";
	times[2]		= 1.0;
	textureName	= "content/o/rotc/p.5.4/textures/rotc/corona.png";
   animTexName = "content/o/rotc/p.5.4/textures/rotc/corona.png";
};

datablock ParticleEmitterData(FrmAlphaDiscFootPuffEmitter)
{
	ejectionPeriodMS = 35;
	periodVarianceMS = 10;
	ejectionVelocity = "5";
	velocityVariance = "0";
	ejectionOffset   = "0";
	thetaMin         = "30";
	thetaMax         = "45";
	phiReferenceVel  = 0;
	phiVariance		= 360;
	overrideAdvances = 0;
	useEmitterColors = false;
	particles = FrmAlphaDiscFootPuffParticle;
   blendStyle = "ADDITIVE";
   paletteSlot = 0;
};

//----------------------------------------------------------------------------
// Spawn explosion
//----------------------------------------------------------------------------

datablock ParticleData(FrmAlphaDiscSpawnExplosion_Cloud)
{
	dragCoeffiecient	  = 0.4;
	gravityCoefficient	= 0;
	inheritedVelFactor	= 0.025;

	lifetimeMS			  = 600;
	lifetimeVarianceMS	= 0;

	useInvAlpha =  false;
	spinRandomMin = -200.0;
	spinRandomMax =  200.0;

	textureName = "content/o/rotc/p.5.4/textures/rotc/corona.png";

	colors[0]	  = "1.0 1.0 1.0 1.0";
	colors[1]	  = "1.0 1.0 1.0 0.0";
	colors[2]	  = "1.0 1.0 1.0 0.0";
	sizes[0]		= 6.0;
	sizes[1]		= 6.0;
	sizes[2]		= 2.0;
	times[0]		= 0.0;
	times[1]		= 0.2;
	times[2]		= 1.0;

	allowLighting = true;
};

datablock ParticleEmitterData(FrmAlphaDiscSpawnExplosion_CloudEmitter)
{
	ejectionPeriodMS = 5;
	periodVarianceMS = 0;

	ejectionVelocity = 6.25;
	velocityVariance = 0.25;

	thetaMin			= 0.0;
	thetaMax			= 90.0;

	lifetimeMS		 = 100;

	particles = "FrmAlphaDiscSpawnExplosion_Cloud";
};

datablock ParticleData(FrmAlphaDiscSpawnExplosion_Dust)
{
	dragCoefficient		= 1.0;
	gravityCoefficient	= -0.01;
	inheritedVelFactor	= 0.0;
	constantAcceleration = 0.0;
	lifetimeMS			  = 1000;
	lifetimeVarianceMS	= 100;
	useInvAlpha			 = true;
	spinRandomMin		  = -90.0;
	spinRandomMax		  = 500.0;
	textureName			 = "content/o/rotc/p.5.4/textures/rotc/smoke_particle.png";
	colors[0]	  = "0.9 0.9 0.9 0.5";
	colors[1]	  = "0.9 0.9 0.9 0.5";
	colors[2]	  = "0.9 0.9 0.9 0.0";
	sizes[0]		= 3.2;
	sizes[1]		= 4.6;
	sizes[2]		= 5.0;
	times[0]		= 0.0;
	times[1]		= 0.7;
	times[2]		= 1.0;
	allowLighting = true;
};

datablock ParticleEmitterData(FrmAlphaDiscSpawnExplosion_DustEmitter)
{
	ejectionPeriodMS = 5;
	periodVarianceMS = 0;
	ejectionVelocity = 15.0;
	velocityVariance = 0.0;
	ejectionOffset	= 0.0;
	thetaMin			= 90;
	thetaMax			= 90;
	phiReferenceVel  = 0;
	phiVariance		= 360;
	overrideAdvances = false;
	lifetimeMS		 = 250;
	particles = "FrmAlphaDiscSpawnExplosion_Dust";
};


datablock ParticleData(FrmAlphaDiscSpawnExplosion_Smoke)
{
	dragCoeffiecient	  = 0.4;
	gravityCoefficient	= -0.5;	// rises slowly
	inheritedVelFactor	= 0.025;

	lifetimeMS			  = 1250;
	lifetimeVarianceMS	= 0;

	useInvAlpha =  true;
	spinRandomMin = -200.0;
	spinRandomMax =  200.0;

	textureName = "content/o/rotc/p.5.4/textures/rotc/smoke_particle.png";

	colors[0]	  = "0.9 0.9 0.9 0.4";
	colors[1]	  = "0.9 0.9 0.9 0.2";
	colors[2]	  = "0.9 0.9 0.9 0.0";
	sizes[0]		= 2.0;
	sizes[1]		= 6.0;
	sizes[2]		= 2.0;
	times[0]		= 0.0;
	times[1]		= 0.5;
	times[2]		= 1.0;

	allowLighting = true;
};

datablock ParticleEmitterData(FrmAlphaDiscSpawnExplosion_SmokeEmitter)
{
	ejectionPeriodMS = 5;
	periodVarianceMS = 0;

	ejectionVelocity = 6.25;
	velocityVariance = 0.25;

	thetaMin			= 0.0;
	thetaMax			= 180.0;

	lifetimeMS		 = 250;

	particles = "FrmAlphaDiscSpawnExplosion_Smoke";
};

datablock ParticleData(FrmAlphaDiscSpawnExplosion_Sparks)
{
	dragCoefficient		= 1;
	gravityCoefficient	= 0.0;
	inheritedVelFactor	= 0.2;
	constantAcceleration = 0.0;
	lifetimeMS			  = 500;
	lifetimeVarianceMS	= 350;
	textureName			 = "content/o/rotc/p.5.4/textures/rotc/particle1.png";
	colors[0]	  = "0.56 0.36 0.26 1.0";
	colors[1]	  = "0.56 0.36 0.26 1.0";
	colors[2]	  = "1.0 0.36 0.26 0.0";
	sizes[0]		= 0.5;
	sizes[1]		= 0.5;
	sizes[2]		= 0.75;
	times[0]		= 0.0;
	times[1]		= 0.5;
	times[2]		= 1.0;
	allowLighting = false;
};

datablock ParticleEmitterData(FrmAlphaDiscSpawnExplosion_SparksEmitter)
{
	ejectionPeriodMS = 2;
	periodVarianceMS = 0;
	ejectionVelocity = 12;
	velocityVariance = 6.75;
	ejectionOffset	= 0.0;
	thetaMin			= 0;
	thetaMax			= 60;
	phiReferenceVel  = 0;
	phiVariance		= 360;
	overrideAdvances = false;
	orientParticles  = true;
	lifetimeMS		 = 100;
	particles = "FrmAlphaDiscSpawnExplosion_Sparks";
};

datablock DebrisData(FrmAlphaDiscSpawnExplosion_SmallDebris)
{
	// shape...
	shapeFile = "content/o/rotc/p.5.4/shapes/rotc/misc/debris1.white.dts";

	// bounce...
	staticOnMaxBounce = true;
	numBounces = 5;

	// physics...
	gravModifier = 2.0;
	elasticity = 0.6;
	friction = 0.1;

	// spin...
	minSpinSpeed = 60;
	maxSpinSpeed = 600;

	// lifetime...
	lifetime = 2.0;
	lifetimeVariance = 1.0;
};

datablock MultiNodeLaserBeamData(FrmAlphaDiscSpawnExplosion_LargeDebris_LaserTrail)
{
	hasLine = true;
	lineColor	= "1.00 1.00 1.00 0.5";

	hasInner = false;
	innerColor = "1.00 1.00 0.00 0.3";
	innerWidth = "0.20";

	hasOuter = true;
	outerColor = "1.00 1.00 1.00 0.2";
	outerWidth = "0.40";

//	bitmap = "content/o/rotc/p.5.4/shapes/rotc/weapons/missilelauncher/explosion.trail";
//	bitmapWidth = 0.25;

	blendMode = 1;
	renderMode = $MultiNodeLaserBeamRenderMode::FaceViewer;
	fadeTime = 1000;
};

datablock ParticleData(FrmAlphaDiscSpawnExplosion_LargeDebris_Particles2)
{
	dragCoefficient		= 1;
	gravityCoefficient	= 0.0;
	windCoefficient		= 0.0;
	inheritedVelFactor	= 0.0;
	constantAcceleration = 0.0;
	lifetimeMS			  = 1000;
	lifetimeVarianceMS	= 0;
	textureName			 = "content/o/rotc/p.5.4/textures/rotc/cross1";
	colors[0]	  = "1.0 1.0 1.0 0.6";
	colors[1]	  = "1.0 1.0 1.0 0.4";
	colors[2]	  = "1.0 1.0 1.0 0.2";
	colors[3]	  = "1.0 1.0 1.0 0.0";
	sizes[0]		= 1.0;
	sizes[1]		= 1.0;
	sizes[2]		= 1.0;
	sizes[3]		= 1.0;
	times[0]		= 0.0;
	times[1]		= 0.333;
	times[2]		= 0.666;
	times[3]		= 1.0;
};

datablock ParticleEmitterData(FrmAlphaDiscSpawnExplosion_LargeDebris_Emitter2)
{
	ejectionPeriodMS = 30;
	periodVarianceMS = 0;
	ejectionVelocity = 2.0;
	velocityVariance = 0.0;
	ejectionOffset	= 0.0;
	thetaMin			= 45;
	thetaMax			= 45;
	phiReferenceVel  = 75000;
	phiVariance		= 0;
	overrideAdvances = false;
	orientParticles  = false;
	lifetimeMS		 = 0;
	particles = "FrmAlphaDiscSpawnExplosion_LargeDebris_Particles2";
};

datablock ParticleData(FrmAlphaDiscSpawnExplosion_LargeDebris_Particles1)
{
	dragCoefficient		= 1;
	gravityCoefficient	= 0.0;
	windCoefficient		= 0.0;
	inheritedVelFactor	= 0.0;
	constantAcceleration = 0.0;
	lifetimeMS			  = 100;
	lifetimeVarianceMS	= 0;
	textureName			 = "content/o/rotc/p.5.4/textures/rotc/cross1";
	colors[0]	  = "1.0 1.0 1.0 1.0";
	colors[1]	  = "1.0 1.0 1.0 1.0";
	colors[2]	  = "1.0 1.0 1.0 0.5";
	colors[3]	  = "1.0 1.0 1.0 0.0";
	sizes[0]		= 2.0;
	sizes[1]		= 2.0;
	sizes[2]		= 2.0;
	sizes[3]		= 2.0;
	times[0]		= 0.0;
	times[1]		= 0.333;
	times[2]		= 0.666;
	times[3]		= 1.0;
};

datablock ParticleEmitterData(FrmAlphaDiscSpawnExplosion_LargeDebris_Emitter1)
{
	ejectionPeriodMS = 5;
	periodVarianceMS = 0;
	ejectionVelocity = 10.0;
	velocityVariance = 0.0;
	ejectionOffset	= 0.0;
	thetaMin			= 0;
	thetaMax			= 180;
	phiReferenceVel  = 0;
	phiVariance		= 360;
	overrideAdvances = false;
	orientParticles  = false;
	lifetimeMS		 = 0;
	particles = "FrmAlphaDiscSpawnExplosion_LargeDebris_Particles1";
};

datablock ExplosionData(FrmAlphaDiscSpawnExplosion_LargeDebris_Explosion)
{
	soundProfile	= MissileLauncherDebrisSound;

	debris = FrmAlphaDiscSpawnExplosion_SmallDebris;
	debrisThetaMin = 0;
	debrisThetaMax = 60;
	debrisNum = 5;
	debrisVelocity = 15.0;
	debrisVelocityVariance = 10.0;
};

datablock DebrisData(FrmAlphaDiscSpawnExplosion_LargeDebris)
{
	// shape...
	shapeFile = "content/o/rotc/p.5.4/shapes/rotc/misc/debris2.white.dts";

	explosion = FrmAlphaDiscSpawnExplosion_LargeDebris_Explosion;

	//laserTrail = FrmAlphaDiscSpawnExplosion_LargeDebris_LaserTrail;
	emitters[0] = FrmAlphaDiscSpawnExplosion_LargeDebris_Emitter2;
	//emitters[1] = FrmAlphaDiscSpawnExplosion_LargeDebris_Emitter1;

	// bounce...
	numBounces = 0;
	explodeOnMaxBounce = true;

	// physics...
	gravModifier = 2.0;
	elasticity = 0.6;
	friction = 0.1;

	// spin...
	minSpinSpeed = 60;
	maxSpinSpeed = 600;

	// lifetime...
	lifetime = 20.0;
	lifetimeVariance = 0.0;
};

datablock ExplosionData(FrmAlphaDiscSpawnExplosion)
{
	soundProfile = FrmAlphaDiscSpawnExplosionSound;

	faceViewer	  = true;
	explosionScale = "0.8 0.8 0.8";

	lifetimeMS = 200;

//	debris = FrmAlphaDiscSpawnExplosion_LargeDebris;
//	debrisThetaMin = 0;
//	debrisThetaMax = 60;
//	debrisNum = 5;
//	debrisVelocity = 30.0;
//	debrisVelocityVariance = 10.0;

//	particleEmitter = FrmAlphaDiscSpawnExplosion_CloudEmitter;
//	particleDensity = 100;
//	particleRadius = 4;

	emitter[0] = FrmAlphaDiscSpawnExplosion_DustEmitter;
	emitter[1] = FrmAlphaDiscSpawnExplosion_SmokeEmitter;
	//emitter[2] = FrmAlphaDiscSpawnExplosion_SparksEmitter;

	// Camera shake
	shakeCamera = true;
	camShakeFreq = "10.0 6.0 9.0";
	camShakeAmp = "20.0 20.0 20.0";
	camShakeDuration = 0.5;
	camShakeRadius = 20.0;

	// Dynamic light
	lightStartRadius = 18;
	lightEndRadius = 0;
	lightStartColor = "1.0 1.0 0.0";
	lightEndColor = "0.0 0.0 0.0";
};

//------------------------------------------------------------------------------
// light images...

datablock ShapeBaseImageData(FrmAlphaDiscLightImage)
{
	// basic item properties
	shapeFile = "content/o/rotc/p.5.4/shapes/rotc/misc/nothing.dts";
	emap = true;

	// mount point & mount offset...
	mountPoint  = 4;
	offset = "0 0 0";

	// light properties...
	lightType = "ConstantLight";
	lightColor = "1 0 0";
	lightTime = 1000;
	lightRadius = 4;
	lightCastsShadows = false;
	lightAffectsShapes = false;

	stateName[0] = "DoNothing";
};

//------------------------------------------------------------------------------
// damage buffer particle emitter...

datablock ParticleData(FrmAlphaDiscDamageBufferEmitter_Particle)
{
	dragCoefficient		= 1.0;
	gravityCoefficient	= -3.0;
	inheritedVelFactor	= 0.0;
	constantAcceleration = 0.0;
	lifetimeMS			  = 500;
	lifetimeVarianceMS	= 0;
	colors[0]	  = "1.0 1.0 1.0 1.0";
	colors[1]	  = "1.0 1.0 1.0 0.2";
	colors[2]	  = "1.0 1.0 1.0 0.0";
	sizes[0]		= 1.0;
	sizes[1]		= 1.0;
	sizes[2]		= 1.0;
	times[0]		= 0.0;
	times[1]		= 0.5;
	times[2]		= 1.0;
	spinRandomMin = 0.0;
	spinRandomMax = 0.0;
	textureName	= "content/o/rotc/p.5.4/textures/rotc/corona";
	allowLighting = false;
};

datablock ParticleEmitterData(FrmAlphaDiscDamageBufferEmitter)
{
	ejectionPeriodMS = 10;
	periodVarianceMS = 0;
	ejectionVelocity = 5;
	velocityVariance = 0;
	ejectionOffset	= 0.0;
	thetaMin			= 90;
	thetaMax			= 90;
	phiReferenceVel  = 0;
	phiVariance		= 360;
	overrideAdvance  = false;
	orientParticles  = false;
	lifetimeMS		 = 0; // forever
	particles = FrmAlphaDiscDamageBufferEmitter_Particle;
};

//------------------------------------------------------------------------------
// damage repair particle emitter...

datablock ParticleData(FrmAlphaDiscRepairEmitter_Particle)
{
	dragCoefficient		= 0.0;
	gravityCoefficient	= 0.0;
	inheritedVelFactor	= 0.0;
	constantAcceleration = 0.0;
	lifetimeMS			  = 220;
	lifetimeVarianceMS	= 0;
	colors[0]	  = "0.996078 0.996078 0.996078 1";
	colors[1]	  = "1 1 1 0";
	colors[2]	  = "1 1 1 1";
	sizes[0]		= "4";
	sizes[1]		= "3";
	sizes[2]		= 0.0;
	times[0]		= 0.0;
	times[1]		= "1";
	times[2]		= 1.0;
	spinRandomMin = 0.0;
	spinRandomMax = 0.0;
	textureName	= "content/xa/notc/core/textures/health.png";
	allowLighting = 0;
   animTexName = "content/xa/notc/core/textures/health.png";
};

datablock ParticleEmitterData(FrmAlphaDiscRepairEmitter)
{
	ejectionPeriodMS = 10;
	periodVarianceMS = 0;
	ejectionVelocity = "0";
	velocityVariance = 0.0;
	ejectionOffset	= "2";
	thetaMin			= 30;
	thetaMax			= 70;
	phiReferenceVel  = 0;
	phiVariance		= 360;
	overrideAdvance  = false;
	orientParticles  = false;
	lifetimeMS		 = 0; // forever
	particles = FrmAlphaDiscRepairEmitter_Particle;
   ejectionOffsetVariance = "2";
   blendStyle = "NORMAL";
   targetLockTimeMS = "480";
   paletteSlot = 0;
};

//------------------------------------------------------------------------------
// damage particle emitter...

datablock ParticleData(FrmAlphaDiscDamageEmitter_Particle)
{
   sizes[0] = "0.4";
   sizes[1] = "0";
   sizes[2] = "0";
   sizes[3] = "0";
   times[1] = "1";
   times[2] = "1";
   inheritedVelFactor = "0";
   lifetimeMS = "1000";
   lifetimeVarianceMS = "0";
   dragCoefficient = "0";
   spinSpeed = "0";
   textureName = "content/xa/notc/core/textures/white.128.png";
   animTexName = "content/xa/notc/core/textures/white.128.png";
   colors[1] = "0.976378 0.992126 0.338583 0";
   colors[2] = "1 1 1 0.330709";
   colors[3] = "1 1 1 0";
   ejectionPeriodMS = "2";
   ejectionVelocity = "0";
   softnessDistance = "1";
   ejectionOffset = "0";
   gravityCoefficient = "2.99634";
   colors[0] = "0.976378 0.992126 0.322835 1";
   useInvAlpha = "0";
};

datablock ParticleEmitterData(FrmAlphaDiscDamageEmitter)
{
   particles = "FrmAlphaDiscDamageEmitter_Particle";
   ejectionPeriodMS = "1";
   periodVarianceMS = "0";
   softnessDistance = "1";
   ejectionVelocity = "15";
   ejectionOffset = "0";
   thetaMin = "0";
   thetaMax = "80";
   orientParticles = "1";
   blendStyle = "ADDITIVE";
   soundProfile = "FrmAlphaDiscBleedEffectSound";
   particleDensity = "2";
   particleRadius = "0.1";
   emitter[0] = "FrmAlphaDiscBleedEffect_Sting_Emitter";
   lifetimeMS = "0";
   lightStartRadius = "4.94118";
   lightStartColor = "1 0 0 1";
   lightEndColor = "0.992126 0 0 1";
   lightStartBrightness = "0.784314";
   lightEndBrightness = "1.80392";
   paletteSlot = -1;
   emitter0 = "FrmAlphaDiscBleedEffect_Sting_Emitter";
   glow = "1";
   targetLockTimeMS = "480";
};

//------------------------------------------------------------------------------
// buffer damage particle emitter...

datablock ParticleData(FrmAlphaDiscBufferDamageEmitter_Particle)
{
   times[0] = "0";
   times[1] = "1";
   sizes[0] = "0.2";
   sizes[1] = "0";
   colors[0] = "1 1 1 1";
   colors[1] = "1 1 1 0";
   inheritedVelFactor = "0";
   lifetimeMS = "1000";
   lifetimeVarianceMS = "0";
   dragCoefficient = "0";
   spinSpeed = "0";
   textureName = "content/xa/notc/core/textures/white.128.png";
   animTexName = "content/xa/notc/core/textures/white.128.png";
   ejectionPeriodMS = "2";
   ejectionVelocity = "0";
   softnessDistance = "1";
   ejectionOffset = "0";
   gravityCoefficient = "2.99634";
   useInvAlpha = "0";
};

datablock ParticleEmitterData(FrmAlphaDiscBufferDamageEmitter)
{
   particles = "FrmAlphaDiscBufferDamageEmitter_Particle";
   ejectionPeriodMS = "1";
   periodVarianceMS = "0";
   softnessDistance = "1";
   ejectionVelocity = "15";
   ejectionOffset = "0";
   thetaMin = "0";
   thetaMax = "80";
   orientParticles = "1";
   blendStyle = "ADDITIVE";
   particleDensity = "2";
   particleRadius = "0.1";
   lifetimeMS = "0";
   paletteSlot = -1;
   glow = "1";
};

//-----------------------------------------------------------------------------
// jump explosion

datablock ParticleData(FrmAlphaDiscJumpExplosion_Cloud)
{
	dragCoeffiecient	  = 0.4;
	gravityCoefficient	= 0;
	inheritedVelFactor	= 0.025;

	lifetimeMS			  = 600;
	lifetimeVarianceMS	= 0;

	useInvAlpha = false;
	spinRandomMin = -200.0;
	spinRandomMax =  200.0;

	textureName = "content/o/rotc/p.5.4/textures/rotc/corona.png";

	colors[0]	  = "1.0 1.0 1.0 1.0";
	colors[1]	  = "1.0 1.0 1.0 0.0";
	colors[2]	  = "1.0 1.0 1.0 0.0";
	sizes[0]		= 2.0;
	sizes[1]		= 2.0;
	sizes[2]		= 0.5;
	times[0]		= 0.0;
	times[1]		= 0.2;
	times[2]		= 1.0;

	allowLighting = true;
};

datablock ParticleEmitterData(FrmAlphaDiscJumpExplosion_CloudEmitter)
{
	ejectionPeriodMS = 1;
	periodVarianceMS = 0;

	ejectionVelocity = 0.25;
	velocityVariance = 0.25;

	thetaMin			= 0.0;
	thetaMax			= 90.0;

	lifetimeMS		 = 100;

	particles = "FrmAlphaDiscJumpExplosion_Cloud";
};

datablock ParticleData(FrmAlphaDiscJumpExplosion_Dust)
{
	dragCoefficient		= 1.0;
	gravityCoefficient	= -0.01;
	inheritedVelFactor	= 0.0;
	constantAcceleration = 0.0;
	lifetimeMS			  = 1000;
	lifetimeVarianceMS	= 100;
	useInvAlpha			 = true;
	spinRandomMin		  = -90.0;
	spinRandomMax		  = 500.0;
	textureName			 = "content/o/rotc/p.5.4/textures/rotc/smoke_particle.png";
	colors[0]	  = "0.9 0.9 0.9 0.5";
	colors[1]	  = "0.9 0.9 0.9 0.5";
	colors[2]	  = "0.9 0.9 0.9 0.0";
	sizes[0]		= 0.9;
	sizes[1]		= 1.5;
	sizes[2]		= 1.6;
	times[0]		= 0.0;
	times[1]		= 0.7;
	times[2]		= 1.0;
	allowLighting = true;
};

datablock ParticleEmitterData(FrmAlphaDiscJumpExplosion_DustEmitter)
{
	ejectionPeriodMS = 10;
	periodVarianceMS = 0;
	ejectionVelocity = 2.0;
	velocityVariance = 0.0;
	ejectionOffset	= 0.0;
	thetaMin			= 0;
	thetaMax			= 180;
	phiReferenceVel  = 0;
	phiVariance		= 360;
	overrideAdvances = false;
	lifetimeMS		 = 50;
	particles = "FrmAlphaDiscJumpExplosion_Dust";
};


datablock ParticleData(FrmAlphaDiscJumpExplosion_Smoke)
{
	dragCoeffiecient	  = 0.4;
	gravityCoefficient	= -0.5;	// rises slowly
	inheritedVelFactor	= 0.025;

	lifetimeMS			  = 1250;
	lifetimeVarianceMS	= 0;

	useInvAlpha =  true;
	spinRandomMin = -200.0;
	spinRandomMax =  200.0;

	textureName = "content/o/rotc/p.5.4/textures/rotc/smoke_particle.png";

	colors[0]	  = "0.9 0.9 0.9 0.4";
	colors[1]	  = "0.9 0.9 0.9 0.2";
	colors[2]	  = "0.9 0.9 0.9 0.0";
	sizes[0]		= 0.6;
	sizes[1]		= 2.0;
	sizes[2]		= 0.6;
	times[0]		= 0.0;
	times[1]		= 0.5;
	times[2]		= 1.0;

	allowLighting = true;
};

datablock ParticleEmitterData(FrmAlphaDiscJumpExplosion_SmokeEmitter)
{
	ejectionPeriodMS = 2;
	periodVarianceMS = 0;

	ejectionVelocity = 2.0;
	velocityVariance = 0.25;

	thetaMin			= 0.0;
	thetaMax			= 180.0;

	lifetimeMS		 = 250;

	particles = "FrmAlphaDiscJumpExplosion_Smoke";
};

datablock ParticleData(FrmAlphaDiscJumpExplosion_Sparks)
{
	dragCoefficient		= 1;
	gravityCoefficient	= 0.0;
	inheritedVelFactor	= 0.2;
	constantAcceleration = 0.0;
	lifetimeMS			  = 500;
	lifetimeVarianceMS	= 350;
	textureName			 = "content/o/rotc/p.5.4/textures/rotc/particle1.png";
	colors[0]	  = "0.56 0.36 0.26 1.0";
	colors[1]	  = "0.56 0.36 0.26 1.0";
	colors[2]	  = "1.0 0.36 0.26 0.0";
	sizes[0]		= 0.5;
	sizes[1]		= 0.5;
	sizes[2]		= 0.75;
	times[0]		= 0.0;
	times[1]		= 0.5;
	times[2]		= 1.0;
	allowLighting = false;
};

datablock ParticleEmitterData(FrmAlphaDiscJumpExplosion_SparksEmitter)
{
	ejectionPeriodMS = 10;
	periodVarianceMS = 0;
	ejectionVelocity = 4;
	velocityVariance = 1;
	ejectionOffset	= 0.0;
	thetaMin			= 0;
	thetaMax			= 60;
	phiReferenceVel  = 0;
	phiVariance		= 360;
	overrideAdvances = false;
	orientParticles  = true;
	lifetimeMS		 = 100;
	particles = "FrmAlphaDiscJumpExplosion_Sparks";
};

datablock MultiNodeLaserBeamData(FrmAlphaDiscJumpExplosion_Debris_LaserTrail)
{
	hasLine = true;
	lineColor	= "1.00 1.00 0.00 0.5";

	hasInner = false;
	innerColor = "1.00 1.00 0.00 0.3";
	innerWidth = "0.20";

	hasOuter = false;
	outerColor = "1.00 1.00 0.00 0.3";
	outerWidth = "0.40";

//	bitmap = "content/o/rotc/p.5.4/shapes/rotc/weapons/hegrenade/lasertrail";
//	bitmapWidth = 0.1;

	blendMode = 1;
	renderMode = $MultiNodeLaserBeamRenderMode::FaceViewer;
	fadeTime = 500;
};

datablock DebrisData(FrmAlphaDiscJumpExplosion_Debris)
{
//	shapeFile = "content/o/rotc/p.5.4/shapes/rotc/weapons/hegrenade/grenade.dts";
//	emitters[0] = GrenadeLauncherParticleEmitter;

	laserTrail = FrmAlphaDiscJumpExplosion_Debris_LaserTrail;

	// bounce...
	numBounces = 3;
	explodeOnMaxBounce = true;

	// physics...
	gravModifier = 5.0;
	elasticity = 0.6;
	friction = 0.1;

	lifetime = 5.0;
	lifetimeVariance = 0.02;
};

datablock ExplosionData(FrmAlphaDiscJumpExplosion)
{
	soundProfile = FrmAlphaDiscJumpExplosionSound;

	lifetimeMS = 200;

	debris = 0; //FrmAlphaDiscJumpExplosion_Debris;
	debrisThetaMin = 0;
	debrisThetaMax = 180;
	debrisNum = 3;
	debrisVelocity = 50.0;
	debrisVelocityVariance = 10.0;

	particleEmitter = FrmAlphaDiscJumpExplosion_CloudEmitter;
	particleDensity = 50;
	particleRadius = 1;

	emitter[0] = FrmAlphaDiscJumpExplosion_DustEmitter;
	emitter[1] = 0; // FrmAlphaDiscJumpExplosion_SmokeEmitter;
	emitter[2] = 0; // FrmAlphaDiscJumpExplosion_SparksEmitter;

	// Camera shake
	shakeCamera = true;
	camShakeFreq = "10.0 6.0 9.0";
	camShakeAmp = "10.0 0.0 0.0";
	camShakeDuration = 0.5;
	camShakeRadius = 1.0;

	// Dynamic light
	lightStartRadius = 15;
	lightEndRadius = 0;
	lightStartColor = "1.0 0.8 0.2 1.0";
	lightEndColor = "1.0 0.8 0.2 0.3";
};

