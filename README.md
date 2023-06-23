# Multiple-Linear-Regression
## Loading data and selecting factors
On this tab of the application, you can select the controlling (regressors) and controllable factors (regressants) for the study. Once the factors have been selected, they are displayed in the left-hand menu (two lists on the left). You can also choose the type of problem to be solved: management or prediction.

![Загрузка_данных](https://github.com/EclipsePLZ/Multiple-Linear-Regression/assets/84061271/2675a7dd-ed64-420c-845e-aca65fbcc6a4)

If you selected the type of task - forecasting, you must set the forecasting parameters.

![Параметры_прогнозирования](https://github.com/EclipsePLZ/Multiple-Linear-Regression/assets/84061271/10f8e4f5-84f2-47d9-b852-a4fcd56cad6c)


## Functional preprocessing of statistical data (1 ver.)
This step is optional.
To improve the predictive properties of the models, each of the regressors is subjected to functional processing (as long as we can improve the correlation between the regressor and the regressant - we apply one of the available preprocessing functions to transform the regressor values).
Thus, for each of the regressors, a list of functions applied in the transformation process is generated.
This step uses the functions proposed by Gusev.

![Обработка_статистических_данных_Гусев](https://github.com/EclipsePLZ/Multiple-Linear-Regression/assets/84061271/5b160fac-dc92-462a-b003-fc9abc81e725)

## Functional preprocessing of statistical data (2 ver.)
This step is optional.
To improve the predictive properties of the models, each of the regressors is subjected to functional processing (as long as we can improve the correlation between the regressor and the regressant - we apply one of the available preprocessing functions to transform the regressor values).
Thus, for each of the regressors, a list of functions applied in the transformation process is generated.
This step uses the functions proposed by Okunev in his dissertation.
([Диссертация июнь 2023.docx](https://github.com/EclipsePLZ/Multiple-Linear-Regression/files/11850490/2023.docx))

![Обработка_статистических_данных_Окунев](https://github.com/EclipsePLZ/Multiple-Linear-Regression/assets/84061271/2067c307-4d2a-40c9-93fc-8434376332b5)

## Forming sets of control factors
This step is optional.
At this step, the regressors whose correlation coefficient exceeds the threshold (set programmatically) are divided into groups. Next, all possible groups of uncorrelated regressors are formed from these groups and then the most appropriate group of control variables is selected for each regressant (selection is based on the calculation of the adjusted coefficient of determination).

![Формирование_наборов_управляющих_факторов](https://github.com/EclipsePLZ/Multiple-Linear-Regression/assets/84061271/6115650d-527c-476d-b676-55ebc9b42c45)

## Filtering control factors
This step is optional.
At this stage, the control factors can be filtered (removed) using one of the approaches - the empirical approach or the classical approach.
The empirical approach consists in comparing the correlation coefficient between the control and the controlled factor; if the correlation coefficient (modulo) is less than the threshold value (set by software), then this factor is considered insignificant and is deleted.
The classical approach is to test the correlation coefficients for significance at a significance level of α = 0.05.

![Фильтрация_факторов](https://github.com/EclipsePLZ/Multiple-Linear-Regression/assets/84061271/403a300a-4311-4f55-abbc-388303386f7f)

## Construction of multiple regression equations and finding the best model
This step is mandatory.
A multiple regression equation is constructed for each of the models. The vector of regression coefficients is found to build the equation. To find the vector of regression coefficients the method of least squares is used. And for each regressor, the best model that satisfies the conditions of adequacy and significance is found.

![Построение_уравнений](https://github.com/EclipsePLZ/Multiple-Linear-Regression/assets/84061271/e1ad6b8c-6510-4765-aebb-dbc8d28ab3eb)

## Setting the definition area parameters
This step is mandatory.
At this step you can define the way of calculating the definition area for the controlling factors. There are three ways to calculate the definition area: empirical, theoretical, symbiosis of areas.
The empirical definition area is calculated by extending the available definition area by a user-defined percentage. 
The theoretical area of definition is calculated as the mean value ± 3 standard deviations.
Symbiosis is calculated as a minimum of minima and a maximum of maxima when combining the empirical and theoretical definition areas.
The expansion of the area of definition can occur in both directions equally or inversely proportional to the distance from the mean (the smaller the distance from the mean - the larger the empirical area of definition in that direction).
Also on this tab you can choose how to find the number of intervals to build equations between regressors.

![Настройка_параметров_имитации_управления](https://github.com/EclipsePLZ/Multiple-Linear-Regression/assets/84061271/e6f6775a-8f79-4289-9b1e-412e8ca65078)

## Simulate control
At this step we can simulate control, i.e. we can change values of controlling factors to obtain required values of controlled factors. At this step you can optionally switch on accounting of influence of controlling factors on each other, in this case if you change one of the controlling factors, the values of both controlled factors (regressants) and other controlling factors (regressors) will change.
If you enter a value outside the definition area, you will be warned about possible inadequate behavior of the model.

![Имитация_управления](https://github.com/EclipsePLZ/Multiple-Linear-Regression/assets/84061271/c0dae15c-15a4-4e94-a20f-126347989d26)

## Loading control factor values for control simulation from a file
This step makes it possible to load a file containing the values of the controlling factors in order to calculate the predicted values of the controlled factors for many observations at once.
After calculating the values of the controllable factors, the control results can be unloaded into an excel file.

![Имитация_управления_из_файла](https://github.com/EclipsePLZ/Multiple-Linear-Regression/assets/84061271/98358e7d-7b89-4115-b0c8-598c6bf3fee5)

## Forecasting for existing indicators
In this step, forecasting is automatically performed for the existing observations of the defining indicators.
Also for each prediction indicator, the prediction error is calculated. And then the average value of the prediction error for each of the indicators is calculated.
You can load the values of the defining indicators from a file to find the prediction for each of the indicators.

![Прогнозирование](https://github.com/EclipsePLZ/Multiple-Linear-Regression/assets/84061271/47f84558-a174-4dd5-a0f4-b9e239432001)

## Load defining indicators from a file to build a forecast
At this step it is possible to load the values of the determinants from the file to build the prediction. After finding the prediction values, you can unload the prediction results to a file.

![Прогнозирование_из_файла](https://github.com/EclipsePLZ/Multiple-Linear-Regression/assets/84061271/7d2fc0eb-03de-4919-9ceb-46ab2a472f50)
