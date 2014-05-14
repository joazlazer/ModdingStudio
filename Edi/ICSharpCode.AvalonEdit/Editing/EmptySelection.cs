// Copyright (c) AlphaSierraPapa for the SharpDevelop Team (for details please see \doc\copyright.txt)
// This code is distributed under the GNU LGPL (for details please see \doc\license.txt)

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Utils;
using System.Windows.Input;

namespace ICSharpCode.AvalonEdit.Editing
{
	sealed class EmptySelection : Selection
	{
		public EmptySelection(TextArea textArea) : base(textArea)
		{
		}
		
		public override Selection UpdateOnDocumentChange(DocumentChangeEventArgs e)
		{
			return this;
		}
		
		public override TextViewPosition StartPosition {
			get { return new TextViewPosition(TextLocation.Empty); }
		}
		
		public override TextViewPosition EndPosition {
			get { return new TextViewPosition(TextLocation.Empty); }
		}
		
		public override ISegment SurroundingSegment {
			get { return null; }
		}
		
		public override Selection SetEndpoint(TextViewPosition endPosition)
		{
			throw new NotSupportedException();
		}
		
		public override Selection StartSelectionOrSetEndpoint(TextViewPosition startPosition, TextViewPosition endPosition)
		{
			var document = textArea.Document;
			if (document == null)
				throw ThrowUtil.NoDocumentAssigned();

			return Create(textArea, startPosition, endPosition);
		}
		
		public override IEnumerable<SelectionSegment> Segments {
			get { return Empty<SelectionSegment>.Array; }
		}
		
		public override string GetText()
		{
			return string.Empty;
		}

    public override void ReplaceSelectionWithText(string newText)
    {
      if (newText == null)
        throw new ArgumentNullException("newText");

      newText = AddSpacesIfRequired(newText, textArea.Caret.Position, textArea.Caret.Position);

      if (newText.Length > 0)
      {
        if (textArea.ReadOnlySectionProvider.CanInsert(textArea.Caret.Offset))
        {
          // Dirkster99 Extension added support for Insert/Overtype mode
          // Replaced with version from here http://community.sharpdevelop.net/forums/t/12345.aspx
          if (textArea.Options.IsInsertMode == false)
          {
            int diff = textArea.Document.GetLineByNumber(textArea.Caret.Line).EndOffset - textArea.Caret.Offset;
            if (diff <= 0)
              textArea.Document.Insert(textArea.Caret.Offset, newText);
            else
            {
              if (newText.Length > diff)
                textArea.Document.Replace(textArea.Caret.Offset, diff, newText);
              else
                textArea.Document.Replace(textArea.Caret.Offset, newText.Length, newText);

              textArea.Caret.Offset += newText.Length;
            }
          }
          else
            textArea.Document.Insert(textArea.Caret.Offset, newText);
        }
      }
      textArea.Caret.VisualColumn = -1;
    }

		public override int Length {
			get { return 0; }
		}
		
		// Use reference equality because there's only one EmptySelection per text area.
		public override int GetHashCode()
		{
			return RuntimeHelpers.GetHashCode(this);
		}
		
		public override bool Equals(object obj)
		{
			return this == obj;
		}
	}
}
