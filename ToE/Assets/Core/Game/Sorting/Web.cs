using Assets.Core.Levels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Core.Game.Sorting
{
    public class Web
    {
        public List<Group> groups { get; set; } = new List<Group>();
        public int k = 0;
        public int k1 = 0;
        public int k2 = 0;
        public void AddLevel(ILevel level)
        {
            int groupNumber = -1;
            Cell cell = new Cell(level);

            for (int groupCounter = 0; groupCounter < groups.Count; groupCounter++)
            {

                for (int cellCounter = 0; cellCounter < groups[groupCounter].cells.Count; cellCounter++)
                {
                    //Проверка родителей
                    for (int parentsCounter = 0; parentsCounter < cell.parents.Count; parentsCounter++)
                    {
                        // Если это родитель, то записать в него потомка
                        if (cell.parents[parentsCounter] == groups[groupCounter].cells[cellCounter].level)
                        {
                            if (groups[groupCounter].cells[cellCounter].childs == null)
                                groups[groupCounter].cells[cellCounter].childs = new List<ILevel>();
                            groups[groupCounter].cells[cellCounter].childs.Add(cell.level);
                            // Ищем самого молодого родителя
                            if (groupNumber < groupCounter)
                                groupNumber = groupCounter;
                        }
                        k2++;
                    }
                    k1++;
                   
                }
                //Заносим уровень в соответствующую группу
              

                k++;
            }
            groups[groupNumber + 1].AddLevel(cell);
        }

        public void RemoveLevel(int ID)
        {

        }
    }
}
