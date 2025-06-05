using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XCharts;
using XCharts.Runtime;
using TMPro;
using UnityEngine.UI;

public class TwoWheelCostManager : MonoBehaviour
{
    public static TwoWheelCostManager instance;

    [SerializeField] bool initialBikeCostsAdded = false;

    [SerializeField] BarChart fuelCostBarChart;
    [SerializeField] SimplifiedLineChart yearlyLineChart;
    [SerializeField] Slider distancePerDaySlider;
    [SerializeField] Slider petrolCostPerDaySlider;
    [SerializeField] Slider electricityCostPerDaySlider;
    [SerializeField] Slider mileageSlider;
    [SerializeField] Slider monthlySavingsSlider;
    [SerializeField] Slider yearlySavingsSlider;
    [SerializeField] Slider monthlyElectricBikeMaintenanceCostSlider;
    [SerializeField] Slider monthlyPetrolBikeMaintenanceCostSlider;
    


    [SerializeField] TextMeshProUGUI savedAmountText;
    [SerializeField] TextMeshProUGUI savedPercentText;
    [SerializeField] TextMeshProUGUI monthlyFuelCostPerKmText;
    [SerializeField] TextMeshProUGUI monthlyElectricityCostPerKmText;

    [SerializeField] TextMeshProUGUI yearlySavedAmountText;
    [SerializeField] TextMeshProUGUI yearlySavedPercentText;
    [SerializeField] TextMeshProUGUI yearlyFuelCostPerKmText;
    [SerializeField] TextMeshProUGUI yearlyEbikeCostPerKmText;

    [SerializeField] TextMeshProUGUI totalYearsSavedAmountText;
    [SerializeField] TextMeshProUGUI totalYearsSavedPercentText;
    [SerializeField] TextMeshProUGUI totalYearsFuelCostPerKmText;
    [SerializeField] TextMeshProUGUI totalYearsEbikeCostPerKmText;


    [SerializeField] TextMeshProUGUI monthlySavedAmountSliderText;
    [SerializeField] TextMeshProUGUI monthlySavedPercentSliderText;

    [SerializeField] TextMeshProUGUI yearlySavedAmountSliderText;
    [SerializeField] TextMeshProUGUI yearlySavedPercentSliderText;
    [Space]

    [Header("SummaryPanel")]

    int monthlyMileage;
    float monthlySavings;
    float totalMonthlyElectricCosts;
    float totalMonthlyPetrolCosts;
    float petrolCostPerKm;
    float electricCostPerKm;

    [SerializeField] TextMeshProUGUI dailyMileageText;
    [SerializeField] TextMeshProUGUI monthlyMileageText;
    [SerializeField] TextMeshProUGUI monthlyFuelCostText;
    [SerializeField] TextMeshProUGUI monthlyElectricCostText;
    [SerializeField] TextMeshProUGUI monthlyPetrolMaintenanceCostText;
    [SerializeField] TextMeshProUGUI monthlyElectricMaintenanceCostText;
    [SerializeField] TextMeshProUGUI monthlySavingsText;
    [SerializeField] TextMeshProUGUI yearlySavingsText;
    [SerializeField] TextMeshProUGUI petrolCostPerKmText;
    [SerializeField] TextMeshProUGUI electricCostPerKmText;
    [SerializeField] TextMeshProUGUI dailyPetrolCostText;
    [SerializeField] TextMeshProUGUI dailyElectricCostText;

    [Space]

    [SerializeField] string yearlyCostChartTitle = " 10-Year Costs Approximation";

    [SerializeField] double[] yearlyFuelCostsList;
    [SerializeField]  double[] yearlyEBikeCostsList;

    [SerializeField] float yearlyFuelCost, yearlyEbikeCost, accYearlyFuelCost, accYearlyeBikeCost;
    [SerializeField] int numberOfYears = 0;
    [SerializeField] int initialFuelBikeCost;
    [SerializeField] int initialEBikeCost;
    [SerializeField] int kmInTotalYears;
    [SerializeField] int batteryReplacementCost = 130000;

    [SerializeField] float totalYearsFuelBikeCosts; 
    [SerializeField] float totalYearsEbikeCosts;

