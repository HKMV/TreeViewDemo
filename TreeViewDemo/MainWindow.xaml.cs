using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TreeViewDemo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// <see cref="PartLMTrees" /> 这是属性的名称.
        /// </summary>
        public const string PartLMTreesPropertyName = "PartLMTrees";

        private List<PartLMTreeData> _partLMTrees;

        /// <summary>
        /// Sets 和 gets PartLMTrees 的属性。
        /// 对该属性值的更改将引发PropertyChanged事件。
        /// </summary>
        public List<PartLMTreeData> PartLMTrees
        {
            get
            {
                if (_partLMTrees == null)
                {
                    _partLMTrees = new List<PartLMTreeData>();
                    _partLMTrees.Add(new PartLMTreeData("墙", new List<PartLMTreeData>() {
                        new PartLMTreeData("基本墙01","1000"),
                        new PartLMTreeData("基本墙02","1001"),
                        new PartLMTreeData("基本墙03","1002"),
                        new PartLMTreeData("基本墙04","1003")
                    }));
                    _partLMTrees.Add(new PartLMTreeData("窗", new List<PartLMTreeData>() {
                        new PartLMTreeData("基本窗01","2000"),
                        new PartLMTreeData("基本窗02","2001"),
                        new PartLMTreeData("基本窗03","2002"),
                        new PartLMTreeData("基本窗04","2003")
                    }));
                }
                return _partLMTrees;
            }

            set
            {
                if (_partLMTrees == value)
                {
                    return;
                }

                _partLMTrees = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PartLMTreesPropertyName));
            }
        }

        //        List<PartLMTreeData>
        private RelayCommand<PartLMTreeData> _checkCommand;

        /// <summary>
        /// CheckCommand 事件.
        /// </summary>
        public RelayCommand<PartLMTreeData> CheckCommand
        {
            get
            {
                return _checkCommand
                    ?? (_checkCommand = new RelayCommand<PartLMTreeData>(
                    (pt) =>
                    {
                        if (pt.childrenList == null)
                        {
                            return;
                        }

                        bool isCheck = pt.isCheck;
                        pt.childrenList.ForEach((child) => { child.isCheck = isCheck; });
                    }));
            }
        }

        /*private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //测试数据

            List<PartLMTreeData> partLMTrees = new List<PartLMTreeData>();
            partLMTrees.Add(new PartLMTreeData("墙", new List<PartLMTreeData>() {
               new PartLMTreeData("基本墙01","1000"),
               new PartLMTreeData("基本墙02","1001"),
               new PartLMTreeData("基本墙03","1002"),
               new PartLMTreeData("基本墙04","1003")
            }));
            partLMTrees.Add(new PartLMTreeData("窗", new List<PartLMTreeData>() {
               new PartLMTreeData("基本窗01","2000"),
               new PartLMTreeData("基本窗02","2001"),
               new PartLMTreeData("基本窗03","2002"),
               new PartLMTreeData("基本窗04","2003")
            }));

            this.tv_selElemList.ItemsSource = partLMTrees;
        }

        //CheckBox单击事件
        private void tv_cbClick(object sender, RoutedEventArgs e)
        {
        }

        //treeView Change事件
        private void tv_selectElem(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;

            Debug.WriteLine(sender.ToString());
        }*/
    }

    public class PartLMTreeData : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public PartLMTreeData()
        {
        }

        public PartLMTreeData(string itemName, string itemId)
        {
            this.itemName = itemName;
            this.itemId = itemId;
        }

        public PartLMTreeData(string itemName, List<PartLMTreeData> childrenList)
        {
            this.itemName = itemName;
            this.childrenList = childrenList;
        }

        public string itemName { get; set; }

        public string itemId { get; set; }

        private bool _isCheck;

        public bool isCheck
        {
            get => _isCheck;
            set
            {
                if (_isCheck.Equals(value))
                {
                    return;
                }

                _isCheck = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("isCheck"));
            }
        }

        public List<PartLMTreeData> childrenList { get; set; }
    }
}