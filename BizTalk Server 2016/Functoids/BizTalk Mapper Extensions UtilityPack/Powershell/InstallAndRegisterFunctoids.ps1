<<<<<<< HEAD
﻿
#Set Paths to configuration and script folders
$scriptpath = $MyInvocation.MyCommand.Path;
$currentDir = Split-Path $scriptpath;
$assemblyDir =(get-item $currentDir).parent.GetDirectories("Deployment").FullName
$configurationFile = "$currentDir\deploymentSettings.xml";

#Load Configuration Data
if(![System.IO.Path]::IsPathRooted($configurationFile))        
{
    $configurationFile = [System.IO.Path]::Combine("$($env:APPDATA)",$configurationFile);       
}

#Load Configuration
[xml]$xmlConfiguration = Get-Content $configurationFile;   

#Set variables
$isDevelopmentEnvironment = $xmlConfiguration.FunctoidDeployment.DeploySettings.isDevelopmentEnvironment;
$bizTalkVersion = $xmlConfiguration.FunctoidDeployment.DeploySettings.bizTalkVersion;


foreach($exlusionItem in $xmlConfiguration.FunctoidDeployment.DeploySettings.ExcludeList.GetElementsByTagName("Item").InnerText)
{
    if($exclusionList -eq $null)
    {
        $exclusionList = @($exlusionItem)
    }
    else
    {
        $exclusionList +=@($exlusionItem)                
    }
}

#Set Mapper Extensions installpath   
$installPath = "C:\Program Files (x86)\Microsoft BizTalk Server $bizTalkVersion\Developer Tools\Mapper Extensions"


$gacItem = $true;
$copyToinstallPath = $false;

#Get the assemblies
foreach ($filepath in [System.IO.Directory]::EnumerateFiles($assemblyDir,"*.dll","TopDirectoryOnly"))
{
    #Get assemly   
    $file = New-Object System.IO.FileInfo($filepath)

       
    if($isDevelopmentEnvironment -eq "true")
    {
        #development environment no need to copy files to GAC, however we need to copy the to the mapper extensions directory
        $gacItem = $true
        $copyToinstallPath = $true
    }
    else
    {
        #non development environment need to copy files to GAC and we don't need to copy the assembly yo the mapper extensions directory
        $gacItem = $true
        $copyToinstallPath = $false
    }
    

  if($exclusionList.Contains($file.Name))  
  {
      $gacItem = $false
  }

  #Add assembly to GAC
  if($gacItem)
  {
      # see if the Enterprise Services Namespace is registered
      if ($null -eq ([AppDomain]::CurrentDomain.GetAssemblies() |? { $_.FullName -eq "System.EnterpriseServices, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" }) ) 
      {
          [System.Reflection.Assembly]::Load("System.EnterpriseServices, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a") | Out-Null
      }

       # create a reference to the publish class
       $publish = New-Object System.EnterpriseServices.Internal.Publish

        # ensure the file that was provided exists
       if ( -not (Test-Path $file -type Leaf) ) 
       {
          throw " - The assembly '$file' does not exist in the path $file"
       }
 
      # ensure the file is strongly signed before installing in the GAC
      if ( [System.Reflection.Assembly]::LoadFile($file).GetName().GetPublicKey().Length -eq 0) 
      {
          throw " - The assembly '$file' must be strongly signed."
      }

      # remove if exists
      Write-Output " - Removing: $file"
      $publish.GacRemove($file);

      # install the assembly in the GAC
      Write-Output " - Installing: $file"
      $publish.GacInstall($file);
  }

  if($copyToinstallPath)
  {
      $destinatinFileName = join-path $installPath $file.Name
      Write-Output " - Copy file $filepath to $installPath"
  
      if(Test-Path($destinatinFileName))
      {
          Remove-Item $destinatinFileName -Recurse
      }
  
      Copy-Item $filepath $installPath
  }


   
}





=======
﻿
#Set Paths to configuration and script folders
$scriptpath = $MyInvocation.MyCommand.Path;
$currentDir = Split-Path $scriptpath;
$assemblyDir =(get-item $currentDir).parent.GetDirectories("Deployment").FullName
$configurationFile = "$currentDir\deploymentSettings.xml";

#Load Configuration Data
if(![System.IO.Path]::IsPathRooted($configurationFile))        
{
    $configurationFile = [System.IO.Path]::Combine("$($env:APPDATA)",$configurationFile);       
}

#Load Configuration
[xml]$xmlConfiguration = Get-Content $configurationFile;   

#Set variables
$isDevelopmentEnvironment = $xmlConfiguration.FunctoidDeployment.DeploySettings.isDevelopmentEnvironment;
$bizTalkVersion = $xmlConfiguration.FunctoidDeployment.DeploySettings.bizTalkVersion;


foreach($exlusionItem in $xmlConfiguration.FunctoidDeployment.DeploySettings.ExcludeList.GetElementsByTagName("Item").InnerText)
{
    if($exclusionList -eq $null)
    {
        $exclusionList = @($exlusionItem)
    }
    else
    {
        $exclusionList +=@($exlusionItem)                
    }
}

#Set Mapper Extensions installpath   
$installPath = "C:\Program Files (x86)\Microsoft BizTalk Server $bizTalkVersion\Developer Tools\Mapper Extensions"


$gacItem = $true;
$copyToinstallPath = $false;

#Get the assemblies
foreach ($filepath in [System.IO.Directory]::EnumerateFiles($assemblyDir,"*.dll","TopDirectoryOnly"))
{
    #Get assemly   
    $file = New-Object System.IO.FileInfo($filepath)

       
    if($isDevelopmentEnvironment -eq "true")
    {
        #development environment no need to copy files to GAC, however we need to copy the to the mapper extensions directory
        $gacItem = $true
        $copyToinstallPath = $true
    }
    else
    {
        #non development environment need to copy files to GAC and we don't need to copy the assembly yo the mapper extensions directory
        $gacItem = $true
        $copyToinstallPath = $false
    }
    

  if($exclusionList.Contains($file.Name))  
  {
      $gacItem = $false
  }

  #Add assembly to GAC
  if($gacItem)
  {
      # see if the Enterprise Services Namespace is registered
      if ($null -eq ([AppDomain]::CurrentDomain.GetAssemblies() |? { $_.FullName -eq "System.EnterpriseServices, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" }) ) 
      {
          [System.Reflection.Assembly]::Load("System.EnterpriseServices, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a") | Out-Null
      }

       # create a reference to the publish class
       $publish = New-Object System.EnterpriseServices.Internal.Publish

        # ensure the file that was provided exists
       if ( -not (Test-Path $file -type Leaf) ) 
       {
          throw " - The assembly '$file' does not exist in the path $file"
       }
 
      # ensure the file is strongly signed before installing in the GAC
      if ( [System.Reflection.Assembly]::LoadFile($file).GetName().GetPublicKey().Length -eq 0) 
      {
          throw " - The assembly '$file' must be strongly signed."
      }

      # remove if exists
      Write-Output " - Removing: $file"
      $publish.GacRemove($file);

      # install the assembly in the GAC
      Write-Output " - Installing: $file"
      $publish.GacInstall($file);
  }

  if($copyToinstallPath)
  {
      $destinatinFileName = join-path $installPath $file.Name
      Write-Output " - Copy file $filepath to $installPath"
  
      if(Test-Path($destinatinFileName))
      {
          Remove-Item $destinatinFileName -Recurse
      }
  
      Copy-Item $filepath $installPath
  }


   
}





>>>>>>> origin/master
