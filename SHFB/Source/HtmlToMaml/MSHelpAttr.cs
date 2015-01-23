//=============================================================================
// System  : Sandcastle Help File Builder Utilities
// File    : MSHelpAttr.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 07/03/2008
// Note    : Copyright 2008, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains a class representing an HTML Help 2.x attribute that can
// be added to the XML data island in each help topic generated by
// BuildAssembler.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy
// of the license should be distributed with the code.  It can also be found
// at the project website: https://GitHub.com/EWSoftware/SHFB.   This notice, the
// author's name, and all copyright notices must remain intact in all
// applications, documentation, and source files.
//
// Version     Date     Who  Comments
// ============================================================================
// 1.6.0.7  03/25/2008  EFW  Created the code
// 1.8.0.0  07/03/2008  EFW  Rewrote to support the MSBuild project format
//=============================================================================

using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Xml;

namespace SandcastleBuilder.Utils
{
    /// <summary>
    /// This represents an HTML Help 2.x attribute that can be added to the
    /// XML data island in each help topic generated by BuildAssembler.
    /// </summary>
    [DefaultProperty("AttributeName"), Serializable]
    public class MSHelpAttr : IComparable<MSHelpAttr>
    {
        #region Private data members
        //=====================================================================

        private string attrName, attrValue;
        private bool isDirty;
        #endregion

        #region Properties
        //=====================================================================

        /// <summary>
        /// This is used to get or set the attribute name
        /// </summary>
        [Category("Attribute"), Description("The name of the attribute"),
          DefaultValue(null)]
        public string AttributeName
        {
            get { return attrName; }
            set
            {
                if(value == null || value.Trim().Length == 0)
                    attrName = "NoName";
                else
                    attrName = value;
            }
        }

        /// <summary>
        /// This is used to get or set the attribute value
        /// </summary>
        [Category("Attribute"), Description("The value of the attribute"),
          DefaultValue(null)]
        public string AttributeValue
        {
            get { return attrValue; }
            set { attrValue = value; }
        }

        /// <summary>
        /// This is used to get or set the dirty state of the item
        /// </summary>
        [Browsable(false)]
        public bool IsDirty
        {
            get { return isDirty; }
            set { isDirty = value; }
        }
        #endregion

        #region Constructor
        //=====================================================================

        /// <summary>
        /// Internal constructor
        /// </summary>
        /// <param name="name">The attribute name</param>
        /// <param name="value">The attribute value</param>
        internal MSHelpAttr(string name, string value)
        {
            if(name == null || name.Trim().Length == 0)
                attrName = "NoName";
            else
                attrName = name;

            attrValue = value;
        }
        #endregion

        #region IComparable<MSHelpAttr> Members
        /// <summary>
        /// Compares this instance to another instance and returns an
        /// indication of their relative values.
        /// </summary>
        /// <param name="other">A MSHelpAttr object to compare</param>
        /// <returns>Returns -1 if this instance is less than the
        /// value, 0 if they are equal, or 1 if this instance is
        /// greater than the value or the value is null.</returns>
        /// <remarks>Entries are sorted by name and then value</remarks>
        public int CompareTo(MSHelpAttr other)
        {
            int result = 0;

            if(other == null)
                return 1;

            result = String.Compare(attrName, other.AttributeName,
                StringComparison.Ordinal);

            if(result == 0)
                result = String.Compare(attrValue, other.AttributeValue,
                    StringComparison.CurrentCulture);

            return result;
        }
        #endregion

        #region Equality, hash code, and To String
        //=====================================================================

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            MSHelpAttr ha = obj as MSHelpAttr;

            if(ha == null)
                return false;

            return (this == ha || (this.AttributeName == ha.AttributeName &&
                this.AttributeValue == ha.AttributeValue));
        }

        /// <summary>
        /// Get a hash code for this item
        /// </summary>
        /// <returns>Returns the hash code for the attribute name and value.</returns>
        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        /// <summary>
        /// Return a string representation of the item
        /// </summary>
        /// <returns>Returns the item in its XML format</returns>
        public override string ToString()
        {
            return String.Format(CultureInfo.InvariantCulture,
                "<MSHelp:Attr Name=\"{0}\" Value=\"{1}\" />",
                attrName, attrValue);
        }
        #endregion
    }
}
