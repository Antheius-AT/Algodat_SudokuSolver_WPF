using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver_WPF.BusinessLogic
{
    public class Change
    {
        public readonly int changedIndex;
        public readonly int changedValue;

        public Change(int changedIndex, int setValue)
        {
            if (this.changedIndex < 0)
                throw new ArgumentException(nameof(changedIndex), "index must not be negative.");

            this.changedIndex = changedIndex;
            this.changedValue = setValue;
        }
    }
}
