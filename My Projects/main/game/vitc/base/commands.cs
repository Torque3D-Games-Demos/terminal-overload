// Copyright information can be found in the file named COPYING
// located in the root directory of this distribution.

function serverCmd_F(%client, %nr)
{
   //echo("serverCmd_F(): client" SPC %client SPC "nr" SPC %nr);
   Game.F(%client, %nr);
}

function serverCmdAction(%client, %nr)
{
   //echo("serverCmdAction(): client" SPC %client SPC "action" SPC %nr);
   Game.clientAction(%client, %nr);
}

// ----------------------------------------------------------------------------
// Settings
// ----------------------------------------------------------------------------

function serverCmdGameSettingsChanged(%client, %settings)
{
   //echo("serverCmdGameSettingsChanged(): client:" SPC %client SPC "settings:" SPC %settings);
   Game.queryClientSettings(%client, %settings);
}

function serverCmdVitcSettings1_Reply(%client, %setting, %value)
{
   //echo("serverCmdVitcSettings1_Reply(): client:" SPC %client SPC "setting:" SPC %setting SPC "value:" SPC %value);
   Game.processClientSettingsReply(%client, %setting, %value);
}

// ----------------------------------------------------------------------------
// Demo Recording
// ----------------------------------------------------------------------------

function serverCmdRecordingDemo(%client, %isRecording)
{
   Game.clientRecordingDemo(%client, %isRecording);
}

// ----------------------------------------------------------------------------
// Player List
// ----------------------------------------------------------------------------

function serverCmdShowingPlayerList(%client, %showing)
{
   //echo("serverCmdShowingPlayerList():" SPC %client SPC %showing);
   %client.zShowingPlayerList = %showing;
   if(%showing)
   {
      // Start updating client's player list.
      %client.updatePlayerListThread();
   }
   else
   {
      // Stop updating client's player list.
      cancel(%client.zUpdatePlayerListThread);
   }
}

//-----------------------------------------------------------------------------
// Discs
//-----------------------------------------------------------------------------

function serverCmdSelectDiscSlot(%client, %slot)
{
   echo("serverCmdLaunchDisc():" SPC %disc);
   %player = %client.player;
   if(isObject(%player))
      if(%player.getDataBlock().isMethod("selectDiscSlot"))
         %player.getDataBlock().selectDiscSlot(%player, %slot);
}

//-----------------------------------------------------------------------------
// B.O.U.N.C.E.
//-----------------------------------------------------------------------------

function serverCmdFireBounce(%client)
{
   //echo("serverCmdFireBounce(): client" SPC %client);
   %player = %client.player;
   if(isObject(%player))
      if(%player.getDataBlock().isMethod("fireBounce"))
         %player.getDataBlock().fireBounce(%player);
}

//-----------------------------------------------------------------------------
// Misc server commands avialable to clients
//-----------------------------------------------------------------------------

function serverCmdManifest(%client, %val)
{
   if(!%val)
      Game.etherformManifest(%client.player);
}

function serverCmdSuicide(%client)
{
   Game.suicide(%client);
}

function serverCmdPlayCel(%client,%anim)
{
   if (isObject(%client.player))
      %client.player.playCelAnimation(%anim);
}

function serverCmdTestAnimation(%client, %anim)
{
   if (isObject(%client.player))
      %client.player.playTestAnimation(%anim);
}

function serverCmdPlayDeath(%client)
{
   if (isObject(%client.player))
      %client.player.playDeathAnimation();
}

// ----------------------------------------------------------------------------
// Throw/Toss
// ----------------------------------------------------------------------------

function serverCmdThrow(%client, %data)
{
   %player = %client.player;
   if(!isObject(%player) || %player.getState() $= "Dead" || !$Game::Running)
      return;
   switch$ (%data)
   {
      case "Weapon":
         %item = (%player.getMountedImage($WeaponSlot) == 0) ? "" : %player.getMountedImage($WeaponSlot).item;
         if (%item !$="")
            %player.throw(%item);
      case "Ammo":
         %weapon = (%player.getMountedImage($WeaponSlot) == 0) ? "" : %player.getMountedImage($WeaponSlot);
         if (%weapon !$= "")
         {
            if(%weapon.ammo !$= "")
               %player.throw(%weapon.ammo);
         }
      default:
         if(%player.hasInventory(%data.getName()))
            %player.throw(%data);
   }
}

// ----------------------------------------------------------------------------
// Force game end and cycle
// Probably don't want this in a final game without some checks.  Anyone could
// restart a game.
// ----------------------------------------------------------------------------

function serverCmdFinishGame()
{
   cycleGame();
}

// ----------------------------------------------------------------------------
// Cycle weapons
// ----------------------------------------------------------------------------

function serverCmdCycleWeapon(%client, %direction)
{
   %client.getControlObject().cycleWeapon(%direction);
}

// ----------------------------------------------------------------------------
// Unmount current weapon
// ----------------------------------------------------------------------------

function serverCmdUnmountWeapon(%client)
{
   %client.getControlObject().unmountImage($WeaponSlot);
}


