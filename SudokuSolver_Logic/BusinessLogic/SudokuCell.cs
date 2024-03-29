﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver_Logic
{
    public class SudokuCell
    {
        public SudokuCell(int content, bool isEditable)
        {
            this.Content = content;
            this.IsEditable = isEditable;
        }

        public bool IsEditable
        {
            get;
            private set;
        }

        public int Content
        {
            get;
            set;
        }
    }
}
