<<<<<<< HEAD
﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BizTalk.String.Functoids {
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
    internal class StringResources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal StringResources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("BizTalk.String.Functoids.StringResources", typeof(StringResources).Assembly);
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
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap IDS_ADVCOMPAREFUNCTOID_BITMAP {
            get {
                object obj = ResourceManager.GetObject("IDS_ADVCOMPAREFUNCTOID_BITMAP", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Compares two specified String objects, ignoring or honoring their case, and returns an boolean that indicates if they are equal or not.
        ///Parameters:
        ///- The first string to compare. 
        ///- The second string to compare. 
        ///- True to ignore case during the comparison; otherwise, False..
        /// </summary>
        internal static string IDS_ADVCOMPAREFUNCTOID_DESCRIPTION {
            get {
                return ResourceManager.GetString("IDS_ADVCOMPAREFUNCTOID_DESCRIPTION", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to String Advance Compare Functoid.
        /// </summary>
        internal static string IDS_ADVCOMPAREFUNCTOID_NAME {
            get {
                return ResourceManager.GetString("IDS_ADVCOMPAREFUNCTOID_NAME", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Returns an boolean that indicates if they are equal or not..
        /// </summary>
        internal static string IDS_ADVCOMPAREFUNCTOID_TOOLTIP {
            get {
                return ResourceManager.GetString("IDS_ADVCOMPAREFUNCTOID_TOOLTIP", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap IDS_CONSTANTFUNCTOID_BITMAP {
            get {
                object obj = ResourceManager.GetObject("IDS_CONSTANTFUNCTOID_BITMAP", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Use the String Constant functoid to set constant values. This functoid only accept one value and allows you to set constant values in elements..
        /// </summary>
        internal static string IDS_CONSTANTFUNCTOID_DESCRIPTION {
            get {
                return ResourceManager.GetString("IDS_CONSTANTFUNCTOID_DESCRIPTION", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to String Constant functoid.
        /// </summary>
        internal static string IDS_CONSTANTFUNCTOID_NAME {
            get {
                return ResourceManager.GetString("IDS_CONSTANTFUNCTOID_NAME", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Returns the a string value.
        /// </summary>
        internal static string IDS_CONSTANTFUNCTOID_TOOLTIP {
            get {
                return ResourceManager.GetString("IDS_CONSTANTFUNCTOID_TOOLTIP", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap IDS_NORMALIZEFUNCTOID_BITMAP {
            get {
                object obj = ResourceManager.GetObject("IDS_NORMALIZEFUNCTOID_BITMAP", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Function normalize the text. It will remove two or more consecutive spaces and replace them with a single space, remove two or more consecutive newlines and replace them with a single newline and &quot;condense&quot; multiple tabs into one.
        ///Parameters:
        ///- String to Normalize.
        /// </summary>
        internal static string IDS_NORMALIZEFUNCTOID_DESCRIPTION {
            get {
                return ResourceManager.GetString("IDS_NORMALIZEFUNCTOID_DESCRIPTION", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to String Normalize Functoid.
        /// </summary>
        internal static string IDS_NORMALIZEFUNCTOID_NAME {
            get {
                return ResourceManager.GetString("IDS_NORMALIZEFUNCTOID_NAME", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Return the string normalize without consecutive spaces, lines or tabs.
        /// </summary>
        internal static string IDS_NORMALIZEFUNCTOID_TOOLTIP {
            get {
                return ResourceManager.GetString("IDS_NORMALIZEFUNCTOID_TOOLTIP", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap IDS_PADLEFTFUNCTOID_BITMAP {
            get {
                object obj = ResourceManager.GetObject("IDS_PADLEFTFUNCTOID_BITMAP", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Returns a new string that right-aligns the characters in this instance by padding them on the left with a specified Unicode character, for a specified total length.
        ///Parameters:
        ///- String that will be pad on the left with a specified Unicode character
        ///- The number of characters in the resulting string, equal to the number of original characters plus any additional padding characters.  
        ///- A Unicode padding character..
        /// </summary>
        internal static string IDS_PADLEFTFUNCTOID_DESCRIPTION {
            get {
                return ResourceManager.GetString("IDS_PADLEFTFUNCTOID_DESCRIPTION", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to String PadLeft Functoid.
        /// </summary>
        internal static string IDS_PADLEFTFUNCTOID_NAME {
            get {
                return ResourceManager.GetString("IDS_PADLEFTFUNCTOID_NAME", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A new string that is equivalent to this instance, but right-aligned and padded on the left with as many paddingChar characters as needed to create a length of totalWidth. However, if totalWidth is less than the length of this instance, the method returns a reference to the existing instance. If totalWidth is equal to the length of this instance, the method returns a new string that is identical to this instance..
        /// </summary>
        internal static string IDS_PADLEFTFUNCTOID_TOOLTIP {
            get {
                return ResourceManager.GetString("IDS_PADLEFTFUNCTOID_TOOLTIP", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap IDS_PADRIGHTFUNCTOID_BITMAP {
            get {
                object obj = ResourceManager.GetObject("IDS_PADRIGHTFUNCTOID_BITMAP", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Returns a new string that left-aligns the characters in this string by padding them on the right with a specified Unicode character, for a specified total length.
        ///Parameters:
        ///- String that will be pad on the left with a specified Unicode character
        ///- The number of characters in the resulting string, equal to the number of original characters plus any additional padding characters. 
        ///- A Unicode padding character..
        /// </summary>
        internal static string IDS_PADRIGHTFUNCTOID_DESCRIPTION {
            get {
                return ResourceManager.GetString("IDS_PADRIGHTFUNCTOID_DESCRIPTION", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to String PadRight Functoid.
        /// </summary>
        internal static string IDS_PADRIGHTFUNCTOID_NAME {
            get {
                return ResourceManager.GetString("IDS_PADRIGHTFUNCTOID_NAME", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A new string that is equivalent to this instance, but left-aligned and padded on the right with as many paddingChar characters as needed to create a length of totalWidth. However, if totalWidth is less than the length of this instance, the method returns a reference to the existing instance. If totalWidth is equal to the length of this instance, the method returns a new string that is identical to this instance..
        /// </summary>
        internal static string IDS_PADRIGHTFUNCTOID_TOOLTIP {
            get {
                return ResourceManager.GetString("IDS_PADRIGHTFUNCTOID_TOOLTIP", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap IDS_REMOVELEADINGZEROSFUNCTOID_BITMAP {
            get {
                object obj = ResourceManager.GetObject("IDS_REMOVELEADINGZEROSFUNCTOID_BITMAP", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Removes any leading zeros from the input string.
        /// </summary>
        internal static string IDS_REMOVELEADINGZEROSFUNCTOID_DESCRIPTION {
            get {
                return ResourceManager.GetString("IDS_REMOVELEADINGZEROSFUNCTOID_DESCRIPTION", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to String remove leading zeros.
        /// </summary>
        internal static string IDS_REMOVELEADINGZEROSFUNCTOID_NAME {
            get {
                return ResourceManager.GetString("IDS_REMOVELEADINGZEROSFUNCTOID_NAME", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Removes any leading zeros from the input string.
        /// </summary>
        internal static string IDS_REMOVELEADINGZEROSFUNCTOID_TOOLTIP {
            get {
                return ResourceManager.GetString("IDS_REMOVELEADINGZEROSFUNCTOID_TOOLTIP", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap IDS_REPLACEFUNCTOID_BITMAP {
            get {
                object obj = ResourceManager.GetObject("IDS_REPLACEFUNCTOID_BITMAP", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to This functoid returns a new string in which all occurrences of a specified string (second parameter) found in the first string are replaced with another specified string (third parameter).
        ///Parameters:
        ///- String where we will replace the values
        ///- The string to be replaced. 
        ///- The string to replace all occurrences of oldValue..
        /// </summary>
        internal static string IDS_REPLACEFUNCTOID_DESCRIPTION {
            get {
                return ResourceManager.GetString("IDS_REPLACEFUNCTOID_DESCRIPTION", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to String Replace Functoid.
        /// </summary>
        internal static string IDS_REPLACEFUNCTOID_NAME {
            get {
                return ResourceManager.GetString("IDS_REPLACEFUNCTOID_NAME", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Returns a string that is equivalent to the current string except that all instances of oldValue are replaced with newValue. If oldValue is not found in the current instance, the method returns the current instance unchanged..
        /// </summary>
        internal static string IDS_REPLACEFUNCTOID_TOOLTIP {
            get {
                return ResourceManager.GetString("IDS_REPLACEFUNCTOID_TOOLTIP", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap IDS_TOTITLECASEFUNCTOID_BITMAP {
            get {
                object obj = ResourceManager.GetObject("IDS_TOTITLECASEFUNCTOID_BITMAP", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Converts the specified string to title case (except for words that are entirely in uppercase, which are considered to be acronyms)
        ///Parameters:
        ///- The string to convert to title case..
        /// </summary>
        internal static string IDS_TOTITLECASEFUNCTOID_DESCRIPTION {
            get {
                return ResourceManager.GetString("IDS_TOTITLECASEFUNCTOID_DESCRIPTION", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to String ToTitleCase functoid.
        /// </summary>
        internal static string IDS_TOTITLECASEFUNCTOID_NAME {
            get {
                return ResourceManager.GetString("IDS_TOTITLECASEFUNCTOID_NAME", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Returns the specified string converted to title case..
        /// </summary>
        internal static string IDS_TOTITLECASEFUNCTOID_TOOLTIP {
            get {
                return ResourceManager.GetString("IDS_TOTITLECASEFUNCTOID_TOOLTIP", resourceCulture);
            }
        }
    }
}
=======
﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BizTalk.String.Functoids {
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
    internal class StringResources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal StringResources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("BizTalk.String.Functoids.StringResources", typeof(StringResources).Assembly);
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
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap IDS_ADVCOMPAREFUNCTOID_BITMAP {
            get {
                object obj = ResourceManager.GetObject("IDS_ADVCOMPAREFUNCTOID_BITMAP", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Compares two specified String objects, ignoring or honoring their case, and returns an boolean that indicates if they are equal or not.
        ///Parameters:
        ///- The first string to compare. 
        ///- The second string to compare. 
        ///- True to ignore case during the comparison; otherwise, False..
        /// </summary>
        internal static string IDS_ADVCOMPAREFUNCTOID_DESCRIPTION {
            get {
                return ResourceManager.GetString("IDS_ADVCOMPAREFUNCTOID_DESCRIPTION", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to String Advance Compare Functoid.
        /// </summary>
        internal static string IDS_ADVCOMPAREFUNCTOID_NAME {
            get {
                return ResourceManager.GetString("IDS_ADVCOMPAREFUNCTOID_NAME", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Returns an boolean that indicates if they are equal or not..
        /// </summary>
        internal static string IDS_ADVCOMPAREFUNCTOID_TOOLTIP {
            get {
                return ResourceManager.GetString("IDS_ADVCOMPAREFUNCTOID_TOOLTIP", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap IDS_CONSTANTFUNCTOID_BITMAP {
            get {
                object obj = ResourceManager.GetObject("IDS_CONSTANTFUNCTOID_BITMAP", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Use the String Constant functoid to set constant values. This functoid only accept one value and allows you to set constant values in elements..
        /// </summary>
        internal static string IDS_CONSTANTFUNCTOID_DESCRIPTION {
            get {
                return ResourceManager.GetString("IDS_CONSTANTFUNCTOID_DESCRIPTION", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to String Constant functoid.
        /// </summary>
        internal static string IDS_CONSTANTFUNCTOID_NAME {
            get {
                return ResourceManager.GetString("IDS_CONSTANTFUNCTOID_NAME", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Returns the a string value.
        /// </summary>
        internal static string IDS_CONSTANTFUNCTOID_TOOLTIP {
            get {
                return ResourceManager.GetString("IDS_CONSTANTFUNCTOID_TOOLTIP", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap IDS_NORMALIZEFUNCTOID_BITMAP {
            get {
                object obj = ResourceManager.GetObject("IDS_NORMALIZEFUNCTOID_BITMAP", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Function normalize the text. It will remove two or more consecutive spaces and replace them with a single space, remove two or more consecutive newlines and replace them with a single newline and &quot;condense&quot; multiple tabs into one.
        ///Parameters:
        ///- String to Normalize.
        /// </summary>
        internal static string IDS_NORMALIZEFUNCTOID_DESCRIPTION {
            get {
                return ResourceManager.GetString("IDS_NORMALIZEFUNCTOID_DESCRIPTION", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to String Normalize Functoid.
        /// </summary>
        internal static string IDS_NORMALIZEFUNCTOID_NAME {
            get {
                return ResourceManager.GetString("IDS_NORMALIZEFUNCTOID_NAME", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Return the string normalize without consecutive spaces, lines or tabs.
        /// </summary>
        internal static string IDS_NORMALIZEFUNCTOID_TOOLTIP {
            get {
                return ResourceManager.GetString("IDS_NORMALIZEFUNCTOID_TOOLTIP", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap IDS_PADLEFTFUNCTOID_BITMAP {
            get {
                object obj = ResourceManager.GetObject("IDS_PADLEFTFUNCTOID_BITMAP", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Returns a new string that right-aligns the characters in this instance by padding them on the left with a specified Unicode character, for a specified total length.
        ///Parameters:
        ///- String that will be pad on the left with a specified Unicode character
        ///- The number of characters in the resulting string, equal to the number of original characters plus any additional padding characters.  
        ///- A Unicode padding character..
        /// </summary>
        internal static string IDS_PADLEFTFUNCTOID_DESCRIPTION {
            get {
                return ResourceManager.GetString("IDS_PADLEFTFUNCTOID_DESCRIPTION", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to String PadLeft Functoid.
        /// </summary>
        internal static string IDS_PADLEFTFUNCTOID_NAME {
            get {
                return ResourceManager.GetString("IDS_PADLEFTFUNCTOID_NAME", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A new string that is equivalent to this instance, but right-aligned and padded on the left with as many paddingChar characters as needed to create a length of totalWidth. However, if totalWidth is less than the length of this instance, the method returns a reference to the existing instance. If totalWidth is equal to the length of this instance, the method returns a new string that is identical to this instance..
        /// </summary>
        internal static string IDS_PADLEFTFUNCTOID_TOOLTIP {
            get {
                return ResourceManager.GetString("IDS_PADLEFTFUNCTOID_TOOLTIP", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap IDS_PADRIGHTFUNCTOID_BITMAP {
            get {
                object obj = ResourceManager.GetObject("IDS_PADRIGHTFUNCTOID_BITMAP", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Returns a new string that left-aligns the characters in this string by padding them on the right with a specified Unicode character, for a specified total length.
        ///Parameters:
        ///- String that will be pad on the left with a specified Unicode character
        ///- The number of characters in the resulting string, equal to the number of original characters plus any additional padding characters. 
        ///- A Unicode padding character..
        /// </summary>
        internal static string IDS_PADRIGHTFUNCTOID_DESCRIPTION {
            get {
                return ResourceManager.GetString("IDS_PADRIGHTFUNCTOID_DESCRIPTION", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to String PadRight Functoid.
        /// </summary>
        internal static string IDS_PADRIGHTFUNCTOID_NAME {
            get {
                return ResourceManager.GetString("IDS_PADRIGHTFUNCTOID_NAME", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A new string that is equivalent to this instance, but left-aligned and padded on the right with as many paddingChar characters as needed to create a length of totalWidth. However, if totalWidth is less than the length of this instance, the method returns a reference to the existing instance. If totalWidth is equal to the length of this instance, the method returns a new string that is identical to this instance..
        /// </summary>
        internal static string IDS_PADRIGHTFUNCTOID_TOOLTIP {
            get {
                return ResourceManager.GetString("IDS_PADRIGHTFUNCTOID_TOOLTIP", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap IDS_REMOVELEADINGZEROSFUNCTOID_BITMAP {
            get {
                object obj = ResourceManager.GetObject("IDS_REMOVELEADINGZEROSFUNCTOID_BITMAP", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Removes any leading zeros from the input string.
        /// </summary>
        internal static string IDS_REMOVELEADINGZEROSFUNCTOID_DESCRIPTION {
            get {
                return ResourceManager.GetString("IDS_REMOVELEADINGZEROSFUNCTOID_DESCRIPTION", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to String remove leading zeros.
        /// </summary>
        internal static string IDS_REMOVELEADINGZEROSFUNCTOID_NAME {
            get {
                return ResourceManager.GetString("IDS_REMOVELEADINGZEROSFUNCTOID_NAME", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Removes any leading zeros from the input string.
        /// </summary>
        internal static string IDS_REMOVELEADINGZEROSFUNCTOID_TOOLTIP {
            get {
                return ResourceManager.GetString("IDS_REMOVELEADINGZEROSFUNCTOID_TOOLTIP", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap IDS_REPLACEFUNCTOID_BITMAP {
            get {
                object obj = ResourceManager.GetObject("IDS_REPLACEFUNCTOID_BITMAP", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to This functoid returns a new string in which all occurrences of a specified string (second parameter) found in the first string are replaced with another specified string (third parameter).
        ///Parameters:
        ///- String where we will replace the values
        ///- The string to be replaced. 
        ///- The string to replace all occurrences of oldValue..
        /// </summary>
        internal static string IDS_REPLACEFUNCTOID_DESCRIPTION {
            get {
                return ResourceManager.GetString("IDS_REPLACEFUNCTOID_DESCRIPTION", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to String Replace Functoid.
        /// </summary>
        internal static string IDS_REPLACEFUNCTOID_NAME {
            get {
                return ResourceManager.GetString("IDS_REPLACEFUNCTOID_NAME", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Returns a string that is equivalent to the current string except that all instances of oldValue are replaced with newValue. If oldValue is not found in the current instance, the method returns the current instance unchanged..
        /// </summary>
        internal static string IDS_REPLACEFUNCTOID_TOOLTIP {
            get {
                return ResourceManager.GetString("IDS_REPLACEFUNCTOID_TOOLTIP", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap IDS_TOTITLECASEFUNCTOID_BITMAP {
            get {
                object obj = ResourceManager.GetObject("IDS_TOTITLECASEFUNCTOID_BITMAP", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Converts the specified string to title case (except for words that are entirely in uppercase, which are considered to be acronyms)
        ///Parameters:
        ///- The string to convert to title case..
        /// </summary>
        internal static string IDS_TOTITLECASEFUNCTOID_DESCRIPTION {
            get {
                return ResourceManager.GetString("IDS_TOTITLECASEFUNCTOID_DESCRIPTION", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to String ToTitleCase functoid.
        /// </summary>
        internal static string IDS_TOTITLECASEFUNCTOID_NAME {
            get {
                return ResourceManager.GetString("IDS_TOTITLECASEFUNCTOID_NAME", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Returns the specified string converted to title case..
        /// </summary>
        internal static string IDS_TOTITLECASEFUNCTOID_TOOLTIP {
            get {
                return ResourceManager.GetString("IDS_TOTITLECASEFUNCTOID_TOOLTIP", resourceCulture);
            }
        }
    }
}
>>>>>>> origin/master
