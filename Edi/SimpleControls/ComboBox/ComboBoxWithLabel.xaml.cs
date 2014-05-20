namespace SimpleControls.ComboBox
{
  using System.Collections;
  using System.Windows;
  using System.Windows.Controls;

  /// <summary>
  /// Interaction logic for ComboBoxWithLabel.xaml
  /// </summary>
  public partial class ComboBoxWithLabel : UserControl
  {
    #region fields
    private static readonly DependencyProperty LabelContentProperty =
      DependencyProperty.Register("LabelContent", typeof(string), typeof(ComboBoxWithLabel));

    private static readonly DependencyProperty ItemsSourceProperty =
      ComboBox.ItemsSourceProperty.AddOwner(typeof(ComboBoxWithLabel));

    private static readonly DependencyProperty DisplayMemberPathProperty =
      ComboBox.DisplayMemberPathProperty.AddOwner(typeof(ComboBoxWithLabel));

    private static readonly DependencyProperty SelectedValuePathProperty =
      ComboBox.SelectedValuePathProperty.AddOwner(typeof(ComboBoxWithLabel));

    private static readonly DependencyProperty SelectedValueProperty =
      ComboBox.SelectedValueProperty.AddOwner(typeof(ComboBoxWithLabel));

    private static readonly DependencyProperty SelectedItemProperty =
      ComboBox.SelectedItemProperty.AddOwner(typeof(ComboBoxWithLabel));

    private static readonly DependencyProperty SelectedIndexProperty =
      ComboBox.SelectedIndexProperty.AddOwner(typeof(ComboBoxWithLabel));
    #endregion fields

    #region constructor
    public ComboBoxWithLabel()
    {
      this.InitializeComponent();
    }
    #endregion constructor

    #region properties
    /// <summary>
    /// Declare ItemsSource and Register as an Owner of ComboBox.ItemsSource
    /// the ComboBoxWithLabel.xaml will bind the ComboBox.ItemsSource to this property
    /// </summary>
    public IEnumerable ItemsSource
    {
      get { return (IEnumerable)GetValue(ComboBoxWithLabel.ItemsSourceProperty); }
      set { SetValue(ComboBoxWithLabel.ItemsSourceProperty, value); }
    }

    /// <summary>
    /// Declare a new LabelContent property that can be bound as well
    /// The ComboBoxWithLable.xaml will bind the Label's content to this
    /// </summary>
    public string LabelContent
    {
      get { return (string)GetValue(ComboBoxWithLabel.LabelContentProperty); }
      set { SetValue(ComboBoxWithLabel.LabelContentProperty, value); }
    }

    /// <summary>
    /// MSDN Reference: http://msdn.microsoft.com/en-us/library/system.windows.controls.combobox.aspx
    /// </summary>
    public string DisplayMemberPath
    {
      get { return (string)GetValue(ComboBoxWithLabel.DisplayMemberPathProperty); }
      set { SetValue(ComboBoxWithLabel.DisplayMemberPathProperty, value); }
    }

    /// <summary>
    /// MSDN Reference: http://msdn.microsoft.com/en-us/library/system.windows.controls.combobox.aspx
    /// </summary>
    public string SelectedValuePath
    {
      get { return (string)GetValue(ComboBoxWithLabel.SelectedValuePathProperty); }
      set { SetValue(ComboBoxWithLabel.SelectedValuePathProperty, value); }
    }

    /// <summary>
    /// MSDN Reference: http://msdn.microsoft.com/en-us/library/system.windows.controls.combobox.aspx
    /// </summary>
    public object SelectedItem
    {
      get { return (object)GetValue(ComboBoxWithLabel.SelectedItemProperty); }
      set { SetValue(ComboBoxWithLabel.SelectedItemProperty, value); }
    }

    /// <summary>
    /// MSDN Reference: http://msdn.microsoft.com/en-us/library/system.windows.controls.combobox.aspx
    /// </summary>
    public object SelectedValue
    {
      get { return (object)GetValue(ComboBoxWithLabel.SelectedValueProperty); }
      set { SetValue(ComboBoxWithLabel.SelectedValueProperty, value); }
    }

    /// <summary>
    /// MSDN Reference: http://msdn.microsoft.com/en-us/library/system.windows.controls.combobox.aspx
    /// </summary>
    public int SelectedIndex
    {
      get { return (int)GetValue(ComboBoxWithLabel.SelectedIndexProperty); }
      set { SetValue(ComboBoxWithLabel.SelectedIndexProperty, value); }
    }
    #endregion properties
  }
}
