using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LottieViewer.Model
{
    public class AnimationFile : NotifyBase
    {
        private string _name;
        private string _fullName;

        public string Name { get => _name; set { _name = value; OnPropertyChanged(); } }
        public string FullName { get => _fullName; set { _fullName = value; OnPropertyChanged(); } }
    }
}
