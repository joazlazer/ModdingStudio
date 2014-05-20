namespace SimpleControls.ComboBox
{
  using System.Collections;
  using System.Windows;
  using System.Windows.Controls;

  /// <summary>
  /// Interaction logic for ComboBoxWithLabel.xaml
  /// </summary>
  public partial class ComboBoxWithLabeledTextInput : UserControl
  {
    #region fields
    #region ComboBox
    private static readonly DependencyProperty ItemsSourceProperty =
      ComboBox.ItemsSourceProperty.AddOwner(typeof(ComboBoxWithLabeledTextInput));

    private static readonly DependencyProperty LabelContentProperty =
      DependencyProperty.Register("LabelContent", typeof(string), typeof(ComboBoxWithLabeledTextInput));

    private static readonly DependencyProperty DisplayMemberPathProperty =
      ComboBox.DisplayMemberPathProperty.AddOwner(typeof(ComboBoxWithLabeledTextInput));

    private static readonly DependencyProperty SelectedValuePathProperty =
      ComboBox.SelectedValuePathProperty.AddOwner(typeof(ComboBoxWithLabeledTextInput));

    private static readonly DependencyProperty SelectedItemProperty =
      ComboBox.SelectedItemProperty.AddOwner(typeof(ComboBoxWithLabeledTextInput));

    private static readonly DependencyProperty SelectedValueProperty =
      ComboBox.SelectedValueProperty.AddOwner(typeof(ComboBoxWithLabeledTextInput));

    private static readonly DependencyProperty SelectedIndexProperty =
      ComboBox.SelectedIndexProperty.AddOwner(typeof(ComboBoxWithLabeledTextInput));
    #endregion ComboBox

    #region TextBox
    private static readonly DependencyProperty LabelTextBoxProperty =
      DependencyProperty.Register("LabelTextBox", typeof(string), typeof(ComboBoxWithLabeledTextInput));

    private static readonly DependencyProperty TextProperty =
      TextBox.TextProperty.AddOwner(typeof(ComboBoxWithLabeledTextInput));
    #endregion TextBox
    #endregion fields

    #region constructor
    public ComboBoxWithLabeledTextInput()
    {
      this.InitializeComponent();
    }
    #endregion constructor

    #region properties
    #region ComboBox
    /// <summary>
    /// Declare ItemsSource and Register as an Owner of ComboBox.ItemsSource
    /// the ComboBoxWithLabeledTextInput.xaml will bind the ComboBox.ItemsSource to this property
    /// </summary>
    public IEnumerable ItemsSource
    {
      get { return (IEnumerable)GetValue(ComboBoxWithLabeledTextInput.ItemsSourceProperty); }
      set { SetValue(ComboBoxWithLabeledTextInput.ItemsSourceProperty, value); }
    }

    /// <summary>
    /// Declare a ComboBox label dependency property
    /// </summary>
    public string LabelContent
    {
      // These proeprties can be bound to. The XAML for this control binds the Label's content to this.
      get { return (string)GetValue(ComboBoxWithLabeledTextInput.LabelContentProperty); }
      set { SetValue(ComboBoxWithLabeledTextInput.LabelContentProperty, value); }
    }

    /// <summary>
    /// MSDN Reference: http://msdn.microsoft.com/en-us/library/system.windows.controls.combobox.aspx
    /// </summary>
    public string DisplayMemberPath
    {
      get { return (string)GetValue(ComboBoxWithLabeledTextInput.DisplayMemberPathProperty); }
      set { SetValue(ComboBoxWithLabeledTextInput.DisplayMemberPathProperty, value); }
    }

    /// <summary>
    /// MSDN Reference: http://msdn.microsoft.com/en-us/library/system.windows.controls.combobox.aspx
    /// </summary>
    public string SelectedValuePath
    {
      get { return (string)GetValue(ComboBoxWithLabeledTextInput.SelectedValuePathProperty); }
      set { SetValue(ComboBoxWithLabeledTextInput.SelectedValuePathProperty, value); }
    }

    /// <summary>
    /// MSDN Reference: http://msdn.microsoft.com/en-us/library/system.windows.controls.combobox.aspx
    /// </summary>
    public object SelectedItem
    {
      get { return (object)GetValue(ComboBoxWithLabeledTextInput.SelectedItemProperty); }
      set { SetValue(ComboBoxWithLabeledTextInput.SelectedItemProperty, value); }
    }

    /// <summary>
    /// MSDN Reference: http://msdn.microsoft.com/en-us/library/system.windows.controls.combobox.aspx
    /// </summary>
    public object SelectedValue
    {
      get { return (object)GetValue(ComboBoxWithLabeledTextInput.SelectedValueProperty); }
      set { SetValue(ComboBoxWithLabeledTextInput.SelectedValueProperty, value); }
    }

    /// <summary>
    /// MSDN Reference: http://msdn.microsoft.com/en-us/library/system.windows.controls.combobox.aspx
    /// </summary>
    public int SelectedIndex
    {
      get { return (int)GetValue(ComboBoxWithLabeledTextInput.SelectedIndexProperty); }
      set { SetValue(ComboBoxWithLabeledTextInput.SelectedIndexProperty, value); }
    }
    #endregion ComboBox

    #region TextBox
    /// <summary>
    /// Declare a TextBox label dependency property
    /// </summary>
    public string LabelTextBox
    {
      // These proeprties can be bound to. The XAML for this control binds the Label's content to this.
      get { return (string)GetValue(ComboBoxWithLabeledTextInput.LabelTextBoxProperty); }
      set { SetValue(ComboBoxWithLabeledTextInput.LabelTextBoxProperty, value); }
    }

    /// <summary>
    /// Declare a TextBox Text dependency property
    /// </summary>
    public string Text
    {
      // These proeprties can be bound to. The XAML for this control binds the Label's content to this.
      get { return (string)GetValue(ComboBoxWithLabeledTextInput.TextProperty); }
      set { SetValue(ComboBoxWithLabeledTextInput.TextProperty, value); }
    }
    #endregion TextBox
    #endregion properties
  }
}
