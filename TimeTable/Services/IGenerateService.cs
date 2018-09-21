using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeTable.Services
{
    public interface IGenerateService
    {
        void Generate();
        void Validate();
    }
}