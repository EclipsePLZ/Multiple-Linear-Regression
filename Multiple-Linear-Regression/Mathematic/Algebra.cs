using System;


namespace Multiple_Linear_Regression {
    public static class Algebra {
        /// <summary>
        /// Transpose matrix
        /// </summary>
        /// <param name="matrix">Matrix</param>
        /// <returns>Transposed matrix</returns>
        public static double[,] Transpose(double[,] matrix) {
            double[,] transposedMatrix = new double[matrix.GetLength(1), matrix.GetLength(0)];

            for (int i = 0; i < matrix.GetLength(0); i++) {
                for (int j = 0; j < matrix.GetLength(1); j++) {
                    transposedMatrix[j, i] = matrix[i, j];
                }
            }

            return transposedMatrix;
        }

        /// <summary>
        /// Inverse matrix
        /// </summary>
        /// <param name="matrix">Matrix</param>
        /// <returns>Inversed matrix</returns>
        public static double[,] Inverse(double[,] matrix) {
            int n = matrix.GetLength(0);
            int m = matrix.GetLength(1);

            double[,] inversedMatrix = Ones(n, m);
            double[,] leftMatrix = matrix.Clone() as double[,];

            // Using the Gaussian method we find the upper triangular matrix
            for (int i = 0; i < n; i++) {
                double mainElem = leftMatrix[i, i];
                for (int j = 0; j < m; j++) {
                    leftMatrix[i, j] /= mainElem;
                    inversedMatrix[i, j] /= mainElem;
                }
                for (int k = 0; k < n; k++) {
                    if (k != i) {
                        double multElem = leftMatrix[k, i];

                        for (int j = 0; j < m; j++) {
                            leftMatrix[k, j] -= leftMatrix[i, j] * multElem;
                            inversedMatrix[k, j] -= inversedMatrix[i, j] * multElem;
                        }
                    }
                }
            }

            return inversedMatrix;
        }

        /// <summary>
        /// Get identity matrix
        /// </summary>
        /// <param name="n">Number of rows</param>
        /// <param name="m">Number of columns</param>
        /// <returns>Identity matrix</returns>
        public static double[,] Ones(int n, int m) {
            if (n != m) {
                throw new Exception("The number of rows and columns must match!");
            }

            double[,] onesMatrix = new double[n, m];

            for (int i = 0; i < n; i++) {
                onesMatrix[i, i] = 1;
            }

            return onesMatrix;
        }

        /// <summary>
        /// Mult matrix by matrix
        /// </summary>
        /// <param name="matrix1">First matrix</param>
        /// <param name="matrix2">Second matrix</param>
        /// <returns>Result matrix</returns>
        public static double[,] Mult(double[,] matrix1, double[,] matrix2) {
            if (matrix1.GetLength(1) != matrix2.GetLength(0)) {
                throw new Exception("Such matrices cannot be multiplied!");
            }

            double[,] resultMatrix = new double[matrix1.GetLength(0), matrix2.GetLength(1)];
            
            for (int i = 0; i < matrix1.GetLength(0); i++) {
                for (int j = 0; j < matrix2.GetLength(1); j++) {
                    for(int k = 0; k < matrix1.GetLength(1); k++) {
                        resultMatrix[i, j] += matrix1[i, k] * matrix2[k, j];
                    }
                }
            }

            return resultMatrix;
        }

        /// <summary>
        /// Mult matrix by vector
        /// </summary>
        /// <param name="matrix">Matrix</param>
        /// <param name="vector">Vector</param>
        /// <returns>Result vector</returns>
        public static double[] Mult(double[,] matrix, double[] vector) {
            if (matrix.GetLength(1) != vector.Length) {
                throw new Exception("Such a matrix and a vector cannot be multiplied!");
            }

            double[] resultVector = new double[matrix.GetLength(0)];

            for (int i = 0; i < matrix.GetLength(0); i++) {
                for (int j = 0; j < vector.Length; j++) {
                    resultVector[i] += matrix[i, j] * vector[j];
                }
            }

            return resultVector;
        }

        /// <summary>
        /// Mult vector by vector
        /// </summary>
        /// <param name="vector1">Vector</param>
        /// <param name="vector2">Vector</param>
        /// <returns>Result value</returns>
        public static double Mult(double[] vector1, double[] vector2) {
            if (vector1.Length != vector2.Length) {
                throw new Exception("Vectors must be the same length!");
            }

            double result = 0.0;

            for (int i = 0; i < vector1.Length; i++) {
                result += vector1[i] * vector2[i];
            }

            return result;
        }

        /// <summary>
        /// Substracting vector2 from vector1
        /// </summary>
        /// <param name="vector1">Values of vector1</param>
        /// <param name="vector2">Values of vector2</param>
        /// <returns>Result of substracting</returns>
        public static double[] Substract(double[] vector1, double[] vector2) {
            if (vector1.Length != vector2.Length) {
                throw new Exception("Vectors must be the same length!");
            }

            double[] resultVector = new double[vector1.Length];

            for (int i = 0; i < vector1.Length; i++) {
                resultVector[i] = vector1[i] - vector2[i];
            }

            return resultVector;
        }
    }
}
