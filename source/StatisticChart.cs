using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;
using static PersonalFinancialManager.source.DataService;

namespace PersonalFinancialManager.source
{
    public class StatisticChart 
    {
        protected Chart chart;
        protected string titleName;
        protected Font font;
        protected Func<IEnumerable<StatisticDataUnit>> getDataFunc;
        protected string dateTimeFormatString;
        protected const int DEFAULT_CHART_AXIS_Y_INTERVAL_COUNT = 10;
        protected const int DEFAULT_CHART_AXIS_Y_INTERVAL = 100;

        public static readonly Color DEFAULT_CHART_SERIES_COLOR = Color.Blue;

        public StatisticChart(ref Chart chart, string titleName, Font font,
            Func<IEnumerable<StatisticDataUnit>> getDataFunc, string dateTimeFormatString
            )
        {
            this.chart = chart;
            this.titleName = titleName;
            this.font = font;
            this.getDataFunc = getDataFunc;
            this.dateTimeFormatString = dateTimeFormatString;
        }

        public virtual void Initialize()
        {
            ChartArea chartArea = new ChartArea();
            chartArea.IsSameFontSizeForAllAxes = true;
            chartArea.BorderDashStyle = ChartDashStyle.Solid;
            chartArea.BorderWidth = 1;
            chartArea.BorderColor = Color.WhiteSmoke;

            chartArea.AxisY.IntervalAutoMode = IntervalAutoMode.FixedCount;

            // Point list contain x and y as a double type, so if you add x as a string,
            // there will be 0 every time you add new x, and therefore chart will have a lot of points with x = 0,
            // and Chart display only one. AxisX.Interval = 1 solves this problem
            chartArea.AxisX.Interval = 1;

            chartArea.AxisX.MajorGrid.LineWidth = 1;
            chartArea.AxisY.MajorGrid.LineWidth = 1;
            chartArea.AxisX.MajorGrid.LineColor = Color.DarkGray;
            chartArea.AxisY.MajorGrid.LineColor = Color.DarkGray;
            chartArea.AxisX.LineWidth = 2;
            chartArea.AxisY.LineWidth = 2;
            chartArea.Position.Auto = false;
            chartArea.Position.X = 0;
            chartArea.Position.Y = 100;
            chartArea.Position.Width = 95;
            chartArea.Position.Height = 95;

            chart.ChartAreas.Add(chartArea);

            Series series = new Series();
            series.ChartType = SeriesChartType.Line;
            series.XValueType = ChartValueType.String;
            series.YValueType = ChartValueType.Int32;
            series.IsXValueIndexed = true;
            series.IsValueShownAsLabel = true;
            series.Font = font;
            series.Color = DEFAULT_CHART_SERIES_COLOR;
            series.BorderWidth = 3;

            chart.Series.Add(series);

            Title title = new Title();
            title.Position.Auto = false;
            title.Position.X = 45.5f;
            title.Position.Y = 2;
            title.Position.Width = 10;
            title.Position.Height = 5;
            title.Font = new Font(font.FontFamily, font.Size + 2, FontStyle.Bold);
            title.Text = titleName;

            chart.Titles.Add(title);
        }

        public virtual void Update()
        {
            chart.Series[0].Points.Clear();

            double maximum = 0;
            int interval;

            foreach (StatisticDataUnit data in getDataFunc())
            {
                int i = chart.Series[0].Points.AddXY(data.date.ToString(dateTimeFormatString), data.Value);
                chart.Series[0].Points[i].Font = new Font(font.FontFamily, font.Size + 1, FontStyle.Bold);

                if (data.date > DateTime.Now)
                    chart.Series[0].Points[i].IsEmpty = data.Value == 0;

                maximum = Math.Max(maximum, data.Value);
            }

            if (maximum == 0)
            {
                interval = DEFAULT_CHART_AXIS_Y_INTERVAL;
                chart.ChartAreas[0].AxisY.Maximum = DEFAULT_CHART_AXIS_Y_INTERVAL * DEFAULT_CHART_AXIS_Y_INTERVAL_COUNT;
            }
            else
            {
                maximum = GetNearestRoundValue(maximum);
                chart.ChartAreas[0].AxisY.Maximum = maximum;
                interval = Convert.ToInt32(maximum / DEFAULT_CHART_AXIS_Y_INTERVAL_COUNT);
            }

            chart.ChartAreas[0].AxisY.Interval = interval;
        }


        protected double GetNearestRoundValue(double n)
        {
            string value = ((int)n).ToString();

            if (value.Length < 2)
            {
                return 10;
            }
            else
            {
                int firstNumber = value[0] - '0';
                int secondNumber = value[1] - '0';

                if (secondNumber < 5)
                {
                    secondNumber = 5;
                }
                else
                {
                    secondNumber = 0;
                    firstNumber++;
                }

                return (firstNumber * 10 + secondNumber) * Math.Pow(10, value.Length - 2);
            }
        }



    }
}
