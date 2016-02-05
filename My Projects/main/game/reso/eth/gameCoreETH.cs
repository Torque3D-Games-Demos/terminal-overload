// Copyright information can be found in the file named COPYING
// located in the root directory of this distribution.

function GameCoreETH::onMissionLoaded(%game)
{
   //echo (%game @"\c4 -> "@ %game.class @" -> GameCoreETH::onMissionLoaded");
   ETH::createTeams(%game);
   Parent::onMissionLoaded(%game);
}

function GameCoreETH::initGameVars(%game)
{
   //echo (%game @"\c4 -> "@ %game.class @" -> GameCoreETH::initGameVars");

   //-----------------------------------------------------------------------------
   // What kind of "player" is spawned is either controlled directly by the
   // SpawnSphere or it defaults back to the values set here. This also controls
   // which SimGroups to attempt to select the spawn sphere's from by walking down
   // the list of SpawnGroups till it finds a valid spawn object.
   // These override the values set in core/scripts/server/spawn.cs
   //-----------------------------------------------------------------------------
   
   // Leave $Game::defaultPlayerClass and $Game::defaultPlayerDataBlock as empty strings ("")
   // to spawn a the $Game::defaultCameraClass as the control object.
   $Game::defaultPlayerClass = "Etherform";
   $Game::defaultPlayerDataBlock = "FrmEtherform";
   $Game::defaultPlayerSpawnGroups = "PlayerSpawnPoints PlayerDropPoints";

   //-----------------------------------------------------------------------------
   // What kind of "camera" is spawned is either controlled directly by the
   // SpawnSphere or it defaults back to the values set here. This also controls
   // which SimGroups to attempt to select the spawn sphere's from by walking down
   // the list of SpawnGroups till it finds a valid spawn object.
   // These override the values set in core/scripts/server/spawn.cs
   //-----------------------------------------------------------------------------
   $Game::defaultCameraClass = "Camera";
   $Game::defaultCameraDataBlock = "Observer";
   $Game::defaultCameraSpawnGroups = "CameraSpawnPoints PlayerSpawnPoints PlayerDropPoints";

   // Set the gameplay parameters
   %game.duration = 0;
   %game.endgameScore = 0;
   %game.endgamePause = 10;
   %game.allowCycling = true;   // Is mission cycling allowed?
}

function GameCoreETH::startGame(%game)
{
   //echo (%game @"\c4 -> "@ %game.class @" -> GameCoreETH::startGame");

   Parent::startGame(%game);
   ETH::startNewRound();
}

function GameCoreETH::endGame(%game)
{
   //echo (%game @"\c4 -> "@ %game.class @" -> GameCoreETH::endGame");

   parent::endGame(%game);
}

function GameCoreETH::onGameDurationEnd(%game)
{
   //echo (%game @"\c4 -> "@ %game.class @" -> GameCoreETH::onGameDurationEnd");

   parent::onGameDurationEnd(%game);
}

function GameCoreETH::prepareClient(%game, %client)
{
   //echo (%game @"\c4 -> "@ %game.class @" -> GameCoreETH::prepareClient");

   Parent::prepareClient(%game, %client);
   
   // Setup LoadingGui layers.
   %t[1] = "";
   %t[2] = "<just:center><color:FFFFFF><font:Quantico:18>" @
           "<spush><font:Quantico:32>TACTICAL ETHERNET<spop><br>" @
           "Capture all the opposing team's territory zones";
   %t[3] = "";
   for(%i = 1; %i <= 3; %i++)
   {
      commandToClient(%client, '_VitcLoadingGui_SetLayer', %i, "", false);
   	%l = strlen(%t[%i]); %n = 0;
   	while(%n < %l)
   	{
   		%chunk = getSubStr(%t[%i], %n, 255);
         commandToClient(%client, '_VitcLoadingGui_SetLayer', %i, %chunk, true);
   		%n += 255;
   	}
   }
}

function GameCoreETH::onClientEnterGame(%game, %client)
{
   echo (%game @"\c4 -> "@ %game.class @" -> GameCoreETH::onClientEnterGame");
   
   Parent::onClientEnterGame(%game, %client);
   
   // Setup loadouts
   %client.zActiveLoadout = 0;
   ETH::resetLoadout(%client);

   ETH::setupHud(%client);

   %team1playerCount = ETH::getTeamPlayerCount(1);
   %team2playerCount = ETH::getTeamPlayerCount(2);
   
   // Join team with less players.
   if(%team1playerCount > %team2playerCount)
      ETH::joinTeam(%client, 2);
   else
      ETH::joinTeam(%client, 1);

   if($Game::Duration)
   {
      %timeLeft = ($Game::StartTime + $Game::Duration) - $Sim::Time;
      commandToClient(%client, 'GameTimer', %timeLeft);
   }
}

