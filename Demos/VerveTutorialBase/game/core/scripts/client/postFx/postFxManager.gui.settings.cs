//-----------------------------------------------------------------------------
// Torque3D - postFXManager.gui.settings.cs - Contains the code for the 
//            application of settings to the controls, and vice versa.
//
//
// Copyright (C) Luma Arcade 2010
// Written By Sven Bergstrom | Luma Arcade
//-----------------------------------------------------------------------------

$PostFXManager::defaultPreset  = "core/scripts/client/postFx/default.postfxpreset.cs";

function PostFXManager::settingsSetEnabled(%this, %bEnablePostFX)
{
   $PostFXManager::PostFX::Enabled = %bEnablePostFX;
   
   //if to enable the postFX, apply the ones that are enabled
   if ( %bEnablePostFX )
   {
      //SSAO, HDR, LightRays, DOF, Sharpness, NV
      
      if ( $PostFXManager::PostFX::EnableSSAO )      
         SSAOPostFx.enable();      
      else
         SSAOPostFx.disable();
      
      if ( $PostFXManager::PostFX::EnableHDR )
         HDRPostFX.enable();
      else
         HDRPostFX.disable();

      if ( $PostFXManager::PostFX::EnableLightRays )
         LightRayPostFX.enable();
      else
         LightRayPostFX.disable();
      
      if ( $PostFXManager::PostFX::EnableDOF )
         DOFPostEffect.enable();
      else
         DOFPostEffect.disable();

      //if ( $PostFXManager::PostFX::EnableSharpness )
         //SharpnessPostFX.enable();
      //else
         //SharpnessPostFX.disable();

      //if ( $PostFXManager::PostFX::EnableNightVision )
         //NightVisionPostFX.enable();
      //else
         //NightVisionPostFX.disable();
      
      postVerbose("% - PostFX Manager - PostFX enabled");      
   }
   else
   {
      //Disable all postFX
      
      SSAOPostFx.disable();
      HDRPostFX.disable();
      LightRayPostFX.disable();
      DOFPostEffect.disable();
      //SharpnessPostFX.disable();
      //NightVisionPostFX.disable();
      
      postVerbose("% - PostFX Manager - PostFX disabled");
   }
}

function PostFXManager::settingsEffectSetEnabled(%this, %sName, %bEnable)
{
   %postEffect = 0;
   
   //Determine the postFX to enable, and apply the boolean
   if(%sName $= "SSAO")
   {
      %postEffect = SSAOPostFx;
      $PostFXManager::PostFX::EnableSSAO = %bEnable;
      //$pref::PostFX::SSAO::Enabled = %bEnable;
   }
   else if(%sName $= "HDR")
   {
      %postEffect = HDRPostFX;
      $PostFXManager::PostFX::EnableHDR = %bEnable;
      //$pref::PostFX::HDR::Enabled = %bEnable;
   }
   else if(%sName $= "LightRays")
   {
      %postEffect = LightRayPostFX;
      $PostFXManager::PostFX::EnableLightRays = %bEnable;
      //$pref::PostFX::LightRays::Enabled = %bEnable;      
   }
   else if(%sName $= "DOF")
   {
      %postEffect = DOFPostEffect;
      $PostFXManager::PostFX::EnableDOF = %bEnable;
      //$pref::PostFX::DOF::Enabled = %bEnable;
   }
   else if(%sName $= "Sharp")
   {
      %postEffect = SharpnessPostFX;
      $PostFXManager::PostFX::EnableSharpness = %bEnable;
      //$pref::PostFX::Sharpness::Enabled = %bEnable;
   }
   else if(%sName $= "NV")
   {
      %postEffect = NightVisionPostFX;
      //$PostFXManager::NightVision::EnableNightVision = %bEnable;
   }
   
   // Apply the change
   if ( %bEnable == true )
   {
      %postEffect.enable();
      postVerbose("% - PostFX Manager - " @ %sName @ " enabled");
   }
   else
   {
      %postEffect.disable();
      postVerbose("% - PostFX Manager - " @ %sName @ " disabled");
   }
}

