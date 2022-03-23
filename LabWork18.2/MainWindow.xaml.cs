using System;
using System.IO;
using System.Linq;
using System.Windows;

namespace LabWork18._2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        FileInfo[] files = new FileInfo[0];
        public int currentPage = 1;
        public int countOfRows = 5;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Load(object sender, RoutedEventArgs e)
        {
            files = new DirectoryInfo(@"C:\Users\ergei\OneDrive\Документы\Humankind").GetFiles("*", SearchOption.AllDirectories);
            NextPage(currentPage);
        }

        private void NextPage(int currentPage)
        {
            if (selectedPage.Text != "" && selectedRow.Text != "")
            {
                countOfRows = Int32.Parse(selectedRow.Text);
                currentPage = Int32.Parse(selectedPage.Text);
            }
            else if (selectedPage.Text != "" && selectedRow.Text == "")
            {
                currentPage = Int32.Parse(selectedPage.Text);
            }
            else if (selectedPage.Text == "" && selectedRow.Text != "")
            {
                countOfRows = Int32.Parse(selectedRow.Text);
            }
            dataGrid.ItemsSource = files.Select(x => new { x.Name, x.FullName, x.Length, x.CreationTime }).OrderBy(x => x.Name).Skip((currentPage - 1) * countOfRows).Take(countOfRows).ToList();
            pageNumber.Text = $"{currentPage}";
            IsButtonEnabled();
        }

        private int LastPage()
        {
            countOfRows = selectedRow.Text != "" ? Int32.Parse(selectedRow.Text) : 5;
            int filesCount = files.Count();
            return Convert.ToInt32(Math.Ceiling((double)filesCount / countOfRows));
        }

        private void IsButtonEnabled()
        {
            prevPageButton.IsEnabled = firstPageButton.IsEnabled = Int32.Parse(pageNumber.Text) > 1;
            nextPageButton.IsEnabled = lastPageButton.IsEnabled = Int32.Parse(pageNumber.Text) < LastPage();
        }

        private void ShowInfo_ButtonClick(object sender, RoutedEventArgs e)
        {
            NextPage(currentPage);
        }

        private void FirstPage_ButtonClick(object sender, RoutedEventArgs e)
        {
            countOfRows = selectedRow.Text != "" ? Int32.Parse(selectedRow.Text) : 5;
            dataGrid.ItemsSource = files.Select(x => new { x.Name, x.FullName, x.Length, x.CreationTime }).OrderBy(x => x.Name)
                    .Skip(currentPage - 1).Take(countOfRows).ToList();
            pageNumber.Text = $"{currentPage}";
        }

        private void PrevPage_ButtonClick(object sender, RoutedEventArgs e)
        {
            currentPage -= 1;
            NextPage(currentPage);
        }

        private void NextPage_ButtonClick(object sender, RoutedEventArgs e)
        {
            currentPage += 1;
            NextPage(currentPage);
        }

        private void LastPage_ButtonClick(object sender, RoutedEventArgs e)
        {
            currentPage = LastPage();
            NextPage(currentPage);
        }
    }
}
