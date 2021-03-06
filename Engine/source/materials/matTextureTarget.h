// Copyright information can be found in the file named COPYING
// located in the root directory of this distribution.

#ifndef _MATTEXTURETARGET_H_
#define _MATTEXTURETARGET_H_

#ifndef _TDICTIONARY_H_
#include "core/util/tDictionary.h"
#endif
#ifndef _REFBASE_H_
#include "core/util/refBase.h"
#endif
#ifndef _GFXTEXTUREHANDLE_H_
#include "gfx/gfxTextureHandle.h"
#endif
#ifndef _MRECT_H_
#include "math/mRect.h"
#endif
#ifndef _GFXSTATEBLOCK_H_
#include "gfx/gfxStateBlock.h"
#endif
#ifndef _UTIL_DELEGATE_H_
#include "core/util/delegate.h"
#endif

struct GFXShaderMacro;
class ConditionerFeature;


///
class NamedTexTarget : public WeakRefBase
{
public:
   
   ///
   static NamedTexTarget* find( const String &name );

   ///
   NamedTexTarget();

   ///
   virtual ~NamedTexTarget();
   
   ///
   bool registerWithName( const String &name );

   ///
   void unregister();

   ///
   bool isRegistered() const { return mIsRegistered; }

   /// Returns the target name we were registered with.
   const String& getName() const { return mName; }

   // Register the passed texture with our name, unregistering "anyone"
   // priorly registered with that name.   
   // Pass NULL to only unregister.
   void setTexture( GFXTextureObject *tex ) { setTexture( 0, tex ); }

   ///
   void setTexture( U32 index, GFXTextureObject *tex );   

   ///
   GFXTextureObject* getTexture( U32 index = 0 ) const;

   /// The delegate used to override the getTexture method.
   /// @see getTexture
   typedef Delegate<GFXTextureObject*(U32)> TexDelegate;

   /// 
   /// @see getTexture
   TexDelegate& getTextureDelegate() { return mTexDelegate; }
   const TexDelegate& getTextureDelegate() const { return mTexDelegate; }

   /// Release all the textures.
   void release();

   // NOTE:
   //
   // The following members are here to support the existing conditioner
   // and target system used for the deferred gbuffer and lighting.
   //
   // We will refactor that system as part of material2 removing the concept
   // of conditioners from C++ (moving them to HLSL/GLSL) and make the shader
   // features which use the texture responsible for setting the correct sampler
   // states.
   //
   // It could be that at this time this class could completely
   // be removed and instead these textures can be registered
   // with the TEXMGR and looked up there exclusively.
   //
   void setViewport( const RectI &viewport ) { mViewport = viewport; }
   const RectI& getViewport() const { return mViewport; }
   void setSamplerState( const GFXSamplerStateDesc &desc ) { mSamplerDesc = desc; }
   void setupSamplerState( GFXSamplerStateDesc *desc ) const  { *desc = mSamplerDesc; }
   void setConditioner( ConditionerFeature *cond ) { mConditioner = cond; }
   ConditionerFeature* getConditioner() const { return mConditioner; }
   void getShaderMacros( Vector<GFXShaderMacro> *outMacros );

protected:
   
   typedef Map<String,NamedTexTarget*> TargetMap;

   ///
   static TargetMap smTargets;

   ///
   bool mIsRegistered;

   /// The target name we were registered with.
   String mName;

   /// The held textures.
   GFXTexHandle mTex[4];

   ///
   TexDelegate mTexDelegate;

   ///
   RectI mViewport; 

   ///
   GFXSamplerStateDesc mSamplerDesc;

   ///
   ConditionerFeature *mConditioner;
};


inline GFXTextureObject* NamedTexTarget::getTexture( U32 index ) const
{
   AssertFatal( index < 4, "NamedTexTarget::getTexture - Got invalid index!" );
   if ( mTexDelegate.empty() )
      return mTex[index];

   return mTexDelegate( index );
}


/// A weak reference to a texture target.
typedef WeakRefPtr<NamedTexTarget> NamedTexTargetRef;

#endif // _MATTEXTURETARGET_H_
