// Copyright information can be found in the file named COPYING
// located in the root directory of this distribution.

function StartServerGui_VITC::updateArgs(%this, %selected)
{
   if(%selected $= "casual")
   {
      %this-->as.setValue(0);
      %this-->am.setValue(0);
      %this-->vamp.setValue(0);
   }
   else if(%selected $= "advanced")
   {
      %this-->as.setValue(1);
      %this-->am.setValue(1);
      %this-->vamp.setValue(1);
   }

   %args = "";
   
   %mode = "eth";
   %map = strlwr(%this->LevelName.text);
   
   if(getSubStr(%map, 0, 2) $= "dm")
      %mode = "dm";
   else if(getSubStr(%map, 0, 3) $= "eth")
      %mode = "eth";
   else if(getSubStr(%map, 0, 2) $= "te")
      %mode = "te";
   else if(getSubStr(%map, 0, 3) $= "ctf")
      %mode = "ctf";
      
   %args = "-mode" SPC %mode SPC
           "-map" SPC %map;
           
   if(!$pref::HostMultiPlayer)
      %args = "-sp" SPC %args;
      
   if(%this-->as.getValue() == false
   && %this-->am.getValue() == false
   && %this-->vamp.getValue() == false)
   {
      if(%selected $= "")
      {
         %this-->typeCasual.setValue(1);
         %this-->typeAdvanced.setValue(0);
         %this-->typeVariant.setValue(0);
      }
   }
   else if(%this-->as.getValue() == true
   && %this-->am.getValue() == true
   && %this-->vamp.getValue() == true)
   {
      if(%selected $= "")
      {
         %this-->typeCasual.setValue(0);
         %this-->typeAdvanced.setValue(1);
         %this-->typeVariant.setValue(0);
      }
      %args = %args SPC "-advanced";
   }
   else
   {
      if(%selected $= "")
      {
         %this-->typeCasual.setValue(0);
         %this-->typeAdvanced.setValue(0);
         %this-->typeVariant.setValue(1);
      }
      if(%this-->as.getValue())
         %args = %args SPC "-as";
      if(%this-->am.getValue())
         %args = %args SPC "-am";
      if(%this-->vamp.getValue())
         %args = %args SPC "-vamp";
   }
      
   shellSetStartServerArgs("vitc", %args);
}

