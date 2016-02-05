// Copyright information can be found in the file named COPYING
// located in the root directory of this distribution.

//-----------------------------------------------------------------------------
// AIPlayer callbacks
// The AIPlayer class implements the following callbacks:
//
//    PlayerData::onStop(%this,%obj)
//    PlayerData::onMove(%this,%obj)
//    PlayerData::onReachDestination(%this,%obj)
//    PlayerData::onMoveStuck(%this,%obj)
//    PlayerData::onTargetEnterLOS(%this,%obj)
//    PlayerData::onTargetExitLOS(%this,%obj)
//    PlayerData::onAdd(%this,%obj)
//
// Since the AIPlayer doesn't implement it's own datablock, these callbacks
// all take place in the PlayerData namespace.
//-----------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// Demo Pathed AIPlayer.
//-----------------------------------------------------------------------------

function DemoPlayer::onReachDestination(%this,%obj)
{
   //echo( %obj @ " onReachDestination" );

   // Moves to the next node on the path.
   // Override for all player.  Normally we'd override this for only
   // a specific player datablock or class of players.
   if (%obj.path !$= "")
   {
      if (%obj.currentNode == %obj.targetNode)
         %this.onEndOfPath(%obj,%obj.path);
      else
         %obj.moveToNextNode();
   }
}

function DemoPlayer::onMoveStuck(%this,%obj)
{
   //echo( %obj @ " onMoveStuck" );
}

function DemoPlayer::onTargetExitLOS(%this,%obj)
{
   //echo( %obj @ " onTargetExitLOS" );
}

function DemoPlayer::onTargetEnterLOS(%this,%obj)
{
   //echo( %obj @ " onTargetEnterLOS" );
}

function DemoPlayer::onEndOfPath(%this,%obj,%path)
{
   %obj.nextTask();
}

function DemoPlayer::onEndSequence(%this,%obj,%slot)
{
   echo("Sequence Done!");
   %obj.stopThread(%slot);
   %obj.nextTask();
}

//-----------------------------------------------------------------------------
//-----------------------------------------------------------------------------

function FrmStandardCat::onReachDestination(%this,%obj)
{
   //echo( %obj @ " onReachDestination" );

   // Moves to the next node on the path.
   // Override for all player.  Normally we'd override this for only
   // a specific player datablock or class of players.
   if (%obj.path !$= "")
   {
      if (%obj.currentNode == %obj.targetNode)
         %this.onEndOfPath(%obj,%obj.path);
      else
         %obj.moveToNextNode();
   }
}

function FrmStandardCat::onMoveStuck(%this,%obj)
{
   //echo( %obj @ " onMoveStuck" );
}

function FrmStandardCat::onTargetExitLOS(%this,%obj)
{
   //echo( %obj @ " onTargetExitLOS" );
}

function FrmStandardCat::onTargetEnterLOS(%this,%obj)
{
   //echo( %obj @ " onTargetEnterLOS" );
}

function FrmStandardCat::onEndOfPath(%this,%obj,%path)
{
   %obj.nextTask();
}

function FrmStandardCat::onEndSequence(%this,%obj,%slot)
{
   echo("Sequence Done!");
   %obj.stopThread(%slot);
   %obj.nextTask();
}

//-----------------------------------------------------------------------------
// AIPlayer methods
//-----------------------------------------------------------------------------

function AIPlayer::followPath(%this,%path,%node)
{
   // Start the player following a path
   if (!isObject(%path))
   {
      %this.path = "";
      return;
   }
   
   if (%node > %path.getCount() - 1)
      %this.targetNode = %path.getCount() - 1;
   else
      %this.targetNode = %node;

   if (%this.path $= %path)
      %this.moveToNode(%this.currentNode);
   else
   {
      %this.path = %path;
      %this.moveToNode(0);
   }
}

function AIPlayer::moveToNextNode(%this)
{
   if (%this.targetNode < 0 || %this.currentNode < %this.targetNode)
   {
      if (%this.currentNode < %this.path.getCount() - 1)
         %this.moveToNode(%this.currentNode + 1);
      else
         %this.moveToNode(0);
   }
   else
      if (%this.currentNode == 0)
         %this.moveToNode(%this.path.getCount() - 1);
      else
         %this.moveToNode(%this.currentNode - 1);
}

