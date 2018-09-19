using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Core.ToePac
{
    public class ToePackage : INotifyPropertyChanged
    {
        public const string FileTypeConst = "PACT";
        public const int VersionConst = 1;
        string _FileType = "PACT";
        int _Version = 1;
        ListResourse _Items = new ListResourse();

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }


        public ToePackage() { }

        public ToePackage(Stream data)
        {
            Deserialization(data);
        }

        public MemoryStream Serialization()
        {
            MemoryStream ms = new MemoryStream();
            var data = new BinaryWriter(ms);
     
            data.Write(this.FileType);
        
            data.Write(this.Version);

            data.Write(this.Items.Count);
            ms.SetLength(64);
            // data.Flush();

            this.Items.Serialization(ms);
    

            //   Debug.WriteLine(ms.Position);

         //   Debug.WriteLine(ms.Position - d);

            data.Flush();



            return ms;
        }

       /* public static PAC Deserialization(Stream pac)
        {
            return new PAC(pac);
        }*/


            public void Deserialization(Stream  pac)
        {

   


            BinaryReader br = new BinaryReader(pac);


            pac.Position = 0;
      

            this._FileType = br.ReadString();
            this._Version = br.ReadInt32();
            if (this.FileType != FileTypeConst)
                throw new Exception("Неверный тип файла");
           if (this.Version != VersionConst)
                throw new Exception("Неверный тип файла");

            this.Items.Deserialization(pac);





        }

        /// <summary>
        /// Тип файла
        /// </summary>
        public string FileType { get => _FileType; }
        /// <summary>
        /// Версия
        /// </summary>
        public int Version { get => _Version; }

        public ListResourse Items { get => _Items; }

    }
}