    [SerializeField] float monthlySavedAmount, yearlySavedAmount, petrolCostPerMonth, eCostPerMonth;
    [SerializeField] float monthlySavedPercent, yearlySavedPercent;
    [SerializeField] float monthlyFuelCostPerKm, monthlyElectricCostPerKm;
    [SerializeField] float yearlyFuelCostPerKm, yearlyElectricCostPerKm;

    [SerializeField] float totalYearsSavedPercent;
    [SerializeField] float inflationRate = 1f;
    [SerializeField] float batteryCapInKwh = 1f;
    

    [SerializeField] float totalYearsElectricCostPerKm, totalYearsSavedAmount, totalYearsFuelCostPerKm;

    private Serie fuelCostSerie, electricityCostSerie;

    private void Start()
    {



        instance = this;

        monthlySavingsSlider.maxValue = 100;
        yearlySavingsSlider.maxValue = 100;



        yearlyFuelCostsList = new double[numberOfYears];
        yearlyEBikeCostsList = new double[numberOfYears];


     //   fuelCostBarChart.SetSize(600, 450);
        fuelCostSerie = fuelCostBarChart.AddSerie<Bar>("Electric Bike Costs Bar");

        var yAxis = fuelCostBarChart.EnsureChartComponent<YAxis>();
        yAxis.minMaxType = Axis.AxisMinMaxType.Default;
        CalculateFuelCostBarChart();

        CalculateYearlySavingsLineChart();
 
    }

    void CalculateFuelCostBarChart()
    {

        fuelCostBarChart.ClearData();
        
        fuelCostBarChart.AnimationEnable(false);

        fuelCostBarChart.AddXAxisData("Monthly Fuel  vs E-Bike Cost");
        fuelCostBarChart.AddData(0, FuelBikeCost());

        
        fuelCostBarChart.AddData(1, EBikeCost());

        MonthlySavings();
    }

    double FuelBikeCost()
    {
        yearlyFuelCost = 0;
        accYearlyFuelCost = 0;

        double fuelCost, costPerDay ;
        costPerDay = distancePerDaySlider.value / mileageSlider.value * petrolCostPerDaySlider.value;
        fuelCost = (costPerDay * 30.0f ) + monthlyPetrolBikeMaintenanceCostSlider.value;

        petrolCostPerMonth = (float)fuelCost;

        monthlyFuelCostPerKm = petrolCostPerMonth / ( distancePerDaySlider.value * 30.0f );

        monthlyFuelCostPerKmText.text = "Rs " + monthlyFuelCostPerKm.ToString("0.0");

        yearlyFuelCost = 12.0f * petrolCostPerMonth;

        if (initialBikeCostsAdded == true)
        {
            //yearlyFuelCostsList[0] = initialFuelBikeCost + yearlyFuelCost;          //cost with added initial fuel bike purchasing cost
            yearlyFuelCostsList[0] = initialFuelBikeCost;
        }
        else
        {
            //yearlyFuelCostsList[0] = yearlyFuelCost;                                //first year cost excluding initial fuel bike purchasing cost
            yearlyFuelCostsList[0] = 0;
        }



        accYearlyFuelCost = (float)yearlyFuelCostsList[0];
        for (int i = 1; i < numberOfYears; i++)
        {
            //float a = 1f;
            //a += 0.07f;

            //yearlyFuelCost *= a;
            accYearlyFuelCost += yearlyFuelCost;
            yearlyFuelCostsList[i] = accYearlyFuelCost;

            
        }

        totalYearsFuelBikeCosts = (float)yearlyFuelCostsList[numberOfYears - 1];

        return fuelCost;
    }


    

