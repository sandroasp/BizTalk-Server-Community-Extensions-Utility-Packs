﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.269
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BizTalk.DynamicGenerators.Functoids {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class DynamicGenerator {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal DynamicGenerator() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("BizTalk.DynamicGenerators.Functoids.DynamicGenerator", typeof(DynamicGenerator).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        internal static System.Drawing.Bitmap IDS_GUIDGENERATORFUNCTOID_BITMAP {
            get {
                object obj = ResourceManager.GetObject("IDS_GUIDGENERATORFUNCTOID_BITMAP", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to This functoid allows you to generate a new Guid.
        /// </summary>
        internal static string IDS_GUIDGENERATORFUNCTOID_DESCRIPTION {
            get {
                return ResourceManager.GetString("IDS_GUIDGENERATORFUNCTOID_DESCRIPTION", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Guid Generator Functoid.
        /// </summary>
        internal static string IDS_GUIDGENERATORFUNCTOID_NAME {
            get {
                return ResourceManager.GetString("IDS_GUIDGENERATORFUNCTOID_NAME", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Returns guid (as string)..
        /// </summary>
        internal static string IDS_GUIDGENERATORFUNCTOID_TOOLTIP {
            get {
                return ResourceManager.GetString("IDS_GUIDGENERATORFUNCTOID_TOOLTIP", resourceCulture);
            }
        }
        
        internal static System.Drawing.Bitmap IDS_PASSWORDGENERATORFUNCTOID_BITMAP {
            get {
                object obj = ResourceManager.GetObject("IDS_PASSWORDGENERATORFUNCTOID_BITMAP", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Use this functoid to build a random password.
        ///Inputs:
        ///- bool useLowerCase
        ///- bool useUpperCase
        ///- bool useNumbers
        ///- bool useSymbols
        ///- int pwdLength.
        /// </summary>
        internal static string IDS_PASSWORDGENERATORFUNCTOID_DESCRIPTION {
            get {
                return ResourceManager.GetString("IDS_PASSWORDGENERATORFUNCTOID_DESCRIPTION", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Password Generator Functoid.
        /// </summary>
        internal static string IDS_PASSWORDGENERATORFUNCTOID_NAME {
            get {
                return ResourceManager.GetString("IDS_PASSWORDGENERATORFUNCTOID_NAME", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Returns random password (string)..
        /// </summary>
        internal static string IDS_PASSWORDGENERATORFUNCTOID_TOOLTIP {
            get {
                return ResourceManager.GetString("IDS_PASSWORDGENERATORFUNCTOID_TOOLTIP", resourceCulture);
            }
        }
        
        internal static System.Drawing.Bitmap IDS_TINYIDGENERATORFUNCTOID_BITMAP {
            get {
                object obj = ResourceManager.GetObject("IDS_TINYIDGENERATORFUNCTOID_BITMAP", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to This functoid allows you to generate a new Tiny Id.
        /// </summary>
        internal static string IDS_TINYIDGENERATORFUNCTOID_DESCRIPTION {
            get {
                return ResourceManager.GetString("IDS_TINYIDGENERATORFUNCTOID_DESCRIPTION", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Tiny Id Generator Functoid.
        /// </summary>
        internal static string IDS_TINYIDGENERATORFUNCTOID_NAME {
            get {
                return ResourceManager.GetString("IDS_TINYIDGENERATORFUNCTOID_NAME", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Returns Tiny Id (as string)..
        /// </summary>
        internal static string IDS_TINYIDGENERATORFUNCTOID_TOOLTIP {
            get {
                return ResourceManager.GetString("IDS_TINYIDGENERATORFUNCTOID_TOOLTIP", resourceCulture);
            }
        }
    }
}
