using Assets.Core.Game.Data;
using Assets.Core.ToePac;
using GalaSoft.MvvmLight;
using GameСreator.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameСreator.ViewModel
{
    public class FilePackage : ObservableObject
    {
        private bool _isActive = true;

        public FilePackage(string path, bool newfile = false)
        {

            File = new FileInfo(path);
            if (newfile)
                Package = new ToePackage();
            else
                Package = new ToePackage(File.Open(FileMode.Open));
            ResourceFileData = new ResourceFileData(Package);
        }

        public FileInfo File { get; }
        public ToePackage Package { get;  }
        public bool IsActive { get => _isActive; set => this.Set("IsActive", ref _isActive, value); }
        public ResourceFileData ResourceFileData { get;  }

        public override string ToString() => File.Name;

        public void Save()
        {
            Package.Items.Clear();
            Package.Items.AddRange(ResourceFileData.GetListResourse());

            using (var fs = File.Open(FileMode.OpenOrCreate))
            {
                fs.SetLength(0);
                Package.Serialization().WriteTo(fs);
 
            }
                
        }
    }
}
