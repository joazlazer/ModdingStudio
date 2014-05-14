// Copyright (c) AlphaSierraPapa for the SharpDevelop Team (for details please see \doc\copyright.txt)
// This code is distributed under the GNU LGPL (for details please see \doc\license.txt)

using System;
using System.Text.RegularExpressions;
using ICSharpCode.AvalonEdit.Utils;

namespace ICSharpCode.AvalonEdit.Rendering
{
	// This class is public because it can be used as a base class for custom links.
	
	/// <summary>
	/// Detects hyperlinks and makes them clickable.
	/// </summary>
	/// <remarks>
	/// This element generator can be easily enabled and configured using the
	/// <see cref="TextEditorOptions"/>.
	/// </remarks>
	public class LinkElementGenerator : VisualLineElementGenerator, IBuiltinElementGenerator
	{
		// a link starts with a protocol (or just with www), followed by 0 or more 'link characters', followed by a link end character
		// (this allows accepting punctuation inside links but not at the end)
    private readonly static Regex defaultLinkRegex = new Regex(@"(\b(https?://|ftp://|www\.)[\w\d\._/\-~%@()+:?&=#!]*[\w\d/])");
    		
		readonly Regex linkRegex;
		
		/// <summary>
		/// Gets/Sets whether the user needs to press Control to click the link.
		/// The default value is true.
		/// </summary>
		public bool RequireControlModifierForClick { get; set; }
		
		/// <summary>
		/// Creates a new LinkElementGenerator.
		/// </summary>
		public LinkElementGenerator()
		{
			this.linkRegex = defaultLinkRegex;
			this.RequireControlModifierForClick = true;
		}
		
		/// <summary>
		/// Creates a new LinkElementGenerator using the specified regex.
		/// </summary>
		protected LinkElementGenerator(Regex regex) : this()
		{
			if (regex == null)
				throw new ArgumentNullException("regex");
			this.linkRegex = regex;
		}
		
		void IBuiltinElementGenerator.FetchOptions(TextEditorOptions options)
		{
			this.RequireControlModifierForClick = options.RequireControlModifierForHyperlinkClick;
		}
		
		Match GetMatch(int startOffset, out int matchOffset)
		{
			int endOffset = CurrentContext.VisualLine.LastDocumentLine.EndOffset;
			StringSegment relevantText = CurrentContext.GetText(startOffset, endOffset - startOffset);
			Match m = linkRegex.Match(relevantText.Text, relevantText.Offset, relevantText.Count);
			matchOffset = m.Success ? m.Index - relevantText.Offset + startOffset : -1;
			return m;
		}
		
		/// <inheritdoc/>
		public override int GetFirstInterestedOffset(int startOffset)
		{
			int matchOffset;
			GetMatch(startOffset, out matchOffset);
			return matchOffset;
		}
		
		/// <inheritdoc/>
		public override VisualLineElement ConstructElement(int offset)
		{
			int matchOffset;
			Match m = GetMatch(offset, out matchOffset);
			if (m.Success && matchOffset == offset) {
				return ConstructElementFromMatch(m);
			} else {
				return null;
			}
		}
		
		/// <summary>
		/// Constructs a VisualLineElement that replaces the matched text.
		/// The default implementation will create a <see cref="VisualLineLinkText"/>
		/// based on the URI provided by <see cref="GetUriFromMatch"/>.
		/// </summary>
		protected virtual VisualLineElement ConstructElementFromMatch(Match m)
		{
			Uri uri = GetUriFromMatch(m);
			if (uri == null)
				return null;
			VisualLineLinkText linkText = new VisualLineLinkText(CurrentContext.VisualLine, m.Length);
			linkText.NavigateUri = uri;
			linkText.RequireControlModifierForClick = this.RequireControlModifierForClick;
			return linkText;
		}
		
