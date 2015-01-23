//===============================================================================================================
// System  : EWSoftware Design Time Attributes and Editors
// File    : FolderPathObjectEditor.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 08/24/2014
// Note    : Copyright 2006-2014, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains a type editor that can display a folder browser dialog to allow selection of a folder path
// at design time.  This can be used in conjunction with the FolderDialogAttribute to specify the folder browser
// dialog's properties.
//
// This code is published under the Microsoft Public License (Ms-PL).  A copy of the license should be
// distributed with the code.  It can also be found at the project website: https://GitHub.com/EWSoftware/SHFB.  This
// notice, the author's name, and all copyright notices must remain intact in all applications, documentation,
// and source files.
//
//    Date     Who  Comments
// ==============================================================================================================
// 08/02/2006  EFW  Created the code
//===============================================================================================================

using System;
using System.ComponentModel;

namespace SandcastleBuilder.Utils.Design
{
    /// <summary>
    /// This is a type editor that can display a folder browser dialog to allow selection of a folder path at
    /// design time.
    /// </summary>
    /// <remarks>This can be used in conjunction with the <see cref="FolderDialogAttribute" /> to specify the
    /// folder browser dialog's properties.</remarks>
    public class FolderPathObjectEditor : FolderPathStringEditor
    {
        /// <summary>
        /// This is overridden to edit the value using a folder browser dialog.
        /// </summary>
        /// <param name="context">The descriptor context</param>
        /// <param name="provider">The provider</param>
        /// <param name="value">The folder path as an object</param>
        /// <returns>The selected folder path as an object</returns>
        [RefreshProperties(RefreshProperties.All)]
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            FolderPath folderPath = value as FolderPath;

            if(folderPath == null)
                return base.EditValue(context, provider, value);

            string path = (string)base.EditValue(context, provider, folderPath.ToString());

            if(path != folderPath.Path)
                value = new FolderPath(path, folderPath.IsFixedPath, folderPath.BasePathProvider);

            return value;
        }
    }
}