function PostFXManager::settingsRefreshSSAO(%this)
{
   //Apply the enabled flag 
   ppOptionsEnableSSAO.setValue($PostFXManager::PostFX::EnableSSAO);   
   
   //Add the items we need to display
   ppOptionsSSAOQuality.clear();
   ppOptionsSSAOQuality.add("Low", 0);
   ppOptionsSSAOQuality.add("Medium", 1);
   ppOptionsSSAOQuality.add("High", 2);
   
   //Set the selected, after adding the items!
   ppOptionsSSAOQuality.setSelected($SSAOPostFx::quality);
   
   //SSAO - Set the values of the sliders, General Tab
   ppOptionsSSAOOverallStrength.setValue($SSAOPostFx::overallStrength);
   ppOptionsSSAOBlurDepth.setValue($SSAOPostFx::blurDepthTol);
   ppOptionsSSAOBlurNormal.setValue($SSAOPostFx::blurNormalTol);
      
   //SSAO - Set the values for the near tab
   ppOptionsSSAONearDepthMax.setValue($SSAOPostFx::sDepthMax);
   ppOptionsSSAONearDepthMin.setValue($SSAOPostFx::sDepthMin);
   ppOptionsSSAONearRadius.setValue($SSAOPostFx::sRadius);
   ppOptionsSSAONearStrength.setValue($SSAOPostFx::sStrength);
   ppOptionsSSAONearToleranceNormal.setValue($SSAOPostFx::sNormalTol);
   ppOptionsSSAONearTolerancePower.setValue($SSAOPostFx::sNormalPow);
   
   //SSAO - Set the values for the far tab
   ppOptionsSSAOFarDepthMax.setValue($SSAOPostFx::lDepthMax);
   ppOptionsSSAOFarDepthMin.setValue($SSAOPostFx::lDepthMin);
   ppOptionsSSAOFarRadius.setValue($SSAOPostFx::lRadius);
   ppOptionsSSAOFarStrength.setValue($SSAOPostFx::lStrength);
   ppOptionsSSAOFarToleranceNormal.setValue($SSAOPostFx::lNormalTol);
   ppOptionsSSAOFarTolerancePower.setValue($SSAOPostFx::lNormalPow);
}

function PostFXManager::settingsRefreshHDR(%this)
{
  //Apply the enabled flag 
   ppOptionsEnableHDR.setValue($PostFXManager::PostFX::EnableHDR);   
    
   ppOptionsHDRBloom.setValue($HDRPostFX::enableBloom);
   ppOptionsHDRBloomBlurBrightPassThreshold.setValue($HDRPostFX::brightPassThreshold);
   ppOptionsHDRBloomBlurMean.setValue($HDRPostFX::gaussMean);
   ppOptionsHDRBloomBlurMultiplier.setValue($HDRPostFX::gaussMultiplier);
   ppOptionsHDRBloomBlurStdDev.setValue($HDRPostFX::gaussStdDev);
   ppOptionsHDRBrightnessAdaptRate.setValue($HDRPostFX::adaptRate);
   ppOptionsHDREffectsBlueShift.setValue($HDRPostFX::enableBlueShift);
   ppOptionsHDREffectsBlueShiftColor.BaseColor = $HDRPostFX::blueShiftColor;
   ppOptionsHDREffectsBlueShiftColor.PickColor = $HDRPostFX::blueShiftColor;
   ppOptionsHDRKeyValue.setValue($HDRPostFX::keyValue);
   ppOptionsHDRMinLuminance.setValue($HDRPostFX::minLuminace);
   ppOptionsHDRToneMapping.setValue($HDRPostFX::enableToneMapping);
   ppOptionsHDRToneMappingAmount.setValue($HDRPostFX::enableToneMapping);
   ppOptionsHDRWhiteCutoff.setValue($HDRPostFX::whiteCutoff);
}

function PostFXManager::settingsRefreshLightrays(%this)
{
  //Apply the enabled flag 
   ppOptionsEnableLightRays.setValue($PostFXManager::PostFX::EnableLightRays);   
    
   ppOptionsLightRaysBrightScalar.setValue($LightRayPostFX::brightScalar);
}

