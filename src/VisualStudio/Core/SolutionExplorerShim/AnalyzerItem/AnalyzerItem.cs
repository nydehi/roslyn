// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System.Linq;
using System.Windows.Media;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.Internal.VisualStudio.PlatformUI;
using Microsoft.VisualStudio.Imaging;
using Microsoft.VisualStudio.Imaging.Interop;
using Microsoft.VisualStudio.Language.Intellisense;

namespace Microsoft.VisualStudio.LanguageServices.Implementation.SolutionExplorer
{
    internal partial class AnalyzerItem : BaseItem
    {
        private readonly AnalyzersFolderItem _analyzersFolder;
        private readonly AnalyzerReference _analyzerReference;

        private static readonly ContextMenuController s_itemContextMenuController =
            new ContextMenuController(
                ID.RoslynCommands.AnalyzerContextMenu,
                items => items.All(item => item is AnalyzerItem));

        public AnalyzerItem(AnalyzersFolderItem analyzersFolder, AnalyzerReference analyzerReference)
            : base(GetNameText(analyzerReference))
        {
            _analyzersFolder = analyzersFolder;
            _analyzerReference = analyzerReference;
        }

        public override ImageMoniker IconMoniker
        {
            get
            {
                return KnownMonikers.CodeInformation;
            }
        }

        public override ImageMoniker ExpandedIconMoniker
        {
            get
            {
                return KnownMonikers.CodeInformation;
            }
        }

        public override ImageMoniker OverlayIconMoniker
        {
            get
            {
                if (_analyzerReference.IsUnresolved)
                {
                    return KnownMonikers.OverlayWarning;
                }
                else
                {
                    return default(ImageMoniker);
                }
            }
        }

        public override object GetBrowseObject()
        {
            return new BrowseObject(this);
        }

        public AnalyzerReference AnalyzerReference
        {
            get { return _analyzerReference; }
        }

        public override IContextMenuController ContextMenuController
        {
            get { return s_itemContextMenuController; }
        }

        public AnalyzersFolderItem AnalyzersFolder
        {
            get { return _analyzersFolder; }
        }

        /// <summary>
        /// Remove this AnalyzerItem from it's folder.
        /// </summary>
        public void Remove()
        {
            _analyzersFolder.RemoveAnalyzer(_analyzerReference.FullPath);
        }

        private static string GetNameText(AnalyzerReference analyzerReference)
        {
            if (analyzerReference.IsUnresolved)
            {
                return analyzerReference.FullPath;
            }
            else
            {
                return analyzerReference.Display;
            }
        }
    }
}