		/// <summary>
		/// Fetches the URI from the regex match. Returns null if the URI format is invalid.
		/// </summary>
		protected virtual Uri GetUriFromMatch(Match match)
		{
			string targetUrl = match.Value;

			if (targetUrl.StartsWith("www.", StringComparison.Ordinal))
				targetUrl = "http://" + targetUrl;

			if (Uri.IsWellFormedUriString(targetUrl, UriKind.Absolute))
        return new Uri(targetUrl, UriKind.Absolute);

      return null;
		}
	}
	
	/// <summary>
	/// Detects e-mail addresses and makes them clickable.
  /// This class is internal because it does not need to be accessed by the user - it can be configured using TextEditorOptions.
	/// </summary>
	/// <remarks>
	/// This element generator can be easily enabled and configured using the
	/// <see cref="TextEditorOptions"/>.
	/// </remarks>
	internal sealed class MailLinkElementGenerator : LinkElementGenerator
	{
    // try to detect email addresses
    private readonly static Regex defaultMailRegex = new Regex(@"\b[\w\d\.\-]+\@[\w\d\.\-]+\.[a-z]{2,6}\b");
    
    /// <summary>
		/// Creates a new MailLinkElementGenerator.
		/// </summary>
		public MailLinkElementGenerator()
			: base(defaultMailRegex)
		{
		}
		
		protected override Uri GetUriFromMatch(Match match)
		{
			return new Uri("mailto:" + match.Value);
		}
	}

  /// <summary>
  /// Detects Windows File System link addresses and makes them clickable.
  /// This class is internal because it does not need to be accessed by the user
  /// - it can be configured using TextEditorOptions.
  /// </summary>
  /// <remarks>
  /// This element generator can be enabled using the <see cref="TextEditorOptions"/>.
  /// </remarks>
  internal sealed class FileLinkElementGenerator : LinkElementGenerator
  {
    // Dirkster99 Extended Regex to include "file://sdfsfd/htr.html" and "C:\Notes.txt" style links
    // The regular expression specified here is later used to construct a URI object which will not
    // work unless the reference is formated OK
    //
    private const string FileLink = @"(\b(file:\/\/)[\w\d\._\-~%@()+:?&=#!][\w\d/._\-~%@()+:?&=#!]*)";
    private const string DriveLink = @"([""][A-Z]:[\\]?[ a-zA-Z0-9\\\.~_\-~%@()+:?&=#!]*)[""]";
    private const string UNCLink = @"([""][\\\\][ a-zA-Z0-9\\\.~_\-~%@()+:?&=#!]*[\\]?[ a-zA-Z0-9\\\.~_\-~%@()+:?&=#!]*)[""]";

    private readonly static Regex defaultLinkRegex = new Regex(
              FileLinkElementGenerator.UNCLink
      + "|" + FileLinkElementGenerator.FileLink
      + "|" + FileLinkElementGenerator.DriveLink
      );

    /// <summary>
    /// Constructor creates a new FileLinkElementGenerator
    /// which can be used to match and highlight links into the Windows file system.
    /// </summary>
    public FileLinkElementGenerator()
      : base(FileLinkElementGenerator.defaultLinkRegex)
    {
    }

    protected override Uri GetUriFromMatch(Match match)
    {
      string targetUrl = match.Value;

      if (targetUrl.Length < 3)     // any kind of file based link requires 3 letters e.g. 'C:\'
        return null;

      // Dirkster99: IsWellFormedUriString is too restrictiv (MS-DOS and UNC paths will not pass)
      // if (Uri.IsWellFormedUriString(targetUrl, UriKind.Absolute))
      if(targetUrl.Length >= 5)
      {
        if (targetUrl.StartsWith("\"") && targetUrl.EndsWith("\""))
          targetUrl = targetUrl.Substring(1, targetUrl.Length - 2);
      }

      Uri uri = null;

      Uri.TryCreate(targetUrl, UriKind.Absolute, out uri);

      return uri;
    }
  }
}
