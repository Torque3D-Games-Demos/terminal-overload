// Copyright information can be found in the file named COPYING
// located in the root directory of this distribution.

#ifndef _SFXFMODEVENT_H_
#define _SFXFMODEVENT_H_

#ifndef _SFXTRACK_H_
   #include "sfx/sfxTrack.h"
#endif
#ifndef _CONSOLETYPES_H_
   #include "console/consoleTypes.h"
#endif
#ifndef _MPOINT2_H_
   #include "math/mPoint2.h"
#endif

#include "fmod_event.h"


class SFXFMODProject;
class SFXFMODEventGroup;


/// An event in an FMOD Designer project.
///
/// This class must not be manually instanced by the user.  Instead, SFXFMODEvents
/// are automatically created when an SFXFMODProject is loaded.
///
/// Be aware that as all the playback happens internally within FMOD's event system,
/// this bypasses the SFX layer and will thus not work with features that rely the
/// structures there.  Namely, sound occlusion (except for FMOD's own occlusion) will
/// not work with FMOD events.
///
/// The parameters of an FMOD event are automatically created and designed using the
/// information in the project.
///
class SFXFMODEvent : public SFXTrack
{
   public:
   
      typedef SFXTrack Parent;
      friend class SFXFMODEventGroup;
      friend class SFXFMODEventSource;
      
   protected:
   
      /// Name of the event in the Designer project.
      String mName;
      
      /// Event group that this event belongs to.
      SFXFMODEventGroup* mGroup;
      
      /// Next event in the group's event chain.
      SFXFMODEvent* mSibling;
      
      /// FMOD event handle when event is open.  Client-side only.
      FMOD_EVENT* mHandle;
      
      ///
      Point2F mParameterRanges[ MaxNumParameters ];
      
      ///
      F32 mParameterValues[ MaxNumParameters ];
      
      /// Group ID for client net sync.
      S32 mGroupId;
      
      ///
      void _createParameters();
      
   public:
   
      ///
      SFXFMODEvent();
      
      ///
      SFXFMODEvent( SFXFMODEventGroup* group, const String& name );
      
      ///
      SFXFMODEvent( SFXFMODEventGroup* group, FMOD_EVENT* handle );
      
      ~SFXFMODEvent();

      /// Create the event object on the FMOD device.
      void acquire();
      
      /// Release the event object on the FMOD device.
      void release();
      
      ///
      const String& getEventName() const { return mName; }
      
      ///
      SFXFMODEventGroup* getEventGroup() const { return mGroup; }
      
      ///
      String getQualifiedName() const;
      
      ///
      bool isDataLoaded() const;
      
      // SFXTrack.
      virtual bool onAdd();
      virtual void onRemove();
      virtual bool preload( bool server, String& errorStr );
      virtual void packData( BitStream* stream );
      virtual void unpackData( BitStream* stream );
      
      static void initPersistFields();
      
      DECLARE_CONOBJECT( SFXFMODEvent );
      DECLARE_CATEGORY( "SFX FMOD" );
      DECLARE_DESCRIPTION( "An FMOD Designer event." );
};

#endif // !_SFXFMODEVENT_H_
