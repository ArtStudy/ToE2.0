using Assets.Core.LevelsStructureInterfaces;
using Assets.Core.ToePac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Core.Game.Data
{
    public abstract class ListDataBase<T> : List<DataItem<T>> where T : IBase
    {

        public ListDataBase() : base() { }
        public ListDataBase(IEnumerable<DataItem<T>> collection) : base(collection) { }
        public ListDataBase(int capacity) : base(capacity) { }

        /// <summary>
        /// Перезагрузка элемента
        /// </summary>
        /// <param name="obj"></param>
        public abstract void ReLoad(DataItem<T> obj);
        /// <summary>
        /// Перезагрузка элемента
        /// </summary>
        /// <param name="obj"></param>
        public abstract void Save(DataItem<T> obj);
        /// <summary>
        /// Перезагрузка элемента
        /// </summary>
        public  abstract string StringNameData { get; }
        /// <summary>
        /// Сохранить все
        /// </summary>
        public void SaveAs()
        {
            for(int i = 0; i < this.Count; i++)
            {
                this.Save(this[i]);
            }
        }

        public ListResourse GetListResourse ()
        {
            ListResourse lr = new ListResourse();
            for (int i = 0; i < this.Count; i++)
            {


                lr.AddRange(this[i].ListResourse);
            }

            return lr;
        }

        public T FindById(int id)
        {
            if (id == 0)
                return default(T);
            return this.Find((item) => item.Value.ID == id).Value;
        }
        public int GenNewID()=> this.Count > 0 ? (this.Max((item) => item.Value.ID))+1 : 1;





        public void SortingByID()
        {
            this.Sort(SortingComp);
        }

        private int SortingComp(DataItem<T> x, DataItem<T> y)
        {
          return  x.Value.ID.CompareTo(y.Value.ID);
        }






        /*  public void Remove(DataItem<T> current)
{

}*/
    }
}
