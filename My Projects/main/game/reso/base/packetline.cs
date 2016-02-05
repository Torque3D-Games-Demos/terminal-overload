// Copyright information can be found in the file named COPYING
// located in the root directory of this distribution.

//------------------------------------------------------------------------------
// Packet Line Camera

datablock PathCameraData(PacketLineCamera)
{
   shapeFile = "content/xa/notc/core/shapes/packet/p1/shape.dae";
   cameraDefaultFov = "120";
   cameraMinFov = "120";
   cameraMaxFov = "120";
   cameraMaxDist = "2.21079";
   useEyePoint = "1";
};

function PacketLineCamera::onNode(%this, %obj, %node)
{
   //echo("PacketLineCamera::onNode()");
   //echo("node:" SPC %node);
   //echo("num nodes:" SPC %obj.zNumNodes);
   if(%node == %obj.zNumNodes-1)
   {
      %obj.startFade(0, 0, true);
      %player = %obj.client.player;
      %player.setTransform(%obj.zOutPos);
      %player.setVelocity("0 0 0");
      %obj.client.control(%player);
   }
}

function PacketLineCamera::addLine(%this, %camera, %line)
{
   //echo("PacketLineCamera::addLine()");
   //echo("line:" SPC %line);

   %subLines = new SimSet();
   %speed = 200;
   
   %numNodes = %line.getCount();
   //echo(%numNodes SPC "nodes");
   for(%i = 0; %i < %numNodes; %i++)
   {
      %currentNode = %line.getObject(%i);
      
      if(%currentNode.isMethod("getCount")) 
      {
         if(%currentNode.getCount() > 0)
            %subLines.add(%currentNode);
         continue;
      }
      
      if(%i < %numNodes-1)
         %nextNode = %line.getObject(%i+1);
      %transform = %currentNode.getTransform();
      //echo("---------------");
      //echo(%transform);
      if(%currentNode.speed !$= "")
      {
         %speed = %currentNode.speed;
      }
      else if(%nextNode !$= "" && %currentNode.msToNext != 0)
      {
         %pos1 = %currentNode.getPosition();
         %pos2 = %nextNode.getPosition();
         %dist = VectorLen(VectorSub(%pos2, %pos1));
         %time = %currentNode.msToNext;
         %speed = %dist / (%time/1000);
         //echo("pos1:" SPC %pos1);
         //echo("pos2:" SPC %pos2);
         //echo("dist:" SPC %dist);
         //echo("time:" SPC %time);
      }
      //echo("speed:" SPC %speed);
      if((%type = %currentNode.type) $= "")
         %type = "Kink";
      if((%path = %currentNode.smoothingType) $= "")
         %path = "Linear";
      %camera.pushBack(%transform, %speed, %type, %path);
      %camera.zOutPos = %transform;
      %camera.zNumNodes++;
   }
   
   %numSubLines = %subLines.getCount();
   //echo("numSubLines:" SPC %numSubLines);
   if(%numSubLines > 0)
   {
      %idx = 0;
      if(%line.zLastUsedSubLineIndex !$= "")
         %idx = %line.zLastUsedSubLineIndex+1;
      if(%idx >= %numSubLines)
         %idx = 0;
      //echo("subLine index:" SPC %idx);
      %subLine = %subLines.getObject(%idx);
      %line.zLastUsedSubLineIndex = %idx;
      if(isObject(%subLine))
         %this.addLine(%camera, %subLine);
   }
   else
   {
      // Add additional terminal node to avoid slowing down at the end.
      %camera.pushBack(%camera.zOutPos, 0, "Normal", "Linear");
      %camera.zNumNodes++;
   }

   %subLines.delete();
}

//------------------------------------------------------------------------------
// Packet Line In Marker

datablock StaticShapeData(PacketLineIn)
{
   category = "PacketLine";
   shapeFile = "core/art/shapes/octahedron.dts";
};

function PacketLineIn::create(%this)
{
   %obj = new StaticMetaShape()
   {
      dataBlock = %this;
      lines = " ";
   };
   return %obj;
}

function PacketLineIn::onAdd(%this, %obj)
{
   //echo("PacketLineIn::onAdd()");
   %obj.zLastUpdatePosition = "";
   %obj.zParticleEmitter = "";
   %obj.zActivationTrigger = "";
   %obj.zProximityTrigger = "";
   %this.updateAssetsThread(%obj);
}

function PacketLineIn::onRemove(%this, %obj)
{
   //echo("PacketLineIn::onRemove()");
   %this.removeAssets(%obj);
}

function PacketLineIn::removeAssets(%this, %obj)
{
   //echo("PacketLineIn::removeAssets()");

   if(isObject(%obj.zParticleEmitter))
      %obj.zParticleEmitter.delete();
      
   if(isObject(%obj.zActivationTrigger))
      %obj.zActivationTrigger.delete();
      
   if(isObject(%obj.zProximityTrigger))
      %obj.zProximityTrigger.delete();
}