//----------------------------------------
function StartServerGui_VITC::onWake( %this )
{
   if($InGuiEditor)
      return;

   StartServerGui_VITC_levelList.clear();
   StartServerGui_VITC->SmallPreviews.clear();
   
   $Server::MissionFileSpec = "content/vitc/mis/*.mis";
   
   %i = 0;
   for(%file = findFirstFile($Server::MissionFileSpec); %file !$= ""; %file = findNextFile($Server::MissionFileSpec))
   {
      // Skip our new level/mission if we arent choosing a level
      // to launch in the editor.
      if ( !%this.launchInEditor )
      {
         if (strstr(%file, "newMission.mis") > -1)
            continue;      
         if (strstr(%file, "newLevel.mis") > -1)
            continue;
      }
      
      %this.addMissionFile( %file );
   }
   
   // Also add the new level mission as defined in the world editor settings
   // if we are choosing a level to launch in the editor.
   if ( %this.launchInEditor )
   {
      %file = EditorSettings.value( "WorldEditor/newLevelFile" );
      if ( %file !$= "" )
         %this.addMissionFile( %file );
   }

   // Sort our list
   StartServerGui_VITC_levelList.sort(0);

   // Set the first row as the selected row
   StartServerGui_VITC_levelList.setSelectedRow(0);

   for (%i = 0; %i < StartServerGui_VITC_levelList.rowCount(); %i++)
   {
      %preview = new GuiBitmapButtonCtrl() {
         internalName = "SmallPreview" @ %i;
         Extent = "58 40";
         bitmap = "shell/graphics/no-preview";
         command = "StartServerGui_VITC.previewSelected(StartServerGui_VITC->SmallPreviews->SmallPreview" @ %i @ ");";
      };

      StartServerGui_VITC->SmallPreviews.add(%preview);

      // Set this small preview visible
      if (%i >= 10)
         %preview.setVisible(false);

      // Set the level index
      %preview.levelIndex = %i;

      // Get the name
      %name = getField(StartServerGui_VITC_levelList.getRowText(%i), 0);

      %preview.levelName = %name;

      %file = getField(StartServerGui_VITC_levelList.getRowText(%i), 1);

      // Find the preview image
      %levelPreview = filePath(%file) @ "/" @ fileBase(%file) @ "_preview";

      // Test against all of the different image formats
      // This should probably be moved into an engine function
      if (isFile(%levelPreview @ ".png") ||
          isFile(%levelPreview @ ".jpg") ||
          isFile(%levelPreview @ ".bmp") ||
          isFile(%levelPreview @ ".gif") ||
          isFile(%levelPreview @ ".jng") ||
          isFile(%levelPreview @ ".mng") ||
          isFile(%levelPreview @ ".tga"))
      {
         %preview.setBitmap(%levelPreview);
      }

      // Get the description
      %desc = getField(StartServerGui_VITC_levelList.getRowText(%i), 2);

      %preview.levelDesc = %desc;
   }

   StartServerGui_VITC->SmallPreviews.firstVisible = -1;
   StartServerGui_VITC->SmallPreviews.lastVisible = -1;

   if (StartServerGui_VITC->SmallPreviews.getCount() > 0)
   {
      StartServerGui_VITC->SmallPreviews.firstVisible = 0;

      if (StartServerGui_VITC->SmallPreviews.getCount() <= 10)
         StartServerGui_VITC->SmallPreviews.lastVisible = StartServerGui_VITC->SmallPreviews.getCount() - 1;
      else
         StartServerGui_VITC->SmallPreviews.lastVisible = 9;
   }

   if (StartServerGui_VITC->SmallPreviews.getCount() > 0)
      StartServerGui_VITC.previewSelected(StartServerGui_VITC->SmallPreviews.getObject(0));

   // If we have 10 or less previews then hide our next/previous buttons
   // and resize to fill their positions
   if (StartServerGui_VITC->SmallPreviews.getCount() <= 10)
   {
      StartServerGui_VITC->PreviousSmallPreviews.setVisible(false);
      StartServerGui_VITC->NextSmallPreviews.setVisible(false);

      %previewPos = StartServerGui_VITC->SmallPreviews.getPosition();
      %previousPos = StartServerGui_VITC->PreviousSmallPreviews.getPosition();

      %previewPosX = getWord(%previousPos, 0);
      %previewPosY = getWord(%previewPos,  1);

      StartServerGui_VITC->SmallPreviews.setPosition(%previewPosX, %previewPosY);

      StartServerGui_VITC->SmallPreviews.colSpacing = 10;//((getWord(NextSmallPreviews.getPosition(), 0)+11)-getWord(PreviousSmallPreviews.getPosition(), 0))/4;
      StartServerGui_VITC->SmallPreviews.refresh();
   }

   if (StartServerGui_VITC->SmallPreviews.getCount() <= 1)
   {
      // Hide the small previews
      StartServerGui_VITC->SmallPreviews.setVisible(false);
   }
   else
   {
      // Make sure the small previews are visible
      StartServerGui_VITC->SmallPreviews.setVisible(true);
   }
   
   %this.updateArgs();
}

function StartServerGui_VITC::addMissionFile( %this, %file )
{
   %levelName = fileBase(%file);
   %levelDesc = "A Torque level";

   %LevelInfoObject = getLevelInfo(%file);

   if (%LevelInfoObject != 0)
   {
      if(%LevelInfoObject.levelName !$= "")
         %levelName = %LevelInfoObject.levelName;
      else if(%LevelInfoObject.name !$= "")
         %levelName = %LevelInfoObject.name;

      if (%LevelInfoObject.desc0 !$= "")
         %levelDesc = %LevelInfoObject.desc0;
         
      %LevelInfoObject.delete();
   }

   StartServerGui_VITC_levelList.addRow( StartServerGui_VITC_levelList.rowCount(), %levelName TAB %file TAB %levelDesc );
}

