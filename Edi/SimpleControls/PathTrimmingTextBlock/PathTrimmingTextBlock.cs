namespace SimpleControls.PathTrimmingTextBlock
{
  using System;
  using System.ComponentModel;
  using System.Globalization;
  using System.Linq;
  using System.Windows;
  using System.Windows.Controls;
  using System.Windows.Media;

  /// <summary>
  /// This PathTrimmingTextBlock textblock attaches itself to the events of a parent container and
  /// displays a trimmed path text when the size of the parent (container) is changed.
  /// 
  /// http://www.codeproject.com/Tips/467054/WPF-PathTrimmingTextBlock
  /// </summary>
  public class PathTrimmingTextBlock : TextBlock
  {
    #region fields
    private FrameworkElement mContainer;

    /// <summary>
    /// Path dependency property that stores the trimmed path
    /// </summary>
    private static readonly DependencyProperty PathProperty =
        DependencyProperty.Register("Path", typeof(string), typeof(PathTrimmingTextBlock), new UIPropertyMetadata(""));
    #endregion fields

    #region constructor
    /// <summary>
    /// Class Constructor
    /// </summary>
    public PathTrimmingTextBlock()
    {
      this.mContainer = null;

      this.Loaded += new RoutedEventHandler(PathTrimmingTextBlock_Loaded);
      this.Unloaded += new RoutedEventHandler(PathTrimmingTextBlock_Unloaded);
    }
    #endregion constructor

    #region properties
    /// <summary>
    /// Path dependency property that stores the trimmed path
    /// </summary>
    public string Path
    {
      get { return (string)GetValue(PathProperty); }
      set { SetValue(PathProperty, value); }
    }
    #endregion properties

    #region methods
    /// <summary>
    /// Textblock is constructed and start its live - lets attach to the
    /// size changed event handler of the containing parent.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void PathTrimmingTextBlock_Loaded(object sender, RoutedEventArgs e)
    {
      FrameworkElement p = this.Parent as FrameworkElement;

      if (p != null)
      {
        this.mContainer = p;
      }
      else
      {
        DependencyObject dp = this.Parent as DependencyObject;

        if (dp != null)
        {
          for (DependencyObject parent = LogicalTreeHelper.GetParent(dp as DependencyObject);
               parent != null;
               parent = LogicalTreeHelper.GetParent(parent as DependencyObject))
          {
            p = parent as FrameworkElement;

            if (p != null)
              break;
          }

          this.mContainer = p;
        }
      }

      if (this.mContainer != null)
      {
        this.mContainer.SizeChanged += new SizeChangedEventHandler(container_SizeChanged);

        Text = GetTrimmedPath(mContainer.ActualWidth);
      }
      //// else
      ////  throw new InvalidOperationException("PathTrimmingTextBlock must have a container such as a Grid.");
    }

    /// <summary>
    /// Remove custom event handlers and clean-up on unload.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void PathTrimmingTextBlock_Unloaded(object sender, RoutedEventArgs e)
    {
      if (mContainer != null)
        mContainer.SizeChanged -= container_SizeChanged;
    }

    /// <summary>
    /// Trim the containing text (path) accordingly whenever the parent container chnages its size.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void container_SizeChanged(object sender, SizeChangedEventArgs e)
    {
      if (mContainer != null)
        Text = GetTrimmedPath(mContainer.ActualWidth);
    }

    /// <summary>
    /// Compute the text to display (with ellipsis) that fits the ActualWidth of the container
    /// </summary>
    /// <param name="width"></param>
    /// <returns></returns>
    string GetTrimmedPath(double width)
    {
      string filename = string.Empty;
      string directory = string.Empty;

      try
      {
        filename = System.IO.Path.GetFileName(Path);
        directory = System.IO.Path.GetDirectoryName(Path);
      }
      catch (Exception)
      {
        directory = Path;
        filename = string.Empty;
      }

      bool widthOK = false;
      bool changedWidth = false;

      TextBlock block = new TextBlock();
      block.Style = this.Style;
      block.FontWeight = this.FontWeight;
      block.FontStyle = this.FontStyle;
      block.FontStretch = this.FontStretch;
      block.FontSize = this.FontSize;
      block.FontFamily = this.FontFamily;

      do
      {
        block.Text = "{0}...{1}".FormatWith(directory, filename);
        block.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));

        widthOK = block.DesiredSize.Width < width;
        //widthOK = formatted.Width < width;

        if (!widthOK)
        {
          if (directory.Length == 0)
            return "...\\" + filename;

          changedWidth = true;
          directory = directory.Substring(0, directory.Length - 1);
        }

      } while (!widthOK);

      if (!changedWidth)
      {
        return Path;
      }

      if (block != null)   // Optimize for speed
        return block.Text;

      return "{0}...{1}".FormatWith(directory, filename);
    }
    #endregion constructor
  }
}