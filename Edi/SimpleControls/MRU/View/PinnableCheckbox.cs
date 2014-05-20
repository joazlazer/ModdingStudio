namespace SimpleControls.MRU.View
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Windows.Controls;
  using System.Windows;

  public class PinnableCheckbox : CheckBox
  {
    #region constructor
    static PinnableCheckbox()
    {
      DefaultStyleKeyProperty.OverrideMetadata(typeof(PinnableCheckbox),
                new FrameworkPropertyMetadata(typeof(PinnableCheckbox)));
    }

    public PinnableCheckbox()
    {
    }
    #endregion constructor

    #region methods
    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();
    }
    #endregion methods
  }
}
