namespace ICSharpCode.AvalonEdit.Edi.BlockSurround
{
  public class BlockDefinition
  {
    #region constructor
    /// <summary>
    /// Class constructor
    /// </summary>
    public BlockDefinition(string blockstart,
                         string blockend,
                         BlockAt typeOfBlock,
                         string fileextension,
                         System.Windows.Input.Key key,
                         System.Windows.Input.ModifierKeys modifier = 0)
      : this()
    {
      this.TypeOfBlock = typeOfBlock;
      this.StartBlock = blockstart;
      this.EndBlock = blockend;
      this.FileExtension = fileextension;

      this.Key = key;
      this.Modifier = modifier;
    }

    /// <summary>
    /// Hide standard constructor to ensur construction with minimal
    /// required data items.
    /// </summary>
    /// <returns></returns>
    protected BlockDefinition()
    {
      this.TypeOfBlock = BlockAt.StartAndEnd;
      this.StartBlock = this.EndBlock = this.FileExtension = string.Empty;

      this.Key = System.Windows.Input.Key.D1;
      this.Modifier = System.Windows.Input.ModifierKeys.Control;
    }
    #endregion constructor

    #region enum
    public enum BlockAt
    {
      Start,
      End,
      StartAndEnd,
    }
    #endregion enum

    #region properties
    /// <summary>
    /// Get/set type of block selection/change
    /// </summary>
    public BlockAt TypeOfBlock { get; set; }

    public string StartBlock { get; set; }

    public string EndBlock { get; set; }

    public string FileExtension { get; set; }

    public System.Windows.Input.Key Key { get; set; }

    public System.Windows.Input.ModifierKeys Modifier { get; set; }
    #endregion properties
  }
}
