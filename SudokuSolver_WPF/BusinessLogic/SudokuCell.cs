using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver_WPF.BusinessLogic
{
    public class SudokuCell
    {
        public SudokuCell(int content)
        {
            this.Content = content;
        }

        public int Content
        {
            get;
            set;
        }
    }
}
