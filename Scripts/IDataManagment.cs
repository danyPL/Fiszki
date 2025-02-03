﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiszki.Scripts
{
    public interface IDataManagement
    {
        virtual void LoadData() { }
        virtual void PushData(object data, ActionTypes type) { }
    }
}
