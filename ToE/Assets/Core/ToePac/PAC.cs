using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameСreator.ToePac
{
    public class PAC
    {
        public const string FileTypeConst = "PACT";
        public const int VersionConst = 1;
        string _FileType = "PACT";
        int _Version = 1;
        List<Item> _Items = new List<Item>();

        public static Stream CreatePAC(PAC pac)
        {
            MemoryStream ms = new MemoryStream();
            var data = new BinaryWriter(ms);

            data.Write(pac.FileType);

            data.Write(pac.Version);

            data.Write(pac.Items.Count);
            // data.Flush();
            var d = ms.Length;
            long pos = ms.Position + (pac.Items.Count * 32); //Текущая позиция
            long curpos = ms.Position;
            foreach (var item in pac.Items)
            {
                data.Write(item.Identifier);
                data.Write(item.Length);
                data.Write((long)item.FileType);

                data.Write(pos);
                ms.SetLength(pos + item.Length
                    +11);
                using (BinaryReader br = new BinaryReader(item.Data))
                {
                    var b = br.ReadBytes((int)item.Length);
                    curpos = ms.Position;
                    ms.Position = pos;
                    ms.Write(b, 0, b.Length);
                    ms.Position = curpos;
                }
                pos += 32;


            }
            Debug.WriteLine(ms.Position - d);

            data.Flush();



            return ms;
        }
        public static PAC GetPAC(Stream  pac)
        {

            PAC Data = new PAC();


            BinaryReader br = new BinaryReader(pac);


            pac.Position = 0;


            Data._FileType = br.ReadString();
            Data._Version = br.ReadInt32();
            if (Data.FileType != FileTypeConst)
                throw new Exception("Неверный тип файла");
           if (Data.Version != VersionConst)
                throw new Exception("Неверный тип файла");

           var countitem = br.ReadInt32();
            long curpos = pac.Position;
            for (int i = 0; i <   countitem; i++ )
            {
                var item = new Item();
                item.Identifier = br.ReadUInt64();
                var length = br.ReadInt64();
                item.FileType = (FileTypes) br.ReadInt64();
                var pos = br.ReadInt64();
                byte[] bytedata = new byte[length];
                curpos = pac.Position;
                pac.Position = pos ;
                pac.Read(bytedata, 0, (int)length);

                item.Data  = new MemoryStream(bytedata);
                pac.Position = curpos;
                Data.Items.Add(item);

            }



            Debug.WriteLine(Data.FileType);
            Debug.WriteLine(Data.Version);

            return Data;
        }

        /// <summary>
        /// Тип файла
        /// </summary>
        public string FileType { get => _FileType; }
        /// <summary>
        /// Версия
        /// </summary>
        public int Version { get => _Version; }

        public List<Item> Items { get => _Items; }

    }
}
