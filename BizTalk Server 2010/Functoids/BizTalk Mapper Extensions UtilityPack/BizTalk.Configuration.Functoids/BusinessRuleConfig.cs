using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.BizTalk.BaseFunctoids;
using System.Reflection;
using Microsoft.RuleEngine;

namespace BizTalk.Configuration.Functoids
{
    [Serializable]
    public class BusinessRuleConfig : BaseFunctoid
    {
        public BusinessRuleConfig()
            : base()
        {
            //ID for this functoid
            this.ID = 10204;

            // resource assembly must be ProjectName.ResourceName if building with VS.Net
            SetupResourceAssembly("BizTalk.Configuration.Functoids.ConfigurationResources", Assembly.GetExecutingAssembly());

            //Setup the Name, ToolTip, Help Description, and the Bitmap for this functoid
            SetName("IDS_BRULESFUNCTOID_NAME");
            SetTooltip("IDS_BRULESFUNCTOID_TOOLTIP");
            SetDescription("IDS_BRULESFUNCTOID_DESCRIPTION");
            SetBitmap("IDS_BRULESFUNCTOID_BITMAP");

            //category for this functoid. This functoid goes under the String Functoid Tab in the
            //VS.Net toolbox for functoids.
            this.Category = FunctoidCategory.None;

            // Set the limits for the number of input parameters.
            this.SetMinParams(2);
            this.SetMaxParams(2);

            // Add one line of code as set out below for each input param. For multiple input params, each line would be identical.
            AddInputConnectionType(ConnectionType.All); //first input

            // The functoid output can go to any node type.
            this.OutputConnectionType = ConnectionType.All;

            // Set the function name that is to be called when invoking this functoid.
            // To test the map in Visual Studio, this functoid does not need to be in the GAC.
            // If using this functoid in a deployed BizTalk app. then it must be in the GAC
            SetExternalFunctionName(GetType().Assembly.FullName, GetType().FullName, "GetBusinessRuleValue");
        }

        /// <summary>    
        /// This function allows you to obtain a definition value from a Vocabulary in the Business Rules Engine.   
        /// </summary>    
        /// <param name="definitionName">Definition Name e.g. Value1</param>    
        /// <param name="vocabularyName">Name of Vocabulary i.e. Config</param>    
        /// <returns>Return Constant Value</returns> 
        public static string GetBusinessRuleValue(string definitionName, string vocabularyName)
        {
            // RuleStore Object        
            RuleStore rlsRuleStore;
            // Vocab Info collection        
            VocabularyInfoCollection vicVocabInfo;
            // Vocab itself       
            Vocabulary vocVocab;
            // Provides the default deployment driver used to import, export, deploy, un-deploy, and        
            // set tracking configuration for published rule sets and vocabularies,        
            // and to retrieve their deployment characteristics.         
            RuleSetDeploymentDriver rsdDriver = new RuleSetDeploymentDriver();
            // The current RuleStore        
            rlsRuleStore = rsdDriver.GetRuleStore();
            // Set Vocabulary based on Vocabulary collection        
            vicVocabInfo = rlsRuleStore.GetVocabularies(vocabularyName, RuleStore.Filter.All);
            // Get the vocabulary itself        
            vocVocab = rlsRuleStore.GetVocabulary(vicVocabInfo[0]);
            // Get the definition        
            VocabularyDefinition vocDef = vocVocab.Definitions.GetByName(definitionName);
            // Set LiteralDefition        
            LiteralDefinition literalDefinition = vocDef as LiteralDefinition;
            // Return Value        
            return literalDefinition.Value.ToString();
        }
    }
}