function GameCoreETH::onClientLeaveGame(%game, %client)
{
   //echo (%game @"\c4 -> "@ %game.class @" -> GameCoreETH::onClientLeaveGame");

   parent::onClientLeaveGame(%game, %client);

}

function GameCoreETH::queryClientSettings(%game, %client, %settings)
{
   //echo (%game @"\c4 -> "@ %game.class @" -> GameCoreETH::queryClientSettings");

   Parent::queryClientSettings(%game, %client, %settings);
   
   %client.paletteColors[0] = "255 255 255 255";
   %client.paletteColors[1] = "255 255 255 255";
   
   commandToClient(%client, 'VitcSettings1_Query', "PlayerColor0");
   commandToClient(%client, 'VitcSettings1_Query', "PlayerColor1");
}

function GameCoreETH::processClientSettingsReply(%game, %client, %setting, %value)
{
   //echo (%game @"\c4 -> "@ %game.class @" -> GameCoreETH::processClientSettingsReply");
   
   %status = "Ignored";
   
   echo(%client.authenticated);

   if(%setting $= "PlayerColor0")
   {
      if(!%client.authenticated)
      {
         %status = "Ignored for unauthenticated players";
      }
      else if(isValidPlayerColor(%value))
      {
         %client.paletteColors[0] = %value SPC "255";
         %status = "Ok";
      }
      else
         %status = "Invalid";

   }
   else if(%setting $= "PlayerColor1")
   {
      if(!%client.authenticated)
      {
         %status = "Ignored for unauthenticated players";
      }
      else if(isValidPlayerColor(%value))
      {
         %client.paletteColors[1] = %value SPC "255";
         %status = "Ok";
      }
      else
         %status = "Invalid";
   }

   commandToClient(%client, 'VitcSettings1_Confirmation', %setting, %status);
}

function GameCoreETH::clientRecordingDemo(%game, %client, %isRecording)
{
   //echo (%game @"\c4 -> "@ %game.class @" -> GameCoreETH::clientRecordingDemo");
   
   if(!%isRecording)
      return;
      
   %client.zDemoRecordingSetupInProgress = true;
   ETH::setupHud(%client);
   %client.control(%client.player);
   %client.zDemoRecordingSetupInProgress = false;
}

function GameCoreETH::updateClientPlayerList(%game, %client)
{
   //echo (%game @"\c4 -> "@ %game.class @" -> GameCoreETH::updateClientPlayerList");
   Parent::updateClientPlayerList(%game, %client);
   ETH::updatePlayerList(%client);
}

function GameCoreETH::loadOut(%game, %player)
{
   //echo (%game @"\c4 -> "@ %game.class @" -> GameCoreETH::loadOut");
   
   Parent::loadOut(%game, %player);
   
   %team = %player.client.team;
   %player.setTeamId(%team.teamId);
   %teamColorF = %team.color;
   %teamColorI = mFloatLength(getWord(%teamColorF, 0)*255, 0) SPC
                 mFloatLength(getWord(%teamColorF, 1)*255, 0) SPC
                 mFloatLength(getWord(%teamColorF, 2)*255, 0) SPC
                 255;
   //echo(%teamColorF SPC "->" SPC %teamColorI);

   %player.paletteColors[0] = %teamColorI;
   %player.paletteColors[1] = %teamColorI;
   %player.paletteColors[13] = "255 255 255 255";
   %player.paletteColors[14] = %teamColorI;

   if(isObject(%player.light))
   {
      %colorI = %player.paletteColors[0];
      %colorF = getWord(%colorI, 0) / 255 SPC
                getWord(%colorI, 1) / 255 SPC
                getWord(%colorI, 2) / 255 SPC
                1;
      %player.light.color = %colorF;
   }

   // Setup ShapeBase HudInfo object team ID
   %player.zShapeBaseHudInfo.setDatasetType(0, $HudInfoDatasetType::TeamID);
   %player.zShapeBaseHudInfo.setDatasetIntField(0, %player.getTeamId());
   %player.zShapeBaseHudInfo.setDatasetType(1, $HudInfoDatasetType::Name);
   %player.zShapeBaseHudInfo.setDatasetStringField(1, %player.client.playerName);

   if(%player.getClassName() $= "Player" || %player.getClassName() $= "AiPlayer")
      ETH::loadoutPlayer(%player);
   else if(%player.getClassName() $= "Etherform")
      ETH::loadoutEtherform(%player);
}

function GameCoreETH::onUnitDestroyed(%game, %obj)
{
   //echo(%game @"\c4 -> "@ %game.class @" -> GameCoreETH::onUnitDestroyed");
   
   Parent::onUnitDestroyed(%game, %obj);
   
   %client = %obj.client;
   if(isObject(%client) && %client.player == %obj)
   {
      ETH::switchToEtherform(%client);
   }
}

