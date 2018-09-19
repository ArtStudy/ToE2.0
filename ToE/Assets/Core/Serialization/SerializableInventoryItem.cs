using Assets.Core.Game.Data.Inventor;
using Assets.Core.Levels;

using Assets.Core.Volutes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Core.Serialization
{
    public class SerializableInventoryItem : SerializableBase
    {
        private const string StringNameInventoryItemData = "InvIt.";

        public SerializableInventoryItem(InventoryItem obj)
        {
            this.Name = obj.Name;
            this.TranslationIdentifier = obj.TranslationIdentifier;
            this.ImproveResponseTime_TypeEnhancements = (byte) obj.ImproveResponseTime.TypeEnhancements;
            this.ImproveResponseTime_Value = obj.ImproveResponseTime.Value;
            this.ImprovingHealth_TypeEnhancements = (byte)obj.ImprovingHealth.TypeEnhancements;
            this.ImprovingHealth_Value = obj.ImprovingHealth.Value;
        }
        public SerializableInventoryItem(MemoryStream stream)
        {
            Deserialization(stream);
        }

        /// <summary>
        /// Идентификатор перевода
        /// </summary>
        public string TranslationIdentifier { get; set; }

        /// /// <summary>
        /// Тип улучшения для здоровья
        /// </summary>
        public byte ImprovingHealth_TypeEnhancements { get; set; }
        /// <summary>
        /// Значение улучшения для здоровья
        /// </summary>
        public double ImprovingHealth_Value { get; set; }
        /// <summary>
        /// Тип улучшения для времени
        /// </summary>
        public byte ImproveResponseTime_TypeEnhancements { get; set; }
        /// <summary>
        /// Значение улучшения для времени
        /// </summary>
        public double ImproveResponseTime_Value { get; set; }


        public void Serialization(Stream stream)
        {
            using (var data = new BinaryWriter(stream))
            {
                data.Write(this.Name); //Поле Name
                data.Write(this.TranslationIdentifier); //Идентификатор перевода

                data.Write(this.ImproveResponseTime_TypeEnhancements); //Поле типа улучшения для времени
                data.Write(this.ImproveResponseTime_Value); //Поле значения улучшения для времени
                data.Write(this.ImprovingHealth_TypeEnhancements); //Поле типа улучшения для здоровья
                data.Write(this.ImprovingHealth_Value); //Поле значения улучшения для здоровья
            }

        }
        public void Deserialization(Stream stream)
        {


            using (var data = new BinaryReader(stream))
            {

                this.Name = data.ReadString(); //Поле Name
                this.TranslationIdentifier = data.ReadString(); //Идентификатор перевода

                this.ImproveResponseTime_TypeEnhancements = data.ReadByte(); //Поле типа улучшения для времени
                this.ImproveResponseTime_Value = data.ReadDouble();//Поле значения улучшения для времени
                this.ImprovingHealth_TypeEnhancements = data.ReadByte();  //Поле типа улучшения для здоровья
                this.ImprovingHealth_Value = data.ReadDouble(); //Поле значения улучшения для здоровья

            }


        }
        public void  ToObject(ref InventoryItem obj)
        {
            obj.Name = this.Name;
            obj.TranslationIdentifier = this.TranslationIdentifier;
            obj.ImproveResponseTime  = new Enhancements { TypeEnhancements = (TypeEnhancements)this.ImproveResponseTime_TypeEnhancements, Value = this.ImproveResponseTime_Value };
            obj.ImprovingHealth  =  new Enhancements { TypeEnhancements = (TypeEnhancements)this.ImprovingHealth_TypeEnhancements, Value = this.ImprovingHealth_Value };

        }

    }
}