function PacketLineIn::updateAssetsThread(%this, %obj)
{
   //echo("PacketLineIn::updateAssetsThread()");
   
   if(!isObject(%obj))
      return;
      
   if(%obj.zUpdateAssetsThread !$= "")
   {
      cancel(%obj.zUpdateAssetsThread);
      %obj.zUpdateAssetsThread = "";
   }
      
   if(%obj.getPosition() !$= %obj.zLastUpdatePosition)
   {
      %this.removeAssets(%obj);
   
      // Set up particle emitter.
      %obj.zParticleEmitter = new ParticleEmitterNode()
      {
         datablock = PacketLineEntryEmitterNode;
         rotation = "0 0 1 0";
         emitter = PacketLineEntryEmitter;
         position = %obj.getWorldBoxCenter();
         scale = "1 1 1";
         velocity = 1;
         locked = true;
         zLineIn = %obj;
      };
      
      // Set up activation trigger.
      %obj.zActivationTrigger = new Trigger()
      {
         polyhedron = "-0.5 0.5 0.0 1.0 0.0 0.0 0.0 -1.0 0.0 0.0 0.0 1.0";
         dataBlock = PacketLineActivationTrigger;
         position = VectorAdd(%obj.getWorldBoxCenter(), "0 0 -1");
         scale = "2 2 2";
         locked = true;
         zLineIn = %obj;
      };
      
      // Set up proximity trigger.
      %obj.zProximityTrigger = new Trigger()
      {
         polyhedron = "-0.5 0.5 0.0 1.0 0.0 0.0 0.0 -1.0 0.0 0.0 0.0 1.0";
         dataBlock = PacketLineProximityTrigger;
         position = VectorAdd(%obj.getWorldBoxCenter(), "0 0 -10");
         scale = "20 20 20";
         locked = true;
         zLineIn = %obj;
      };
   }

   %obj.zLastUpdatePosition = %obj.getPosition();

   %obj.zUpdateAssetsThread = %this.schedule(1000, "updateAssetsThread", %obj);
}

function PacketLineIn::prepareCamera(%this, %obj, %camera)
{
   //echo("PacketLineIn::prepareCamera()");

   %camera.zNumNodes = 0;
   $cam = %camera;

   %camera.reset(0);

   %line = "";
   %numLines = getWordCount(%obj.lines);
   if(%numLines == 0)
      return;

   %line = getWord(%obj.lines, getRandom(0, %numLines-1));
   if(!isObject(%line))
      return;
      
   %camera.getDataBlock().addLine(%camera, %line);
   %camera.popFront();

   %camera.startFade(0, 0, true);
   %camera.setPosition(0.0);
   %camera.setState("stop");

   %camera.zPreparedLine = %line;
}

//------------------------------------------------------------------------------
// Packet Line Activation Trigger

datablock TriggerData(PacketLineActivationTrigger)
{
   category = "PacketLine";
   tickPeriodMS = 256;
   clientSide = false;
};

function PacketLineActivationTrigger::onEnterTrigger(%this, %obj, %enter)
{
   //echo("PacketLineActivationTrigger::onEnterTrigger()");
   
   if(getWordCount(%obj.zLineIn.lines) == 0)
   {
      error("PacketLineActivationTrigger" SPC %obj.getId() SPC "no lines!");
      return;
   }
   
   %client = %enter.client;
   if(!isObject(%client))
      return;
      
   if(%enter != %client.player)
      return;
      
   %camera = %client.packetLineCamera;
   if(!isObject(%camera))
      return;
      
   if(strstr(%obj.lines, %camera.zPreparedLine) == 0)
      %obj.zLineIn.getDataBlock().prepareCamera(%obj.zLineIn, %camera);

   %camera.startFade(0, 0, false);
   %camera.setPosition(0.0);
   %camera.setState("forward");
   //%camera.setTarget(1.0);
   
   %client.setControlObject(%camera);

   %player = %client.player;
   %player.setTransform("99999 99999 99999"); // yuck ;)
   %player.setVelocity("0 0 0");
}

//------------------------------------------------------------------------------
// Packet Line Proximity Trigger

datablock TriggerData(PacketLineProximityTrigger)
{
   category = "PacketLine";
   tickPeriodMS = 256;
   clientSide = false;
};

function PacketLineProximityTrigger::onEnterTrigger(%this, %obj, %enter)
{
   //echo("PacketLineProximityTrigger::onEnterTrigger()");
   
   if(!isObject(%obj.zLineIn))
   {
      error("PacketLineProximityTrigger" SPC %obj.getId() SPC "no line in!");
      return;
   }
   
   %client = %enter.client;
   if(!isObject(%client))
      return;
      
   if(%enter != %client.player)
      return;
      
   %camera = %client.packetLineCamera;
   if(!isObject(%camera))
      return;
      
   %obj.zLineIn.getDataBlock().prepareCamera(%obj.zLineIn, %camera);
}
