using System.Collections.ObjectModel;
using System.Security.Cryptography;

namespace Cours_Task5_Tree
{
    public partial class MainPage : ContentPage
    {
        
        public MainPage()
        {
            InitializeComponent();

        }
        ObservableCollection<int> Numbers;
        private void CreateBinaryTree(int[] arr)
        {
            var Tree = new BinaryTree(arr);
            Board.Drawable = new GraphicsDrawable(Tree);
            
        }
        private int[] CreateArray() 
        {
            try
            {
                int N = int.Parse(EntryN.Text);
                int Min = int.Parse(EntryMin.Text);
                int Max = int.Parse(EntryMax.Text);

                int[] ints = new int[N];
                Random random = new();

                for (int i = 0; i < N; i++)
                {
                    ints[i] = random.Next(Min, Max);
                }

                Numbers = new(ints);
            }
            catch
            {
                DisplayAlert("Ошибка", "Неправильынй ввод!", "ОК");
            }
            return null;
        }

        private void ButtonCreateArray_Clicked(object sender, EventArgs e)
        {
            CreateArray();
            ListNumbers.ItemsSource = Numbers;
            if (Numbers != null)
            {
                ButtonBuildTree.IsEnabled = true;
            }
            else
            { 
                ButtonBuildTree.IsEnabled = false;
            }
        }

        /*private int FindMostLongestLevel(BinaryTreeNode tree)
        {
            if (tree == null)
                return -1;

            int LevelsCount = tree._MainRoot.Height;
            int LogngestLevelNodesCount = 1;
            
            foreach(var node in new[] { tree.LeftChildNode, tree.RightChildNode})
        }
        private int*/

        private void ButtonBuildTree_Clicked(object sender, EventArgs e)
        {
            CreateBinaryTree(Numbers.ToArray());
        }
        private bool IsEnableButton()
        {
            if (EntryMin.Text != null &&
                EntryMax.Text != null &&
                EntryN.Text != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            if(e.NewTextValue == string.Empty)
            {
                return;
            }

            ButtonCreateArray.IsEnabled = IsEnableButton();   
        }

        private void EntryN_Completed(object sender, EventArgs e)
        {
            EntryMin.Focus();
        }

        private void EntryMin_Completed(object sender, EventArgs e)
        {
            EntryMax.Focus();
        }

        private void EntryMax_Completed(object sender, EventArgs e)
        {
            if (IsEnableButton())
            {
                CreateArray();
            }
        }
    }
    
}