function StartServerGui_VITC::onSleep( %this )
{
   // This is set from the outside, only stays true for a single wake/sleep
   // cycle.
   %this.launchInEditor = false;
}

function StartServerGui_VITC::previewSelected(%this, %preview)
{
   // Set the selected level
   if (isObject(%preview) && %preview.levelIndex !$= "")
      StartServerGui_VITC_levelList.setSelectedRow(%preview.levelIndex);
   else
      StartServerGui_VITC_levelList.setSelectedRow(-1);

   // Set the large preview image
   if (isObject(%preview) && %preview.bitmap !$= "")
      %this->CurrentPreview.setBitmap(%preview.bitmap);
   else
      %this->CurrentPreview.setBitmap("notc/client/graphics/no-preview");

   // Set the current level name
   if (isObject(%preview) && %preview.levelName !$= "")
      %this->LevelName.setText(%preview.levelName);
   else
      %this->LevelName.setText("Level");

   // Set the current level description
   if (isObject(%preview) && %preview.levelDesc !$= "")
      %this->LevelDescription.setText(%preview.levelDesc);
   else
      %this->LevelDescription.setText("A Torque Level");
      
   %this.updateArgs();
}

function StartServerGui_VITC::previousPreviews(%this)
{
   %prevHiddenIdx = %this->SmallPreviews.firstVisible - 1;

   if (%prevHiddenIdx < 0)
      return;

   %lastVisibleIdx = %this->SmallPreviews.lastVisible;

   if (%lastVisibleIdx >= %this->SmallPreviews.getCount())
      return;

   %prevHiddenObj  = %this->SmallPreviews.getObject(%prevHiddenIdx);
   %lastVisibleObj = %this->SmallPreviews.getObject(%lastVisibleIdx);

   if (isObject(%prevHiddenObj) && isObject(%lastVisibleObj))
   {
      %this->SmallPreviews.firstVisible--;
      %this->SmallPreviews.lastVisible--;

      %prevHiddenObj.setVisible(true);
      %lastVisibleObj.setVisible(false);
   }
}

function StartServerGui_VITC::nextPreviews(%this)
{
   %firstVisibleIdx = %this->SmallPreviews.firstVisible;

   if (%firstVisibleIdx < 0)
      return;

   %firstHiddenIdx = %this->SmallPreviews.lastVisible + 1;

   if (%firstHiddenIdx >= %this->SmallPreviews.getCount())
      return;

   %firstVisibleObj = %this->SmallPreviews.getObject(%firstVisibleIdx);
   %firstHiddenObj  = %this->SmallPreviews.getObject(%firstHiddenIdx);

   if (isObject(%firstVisibleObj) && isObject(%firstHiddenObj))
   {
      %this->SmallPreviews.firstVisible++;
      %this->SmallPreviews.lastVisible++;

      %firstVisibleObj.setVisible(false);
      %firstHiddenObj.setVisible(true);
   }
}

//----------------------------------------
function getLevelInfo( %missionFile )
{
   %file = new FileObject();
   
   %LevelInfoObject = "";
   
   if ( %file.openForRead( %missionFile ) ) {
		%inInfoBlock = false;
		
		while ( !%file.isEOF() ) {
			%line = %file.readLine();
			%line = trim( %line );
			
			if( %line $= "new ScriptObject(LevelInfo) {" )
				%inInfoBlock = true;
         else if( %line $= "new LevelInfo(theLevelInfo) {" )
				%inInfoBlock = true;
			else if( %inInfoBlock && %line $= "};" ) {
				%inInfoBlock = false;
				%LevelInfoObject = %LevelInfoObject @ %line; 
				break;
			}
			
			if( %inInfoBlock )
			   %LevelInfoObject = %LevelInfoObject @ %line @ " "; 	
		}
		
		%file.close();
	}
   %file.delete();

	if( %LevelInfoObject !$= "" )
	{
	   %LevelInfoObject = "%LevelInfoObject = " @ %LevelInfoObject;
	   eval( %LevelInfoObject );

      return %LevelInfoObject;
	}
	
	// Didn't find our LevelInfo
   return 0; 
}
