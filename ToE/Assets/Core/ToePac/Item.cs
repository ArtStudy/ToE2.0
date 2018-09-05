using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Core.ToePac
{
    public class Item : INotifyPropertyChanged
    {


        Stream stream;
        private string _name = string.Empty;
        private ulong _identifier = 0;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }


        //      UInt16 _FileType = 0;
        public Item()
        {

        }
        public Item(Stream data)
        {
            Deserialization(data);
        }

        public void Serialization(Stream st, int version = 1)
        {


            stream = st;
            // stream.Position = positionitem;
            var bw = new BinaryWriter(stream);

            bw.Write(this.Identifier);
            bw.Write(this.Name.PadRight(65).Remove(64).ToCharArray());
            bw.Write((long)this.FileType);
          //  this.Version = version;
            bw.Write(this.Version);
            bw.Write(this.Length);
            this.Position = st.Length;
            bw.Write(this.Position);

            stream.Position = this.Position;
            stream.Write(this.Data.ToArray(), 0, (int) this.Data.Length);
 

            /*   BinaryReader br = new BinaryReader(this.Data);

               var b = br.ReadBytes((int)this.Data.Length);

               stream.Position = this.Position;
               stream.Write(b, 0, b.Length);*/





        }
        public Item Deserialization(Stream data)
        {

            BinaryReader br = new BinaryReader(data);


            this.Identifier = br.ReadUInt64();
            this.Name = new string(br.ReadChars(64));
            this.FileType = (FileTypes)br.ReadInt64();
            this.Version = br.ReadInt32();
            var length = br.ReadInt64();
            this.Position = br.ReadInt64();

            byte[] bytedata = new byte[length];

            data.Position = this.Position;
            data.Read(bytedata, 0, (int)length);

            this.Data = new MemoryStream(bytedata);



            return null;
        }

        public override string ToString()
        {
            return this.Name + "_" + String.Format("0x{0:X}", this.Identifier) + "_" + this.FileType + "_" + this.Version + ".res";
        }


        /// <summary>
        /// Идентификатор файла
        /// </summary>
        public UInt64 Identifier { get => _identifier; set { _identifier = value; OnPropertyChanged("Identifier"); } }
        public string Name { get => _name; set { _name = value; OnPropertyChanged("Name"); } }

        /// <summary>
        /// Тип файла
        /// </summary>
        public FileTypes FileType { get; set; } = FileTypes.Unknown;

        /// <summary>
        /// Длинна файла
        /// </summary>
        public long Length { get => Data.Length; }

        /// <summary>
        /// Длинна файла
        /// </summary>
        public int Version { get;  set; } = 1;
        /// <summary>
        /// Данные
        /// </summary>
        public MemoryStream Data { get; set; }

        /// <summary>
        /// Позиция файла
        /// </summary>
        private long Position { get; set; }

    }
    public enum FileTypes : long
    {
        Unknown = 0,
        Boss = 1,
        Level = 2
    }
}
