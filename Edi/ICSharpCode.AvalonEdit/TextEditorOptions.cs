// Copyright (c) AlphaSierraPapa for the SharpDevelop Team (for details please see \doc\copyright.txt)
// This code is distributed under the GNU LGPL (for details please see \doc\license.txt)

using System;
using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace ICSharpCode.AvalonEdit
{
	/// <summary>
	/// A container for the text editor options.
  /// 
  /// TextEditor optinos can be edit with this class as viewmodel.
  /// TextEditor options can be persisted (if an option is a non-default)
  /// since this class is serializable.
	/// </summary>
	[Serializable]
	public class TextEditorOptions : INotifyPropertyChanged
	{
    #region fields
    private bool mShowSpaces = false;
    private bool mShowTabs = false;
    private bool mShowEndOfLine = false;
    private bool mShowBoxForControlCharacters = true;
    private bool mEnableHyperlinks = true;
    private bool mEnableFileHyperlinks = true;
    private bool mEnableEmailHyperlinks = true;
    private bool mRequireControlModifierForHyperlinkClick = true;
    private int mIndentationSize = 4;
    private bool mConvertTabsToSpaces = false;
    
    [NonSerialized]
    bool mIsInsertMode = true;

    private bool mCutCopyWholeLine = true;
    private bool mAllowScrollBelowDocument = false;
    private double mWordWrapIndentation = 0;
    private bool mInheritWordWrapIndentation = true;
    private bool mEnableRectangularSelection = true;
    private bool mEnableTextDragDrop = true;
    private bool mEnableVirtualSpace = false;
    private bool mEnableImeSupport = true;
    private bool mShowColumnRuler = false;
    private int mColumnRulerPosition = 80;
    private bool mEnableCopyHighlighting = false;
    private bool mEnableCodeCompletion = false;
    private bool mEnableHighlightBrackets = true;
    #endregion fields

		#region ctor
		/// <summary>
		/// Initializes an empty instance of TextEditorOptions.
		/// </summary>
		public TextEditorOptions()
		{
		}
		
		/// <summary>
		/// Initializes a new instance of TextEditorOptions by copying all values
		/// from <paramref name="options"/> to the new instance.
		/// </summary>
		public TextEditorOptions(TextEditorOptions options)
		{
      if (options == null)
        return;

			// get all the fields in the class
			FieldInfo[] fields = typeof(TextEditorOptions).GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
			
			// copy each value over to 'this'
			foreach(FieldInfo fi in fields)
      {
				if (fi.IsNotSerialized == false)
				{
          fi.SetValue(this, fi.GetValue(options));
        }
			}
		}
		#endregion
		
		#region PropertyChanged event handling
		/// <inheritdoc/>
		[field: NonSerialized]
		public event PropertyChangedEventHandler PropertyChanged;
    #endregion PropertyChanged event handling

    #region properties
    #region ShowSpaces / ShowTabs / ShowEndOfLine / ShowBoxForControlCharacters
		/// <summary>
		/// Gets/Sets whether to show · for spaces.
		/// </summary>
		/// <remarks>The default value is <c>false</c>.</remarks>
		[DefaultValue(false)]
		public virtual bool ShowSpaces {
			get { return mShowSpaces; }
			set {
				if (mShowSpaces != value) {
					mShowSpaces = value;
					OnPropertyChanged("ShowSpaces");
				}
			}
		}
	
		/// <summary>
		/// Gets/Sets whether to show » for tabs.
		/// </summary>
		/// <remarks>The default value is <c>false</c>.</remarks>
		[DefaultValue(false)]
		public virtual bool ShowTabs {
			get { return mShowTabs; }
			set {
				if (mShowTabs != value) {
					mShowTabs = value;
					OnPropertyChanged("ShowTabs");
				}
			}
		}
		
		/// <summary>
		/// Gets/Sets whether to show ¶ at the end of lines.
		/// </summary>
		/// <remarks>The default value is <c>false</c>.</remarks>
		[DefaultValue(false)]
		public virtual bool ShowEndOfLine {
			get { return mShowEndOfLine; }
			set {
				if (mShowEndOfLine != value) {
					mShowEndOfLine = value;
					OnPropertyChanged("ShowEndOfLine");
				}
			}
		}
		
		/// <summary>
		/// Gets/Sets whether to show a box with the hex code for control characters.
		/// </summary>
		/// <remarks>The default value is <c>true</c>.</remarks>
		[DefaultValue(true)]
		public virtual bool ShowBoxForControlCharacters {
			get { return mShowBoxForControlCharacters; }
			set {
				if (mShowBoxForControlCharacters != value) {
					mShowBoxForControlCharacters = value;
					OnPropertyChanged("ShowBoxForControlCharacters");
				}
			}
		}
		#endregion
		
		#region EnableHyperlinks		
		/// <summary>
		/// Gets/Sets whether to enable clickable hyperlinks in the editor.
		/// </summary>
		/// <remarks>The default value is <c>true</c>.</remarks>
		[DefaultValue(true)]
		public virtual bool EnableHyperlinks {
			get { return mEnableHyperlinks; }
			set {
				if (mEnableHyperlinks != value) {
					mEnableHyperlinks = value;
					OnPropertyChanged("EnableHyperlinks");
				}
			}
		}

    #region file hyperlinks
    /// <summary>
    /// Gets/Sets whether to enable clickable hyperlinks in the editor.
    /// </summary>
    /// <remarks>The default value is <c>true</c>.</remarks>
    [DefaultValue(true)]
    public virtual bool EnableFileHyperlinks
    {
      get { return mEnableFileHyperlinks; }
      set
      {
        if (mEnableFileHyperlinks != value)
        {
          mEnableFileHyperlinks = value;
          OnPropertyChanged("EnableFileHyperlinks");
        }
      }
    }
    #endregion file hyperlinks

    #region mail hyperlinks		
		/// <summary>
		/// Gets/Sets whether to enable clickable hyperlinks for e-mail addresses in the editor.
		/// </summary>
		/// <remarks>The default value is <c>true</c>.</remarks>
		[DefaultValue(true)]
		public virtual bool EnableEmailHyperlinks {
			get { return mEnableEmailHyperlinks; }
			set {
				if (mEnableEmailHyperlinks != value) {
					mEnableEmailHyperlinks = value;
					OnPropertyChanged("EnableEMailHyperlinks");
				}
			}
		}
    #endregion mail hyperlinks
		
		/// <summary>
		/// Gets/Sets whether the user needs to press Control to click hyperlinks.
		/// The default value is true.
		/// </summary>
		/// <remarks>The default value is <c>true</c>.</remarks>
		[DefaultValue(true)]
		public virtual bool RequireControlModifierForHyperlinkClick {
			get { return mRequireControlModifierForHyperlinkClick; }
			set {
				if (mRequireControlModifierForHyperlinkClick != value) {
					mRequireControlModifierForHyperlinkClick = value;
					OnPropertyChanged("RequireControlModifierForHyperlinkClick");
				}
			}
		}
		#endregion
		
		#region TabSize / IndentationSize / ConvertTabsToSpaces / GetIndentationString	
		/// <summary>
		/// Gets/Sets the width of one indentation unit.
		/// </summary>
		/// <remarks>The default value is 4.</remarks>
		[DefaultValue(4)]
		public virtual int IndentationSize {
			get { return mIndentationSize; }
			set {
				if (value < 1)
					throw new ArgumentOutOfRangeException("value", value, "value must be positive");
				// sanity check; a too large value might cause WPF to crash internally much later
				// (it only crashed in the hundred thousands for me; but might crash earlier with larger fonts)
				if (value > 1000)
					throw new ArgumentOutOfRangeException("value", value, "indentation size is too large");
				if (mIndentationSize != value) {
					mIndentationSize = value;
					OnPropertyChanged("IndentationSize");
					OnPropertyChanged("IndentationString");
				}
			}
		}
		
		/// <summary>
		/// Gets/Sets whether to use spaces for indentation instead of tabs.
		/// </summary>
		/// <remarks>The default value is <c>false</c>.</remarks>
		[DefaultValue(false)]
		public virtual bool ConvertTabsToSpaces {
			get { return mConvertTabsToSpaces; }
			set {
				if (mConvertTabsToSpaces != value) {
					mConvertTabsToSpaces = value;
					OnPropertyChanged("ConvertTabsToSpaces");
					OnPropertyChanged("IndentationString");
				}
			}
		}
		
		/// <summary>
		/// Gets the text used for indentation.
		/// </summary>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods")]
		[Browsable(false)]
		public string IndentationString {
			get { return GetIndentationString(1); }
		}
		
		/// <summary>
		/// Gets text required to indent from the specified <paramref name="column"/> to the next indentation level.
		/// </summary>
		public virtual string GetIndentationString(int column)
		{
			if (column < 1)
				throw new ArgumentOutOfRangeException("column", column, "Value must be at least 1.");
			int indentationSize = this.IndentationSize;
			if (ConvertTabsToSpaces) {
				return new string(' ', indentationSize - ((column - 1) % indentationSize));
			} else {
				return "\t";
			}
		}
		#endregion

    #region InsertMode
    /// <summary>
    /// Dirkster99 Extension
    /// Gets/Sets whether Insert/Overtype mode is active or not:
    /// </summary>
    [DefaultValue(true)]
    public virtual bool IsInsertMode
    {
      get { return this.mIsInsertMode; }
      set
      {
        if (this.mIsInsertMode != value)
        {
          this.mIsInsertMode = value;
          OnPropertyChanged("IsInsertMode");
        }
      }
    }
    #endregion InsertMode

		/// <summary>
		/// Gets/Sets whether copying without a selection copies the whole current line.
		/// </summary>
		[DefaultValue(true)]
		public virtual bool CutCopyWholeLine {
			get { return mCutCopyWholeLine; }
			set {
				if (mCutCopyWholeLine != value) {
					mCutCopyWholeLine = value;
					OnPropertyChanged("CutCopyWholeLine");
				}
			}
		}
				
		/// <summary>
		/// Gets/Sets whether the user can scroll below the bottom of the document.
		/// The default value is false; but it a good idea to set this property to true when using folding.
		/// </summary>
		[DefaultValue(false)]
		public virtual bool AllowScrollBelowDocument {
			get { return mAllowScrollBelowDocument; }
			set {
				if (mAllowScrollBelowDocument != value) {
					mAllowScrollBelowDocument = value;
					OnPropertyChanged("AllowScrollBelowDocument");
				}
			}
		}
				
		/// <summary>
		/// Gets/Sets the indentation used for all lines except the first when word-wrapping.
		/// The default value is 0.
		/// </summary>
		[DefaultValue(0.0)]
		public virtual double WordWrapIndentation {
			get { return mWordWrapIndentation; }
			set {
				if (double.IsNaN(value) || double.IsInfinity(value))
					throw new ArgumentOutOfRangeException("value", value, "value must not be NaN/infinity");
				if (value != mWordWrapIndentation) {
					mWordWrapIndentation = value;
					OnPropertyChanged("WordWrapIndentation");
				}
			}
		}
				
		/// <summary>
		/// Gets/Sets whether the indentation is inherited from the first line when word-wrapping.
		/// The default value is true.
		/// </summary>
		/// <remarks>When combined with <see cref="WordWrapIndentation"/>, the inherited indentation is added to the word wrap indentation.</remarks>
		[DefaultValue(true)]
		public virtual bool InheritWordWrapIndentation {
			get { return mInheritWordWrapIndentation; }
			set {
				if (value != mInheritWordWrapIndentation) {
					mInheritWordWrapIndentation = value;
					OnPropertyChanged("InheritWordWrapIndentation");
				}
			}
		}
				
		/// <summary>
		/// Enables rectangular selection (press ALT and select a rectangle)
		/// </summary>
		[DefaultValue(true)]
		public bool EnableRectangularSelection {
			get { return mEnableRectangularSelection; }
			set {
				if (mEnableRectangularSelection != value) {
					mEnableRectangularSelection = value;
					OnPropertyChanged("EnableRectangularSelection");
				}
			}
		}
				
		/// <summary>
		/// Enable dragging text within the text area.
		/// </summary>
		[DefaultValue(true)]
		public bool EnableTextDragDrop {
			get { return mEnableTextDragDrop; }
			set {
				if (mEnableTextDragDrop != value) {
					mEnableTextDragDrop = value;
					OnPropertyChanged("EnableTextDragDrop");
				}
			}
		}
				
		/// <summary>
		/// Gets/Sets whether the user can set the caret behind the line ending
		/// (into "virtual space").
		/// Note that virtual space is always used (independent from this setting)
		/// when doing rectangle selections.
		/// </summary>
		[DefaultValue(false)]
		public virtual bool EnableVirtualSpace {
			get { return mEnableVirtualSpace; }
			set {
				if (mEnableVirtualSpace != value) {
					mEnableVirtualSpace = value;
					OnPropertyChanged("EnableVirtualSpace");
				}
			}
		}
			
		/// <summary>
		/// Gets/Sets whether the support for Input Method Editors (IME)
		/// for non-alphanumeric scripts (Chinese, Japanese, Korean, ...) is enabled.
		/// </summary>
		[DefaultValue(true)]
		public virtual bool EnableImeSupport {
			get { return mEnableImeSupport; }
			set {
				if (mEnableImeSupport != value) {
					mEnableImeSupport = value;
					OnPropertyChanged("EnableImeSupport");
				}
			}
		}
				
		/// <summary>
		/// Gets/Sets whether the column ruler should be shown.
		/// </summary>
		[DefaultValue(false)]
		public virtual bool ShowColumnRuler {
			get { return mShowColumnRuler; }
			set {
				if (mShowColumnRuler != value) {
					mShowColumnRuler = value;
					OnPropertyChanged("ShowColumnRuler");
				}
			}
		}
			
		/// <summary>
		/// Gets/Sets where the column ruler should be shown.
		/// </summary>
		[DefaultValue(80)]
		public virtual int ColumnRulerPosition {
			get { return mColumnRulerPosition; }
			set
      {
				if (this.mColumnRulerPosition != value)
        {
          this.mColumnRulerPosition = value;
          this.OnPropertyChanged("ColumnRulerPosition");
				}
			}
		}

    /// <summary>
    /// Get/set option to determine whether Copy function should copy
    /// highlighting information or not (copy with highlighting or without).
    /// </summary>
    [DefaultValue(false)]
    public virtual bool EnableCopyHighlighting
    {
      get { return this.mEnableCopyHighlighting; }
      set
      {
        if (this.mEnableCopyHighlighting != value)
        {
          this.mEnableCopyHighlighting = value;
          OnPropertyChanged("CopyHighlighting");
        }
      }
    }

    /// <summary>
    /// Get/set option to determine whether Code Completion - complete
    /// typed entries with completion window is activ or not.
    /// </summary>
    [DefaultValue(false)]
    public virtual bool EnableCodeCompletion
    {
      get { return this.mEnableCodeCompletion; }
      set
      {
        if (this.mEnableCodeCompletion != value)
        {
          this.mEnableCodeCompletion = value;
          OnPropertyChanged("EnableCodeCompletion");
        }
      }
    }

    /// <summary>
    /// Get/set option to determine whether bracket should
    /// be highlighted in the code or not.
    /// The two brackets "( .... )" are highlighted if answer is yes.
    /// </summary>
    [DefaultValue(true)]
    public virtual bool EnableHighlightBrackets
    {
      get
      {
        return this.mEnableHighlightBrackets;
      }

      set
      {
        if (this.mEnableHighlightBrackets != value)
        {
          this.mEnableHighlightBrackets = value;
          OnPropertyChanged("EnableHighlightBrackets");
        }
      }
    }
    #endregion properties

    #region methods
    #region PropertyChanged event handling
    /// <summary>
    /// Raises the PropertyChanged event.
    /// </summary>
    /// <param name="propertyName">The name of the changed property.</param>
    protected void OnPropertyChanged(string propertyName)
    {
      OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
    }

    /// <summary>
    /// Raises the PropertyChanged event.
    /// </summary>
    protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
    {
      if (PropertyChanged != null)
      {
        PropertyChanged(this, e);
      }
    }
    #endregion PropertyChanged event handling
    #endregion methods
	}
}