function PostFXManager::settingsRefreshDOF(%this)
{
  //Apply the enabled flag 
   //ppOptionsEnableDOF.setValue($PostFXManager::PostFX::EnableDOF);   


   ppOptionsDOFEnableDOF.setValue($PostFXManager::PostFX::EnableDOF);
   ppOptionsDOFEnableAutoFocus.setValue($DOFPostFx::EnableAutoFocus);
   
   ppOptionsDOFFarBlurMinSlider.setValue($DOFPostFx::BlurMin);
   ppOptionsDOFFarBlurMaxSlider.setValue($DOFPostFx::BlurMax);
   
   ppOptionsDOFFocusRangeMinSlider.setValue($DOFPostFx::FocusRangeMin);
   ppOptionsDOFFocusRangeMaxSlider.setValue($DOFPostFx::FocusRangeMax);
   
   ppOptionsDOFBlurCurveNearSlider.setValue($DOFPostFx::BlurCurveNear);
   ppOptionsDOFBlurCurveFarSlider.setValue($DOFPostFx::BlurCurveFar);

}

function PostFXManager::settingsRefreshSharpness(%this)
{
  //Apply the enabled flag 
   ppOptionsEnableSharpness.setValue($PostFXManager::PostFX::EnableSharpness);   
 

   ppOptionsSharpenRange.setValue($SharpPostFx::sharpRange);
   ppOptionsSharpenStrength.setValue($SharpPostFx::sharpPower);
   ppOptionsSharpenWidth.setValue($SharpPostFx::sharpWidth);
   
}

function PostFXManager::settingsRefreshNightVision(%this)
{
   //Apply the enabled flag 
   ppOptionsEnableNV.setValue($PostFXManager::PostFX::EnableNightVision);
   
   ppOptionsNVDistFreq.setValue($NVPostFx::DistFreq);
   ppOptionsNVDistMul.setValue($NVPostFx::DistMul);
   ppOptionsNVDistRoll.setValue($NVPostFx::DistRoll);

   ppOptionsNVBright.setValue($NVPostFx::brightThreshold);
   ppOptionsNVHighMul.setValue($NVPostFx::highMultiplier);
   ppOptionsNVLowMul.setValue($NVPostFx::lowMultiplier);
 
}

function PostFXManager::settingsRefreshAll(%this)
{    
   $PostFXManager::PostFX::Enabled           = $pref::enablePostEffects;
   $PostFXManager::PostFX::EnableSSAO        = SSAOPostFx.isEnabled();
   $PostFXManager::PostFX::EnableHDR         = HDRPostFX.isEnabled();
   $PostFXManager::PostFX::EnableLightRays   = LightRayPostFX.isEnabled();
   $PostFXManager::PostFX::EnableDOF         = DOFPostEffect.isEnabled();
   $PostFXManager::PostFX::EnableSharpness   = "";
   $PostFXManager::PostFX::EnableNightVision = "";
   
   //For all the postFX here, apply the active settings in the system
   //to the gui controls.
   
   %this.settingsRefreshSSAO();
   %this.settingsRefreshHDR();
   %this.settingsRefreshLightrays();
   %this.settingsRefreshDOF();
   %this.settingsRefreshSharpness();
   %this.settingsRefreshNightVision();

   postVerbose("% - PostFX Manager - GUI values updated.");
}