function AIPlayer::moveToNode(%this,%index)
{
   //echo("AIPlayer::moveToNode()");
   //echo(%index);
   
   // Move to the given path node index
   %this.currentNode = %index;
   %node = %this.path.getObject(%index);
   %this.setMoveDestination(%node.getTransform());
}

//-----------------------------------------------------------------------------
//
//-----------------------------------------------------------------------------

function AIPlayer::pushTask(%this,%method)
{
   if (%this.taskIndex $= "")
   {
      %this.taskIndex = 0;
      %this.taskCurrent = -1;
   }
   %this.task[%this.taskIndex] = %method;
   %this.taskIndex++;
   if (%this.taskCurrent == -1)
      %this.executeTask(%this.taskIndex - 1);
}

function AIPlayer::clearTasks(%this)
{
   %this.taskIndex = 0;
   %this.taskCurrent = -1;
}

function AIPlayer::nextTask(%this)
{
   if (%this.taskCurrent != -1)
      if (%this.taskCurrent < %this.taskIndex - 1)
         %this.executeTask(%this.taskCurrent++);
      else
         %this.taskCurrent = -1;
}

function AIPlayer::executeTask(%this,%index)
{
   %this.taskCurrent = %index;
   eval(%this.getId() @"."@ %this.task[%index] @";");
}

//-----------------------------------------------------------------------------

function AIPlayer::singleShot(%this)
{
   // The shooting delay is used to pulse the trigger
   %this.trigger = %this.schedule(%this.shootingDelay, singleShot);
   if(%this.getDamageState() !$= "Enabled")
      return;
   %this.setImageTrigger(0, true);
   %this.setImageTrigger(0, false);
}

//-----------------------------------------------------------------------------

function AIPlayer::wait(%this, %time)
{
   %this.schedule(%time * 1000, "nextTask");
}

function AIPlayer::done(%this,%time)
{
   %this.schedule(0, "delete");
}

function AIPlayer::fire(%this,%bool)
{
   if (%bool)
   {
      cancel(%this.trigger);
      %this.singleShot();
   }
   else
      cancel(%this.trigger);
   %this.nextTask();
}

function AIPlayer::aimAt(%this,%object)
{
   //echo("Aim: "@ %object);
   %this.setAimObject(%object);
   %this.nextTask();
}

function AIPlayer::animate(%this,%seq)
{
   //%this.stopThread(0);
   //%this.playThread(0,%seq);
   %this.setActionThread(%seq);
}

// ----------------------------------------------------------------------------
// Some handy getDistance/nearestTarget functions for the AI to use
// ----------------------------------------------------------------------------

function AIPlayer::getTargetDistance(%this, %target)
{
   echo("\c4AIPlayer::getTargetDistance("@ %this @", "@ %target @")");
   $tgt = %target;
   %tgtPos = %target.getPosition();
   %eyePoint = %this.getWorldBoxCenter();
   %distance = VectorDist(%tgtPos, %eyePoint);
   echo("Distance to target = "@ %distance);
   return %distance;
}

function AIPlayer::getNearestPlayerTarget(%this)
{
   echo("\c4AIPlayer::getNearestPlayerTarget("@ %this @")");

   %index = -1;
   %botPos = %this.getPosition();
   %count = ClientGroup.getCount();
   for(%i = 0; %i < %count; %i++)
   {
      %client = ClientGroup.getObject(%i);
      if (%client.player $= "" || %client.player == 0)
         return -1;
      %playerPos = %client.player.getPosition();

      %tempDist = VectorDist(%playerPos, %botPos);
      if (%i == 0)
      {
         %dist = %tempDist;
         %index = %i;
      }
      else
      {
         if (%dist > %tempDist)
         {
            %dist = %tempDist;
            %index = %i;
         }
      }
   }
   return %index;
}

