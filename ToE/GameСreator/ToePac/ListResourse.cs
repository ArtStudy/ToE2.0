using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameСreator.ToePac
{
    public class ListResourse : List<Item> , INotifyPropertyChanged
    {
        long StartPosition = 64;
        Stream stream;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        public List<Item> GetResourcesByType(FileTypes filetype) => this.FindAll((item) => item.FileType == filetype);




        public ListResourse()
        {

        }
        public ListResourse(Stream data)
        {
            Deserialization(data);
        }

        public void Serialization(Stream st)
        {
            st.Position = 64;
        
            st.SetLength(64 + (this.Count * 128));
            st.Position = 64;
            for (var i = 0; i < this.Count; i++)
            {
                st.Position = 64 + (128 * i);
                var item = this[i];
                item.Serialization(st);
                //  Debug.WriteLine(ms.Position - d);
            }
        }
        public void Deserialization(Stream data)
        {
            BinaryReader br = new BinaryReader(data);
            var countitem = br.ReadInt32();

            for (int i = 0; i < countitem; i++)
            {
                data.Position = 64 + (128 * i);

                this.Add(new Item(data));

            }

        }


    }
}
