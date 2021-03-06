// Copyright information can be found in the file named COPYING
// located in the root directory of this distribution.

#include "platform/platform.h"
#include "T3D/physics/physX/pxStream.h"

#include "console/console.h"
#include "console/consoleTypes.h"
#include "core/strings/stringFunctions.h"


PxMemStream::PxMemStream() 
   :  mMemStream( 1024 )
{
}

PxMemStream::~PxMemStream()
{
}

void PxMemStream::resetPosition()
{
   mMemStream.setPosition( 0 );
}

NxU8 PxMemStream::readByte() const 
{ 
   NxU8 out;
   mMemStream.read( &out );
   return out;
}

NxU16	PxMemStream::readWord() const
{ 
   NxU16 out;
   mMemStream.read( &out );
   return out;
}

NxU32	PxMemStream::readDword() const
{ 
   NxU32 out;
   mMemStream.read( &out );
   return out;
}

float	PxMemStream::readFloat()	const
{ 
   float out;
   mMemStream.read( &out );
   return out;
}

double PxMemStream::readDouble() const
{ 
   double out;
   mMemStream.read( &out );
   return out;
}

void PxMemStream::readBuffer( void *buffer, NxU32 size ) const
{ 
   mMemStream.read( size, buffer );
}

NxStream& PxMemStream::storeByte( NxU8 b )
{
   mMemStream.write( b );
   return *this;
}

NxStream& PxMemStream::storeWord( NxU16 w )
{
   mMemStream.write( w );
   return *this;
}

NxStream& PxMemStream::storeDword( NxU32 d )
{
   mMemStream.write( d );
   return *this;
}

NxStream& PxMemStream::storeFloat( NxReal f )
{
   mMemStream.write( f );
   return *this;
}

NxStream& PxMemStream::storeDouble( NxF64 f )
{
   mMemStream.write( f );
   return *this;
}

NxStream& PxMemStream::storeBuffer( const void *buffer, NxU32 size )
{
   mMemStream.write( size, buffer );
   return *this;
}


bool gPhysXLogWarnings = false;

PxConsoleStream::PxConsoleStream()
{
}

PxConsoleStream::~PxConsoleStream()
{
}

void PxConsoleStream::reportError( NxErrorCode code, const char *message, const char* file, int line )
{
   #ifdef TORQUE_DEBUG

      // If we're in debug mode and the error code is serious then
      // pop up a message box to make sure we see it.
      if ( code < NXE_DB_INFO )
      {
         UTF8 info[1024];
         dSprintf( info, 1024, "File: %s\nLine: %d\n%s", file, line, message );
         Platform::AlertOK( "PhysX Error", info );
      }

   #endif

   // In all other cases we just dump the message to the console.
   if ( code == NXE_DB_WARNING )
   {
      if ( gPhysXLogWarnings )
         Con::printf( "PhysX Warning:\n   %s(%d) : %s\n", file, line, message );
   }
   else
      Con::printf( "PhysX Error:\n   %s(%d) : %s\n", file, line, message );
}

NxAssertResponse PxConsoleStream::reportAssertViolation (const char *message, const char *file,int line)
{
   // Assert if we're in debug mode...
   bool triggerBreak = false;
   #ifdef TORQUE_DEBUG
      triggerBreak = PlatformAssert::processAssert( PlatformAssert::Fatal, file, line,  message );
   #endif

   // In all other cases we just dump the message to the console.
   Con::errorf( "PhysX Assert:\n   %s(%d) : %s\n", file, line, message );

   return triggerBreak ? NX_AR_BREAKPOINT : NX_AR_CONTINUE;
}

void PxConsoleStream::print( const char *message )
{
   Con::printf( "PhysX Says: %s\n", message );
}