function PostFXManager::settingsApplyFromPreset(%this)
{
   postVerbose("% - PostFX Manager - Applying from preset");

   //SSAO Settings
   $SSAOPostFx::blurDepthTol           = $PostFXManager::Settings::SSAO::blurDepthTol;
   $SSAOPostFx::blurNormalTol          = $PostFXManager::Settings::SSAO::blurNormalTol;
   $SSAOPostFx::lDepthMax              = $PostFXManager::Settings::SSAO::lDepthMax;
   $SSAOPostFx::lDepthMin              = $PostFXManager::Settings::SSAO::lDepthMin;
   $SSAOPostFx::lDepthPow              = $PostFXManager::Settings::SSAO::lDepthPow;
   $SSAOPostFx::lNormalPow             = $PostFXManager::Settings::SSAO::lNormalPow;
   $SSAOPostFx::lNormalTol             = $PostFXManager::Settings::SSAO::lNormalTol;
   $SSAOPostFx::lRadius                = $PostFXManager::Settings::SSAO::lRadius;
   $SSAOPostFx::lStrength              = $PostFXManager::Settings::SSAO::lStrength;
   $SSAOPostFx::overallStrength        = $PostFXManager::Settings::SSAO::overallStrength;
   $SSAOPostFx::quality                = $PostFXManager::Settings::SSAO::quality;
   $SSAOPostFx::sDepthMax              = $PostFXManager::Settings::SSAO::sDepthMax;
   $SSAOPostFx::sDepthMin              = $PostFXManager::Settings::SSAO::sDepthMin;
   $SSAOPostFx::sDepthPow              = $PostFXManager::Settings::SSAO::sDepthPow;
   $SSAOPostFx::sNormalPow             = $PostFXManager::Settings::SSAO::sNormalPow;
   $SSAOPostFx::sNormalTol             = $PostFXManager::Settings::SSAO::sNormalTol;
   $SSAOPostFx::sRadius                = $PostFXManager::Settings::SSAO::sRadius;
   $SSAOPostFx::sStrength              = $PostFXManager::Settings::SSAO::sStrength;
   
   //HDR settings
   $HDRPostFX::adaptRate               = $PostFXManager::Settings::HDR::adaptRate;
   $HDRPostFX::blueShiftColor          = $PostFXManager::Settings::HDR::blueShiftColor;
   $HDRPostFX::brightPassThreshold     = $PostFXManager::Settings::HDR::brightPassThreshold; 
   $HDRPostFX::enableBloom             = $PostFXManager::Settings::HDR::enableBloom;
   $HDRPostFX::enableBlueShift         = $PostFXManager::Settings::HDR::enableBlueShift;
   $HDRPostFX::enableToneMapping       = $PostFXManager::Settings::HDR::enableToneMapping;
   $HDRPostFX::gaussMean               = $PostFXManager::Settings::HDR::gaussMean;
   $HDRPostFX::gaussMultiplier         = $PostFXManager::Settings::HDR::gaussMultiplier;
   $HDRPostFX::gaussStdDev             = $PostFXManager::Settings::HDR::gaussStdDev;
   $HDRPostFX::keyValue                = $PostFXManager::Settings::HDR::keyValue;
   $HDRPostFX::minLuminace             = $PostFXManager::Settings::HDR::minLuminace;
   $HDRPostFX::whiteCutoff             = $PostFXManager::Settings::HDR::whiteCutoff;
   
   //Light rays settings
   $LightRayPostFX::brightScalar       = $PostFXManager::Settings::LightRays::brightScalar;
   
   //DOF settings   
   $DOFPostFx::EnableAutoFocus         = $PostFXManager::Settings::DOF::EnableAutoFocus;
   $DOFPostFx::BlurMin                 = $PostFXManager::Settings::DOF::BlurMin;
   $DOFPostFx::BlurMax                 = $PostFXManager::Settings::DOF::BlurMax;
   $DOFPostFx::FocusRangeMin           = $PostFXManager::Settings::DOF::FocusRangeMin;
   $DOFPostFx::FocusRangeMax           = $PostFXManager::Settings::DOF::FocusRangeMax;
   $DOFPostFx::BlurCurveNear           = $PostFXManager::Settings::DOF::BlurCurveNear;
   $DOFPostFx::BlurCurveFar            = $PostFXManager::Settings::DOF::BlurCurveFar;

   //Sharpen effect settings
   $SharpPostFx::sharpRange            = $PostFXManager::Settings::Sharpen::sharpRange;
   $SharpPostFx::sharpWidth            = $PostFXManager::Settings::Sharpen::sharpWidth;
   $SharpPostFx::sharpPower            = $PostFXManager::Settings::Sharpen::sharpPower;
   
   //Night vision 
   $NVPostFx::brightThreshold          = $PostFXManager::Settings::NightVision::brightThreshold;
   $NVPostFx::DistFreq                 = $PostFXManager::Settings::NightVision::DistFreq;
   $NVPostFx::DistMul                  = $PostFXManager::Settings::NightVision::DistMul;
   $NVPostFx::DistRoll                 = $PostFXManager::Settings::NightVision::DistRoll;
   $NVPostFx::lowMultiplier            = $PostFXManager::Settings::NightVision::lowMultiplier;
   $NVPostFx::highMultiplier           = $PostFXManager::Settings::NightVision::highMultiplier;
   
   if ( $PostFXManager::forceEnableFromPresets )
   {
      $PostFXManager::PostFX::Enabled           = $PostFXManager::Settings::EnablePostFX;
      $PostFXManager::PostFX::EnableNightVision = $PostFXManager::Settings::EnableNightVision;
      $PostFXManager::PostFX::EnableSharpen     = $PostFXManager::Settings::EnableSharpen;
      $PostFXManager::PostFX::EnableDOF         = $PostFXManager::Settings::EnableDOF;
      $PostFXManager::PostFX::EnableLightRays   = $PostFXManager::Settings::EnableLightRays;
      $PostFXManager::PostFX::EnableHDR         = $PostFXManager::Settings::EnableHDR;
      $PostFXManager::PostFX::EnableSSAO        = $PostFXManager::Settings::EnabledSSAO;

      %this.settingsSetEnabled( true );
   }
   
   //make sure we apply the correct settings to the DOF
   ppOptionsUpdateDOFSettings();
   
   // Update the actual GUI controls if its awake ( otherwise it will when opened ).
   if ( PostFXManager.isAwake() )
      %this.settingsRefreshAll();      
}

