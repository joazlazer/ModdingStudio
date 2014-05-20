namespace SimpleControls.MRU.ViewModel
{
  using System;
  using System.Linq;
  using System.Linq.Expressions;
  using System.IO;
  using System.Windows;
  using System.Windows.Input;
  using System.Xml.Serialization;
  using Microsoft.Win32;

  using Model;
  using SimpleControls.Command;
  using System.Collections.Generic;
  using System.Collections.ObjectModel;

  /// <summary>
  /// This enumeration is used to control the behaviour of pinned entries.
  /// </summary>
  public enum MRUSortMethod
  {
    /// <summary>
    /// Pinned entries are sorted and displayed at the beginning of the list or just be bookmarked
    /// and stay wehere they are in the list.
    /// </summary>
    PinnedEntriesFirst = 0,

    /// <summary>
    /// Pinned entries are just be bookmarked and stay wehere they are in the list. This can be useful
    /// for a list of favourites (which stay if pinned) while other unpinned entries are changed as the
    /// user keeps opening new items and thus, changing the MRU list...
    /// </summary>
    UnsortedFavourites = 1
  }

  public class MRUListVM : Base.BaseViewModel
  {
    #region Fields
    private MRUSortMethod mPinEntryAtHeadOfList = MRUSortMethod.PinnedEntriesFirst;

    private ObservableCollection<MRUEntryVM> mListOfMRUEntries;
    
    private int mMaxMruEntryCount;

    private RelayCommand _removeLastEntryCommand;
    private RelayCommand _removeFirstEntryCommand;
    #endregion Fields

    #region Constructor
    public MRUListVM()
    {
      this.mMaxMruEntryCount = 15;
      this.mPinEntryAtHeadOfList = MRUSortMethod.PinnedEntriesFirst;
    }

    public MRUListVM(MRUSortMethod pinEntryAtHeadOfList = MRUSortMethod.PinnedEntriesFirst)
      : this()
    {
      this.mPinEntryAtHeadOfList = pinEntryAtHeadOfList;
    }
    #endregion Constructor

    #region Properties
    [XmlAttribute(AttributeName = "MinValidMRUCount")]
    public int MinValidMruEntryCount
    {
      get
      {
        return 5;
      }
    }

    [XmlAttribute(AttributeName = "MaxValidMRUCount")]
    public int MaxValidMruEntryCount
    {
      get
      {
        return 256;
      }
    }

    [XmlAttribute(AttributeName = "MaxMruEntryCount")]
    public int MaxMruEntryCount
    {
      get
      {
        return this.mMaxMruEntryCount;
      }

      set
      {
        if(this.mMaxMruEntryCount != value)
        {
          if (value < this.MinValidMruEntryCount || value > this.MaxValidMruEntryCount)
            throw new ArgumentOutOfRangeException("MaxMruEntryCount", value, "Valid values are: value >= 5 and value <= 256");

          this.mMaxMruEntryCount = value;

          this.NotifyPropertyChanged(() => this.MaxMruEntryCount);
        }
      }
    }

    /// <summary>
    /// Get/set property to determine whether a pinned entry is shown
    /// 1> at the beginning of the MRU list
    /// or
    /// 2> remains where it currently is.
    /// </summary>
    [XmlAttribute(AttributeName = "SortMethod")]
    public MRUSortMethod PinSortMode
    {
      get
      {
        return this.mPinEntryAtHeadOfList;
      }

      set
      {
        if (this.mPinEntryAtHeadOfList != value)
        {
          this.mPinEntryAtHeadOfList = value;
          this.NotifyPropertyChanged(() => this.PinSortMode);
        }
      }
    }

    [XmlArrayItem("MRUList", IsNullable = false)]
    public ObservableCollection<MRUEntryVM> ListOfMRUEntries
    {
      get
      {
        return this.mListOfMRUEntries;
      }

      set
      {
        if (this.mListOfMRUEntries != value)
        {
          this.mListOfMRUEntries = value;

          this.NotifyPropertyChanged(() => this.ListOfMRUEntries);
        }
      }
    }

    #region RemoveEntryCommands
    public ICommand RemoveFirstEntryCommand
    {
      get
      {
        if (_removeFirstEntryCommand == null)
          _removeFirstEntryCommand =
              new RelayCommand(() => this.OnRemoveMRUEntry(Model.MRUList.Spot.First));

        return _removeFirstEntryCommand;
      }
    }
    
    public ICommand RemoveLastEntryCommand
    {
      get
      {
        if (_removeLastEntryCommand == null)
          _removeLastEntryCommand = new RelayCommand(() => this.OnRemoveMRUEntry(Model.MRUList.Spot.Last));

        return _removeLastEntryCommand;
      }
    }

    #endregion RemoveEntryCommands
    #endregion Properties

    #region Methods
    #region AddRemove Methods
    private void OnRemoveMRUEntry(Model.MRUList.Spot addInSpot = Model.MRUList.Spot.Last)
    {
      if (this.mListOfMRUEntries == null)
        return;

      if (this.mListOfMRUEntries.Count == 0)
        return;

      switch (addInSpot)
      {
        case MRUList.Spot.Last:
          this.mListOfMRUEntries.RemoveAt(this.mListOfMRUEntries.Count - 1);
          break;
        case MRUList.Spot.First:
          this.mListOfMRUEntries.RemoveAt(0);
          break;

        default:
          break;
      }

      //// this.NotifyPropertyChanged(() => this.ListOfMRUEntries);
    }

    private int CountPinnedEntries()
    {
      if (this.mListOfMRUEntries != null)
        return this.mListOfMRUEntries.Count(mru => mru.IsPinned == true);

      return 0;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="bPinOrUnPinMruEntry"></param>
    /// <param name="mruEntry"></param>
    public bool PinUnpinEntry(bool bPinOrUnPinMruEntry, MRUEntryVM mruEntry)
    {
      try
      {
        if (this.mListOfMRUEntries == null)
          return false;

        int PinnedMruEntryCount = this.CountPinnedEntries();

        // pin an MRU entry into the next available pinned mode spot
        if(bPinOrUnPinMruEntry == true)
        {
          MRUEntryVM e = this.mListOfMRUEntries.Single(mru => mru.IsPinned == false && mru.PathFileName == mruEntry.PathFileName);

          if (this.PinSortMode == MRUSortMethod.PinnedEntriesFirst)
            this.mListOfMRUEntries.Remove(e);

          e.IsPinned = true;

          if (this.PinSortMode == MRUSortMethod.PinnedEntriesFirst)
            this.mListOfMRUEntries.Insert(PinnedMruEntryCount, e);

          PinnedMruEntryCount += 1;
          //// this.NotifyPropertyChanged(() => this.ListOfMRUEntries);

          return true;
        }
        else
        {
          // unpin an MRU entry into the next available unpinned spot
          MRUEntryVM e = this.mListOfMRUEntries.Single(mru => mru.IsPinned == true && mru.PathFileName == mruEntry.PathFileName);

          if (this.PinSortMode == MRUSortMethod.PinnedEntriesFirst)
            this.mListOfMRUEntries.Remove(e);
          
          e.IsPinned = false;
          PinnedMruEntryCount -= 1;

          if (this.PinSortMode == MRUSortMethod.PinnedEntriesFirst)
            this.mListOfMRUEntries.Insert(PinnedMruEntryCount, e);

          //// this.NotifyPropertyChanged(() => this.ListOfMRUEntries);

          return true;
        }
      }
      catch (Exception exp)
      {
        MessageBox.Show(this.AppName + " encountered an error when pinning an entry:" + Environment.NewLine
                      + Environment.NewLine
                      + exp.ToString(), "Error when pinning an MRU entry", MessageBoxButton.OK, MessageBoxImage.Error);
      }

      return  false;
    }

    /// <summary>
    /// Standard short-cut method to add a new unpinned entry from a string
    /// </summary>
    /// <param name="newEntry">File name and path file</param>
    public void AddMRUEntry(string newEntry)
    {
      if (newEntry == null || newEntry == string.Empty)
        return;

      this.AddMRUEntry(new MRUEntryVM() { IsPinned = false, PathFileName = newEntry });
    }

    public void AddMRUEntry(MRUEntryVM newEntry)
    {
      if (newEntry == null) return;

      try
      {
        if (this.mListOfMRUEntries == null)
          this.mListOfMRUEntries = new ObservableCollection<MRUEntryVM>();

        // Remove all entries that point to the path we are about to insert
        MRUEntryVM e = this.mListOfMRUEntries.SingleOrDefault(item => newEntry.PathFileName == item.PathFileName);

        if (e != null)
        {
          // Do not change an entry that has already been pinned -> its pinned in place :)
          if (e.IsPinned == true)
            return;

          this.mListOfMRUEntries.Remove(e);
        }

        // Remove last entry if list has grown too long
        if (this.MaxMruEntryCount <= this.mListOfMRUEntries.Count)
          this.mListOfMRUEntries.RemoveAt(this.mListOfMRUEntries.Count-1);

        // Add model entry in ViewModel collection (First pinned entry or first unpinned entry)
        if(newEntry.IsPinned == true)
          this.mListOfMRUEntries.Insert(0, new MRUEntryVM(newEntry));
        else
        {
          this.mListOfMRUEntries.Insert(this.CountPinnedEntries(), new MRUEntryVM(newEntry));
        }
      }
      catch (Exception exp)
      {
        MessageBox.Show(exp.ToString(), "An error has occurred", MessageBoxButton.OK, MessageBoxImage.Error);
      }
      ////finally
      ////{
      ////   this.NotifyPropertyChanged(() => this.ListOfMRUEntries);
      ////}
    }

    public bool RemoveEntry(string fileName)
    {
      try
      {
        if (this.mListOfMRUEntries == null)
          return false;

        MRUEntryVM e = this.mListOfMRUEntries.Single(mru => mru.PathFileName == fileName);

        this.mListOfMRUEntries.Remove(e);

        //// this.NotifyPropertyChanged(() => this.ListOfMRUEntries);

        return true;
      }
      catch (Exception exp)
      {
        MessageBox.Show(this.AppName + " encountered an error when removing an entry:" + Environment.NewLine
                      + Environment.NewLine
                      + exp.ToString(), "Error when pinning an MRU entry", MessageBoxButton.OK, MessageBoxImage.Error);
      }

      return false;
    }

    public bool RemovePinEntry(MRUEntryVM mruEntry)
    {
      try
      {
        if (this.mListOfMRUEntries == null)
          return false;

        MRUEntryVM e = this.mListOfMRUEntries.Single(mru => mru.PathFileName == mruEntry.PathFileName);

        this.mListOfMRUEntries.Remove(e);

        //// this.NotifyPropertyChanged(() => this.ListOfMRUEntries);

        return true;
      }
      catch (Exception exp)
      {
        MessageBox.Show(this.AppName + " encountered an error when removing an entry:" + Environment.NewLine
                      + Environment.NewLine
                      + exp.ToString(), "Error when pinning an MRU entry", MessageBoxButton.OK, MessageBoxImage.Error);
      }

      return false;
    }
    #endregion AddRemove Methods

    public MRUEntryVM FindMRUEntry(string filePathName)
    {
      try
      {
        if (this.mListOfMRUEntries == null)
          return null;

        return this.mListOfMRUEntries.SingleOrDefault(mru => mru.PathFileName == filePathName);
      }
      catch (Exception exp)
      {
        MessageBox.Show(this.AppName + " encountered an error when removing an entry:" + Environment.NewLine
                      + Environment.NewLine
                      + exp.ToString(), "Error when pinning an MRU entry", MessageBoxButton.OK, MessageBoxImage.Error);

        return null;
      }
    }

    private string AppName
    {
      get
      {
        return Application.ResourceAssembly.GetName().Name;
      }
    }
    #endregion Methods
  }
}
