using LottieSharp;
using LottieViewer.Model;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace LottieViewer
{
    /// <summary>
    /// Interaction logic for LottieViewerToolWindowControl.
    /// </summary>
    public partial class LottieViewerToolWindowControl : UserControl
    {
        private LottieViewerViewModel ViewModel { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="LottieViewerToolWindowControl"/> class.
        /// </summary>
        public LottieViewerToolWindowControl()
        {
            this.InitializeComponent();
            ViewModel = new LottieViewerViewModel();            
        }

        /// <summary>
        /// Handles click on the button by displaying a message box.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event args.</param>
        [SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions", Justification = "Sample code")]
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Default event handler naming pattern")]
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(
                string.Format(System.Globalization.CultureInfo.CurrentUICulture, "Invoked '{0}'", this.ToString()),
                "LottieViewerToolWindow");
        }


        private void OpenJsonButton_Click(object sender, RoutedEventArgs e)
        {
            var openDialog = new Microsoft.Win32.OpenFileDialog();
            openDialog.Filter = "Lottie files|*.json";
            openDialog.Title = "Select one or more Lottie files";
            openDialog.Multiselect = true;
            if(openDialog.ShowDialog() == true)
            {
                try
                {
                    foreach(var item in openDialog.FileNames)
                    {
                        var newAnimation = new AnimationFile { FullName = item, Name = System.IO.Path.GetFileName(item) };
                        ViewModel.Animations.Add(newAnimation);
                    }
                    AnimationList.ItemsSource = ViewModel.Animations;
                    LottieAnimationView.Visibility = Visibility.Visible;
                    
                    AnimationList.SelectedItem = ViewModel.Animations[0];
                }
                catch (System.Exception)
                {
                    MessageBox.Show("Unable to open the specified animation", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
        }

        private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/ascora/LottieSharp");
        }

        private void PlayJsonButton_Click(object sender, RoutedEventArgs e)
        {
            if (LottieAnimationView.FileName != null)
                LottieAnimationView.PlayAnimation();
        }

        private void PauseJsonButton_Click(object sender, RoutedEventArgs e)
        {
            if(LottieAnimationView.FileName != null)
                LottieAnimationView.PauseAnimation();
        }

        private void AnimationList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!ViewModel.Animations.Any())
                return;

            var item = e.AddedItems[0] as AnimationFile;
            if (item != null)
            {
                LottieAnimationView.FileName = item.FullName;
                LottieAnimationView.PlayAnimation();
            }
        }

        private void ClearListButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ViewModel != null)
                {
                    LottieAnimationView.CancelAnimation();

                    LottieAnimationView.Visibility = Visibility.Hidden;
                        
                    ViewModel.Animations.Clear();
                }
            }
            catch 
            {

            }
        }

        private void ResumeJsonButton_Click(object sender, RoutedEventArgs e)
        {
            if (LottieAnimationView.FileName != null)
                LottieAnimationView.ResumeAnimation();
        }

        private void NextJsonButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AnimationList.SelectedIndex += 1;
            }
            catch (System.Exception)
            {
                AnimationList.SelectedIndex = 0;
            }
        }

        private void PrevJsonButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AnimationList.SelectedIndex -= 1;
            }
            catch (System.Exception)
            {
                AnimationList.SelectedIndex = 0;
            }
        }
    }
}