function PostFXManager::settingsApplySSAO(%this)
{   
   $PostFXManager::Settings::SSAO::blurDepthTol             = $SSAOPostFx::blurDepthTol;
   $PostFXManager::Settings::SSAO::blurNormalTol            = $SSAOPostFx::blurNormalTol;
   $PostFXManager::Settings::SSAO::lDepthMax                = $SSAOPostFx::lDepthMax;
   $PostFXManager::Settings::SSAO::lDepthMin                = $SSAOPostFx::lDepthMin;
   $PostFXManager::Settings::SSAO::lDepthPow                = $SSAOPostFx::lDepthPow;
   $PostFXManager::Settings::SSAO::lNormalPow               = $SSAOPostFx::lNormalPow;
   $PostFXManager::Settings::SSAO::lNormalTol               = $SSAOPostFx::lNormalTol;
   $PostFXManager::Settings::SSAO::lRadius                  = $SSAOPostFx::lRadius;
   $PostFXManager::Settings::SSAO::lStrength                = $SSAOPostFx::lStrength;
   $PostFXManager::Settings::SSAO::overallStrength          = $SSAOPostFx::overallStrength;
   $PostFXManager::Settings::SSAO::quality                  = $SSAOPostFx::quality;
   $PostFXManager::Settings::SSAO::sDepthMax                = $SSAOPostFx::sDepthMax;
   $PostFXManager::Settings::SSAO::sDepthMin                = $SSAOPostFx::sDepthMin;
   $PostFXManager::Settings::SSAO::sDepthPow                = $SSAOPostFx::sDepthPow;
   $PostFXManager::Settings::SSAO::sNormalPow               = $SSAOPostFx::sNormalPow;
   $PostFXManager::Settings::SSAO::sNormalTol               = $SSAOPostFx::sNormalTol;
   $PostFXManager::Settings::SSAO::sRadius                  = $SSAOPostFx::sRadius;
   $PostFXManager::Settings::SSAO::sStrength                = $SSAOPostFx::sStrength;

   postVerbose("% - PostFX Manager - Settings Saved - SSAO");    
   
}

function PostFXManager::settingsApplyHDR(%this)
{   
   $PostFXManager::Settings::HDR::adaptRate                 = $HDRPostFX::adaptRate;
   $PostFXManager::Settings::HDR::blueShiftColor            = $HDRPostFX::blueShiftColor;
   $PostFXManager::Settings::HDR::brightPassThreshold       = $HDRPostFX::brightPassThreshold;
   $PostFXManager::Settings::HDR::enableBloom               = $HDRPostFX::enableBloom;
   $PostFXManager::Settings::HDR::enableBlueShift           = $HDRPostFX::enableBlueShift;
   $PostFXManager::Settings::HDR::enableToneMapping         = $HDRPostFX::enableToneMapping;
   $PostFXManager::Settings::HDR::gaussMean                 = $HDRPostFX::gaussMean;
   $PostFXManager::Settings::HDR::gaussMultiplier           = $HDRPostFX::gaussMultiplier;
   $PostFXManager::Settings::HDR::gaussStdDev               = $HDRPostFX::gaussStdDev;
   $PostFXManager::Settings::HDR::keyValue                  = $HDRPostFX::keyValue;
   $PostFXManager::Settings::HDR::minLuminace               = $HDRPostFX::minLuminace;
   $PostFXManager::Settings::HDR::whiteCutoff               = $HDRPostFX::whiteCutoff;
   
   postVerbose("% - PostFX Manager - Settings Saved - HDR");   
   
}

