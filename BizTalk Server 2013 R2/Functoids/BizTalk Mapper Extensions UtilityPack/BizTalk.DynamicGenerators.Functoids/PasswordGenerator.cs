using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.BizTalk.BaseFunctoids;
using System.Reflection;

namespace BizTalk.DynamicGenerators.Functoids
{
    [Serializable]
    public class PasswordGenerator : BaseFunctoid
    {
        public PasswordGenerator() : base()
		{
			//ID for this functoid
			this.ID = 10600;

			// resource assembly must be ProjectName.ResourceName if building with VS.Net
            SetupResourceAssembly("BizTalk.DynamicGenerators.Functoids.DynamicGenerator", Assembly.GetExecutingAssembly());

			//Setup the Name, ToolTip, Help Description, and the Bitmap for this functoid
			SetName("IDS_PASSWORDGENERATORFUNCTOID_NAME");
            SetTooltip("IDS_PASSWORDGENERATORFUNCTOID_TOOLTIP");
            SetDescription("IDS_PASSWORDGENERATORFUNCTOID_DESCRIPTION");
            SetBitmap("IDS_PASSWORDGENERATORFUNCTOID_BITMAP");

            //category for this functoid. This functoid goes under the String Functoid Tab in the
			//VS.Net toolbox for functoids.
			this.Category = FunctoidCategory.String;

            // Set the limits for the number of input parameters.
			this.SetMinParams(5);
			this.SetMaxParams(5);

            // Add one line of code as set out below for each input param. For multiple input params, each line would be identical.
			AddInputConnectionType(ConnectionType.All); //first input
            AddInputConnectionType(ConnectionType.All); //second input
            AddInputConnectionType(ConnectionType.All); //third input
            AddInputConnectionType(ConnectionType.All); //fourth input
            AddInputConnectionType(ConnectionType.All); //fifth input

            // The functoid output can go to any node type.
			this.OutputConnectionType = ConnectionType.All;

            // Set the function name that is to be called when invoking this functoid.
            // To test the map in Visual Studio, this functoid does not need to be in the GAC.
            // If using this functoid in a deployed BizTalk app. then it must be in the GAC
            SetExternalFunctionName(GetType().Assembly.FullName, GetType().FullName, "GeneratePassword");
		}

		//this is the function that gets called when the Map is executed which has this functoid.
        public string GeneratePassword(bool useLowerCase, bool useUpperCase, bool useNumbers, bool useSymbols, int pwdLength)
		{
            bool isActive = false;

            string[] Lower = new string[26] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
            string[] Upper = new string[26] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
            string[] Number = new string[10] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
            string[] Symbol = new string[] { "!", "@", "#", "$", "%", "&", "*", "?" };
            string pwd = string.Empty;

            bool useLowerCaseChar = useLowerCase;
            bool useUpperCaseChar = useUpperCase;
            bool useNumbersChar = useNumbers;
            bool useSymbolsChar = useSymbols;

            for (int i = 0; i < pwdLength; i++) //generate each character value
            {
                Random rand = new System.Random(Guid.NewGuid().GetHashCode());
                int rplace = 0;
                int rplace2 = 0;
                isActive = false;

                #region What type of Char
                while (!isActive)
                {
                    rplace = rand.Next(0, 4);
                    switch (rplace)
                    {
                        case 0:
                            isActive = useLowerCaseChar;
                            break;
                        case 1:
                            isActive = useUpperCaseChar;
                            break;
                        case 2:
                            isActive = useNumbersChar;
                            break;
                        case 3:
                            isActive = useSymbolsChar;
                            break;
                        default:
                            isActive = true;
                            break;
                    }
                }
                #endregion

                switch (rplace)
                {
                    case 0:
                        rplace2 = rand.Next(0, 26);
                        pwd += Lower[rplace2];
                        break;
                    case 1:
                        rplace2 = rand.Next(0, 26);
                        pwd += Upper[rplace2];
                        break;
                    case 2:
                        rplace2 = rand.Next(0, 10);
                        pwd += Number[rplace2];
                        break;
                    case 3:
                        rplace2 = rand.Next(0, 8);
                        pwd += Symbol[rplace2];
                        break;
                    default:
                        pwd += "";
                        break;
                }
            }

            return pwd;
		}
    }
}
