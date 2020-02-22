using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoPOA.Helpers.OptionEnum
{
    public enum ToastType
    {

        [Description("success")]
        Success,

        [Description("info")]
        Info,

        [Description("warning")]
        Warning,

        [Description("error")]
        Error,

    }
}