function PostFXManager::settingsApplyLightRays(%this)
{   
   $PostFXManager::Settings::LightRays::brightScalar        = $LightRayPostFX::brightScalar;
   
   postVerbose("% - PostFX Manager - Settings Saved - Light Rays");   
   
}

function PostFXManager::settingsApplyDOF(%this)
{
   $PostFXManager::Settings::DOF::EnableAutoFocus           = $DOFPostFx::EnableAutoFocus;   
   $PostFXManager::Settings::DOF::BlurMin                   = $DOFPostFx::BlurMin;
   $PostFXManager::Settings::DOF::BlurMax                   = $DOFPostFx::BlurMax;
   $PostFXManager::Settings::DOF::FocusRangeMin             = $DOFPostFx::FocusRangeMin;
   $PostFXManager::Settings::DOF::FocusRangeMax             = $DOFPostFx::FocusRangeMax;
   $PostFXManager::Settings::DOF::BlurCurveNear             = $DOFPostFx::BlurCurveNear;
   $PostFXManager::Settings::DOF::BlurCurveFar              = $DOFPostFx::BlurCurveFar;
   
   postVerbose("% - PostFX Manager - Settings Saved - DOF");   
   
}

function PostFXManager::settingsApplySharpen(%this)
{   
   $PostFXManager::Settings::Sharpen::sharpRange            = $SharpPostFx::sharpRange;
   $PostFXManager::Settings::Sharpen::sharpWidth            = $SharpPostFx::sharpWidth;
   $PostFXManager::Settings::Sharpen::sharpPower            = $SharpPostFx::sharpPower;

   postVerbose("% - PostFX Manager - Settings Saved - Sharpen");   
}

function PostFXManager::settingsApplyNightVision(%this)
{   
   $PostFXManager::Settings::NightVision::brightThreshold   = $NVPostFx::brightThreshold;
   $PostFXManager::Settings::NightVision::DistFreq          = $NVPostFx::DistFreq;
   $PostFXManager::Settings::NightVision::DistMul           = $NVPostFx::DistMul;
   $PostFXManager::Settings::NightVision::DistRoll          = $NVPostFx::DistRoll;
   $PostFXManager::Settings::NightVision::highMultiplier    = $NVPostFx::highMultiplier;
   $PostFXManager::Settings::NightVision::lowMultiplier     = $NVPostFx::lowMultiplier;
   
   postVerbose("% - PostFX Manager - Settings Saved - Night Vision");   
}

function PostFXManager::settingsApplyAll(%this, %sFrom)
{
   // Apply settings which control if effects are on/off altogether.
   $PostFXManager::Settings::EnablePostFX        = $PostFXManager::PostFX::Enabled;  
   $PostFXManager::Settings::EnableNightVision   = $PostFXManager::PostFX::EnableNightVision;
   $PostFXManager::Settings::EnableSharpen       = $PostFXManager::PostFX::EnableSharpen;
   $PostFXManager::Settings::EnableDOF           = $PostFXManager::PostFX::EnableDOF;
   $PostFXManager::Settings::EnableLightRays     = $PostFXManager::PostFX::EnableLightRays;
   $PostFXManager::Settings::EnableHDR           = $PostFXManager::PostFX::EnableHDR;
   $PostFXManager::Settings::EnabledSSAO         = $PostFXManager::PostFX::EnableSSAO;
      
   // Apply settings should save the values in the system to the 
   // the preset structure ($PostFXManager::Settings::*)

   // SSAO Settings
   %this.settingsApplySSAO();
   // HDR settings
   %this.settingsApplyHDR();
   // Light rays settings
   %this.settingsApplyLightRays();
   // DOF
   %this.settingsApplyDOF();
   // Sharpness
   %this.settingsApplySharpen();
   // NV
   %this.settingsApplyNightVision();
   
   postVerbose("% - PostFX Manager - All Settings applied to $PostFXManager::Settings");
}

function PostFXManager::settingsApplyDefaultPreset(%this)
{
   PostFXManager::loadPresetHandler($PostFXManager::defaultPreset);
}