function GameCoreETH::clientAction(%game, %client, %nr)
{
   echo(%game @"\c4 -> "@ %game.class @" -> GameCoreETH::clientAction");

   %obj = %client.getControlObject();
   if(!isObject(%obj))
      return;

   %obj.getDataBlock().clientAction(%obj, %nr);
}

function GameCoreETH::etherformManifest(%game, %obj)
{
   echo(%game @"\c4 -> "@ %game.class @" -> GameCoreETH::etherformManifest");
   
   %client = %obj.client;
   
   if(!isObject(%client))
      return;
      
   if(%obj.getClassName() !$= "Etherform")
      return;
      
   %percent = %client.zLoadoutProgress[%client.zActiveLoadout];

   if(%percent < 0.5)
   {
      %client.BeepMsg("Class needs at least 50% health to manifest!");
      return;
   }

   if(%client.player.getEnergyLevel() < 50)
   {
      %client.BeepMsg("You need at least 50% energy to manifest!");
      return;
   }

   %ownTeamId = %client.player.getTeamId();

   %inOwnZone = false;
   %inOwnTerritory = false;
   %inEnemyZone = false;

   %pos = %obj.getPosition();
   InitContainerRadiusSearch(%pos, 0.0001, $TypeMasks::TacticalZoneObjectType);
   while((%srchObj = containerSearchNext()) != 0)
   {
      // object actually in this zone?
      %inSrchZone = false;
      for(%i = 0; %i < %srchObj.getNumObjects(); %i++)
      {
         if(%srchObj.getObject(%i) == %client.player)
         {
            %inSrchZone = true;
            break;
         }
      }
      if(!%inSrchZone)
         continue;

      %zoneTeamId = %srchObj.getTeamId();
      %zoneBlocked = %srchObj.zBlocked;

      if(%zoneTeamId != %ownTeamId && %zoneTeamId != 0)
      {
         %inEnemyZone = true;
         break;
      }
      else if(%zoneTeamId == %ownTeamId)
      {
         %inOwnZone = true;
         if(%srchObj.getDataBlock().getName() $= "TerritoryZone"
         || %srchObj.getDataBlock().isTerritoryZone)
            %inOwnTerritory = true;
      }
   }

   if(%inEnemyZone)
   {
      %client.BeepMsg("You can not manifest in an enemy zone!");
      return;
   }
   else if(%inOwnZone && !%inOwnTerritory)
   {
      %client.BeepMsg("This is not a territory zone!");
      return;
   }
   else if(!%inOwnZone)
   {
      %client.BeepMsg("You can only manifest in your team's territory zones!");
      return;
   }
   else if(%zoneBlocked)
   {
      %client.BeepMsg("This zone is currently blocked!");
      return;
   }
   
   %data = FrmStandardcat;
   switch(%client.zActiveLoadout)
   {
      case 2:
         %data = FrmSnipercat;
   }

   %player = new Player() {
      dataBlock = %data;
      client = %client;
      teamId = %client.team.teamId;
      isCAT = true;
   };
   MissionCleanup.add(%player);
   copyPalette(%obj, %player);

   Game.loadOut(%player);

   %mat = %obj.getTransform();
   %dmg = %obj.getDamageLevel();
   %nrg = %obj.getEnergyLevel();
   %buf = %obj.getDamageBufferLevel();
   %vel = %obj.getVelocity();

   %player.setTransform(%mat);
   %player.setTransform(%pos);
   %player.setDamageLevel(%player.getDataBlock().maxDamage * (1-%percent));
   //%player.setShieldLevel(%buf);

	//if(%tagged || $Server::Game.tagMode == $Server::Game.alwaystag)
	//	%player.setTagged();

   %client.control(%player);

   // Remove any z-velocity.
   %vel = getWord(%vel, 0) SPC getWord(%vel, 1) SPC "0";

   %player.setEnergyLevel(%nrg);
   %player.setVelocity(VectorScale(%vel, 0.25));

   %client.player.schedule(9, "delete");
	%client.player = %player;
}

function GameCoreETH::suicide(%game, %client)
{
   //echo (%game @"\c4 -> "@ %game.class @" -> GameCoreETH::suicide");
   ETH::switchToEtherform(%client);
}

function GameCoreETH::F(%game, %client, %nr)
{
   //echo (%game @"\c4 -> "@ %game.class @" -> GameCoreETH::F");
   if(%nr >= 1 && %nr <= 2)
      ETH::joinTeam(%client, %nr);
}

function GameCoreETH::onZoneOwnerChanged(%game, %zone)
{
   //echo (%game @"\c4 -> "@ %game.class @" -> GameCoreETH::onZoneOwnerChanged");
   ETH::checkRoundEnd();
}

