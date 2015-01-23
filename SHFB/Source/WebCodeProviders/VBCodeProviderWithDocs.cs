//=============================================================================
// System  : EWSoftware Custom Code Providers
// File    : VBCodeProviderWithDocs.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 02/03/2009
// Note    : Copyright 2008-2009, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains a custom VB.NET code provider that is able to output
// an individual XML comments file for each unit that is compiled to a folder
// of your choice.  This changes the default behavior which overwrites the
// comments file on each invocation and/or dumps them into the temporary
// ASP.NET compilation folder.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy
// of the license should be distributed with the code.  It can also be found
// at the project website: https://GitHub.com/EWSoftware/SHFB.   This notice, the
// author's name, and all copyright notices must remain intact in all
// applications, documentation, and source files.
//
// Version     Date     Who  Comments
// ============================================================================
// 1.0.0.0  04/06/2008  EFW  Created the code
// 1.1.0.0  02/03/2009  EFW  Added support for providerOptions
//=============================================================================

using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;

using Microsoft.VisualBasic;

namespace EWSoftware.CodeDom
{
    /// <summary>
    /// This is a custom VB.NET code provider that is able to output an
    /// individual XML comments file for each unit that is compiled to a folder
    /// of your choice.
    /// </summary>
    /// <remarks>This changes the default behavior which overwrites the
    /// comments file on each invocation and/or dumps them into the temporary
    /// ASP.NET compilation folder.
    /// 
    /// <p/>A <c>/docpath:[path]</c> option should be added to the
    /// <c>compilerOptions</c> attribute in the <b>Web.config</b> file to
    /// specify the path to which the XML comments files will be written.
    /// The filenames will match the assembly names generated by the
    /// compiler.</remarks>
    /// <example>
    /// <code lang="xml" title="Example Compiler Configuration">
    /// <![CDATA[
    /// <configuration>
    ///     <system.codedom>
    ///       <compilers>
    ///         <!-- For VB.NET -->
    ///         <compiler language="vb;vbs;visualbasic;vbscript" extension=".vb"
    ///           compilerOptions="/docpath:C:\Publish\Doc"
    ///           type="EWSoftware.CodeDom.VBCodeProviderWithDocs,
    ///               EWSoftware.CodeDom, Version=1.0.0.0, Culture=neutral,
    ///               PublicKeyToken=d633d7d5b41cbb65" />
    ///       </compilers>
    ///     </system.codedom>
    /// </configuration>]]>
    /// </code>
    /// </example>
    public class VBCodeProviderWithDocs : VBCodeProvider
    {
        #region Private data members
        //=====================================================================

        // Additional options to add.  For some reason, when using the custom
        // code providers, several of the common import statements do not get
        // included automatically.  As such, we will add some of the common
        // ones here.  Any additional items will have to be added to the
        // compilerOptions attribute manually in Web.config.
        private static string[] additionalOptions = {
            "/imports:System,System.Collections,System.Collections.Generic," +
            "System.Collections.ObjectModel,System.Configuration," +
            "System.Data,System.Web,System.Web.Configuration,System.Web.UI," +
            "System.Web.UI.HtmlControls,System.Web.UI.WebControls," +
            "System.Web.Util,System.Xml,Microsoft.VisualBasic" };
        #endregion

        #region Constructors
        //=====================================================================

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <overloads>There are two overloads for the constructor</overloads>
        public VBCodeProviderWithDocs() : base()
        {
        }

        /// <summary>
        /// This constructor is passed a provider options dictionary
        /// </summary>
        /// <param name="providerOptions">The provider options</param>
        public VBCodeProviderWithDocs(
          IDictionary<string, string> providerOptions) : base(providerOptions)
        {
        }
        #endregion

        #region CodeDomProvider overrides
        //=====================================================================

        /// <inheritdoc />
        public override CompilerResults CompileAssemblyFromDom(
          CompilerParameters options, params CodeCompileUnit[] compilationUnits)
        {
            CodeProviderHelper.ReplaceDocPathOption(options, additionalOptions);
            return base.CompileAssemblyFromDom(options, compilationUnits);
        }

        /// <inheritdoc />
        public override CompilerResults CompileAssemblyFromFile(
          CompilerParameters options, params string[] fileNames)
        {
            CodeProviderHelper.ReplaceDocPathOption(options, additionalOptions);
            return base.CompileAssemblyFromFile(options, fileNames);
        }

        /// <inheritdoc />
        public override CompilerResults CompileAssemblyFromSource(
          CompilerParameters options, params string[] sources)
        {
            CodeProviderHelper.ReplaceDocPathOption(options, additionalOptions);
            return base.CompileAssemblyFromSource(options, sources);
        }
        #endregion
    }
}
