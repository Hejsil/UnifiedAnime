using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using UnifiedAnime.AniList;
using UnifiedAnime.Model;
using UnifiedAnime.WPF.Annotations;

namespace UnifiedAnime.WPF.Views
{
    /// <summary>
    /// Interaction logic for SearchControl.xaml
    /// </summary>
    public partial class SearchControl : UserControl, INotifyPropertyChanged
    {
        public IAnimeBrowser Browser => ((MainWindow) Parent).Browser;

        private IAnimeInfo[] _animes;
        public IAnimeInfo[] Animes
        {
            get => _animes;
            set
            {
                _animes = value;
                OnPropertyChanged();
            }
        }

        public SearchControl()
        {
            InitializeComponent();
        }

        private void SearchBox_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
                Animes = Browser.SearchAnime(((TextBox) sender).Text).Data;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