function AIPlayer::getNearestTarget(%this)
{
	%target = 0;

	%position = %this.getPosition();
	%radius = 500;

   %typeMask = $TypeMasks::PlayerObjectType | $TypeMasks::VehicleObjectType;
	InitContainerRadiusSearch(%position, %radius, %typeMask);
	while ((%targetObject = containerSearchNext()) != 0)
	{
		//if(%targetObject.teamId > 0
		//&& %targetObject.getDamageState() $= "Enabled"
		//&& %targetObject.teamId != %this.teamId )
      if(%targetObject == %this)
         continue;
         
  		if(%targetObject.getTeamId() == %this.getTeamId())
         continue;
  
  		if(%targetObject.getDamageState() !$= "Enabled")
         continue;

      %target = %targetObject;
		break;
	}
 
   return %target;
}

//-----------------------------------------------------------------------------

function AIPlayer::think(%player)
{
   // Thinking allows us to consider other things...
   %player.schedule(500, think);
   
   if(%player.getDamageState() !$= "Enabled")
      return;

   %targ = %player.getNearestTarget();
   if(isObject(%targ))
   {
      %player.aimAt(%targ);
   }
}

function AIPlayer::spawn(%teamId, %weapon)
{
   if(%teamId $= "")
      %teamId = 2;

   if(%weapon $= "")
      %weapon = 1;

   if(!isObject($aiPlayerPseudoClient))
   {
      $aiPlayerPseudoClient = new ScriptObject();
      MissionCleanup.add($aiPlayerPseudoClient);
   }
   
   $aiPlayerPseudoClient.zActiveLoadout = %weapon;
   $aiPlayerPseudoClient.team = Game.team[%teamId];

   // Create the demo player object
   %player = new AiPlayer()
   {
      dataBlock = FrmStandardCat;
      client = $aiPlayerPseudoClient;
      path = "";
   };
   MissionCleanup.add(%player);
   
   %spawnGroups = $Game::DefaultPlayerSpawnGroups;
   %spawnGroups = "Team"@%teamId@"SpawnPoints" SPC %spawnGroups;
   
   %spawnPoint = pickPlayerSpawnPoint(%spawnGroups);
   %player.setTransform(%spawnPoint.getPosition());
   //%player.setTargetingMask($TargetingMask::Launcher);
   $aiPlayer = %player;
   
   if (isObject(%player))
   {
      Game.loadOut(%player);
      
      %player.setInventory(WpnSMG3, 1);
      %player.setInventory(WpnSMG4, 1);
      %player.setInventory(WpnSG3, 1);
      %player.setInventory(WpnSG2, 1);
      %player.setInventory(WpnMG2, 1);
      %player.setInventory(WpnMGL2, 1);
      %player.setInventory(WpnSR2, 1);
      %player.setInventory(WpnRFL1, 1);
      
      %player.shootingDelay = 100;
      //%player.followPath(%path, -1);
      %player.setMoveSpeed(1.0);
      %player.think();
      
      if(%weapon == 1)
      {
         %player.mountImage(WpnSMG3Image, 0);
         %player.setImageTrigger(0, true);
      }
      else if(%weapon == 2)
      {
         %player.mountImage(WpnSG3Image, 0);
         %player.singleShot();
      }
      else if(%weapon == 3)
      {
         %player.mountImage(WpnMGL2Image, 0);
         %player.singleShot();
      }
      else if(%weapon == 4)
      {
         %player.mountImage(WpnSG2Image, 0);
         %player.singleShot();
      }
      else if(%weapon == 5)
      {
         %player.mountImage(WpnSR2Image, 0);
         %player.singleShot();
      }
      else if(%weapon == 6)
      {
         %player.mountImage(WpnMG2Image, 0);
         %player.setImageTrigger(0, true);
      }
      else if(%weapon == 7)
      {
         %player.mountImage(WpnSMG4Image, 0);
         %player.singleShot();
      }
      else if(%weapon == 8)
      {
         %player.mountImage(WpnRFL1Image, 0);
         %player.singleShot();
      }

      return %player;
   }
   else
      return 0;
}
