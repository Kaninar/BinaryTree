using System.Collections.ObjectModel;
using System.Security.Cryptography;

namespace Cours_Task5_Tree
{
    public partial class MainPage : ContentPage
    {
        BinaryTree _Tree;
        public MainPage()
        {
            InitializeComponent();

        }
        ObservableCollection<int> Numbers;
        private void CreateBinaryTree(int[] arr)
        {
            var Tree = new BinaryTree(arr);
            _Tree = Tree;
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

        private (int, int) FindMostLongestLevel(BinaryTree tree)
        {
            
            int LevelsCount = tree._MainRoot.Height;
            
            int[] LevelsCounting = new int[LevelsCount];
            LevelsCounting[0] = 1;
            ReturnCount(tree._MainRoot, LevelsCounting);

            int LongestLevelIndex = 0;
            int LongestLevel = 1;
            for (int i = 1; i < LevelsCount; i++)
            {
                if (LevelsCounting[i] > LongestLevel)
                { 
                    LongestLevel = LevelsCounting[i];
                    LongestLevelIndex = i;
                }
            }
            return (LongestLevelIndex + 1, LevelsCounting[LongestLevelIndex]);
        }
        private void ReturnCount(BinaryTreeNode parent, int[]counting, int level = 1)
        {
            if (level >= _Tree.Height)
                return;
            int LevelIndex = 8 - parent.Height;
            int Count = 0;

            if (parent.LeftChildNode != null)
            {
                Count++;
                ReturnCount(parent.LeftChildNode, counting, level+1);
            }

            if (parent.RightChildNode != null)
            { 
                Count++;
                ReturnCount(parent.RightChildNode, counting, level+1) ;
            }

            counting[level] += Count;
        }

        private void ButtonBuildTree_Clicked(object sender, EventArgs e)
        {
            CreateBinaryTree(Numbers.ToArray());
            (int Level, int Count) Result = FindMostLongestLevel(_Tree);
            LabelAnswer.Text = $"Больше всего узлов на {Result.Level} уровне ({Result.Count} листьев) (Сверху-Вниз)";
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
