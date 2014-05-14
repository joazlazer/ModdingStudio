// Copyright (c) AlphaSierraPapa for the SharpDevelop Team (for details please see \doc\copyright.txt)
// This code is distributed under the GNU LGPL (for details please see \doc\license.txt)

using System;
using System.Windows;
using System.Windows.Media;

using ICSharpCode.AvalonEdit.Rendering;
using ICSharpCode.AvalonEdit.Utils;

namespace ICSharpCode.AvalonEdit.Rendering
{
	/// <summary>
	/// Renders a ruler at a certain column.
	/// </summary>
	sealed class ColumnRulerRenderer : IBackgroundRenderer
	{
		Pen pen;
		int column;
		TextView textView;
		
		public static readonly Color DefaultForeground = Colors.LightGray;
		
		public ColumnRulerRenderer(TextView textView)
		{
			if (textView == null)
				throw new ArgumentNullException("textView");
			
			this.pen = new Pen(new SolidColorBrush(DefaultForeground), 1);
			this.pen.Freeze();
			this.textView = textView;

      ColumnRulerRenderer oldRenderer = null;

      // Make sure there is only one of this type of background renderer
      // Otherwise, we might keep adding and WPF keeps drawing them on top of each other
      foreach (var item in this.textView.BackgroundRenderers)
      {
        if (item != null)
        {
          if (item is ColumnRulerRenderer)
            oldRenderer = item as ColumnRulerRenderer;
        }
      }

      this.textView.BackgroundRenderers.Remove(oldRenderer);

			this.textView.BackgroundRenderers.Add(this);
		}
		
		public KnownLayer Layer {
			get { return KnownLayer.Background; }
		}

    /// <summary>
    /// Set the Column Ruler of the texteditor to the requested column.
    /// </summary>
    /// <param name="column"></param>
    /// <param name="pen"></param>
		public void SetRuler(int column, Pen pen)
		{
			if (this.column != column)
      {
				this.column = column;
				textView.InvalidateLayer(this.Layer);
			}

			if (this.pen != pen)
      {
				this.pen = pen;
				textView.InvalidateLayer(this.Layer);
			}
		}
		
		public void Draw(TextView textView, System.Windows.Media.DrawingContext drawingContext)
		{
			if (column < 1) return;
			double offset = textView.WideSpaceWidth * column;
			Size pixelSize = PixelSnapHelpers.GetPixelSize(textView);
			double markerXPos = PixelSnapHelpers.PixelAlign(offset, pixelSize.Width);
			Point start = new Point(markerXPos, 0);
			Point end = new Point(markerXPos, Math.Max(textView.DocumentHeight, textView.ActualHeight));
			
			drawingContext.DrawLine(pen, start, end);
		}
	}
}
