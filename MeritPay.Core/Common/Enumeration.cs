using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeritPay.Core.Common
{
    public enum UserPermission
    {
        FullAccess,
        BranchAccess
    }


    public enum EventType
    {
        Successfull = 1,
        Error = 2,
        Information = 3,
        Warning = 4,
        UnHandledException = 5
    }
}
