// Copyright information can be found in the file named COPYING
// located in the root directory of this distribution.

#ifndef _GUIBITMAPBUTTON_H_
#define _GUIBITMAPBUTTON_H_

#ifndef _GUIBUTTONCTRL_H_
   #include "gui/buttons/guiButtonCtrl.h"
#endif
#ifndef _GFXTEXTUREMANAGER_H_
   #include "gfx/gfxTextureManager.h"
#endif


/// A button control that uses bitmaps as its different button states.
///
/// Set 'bitmap' console field to base name of bitmaps to use.  This control will
///
/// append '_n' for normal
/// append '_h' for highlighted
/// append '_d' for depressed
/// append '_i' for inactive
///
/// If a bitmap cannot be found it will use the default bitmap to render.
///
/// Additionally, a bitmap button can be made to react to keyboard modifiers.  These can be
/// either CTRL/CMD, ALT, or SHIFT (but no combination of them.)  To assign a different bitmap
/// for a modifier state, prepend "_ctrl", _"alt", or "_shift" to the state postfix.
///
/// To implement different handlers for the modifier states, use the "onDefaultClick",
/// "onCtrlClick", "onAltClick", and "onShiftClick" methods.
///
class GuiBitmapButtonCtrl : public GuiButtonCtrl
{
   public:
   
      typedef GuiButtonCtrl Parent;
      
      enum BitmapMode
      {
         BitmapStretched,
         BitmapCentered,
      };

   protected:
   
      enum Modifier
      {
         ModifierNone,
         ModifierCtrl,
         ModifierAlt,
         ModifierShift,
         
         NumModifiers
      };
   
      enum State
      {
         NORMAL,
         HILIGHT,
         DEPRESSED,
         INACTIVE
      };
      
      struct Textures
      {
         /// Texture for normal state.
         GFXTexHandle mTextureNormal;
         
         /// Texture for highlight state.
         GFXTexHandle mTextureHilight;
         
         /// Texture for depressed state.
         GFXTexHandle mTextureDepressed;
         
         /// Texture for inactive state.
         GFXTexHandle mTextureInactive;
      };

      /// Make control extents equal to bitmap size.
      bool mAutoFitExtents;
      
      /// Allow switching out images according to modifier presses.
      bool mUseModifiers;
      
      /// Allow switching images according to mouse states.  On by default.
      /// Switch off when not needed as it otherwise results in a lot of costly
      /// texture loads.
      bool mUseStates;
      
      ///
      BitmapMode mBitmapMode;

      /// File name for bitmap.
      String mBitmapName;
      
      ///
      Textures mTextures[ NumModifiers ];
      
      virtual void renderButton( GFXTexHandle &texture, const Point2I& offset, const RectI& updateRect );
      
      static bool _setAutoFitExtents( void *object, const char *index, const char *data );
      static bool _setBitmap( void *object, const char *index, const char *data );
      
      State getState() const
      {
         if( mActive )
         {
				if( mDepressed || mStateOn ) return DEPRESSED;
            if( mMouseOver ) return HILIGHT;
            return NORMAL;
         }
         else
            return INACTIVE;
      }
      
      Modifier getCurrentModifier();
      GFXTexHandle& getTextureForCurrentState();
      
      /// @name Callbacks
      /// @{
      
      DECLARE_CALLBACK( void, onDefaultClick, () );
      DECLARE_CALLBACK( void, onCtrlClick, () );
      DECLARE_CALLBACK( void, onAltClick, () );
      DECLARE_CALLBACK( void, onShiftClick, () );
      
      /// @}

   public:
                           
      GuiBitmapButtonCtrl();

      void setAutoFitExtents( bool state );
      void setBitmap( const String& name );
      void setBitmapHandles( GFXTexHandle normal, GFXTexHandle highlighted, GFXTexHandle depressed, GFXTexHandle inactive );

      //Parent methods
      virtual bool onWake();
      virtual void onSleep();
      virtual void onAction();
      virtual void inspectPostApply();

      virtual void onRender(Point2I offset, const RectI &updateRect);

      static void initPersistFields();

      DECLARE_CONOBJECT(GuiBitmapButtonCtrl);
      DECLARE_DESCRIPTION( "A button control rendered entirely from bitmaps.\n"
                           "The individual button states are represented with separate bitmaps." );
};

typedef GuiBitmapButtonCtrl::BitmapMode GuiBitmapMode;
DefineEnumType( GuiBitmapMode );

/// Extension of GuiBitmapButtonCtrl that also display a text label on the button.
class GuiBitmapButtonTextCtrl : public GuiBitmapButtonCtrl
{
   public:
   
      typedef GuiBitmapButtonCtrl Parent;
      
   protected:
   
      virtual void renderButton( GFXTexHandle &texture, const Point2I& offset, const RectI& updateRect );

   public:

      DECLARE_CONOBJECT( GuiBitmapButtonTextCtrl );
      DECLARE_DESCRIPTION( "An extension of GuiBitmapButtonCtrl that also renders a text\n"
                           "label on the button." );
};

#endif //_GUI_BITMAP_BUTTON_CTRL_H
