# Multiple-Linear-Regression
## Loading data and selecting factors
On this tab of the application, you can select the controlling (regressors) and controllable factors (regressants) for the study. Once the factors have been selected, they are displayed in the left-hand menu (two lists on the left).

![Загрузка_данных](https://github.com/EclipsePLZ/Multiple-Linear-Regression/assets/84061271/31e31a9b-583d-48df-8be9-e422c74252af)

## Forming sets of control factors
This step is optional.
At this step, the regressors whose correlation coefficient exceeds the threshold (set programmatically) are divided into groups. Next, all possible groups of uncorrelated regressors are formed from these groups and then the most appropriate group of control variables is selected for each regressant (selection is based on the calculation of the adjusted coefficient of determination).

![Формирование_наборов_управляющих_факторов](https://github.com/EclipsePLZ/Multiple-Linear-Regression/assets/84061271/6b29eec9-466e-404e-86b1-23079e89b90e)

## Functional preprocessing of statistical data
This step is optional.
To improve the predictive properties of the models, each of the regressors is subjected to functional processing (as long as we can improve the correlation between the regressor and the regressant - we apply one of the available preprocessing functions to transform the regressor values).
Thus, for each of the regressors, a list of functions applied in the transformation process is generated.

![Обработка_статистических_данных](https://github.com/EclipsePLZ/Multiple-Linear-Regression/assets/84061271/84a2ac9c-45c4-4b94-a084-234ad3136d4a)

## Filtering control factors
This step is optional.
At this stage, the control factors can be filtered (removed) using one of the approaches - the empirical approach or the classical approach.
The empirical approach consists in comparing the correlation coefficient between the control and the controlled factor; if the correlation coefficient (modulo) is less than the threshold value (set by software), then this factor is considered insignificant and is deleted.
The classical approach is to test the correlation coefficients for significance at a significance level of α = 0.05.

![Фильтрация_факторов](https://github.com/EclipsePLZ/Multiple-Linear-Regression/assets/84061271/4f4991a3-431c-43b4-9bf6-8e3618f34a41)

## Construction of multiple regression equations
This step is mandatory.
A multiple regression equation is constructed for each of the models. The vector of regression coefficients is found to build the equation. To find the vector of regression coefficients the method of least squares is used.

![Построение_уравнений](https://github.com/EclipsePLZ/Multiple-Linear-Regression/assets/84061271/8fdcc236-2f4a-4782-ae40-6b708684b402)

## Setting the definition area parameters
This step is mandatory.
At this step you can define the way of calculating the definition area for the controlling factors. There are three ways to calculate the definition area: empirical, theoretical, symbiosis of areas.
The empirical definition area is calculated by extending the available definition area by a user-defined percentage. 
The theoretical area of definition is calculated as the mean value ± 3 standard deviations.
Symbiosis is calculated as a minimum of minima and a maximum of maxima when combining the empirical and theoretical definition areas.
The expansion of the area of definition can occur in both directions equally or inversely proportional to the distance from the mean (the smaller the distance from the mean - the larger the empirical area of definition in that direction).

![Настройка_параметров_имитации_управления](https://github.com/EclipsePLZ/Multiple-Linear-Regression/assets/84061271/6df40fe5-7154-4651-af4a-9ced00c48ca4)

## Simulate control
At this step we can simulate control, i.e. we can change values of controlling factors to obtain required values of controlled factors. At this step you can optionally switch on accounting of influence of controlling factors on each other, in this case if you change one of the controlling factors, the values of both controlled factors (regressants) and other controlling factors (regressors) will change.
If you enter a value outside the definition area, you will be warned about possible inadequate behavior of the model.

![Имитация_управления](https://github.com/EclipsePLZ/Multiple-Linear-Regression/assets/84061271/514947bb-7087-41e2-a0f0-707cfc6a4be1)

## Loading control factor values for control simulation from a file
This step makes it possible to load a file containing the values of the controlling factors in order to calculate the predicted values of the controlled factors for many observations at once.
After calculating the values of the controllable factors, the control results can be unloaded into an excel file.

![Имитация_управления_из_файла](https://github.com/EclipsePLZ/Multiple-Linear-Regression/assets/84061271/8d9b15f7-53b6-4dcb-b968-54e8731786bc)

