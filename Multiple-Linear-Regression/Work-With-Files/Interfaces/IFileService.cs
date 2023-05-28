using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multiple_Linear_Regression.Work_WIth_Files.Interfaces {
    internal interface IFileService {
        List<List<string>> Open(string filePath);
    }
}
