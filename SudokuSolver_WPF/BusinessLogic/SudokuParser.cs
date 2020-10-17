using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver_WPF.BusinessLogic
{
    public class SudokuParser
    {
        public SudokuCell[] Parse(string input)
        {
            var test = input.Split('\r', '\n').Where(p => !string.IsNullOrWhiteSpace(p));
            throw new NotImplementedException();
        }
    }
}
