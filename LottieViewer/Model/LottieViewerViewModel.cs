using System.Collections.ObjectModel;

namespace LottieViewer.Model
{
    public class LottieViewerViewModel : NotifyBase
    {
        private ObservableCollection<AnimationFile> _animations;

        public ObservableCollection<AnimationFile> Animations
        {
            get { return _animations; }
            set { _animations = value; OnPropertyChanged(); }
        }

        public LottieViewerViewModel()
        {
            Animations = new ObservableCollection<AnimationFile>();
        }
    }
}
