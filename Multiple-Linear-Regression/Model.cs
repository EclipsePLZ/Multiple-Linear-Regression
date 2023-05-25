using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multiple_Linear_Regression {
    public class Model {
        public string RegressantName { get; }
        public List<string> RegressorsNames { get; private set; }
        public List<double> RegressantValues { get; private set; }
        public Dictionary<string, List<double>> Regressors { get; private set; }
        public Dictionary<string, List<string>> ProcessFunctions { get; private set; }

        public Model(string regerssantName, List<double> regressantValues, 
            List<string> regressorsNames = null, Dictionary<string, List<double>> regressors = null) {
            RegressantName = regerssantName;
            RegressantValues = regressantValues;
            RegressorsNames = regressorsNames;
            Regressors = regressors;
            ProcessFunctions = new Dictionary<string, List<string>>();
        }

        public void SetNewRegressors(Dictionary<string, List<double>> regressors) {
            Regressors = new Dictionary<string, List<double>>(regressors);
        }

        public void SetRegressorsNames(List<string> regressorsNames) {
            RegressorsNames = new List<string>(regressorsNames);
        }
    }
}
