using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.FileProperties;
using Windows.Storage.Streams;
using Windows.Storage;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Controls;
using Database.Models;

namespace SpaceInvaders
{
    
    public class ViewModel
    {
        public ObservableCollection<Skin> SkinsList { get; } =
      new ObservableCollection<Skin>();

        public void ThumbRemove(int index)
        {

            SkinsList.RemoveAt(index);
        }

        public void ThumbUpdate(Skin skin, int index)
        {
            SkinsList.RemoveAt(index);
            SkinsList.Insert(index, new Skin(skin.skin));

        }


    }
}