    double EBikeCost()
    {
        accYearlyeBikeCost = 0;
        yearlyEbikeCost = 0;

        double eCost, costPerDay;
        costPerDay = distancePerDaySlider.value / mileageSlider.value * electricityCostPerDaySlider.value;
        eCost = (costPerDay * 30 * batteryCapInKwh ) + monthlyElectricBikeMaintenanceCostSlider.value;

        eCostPerMonth = (float)eCost;


        monthlyElectricCostPerKm = eCostPerMonth / ( distancePerDaySlider.value * 30 );

        monthlyElectricityCostPerKmText.text = "Rs " + monthlyElectricCostPerKm.ToString("0.0");

        yearlyEbikeCost = 12 * eCostPerMonth;

        if (initialBikeCostsAdded == true)
        {
           // yearlyEBikeCostsList[0] = yearlyEbikeCost + initialEBikeCost;          //cost with added initial electric bike cost
              yearlyEBikeCostsList[0] = initialEBikeCost;
        }
        else
        {
            //yearlyEBikeCostsList[0] = yearlyEbikeCost;               //first year cost excluding initial Electric bike cost
              yearlyEBikeCostsList[0] = 0;
        }


        accYearlyeBikeCost = (float)yearlyEBikeCostsList[0];

        for (int i = 1; i < numberOfYears; i++)
        {
            //float a = 1f;
            //a += 0.07f;                   // for inflation
            //yearlyEbikeCost *= a; 
            accYearlyeBikeCost += yearlyEbikeCost;

            if (i == 6)
            {
                accYearlyeBikeCost += batteryReplacementCost;          //battery replacement cost
            }

            yearlyEBikeCostsList[i] = accYearlyeBikeCost;

            
        }


        totalYearsEbikeCosts = (float)yearlyEBikeCostsList[numberOfYears - 1];

        return eCost;
    }

    public void UpdateChart()
    {
        CalculateFuelCostBarChart();
        CalculateYearlySavingsLineChart();

    }


    void MonthlySavings()
    {
        monthlySavedAmount = petrolCostPerMonth - eCostPerMonth;
        monthlySavedPercent = monthlySavedAmount / petrolCostPerMonth * 100;

        Debug.Log(monthlySavedPercent);

        savedAmountText.text = "Rs " + monthlySavedAmount.ToString();
        savedPercentText.text = monthlySavedPercent.ToString("0.00") + "%";


        SavingsSliders();

    }

    void YearlySavings()
    {
        
        // Per year per km costs and savings calculations
        yearlyFuelCostPerKm = yearlyFuelCost / (distancePerDaySlider.value * 365);
        yearlyElectricCostPerKm = yearlyEbikeCost / (distancePerDaySlider.value * 365);
        yearlySavedAmount = yearlyFuelCost - yearlyEbikeCost;
        yearlySavedPercent = (yearlySavedAmount / yearlyFuelCost) * 100;

        yearlySavedPercentText.text = yearlySavedPercent.ToString("0.0") + "%";
        yearlySavedAmountText.text = "Rs " + yearlySavedAmount.ToString();
        yearlyFuelCostPerKmText.text = "Rs " + yearlyFuelCostPerKm.ToString("0.0");
        yearlyEbikeCostPerKmText.text = "Rs " + yearlyElectricCostPerKm.ToString("0.0");


        //total years cost per km and savings calculations
        kmInTotalYears = numberOfYears * 12 * 30 * (int)distancePerDaySlider.value;

        totalYearsFuelCostPerKm = totalYearsFuelBikeCosts / kmInTotalYears;
        totalYearsElectricCostPerKm = totalYearsEbikeCosts / kmInTotalYears;

        totalYearsSavedAmount = totalYearsFuelBikeCosts - totalYearsEbikeCosts;
        totalYearsSavedPercent = (totalYearsSavedAmount / totalYearsFuelBikeCosts ) * 100;

        totalYearsSavedPercentText.text = totalYearsSavedPercent.ToString("0.0") + "%" + " Saved";
        totalYearsSavedAmountText.text = "Rs " + totalYearsSavedAmount.ToString() + " Saved";
        totalYearsFuelCostPerKmText.text = "Rs " + totalYearsFuelCostPerKm.ToString("0.0");
        totalYearsEbikeCostPerKmText.text = "Rs " + totalYearsElectricCostPerKm.ToString("0.0");
    }


