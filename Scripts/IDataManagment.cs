﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiszki.Scripts
{
    internal interface IDataManagement
    {
        void LoadData();
        void PushData(object data, ActionTypes type);
    }
}
