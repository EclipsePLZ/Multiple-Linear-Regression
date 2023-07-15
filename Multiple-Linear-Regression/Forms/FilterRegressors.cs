using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multiple_Linear_Regression.Forms {
    public class FilterRegressors {

        /// <summary>
        /// Filter unimportant regressors by classic way
        /// </summary>
        /// <param name="models">List of models for all regressants</param>
        public void ClassicWayToFilterRegressors(List<List<Model>> models) {
            foreach (var listModels in models) {
                foreach (var model in listModels) {
                    model.ClassicWayFilterRegressors();
                }
            }
        }

        /// <summary>
        /// Filter unimportant regressors by empirical way
        /// </summary>
        /// <param name="models">List of models for all regressants</param>
        /// <param name="threshold">Threshold value of correlation coefficient for regressors</param>
        public void EmpiricalWayToFilterRegressors(double threshold, List<List<Model>> models) {
            foreach (var listModels in models) {
                foreach (var model in listModels) {
                    model.EmpiricalWayFilterRegressors(threshold);
                }
            }
        }

        /// <summary>
        /// Cancel filtering regressors for all models
        /// </summary>
        /// <param name="models">List of models for all regressants</param>
        public void CancelFilteringRegressors(List<List<Model>> models) {
            // Restore non-filter regressors for each model for each regressant
            foreach (var listModels in models) {
                listModels.ForEach(model => model.RestoreNonFilterRegressors());
            }
        }
    }
}
