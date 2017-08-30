using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace GTI.Modules.ProductCenter.UI.Discounts
{
    /// <summary>
    /// Interaction logic for MultiSelectComboBox.xaml
    /// </summary>
    public partial class MultiSelectComboBox : UserControl
    {
        private const string ALL_SELECTED_TEXT = "All";

        private bool m_allSelected = false;
        private ObservableCollection<Node> nodeList;

        #region Constructor
        public MultiSelectComboBox()
        {
            InitializeComponent();
            nodeList = new ObservableCollection<Node>();
            //SelectedItems = new Dictionary<int, string>();
        }
        #endregion

        #region Dependency Properties

        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register("ItemsSource", typeof(Dictionary<int, string>),
            typeof(MultiSelectComboBox), new UIPropertyMetadata(null, new PropertyChangedCallback(MultiSelectComboBox.OnItemsSourceChanged)));
        public static readonly DependencyProperty SelectedItemsProperty = DependencyProperty.Register("SelectedItems", typeof(Dictionary<int, string>),
            typeof(MultiSelectComboBox), new UIPropertyMetadata(null, new PropertyChangedCallback(MultiSelectComboBox.OnSelectedItemsChanged)));
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), 
            typeof(MultiSelectComboBox), new UIPropertyMetadata(string.Empty));
        public static readonly DependencyProperty DefaultTextProperty = DependencyProperty.Register("DefaultText", typeof(string), 
            typeof(MultiSelectComboBox), new UIPropertyMetadata(string.Empty));

        public void SetToEmpty()
        {
            this.Text = this.DefaultText;
        }

        public Dictionary<int, string> ItemsSource
        {
            get 
            { 
                return (Dictionary<int, string>)GetValue(ItemsSourceProperty); 
            }
            set
            {
                SetValue(ItemsSourceProperty, value);
            }
        }

        public Dictionary<int, string> SelectedItems
        {
            get 
            { 
                return (Dictionary<int, string>)GetValue(SelectedItemsProperty);
            }
            set
            {
                SetValue(SelectedItemsProperty, value);
            }
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public string DefaultText
        {
            get { return (string)GetValue(DefaultTextProperty); }
            set { SetValue(DefaultTextProperty, value); }
        }
        #endregion

        #region Events
        /// <summary>
        /// Actions that occur when the list of available items changes
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MultiSelectComboBox control = (MultiSelectComboBox)d;
            control.DisplayInControl();
        }

        /// <summary>
        /// Actions that occur when the "selected items" source changes
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnSelectedItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MultiSelectComboBox control = (MultiSelectComboBox)d;
            control.SelectNodes();
            control.SetText();
        }

        /// <summary>
        /// Actions that occur when one of the contained checkbox's status changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            CheckBox clickedBox = (CheckBox)sender;
            bool isChecked = clickedBox.IsChecked ?? false;

            if (String.Equals(clickedBox.Content.ToString(), ALL_SELECTED_TEXT))
            {
                m_allSelected = isChecked;
                foreach (Node node in nodeList)
                {
                    node.IsSelected = isChecked;
                }
            }
            else
            {
                int _selectedCount = 0;
                foreach (Node s in nodeList)
                {
                    if (s.IsSelected && !String.Equals(s.Title, ALL_SELECTED_TEXT))
                        _selectedCount++;
                    else if (!isChecked && m_allSelected && String.Equals(s.Title, ALL_SELECTED_TEXT)) // uncheck the "all" checkbox
                    {
                        s.IsSelected = false;
                        m_allSelected = false;
                    }
                }

                //if (_selectedCount == nodeList.Count - 1)
                //    nodeList.FirstOrDefault(i => i.Title == "All").IsSelected = true;           
            }
            SetSelectedItems();
            SetText();

        }
        #endregion
        
        #region Methods

        public void UncheckAllItems()
        {
            foreach (Node node in nodeList)
            {
                node.IsSelected = false;
            }

         
            SetSelectedItems();
            SetText();
        }

        /// <summary>
        /// Sets status of the contained checkboxes to the list of the selected items
        /// </summary>
        private void SelectNodes()
        {
            if (SelectedItems == null)
                SelectedItems = new Dictionary<int, string>();

            if (SelectedItems.Any(x => String.Equals(x.Value, ALL_SELECTED_TEXT)))   // if all are selected, go through contained list, select everything, and return early
            {
                m_allSelected = true;
                foreach (Node box in nodeList)
                {
                    box.IsSelected = true;
                }
            }
            else
            {
                foreach (var node in nodeList) // reset all the values
                    node.IsSelected = false;

                foreach (KeyValuePair<int, string> keyValue in SelectedItems) // select all in the sent-in list
                {
                    Node node = nodeList.FirstOrDefault(i => i.Id == keyValue.Key);
                    if (node != null) // else add it? Not sure; leaving alone for now.
                        node.IsSelected = true;
                }
            }
        }

        /// <summary>
        /// Sets the 'list of selected items' to the list of items that are checked in the UI
        /// </summary>
        private void SetSelectedItems()
        {
            if (SelectedItems == null)
            {
                SelectedItems = new Dictionary<int, string>();
            }
                
            SelectedItems.Clear();

            foreach (Node node in nodeList)
            {
                if (node.IsSelected)// && node.Title != ALL_SELECTED_TEXT)
                {
                    if (this.ItemsSource.Count > 0)
                    {
                        SelectedItems.Add(ItemsSource.FirstOrDefault(x => x.Key == node.Id).Key, node.Title);
                    }
                }
            }
        }

        private void DisplayInControl()
        {
            nodeList.Clear();
            foreach (KeyValuePair<int, string> keyValue in this.ItemsSource)
            {
                Node node = new Node(keyValue.Value, keyValue.Key);
                nodeList.Add(node);
            }
            MultiSelectCombo.ItemsSource = nodeList;
        }

        /// <summary>
        /// Sets the displayed text to reflect the list of items that are checked
        /// </summary>
        private void SetText()
        {
            if (this.SelectedItems != null)
            {
                StringBuilder displayText = new StringBuilder();
                foreach (Node s in nodeList)
                {
                    if (s.IsSelected == true && s.Title == ALL_SELECTED_TEXT)
                    {
                        displayText = new StringBuilder();
                        displayText.Append("All");
                        break;
                    }
                    else if (s.IsSelected == true && s.Title != ALL_SELECTED_TEXT)
                    {
                        displayText.Append(s.Title);
                        displayText.Append(',');
                    }
                }
                this.Text = displayText.ToString().TrimEnd(new char[] { ',' });
            }

            if (string.IsNullOrEmpty(this.Text))
            {
                this.Text = this.DefaultText;
            }
        }

        /// <summary>
        /// Returns whether or not all items are checked
        /// </summary>
        public bool AllItemsSelected
        {
            get
            {
                return String.Equals(this.Text, ALL_SELECTED_TEXT);
            }
        }

        #endregion
    }

    public class Node : INotifyPropertyChanged
    {
        #region Member Variable

        private string Member_title;
        private bool Member_isSelected;
        private int Member_id;

        #endregion

        #region ctor

        public Node(string title, int id)
        {
            Title = title;
            Id = id;
        }
        #endregion

        #region Properties

        public int Id
        {
            get
            {
                return Member_id;
            }
            set
            {
                Member_id = value;
                NotifyPropertyChanged("Id");
            }
        }

        public string Title
        {
            get
            {
                return Member_title;
            }
            set
            {
                Member_title = value;
                NotifyPropertyChanged("Title");
            }
        }
        public bool IsSelected
        {
            get
            {
                return Member_isSelected;
            }
            set
            {
                Member_isSelected = value;
                NotifyPropertyChanged("IsSelected");
            }
        }

        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;
       
        #endregion

    }
}