    void CalculateYearlySavingsLineChart()
    {
       // var yearlyLineChart = gameObject.GetComponent<SimplifiedLineChart>();
        if (yearlyLineChart == null)
        {
            yearlyLineChart = gameObject.AddComponent<SimplifiedLineChart>();
            yearlyLineChart.Init();
            yearlyLineChart.SetSize(800, 450);
        }
        yearlyLineChart.EnsureChartComponent<Title>().show = true;
        yearlyLineChart.EnsureChartComponent<Title>().text = yearlyCostChartTitle;

        yearlyLineChart.EnsureChartComponent<Tooltip>().show = true;
        yearlyLineChart.EnsureChartComponent<Legend>().show = false;

        var xAxis = yearlyLineChart.EnsureChartComponent<XAxis>();
        var yAxis = yearlyLineChart.EnsureChartComponent<YAxis>();
        xAxis.show = true;
        yAxis.show = true;
        xAxis.type = Axis.AxisType.Category;
        yAxis.type = Axis.AxisType.Value;

        xAxis.splitNumber = 10;
        xAxis.boundaryGap = true;

        yearlyLineChart.RemoveData();
        yearlyLineChart.AddSerie<SimplifiedLine>();
        yearlyLineChart.AddSerie<SimplifiedLine>();
        for (int i = 0; i < numberOfYears; i++)
        {
            yearlyLineChart.AddXAxisData("x"+i);
            yearlyLineChart.AddData(0, yearlyFuelCostsList[i]);
            yearlyLineChart.AddData(1, yearlyEBikeCostsList[i]);
        }

        YearlySavings();
        
    }



    void SavingsSliders()
    {
        monthlySavingsSlider.value = monthlySavedPercent;
        monthlySavedAmountSliderText.text = "Rs. " + monthlySavedAmount.ToString("0") + " Saved";
        monthlySavedPercentSliderText.text =  monthlySavedPercent.ToString("0.0") + " % Saved";

        //yearlySavingsSlider.value = yearlySavedPercent;
        //yearlySavedAmountSliderText.text = "Rs. " + yearlySavedAmount.ToString("0") + " Saved";
        //yearlySavedPercentSliderText.text = yearlySavedPercent.ToString("0.0") + " % Saved";



        DailySummaryPanel();

    }

    void ReduceZeroes()
    {
        

        
    }


    void DailySummaryPanel()
    {
        monthlyMileage = (int)distancePerDaySlider.value * 30;

        dailyElectricCostText.text = "Rs " + electricityCostPerDaySlider.value.ToString() + " per charge";
        dailyPetrolCostText.text = "Rs " + petrolCostPerDaySlider.value.ToString() + " per litre";

        dailyMileageText.text = "Costs for Daily Mileage of " + distancePerDaySlider.value.ToString() + " km";
        monthlyMileageText.text = "Costs for Monthly Mileage of " + monthlyMileage.ToString() + " km";

        monthlyFuelCostText.text = "Rs " + petrolCostPerMonth.ToString("0");
        monthlyElectricCostText.text = "Rs " + eCostPerMonth.ToString("0");

        monthlyPetrolMaintenanceCostText.text = "Rs " + monthlyPetrolBikeMaintenanceCostSlider.value.ToString();
        monthlyElectricMaintenanceCostText.text = "Rs " + monthlyElectricBikeMaintenanceCostSlider.value.ToString();

        
        totalMonthlyElectricCosts = eCostPerMonth /*+ monthlyElectricBikeMaintenanceCostSlider.value*/;
        totalMonthlyPetrolCosts = petrolCostPerMonth /*+ monthlyPetrolBikeMaintenanceCostSlider.value*/;

        monthlySavings = totalMonthlyPetrolCosts - totalMonthlyElectricCosts;

        monthlySavingsText.text = "Rs " + monthlySavings.ToString() + " saved per Month";
        yearlySavingsText.text = "Rs " + (monthlySavings * 12).ToString() + " saved per year";


        petrolCostPerKm = totalMonthlyPetrolCosts / monthlyMileage;
        electricCostPerKm = totalMonthlyElectricCosts / monthlyMileage;

        petrolCostPerKmText.text = "Rs " + petrolCostPerKm.ToString("0.0") + " per km";
        electricCostPerKmText.text = "Rs " + electricCostPerKm.ToString("0.0") + " per km";
    }
}
