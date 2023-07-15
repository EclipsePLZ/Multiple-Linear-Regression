using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multiple_Linear_Regression.Work_WIth_Files.Interfaces {
    public interface IFileService {
        List<List<string>> Open(string filePath);

        void Save(string filename, List<List<string>> rows);
    }
}
