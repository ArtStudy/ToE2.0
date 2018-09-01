using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameСreator.ToePac
{
    public class Item
    {
    
        UInt16 _FileType = 0;

    

        /// <summary>
        /// Идентификатор файла
        /// </summary>
        public UInt64 Identifier { get; set; } = 0;



        /// <summary>
        /// Тип файла
        /// </summary>
        public FileTypes FileType { get; set; } = FileTypes.Unknown;
         
        /// <summary>
        /// Длинна файла
        /// </summary>
        public long Length { get => Data.Length; }
        /// <summary>
        /// 
        /// </summary>
        public Stream Data { get; set; } = Stream.Null;

    }
    public enum FileTypes : long
    {
        Unknown = 0,
        Json = 1
    }
}
