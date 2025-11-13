using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;
using static PersonalFinancialManager.source.DataService;

namespace PersonalFinancialManager.source
{
    public class SpecialStatisticChart : StatisticChart
    {
        private Func<DateTime, DateTime, (int, int, int), IEnumerable<DataService.StatisticDataUnit>> getSpecialDataFunc;

        private TextBox specialChartSeries1DateFromTextBox;
        private TextBox specialChartSeries1DateUntilTextBox;
        private CheckBox specialChartSeries1HideCheckBox;
        private CheckBox specialChartSeries3HideCheckBox;
        private TextBox specialChartSeries3DateUntilTextBox;
        private TextBox specialChartSeries3DateFromTextBox;
        private CheckBox specialChartSeries2HideCheckBox;
        private TextBox specialChartSeries2DateUntilTextBox;
        private TextBox specialChartSeries2DateFromTextBox;
        public ComboBox specialChartIntervalComboBox;


        private readonly Dictionary<string, SpecialChartInterval> SPECIAL_CHART_INTERVAL_PAIRS = new Dictionary<string, SpecialChartInterval>()
        {
             { "День", SpecialChartInterval.Day },
             { "Месяц", SpecialChartInterval.Month },
             { "Год", SpecialChartInterval.Year }
        };


        private readonly Color SPECIAL_CHART_SERIES2_COLOR = Color.Purple;
        private readonly Color SPECIAL_CHART_SERIES3_COLOR = Color.Red;




        public SpecialStatisticChart(ref Chart chart, Font font,
            Func<DateTime, DateTime, (int, int, int), IEnumerable<DataService.StatisticDataUnit>> getDataFunc,
            ref TextBox specialChartSeries1DateFromTextBox,
            ref TextBox specialChartSeries1DateUntilTextBox,
            ref CheckBox specialChartSeries1HideCheckBox,
            ref CheckBox specialChartSeries3HideCheckBox,
            ref TextBox specialChartSeries3DateUntilTextBox,
            ref TextBox specialChartSeries3DateFromTextBox,
            ref CheckBox specialChartSeries2HideCheckBox,
            ref TextBox specialChartSeries2DateUntilTextBox,
            ref TextBox specialChartSeries2DateFromTextBox,
            ref ComboBox specialChartIntervalComboBox
            )
            : base(ref chart, String.Empty, font, null, String.Empty)
        {
            this.getSpecialDataFunc = getDataFunc;
            this.specialChartSeries1DateFromTextBox = specialChartSeries1DateFromTextBox;
            this.specialChartSeries1DateUntilTextBox = specialChartSeries1DateUntilTextBox;
            this.specialChartSeries1HideCheckBox = specialChartSeries1HideCheckBox;
            this.specialChartSeries3HideCheckBox = specialChartSeries3HideCheckBox;
            this.specialChartSeries3DateUntilTextBox = specialChartSeries3DateUntilTextBox;
            this.specialChartSeries3DateFromTextBox = specialChartSeries3DateFromTextBox;
            this.specialChartSeries2HideCheckBox = specialChartSeries2HideCheckBox;
            this.specialChartSeries2DateUntilTextBox = specialChartSeries2DateUntilTextBox;
            this.specialChartSeries2DateFromTextBox = specialChartSeries2DateFromTextBox;
            this.specialChartIntervalComboBox = specialChartIntervalComboBox;
        }

        private static class SpecialChartInput
        {
            public static TextBox[] FromTextBoxes;
            public static TextBox[] UntilTextBoxes;
            public static CheckBox[] CheckBoxes;
        }

        public override void Initialize()
        {
            base.Initialize();

            chart.ChartAreas[0].Position.X = 0;
            chart.ChartAreas[0].Position.Y = 0;
            chart.ChartAreas[0].Position.Width = 100;
            chart.ChartAreas[0].Position.Height = 80;

            Legend legend = new Legend();

            legend.Position.Height = 15;
            legend.Position.Width = 25;
            legend.Position.X = 0;
            legend.Position.Y = 83;
            legend.LegendStyle = LegendStyle.Column;
            legend.Font = font;

            chart.Legends.Add(legend);

            Series secondSeries = new Series();
            Series thirdSeries = new Series();
            thirdSeries.ChartType = secondSeries.ChartType = chart.Series[0].ChartType;
            thirdSeries.XValueType = secondSeries.XValueType = chart.Series[0].XValueType;
            thirdSeries.YValueType = secondSeries.YValueType = chart.Series[0].YValueType;
            thirdSeries.IsXValueIndexed = secondSeries.IsXValueIndexed = chart.Series[0].IsXValueIndexed;
            thirdSeries.IsValueShownAsLabel = secondSeries.IsValueShownAsLabel = chart.Series[0].IsValueShownAsLabel;
            thirdSeries.Font = secondSeries.Font = chart.Series[0].Font;
            secondSeries.Color = SPECIAL_CHART_SERIES2_COLOR;
            thirdSeries.Color = SPECIAL_CHART_SERIES3_COLOR;
            thirdSeries.BorderWidth = secondSeries.BorderWidth = chart.Series[0].BorderWidth;


            chart.Series.Add(secondSeries);
            chart.Series.Add(thirdSeries);

            chart.Series[0].Name = "График под номером 1";
            chart.Series[1].Name = "График под номером 2";
            chart.Series[2].Name = "График под номером 3";


            foreach (KeyValuePair<string, SpecialChartInterval> pair in SPECIAL_CHART_INTERVAL_PAIRS)
            {
                specialChartIntervalComboBox.Items.Add(pair.Key);
            }

            SpecialChartInput.FromTextBoxes = new TextBox[] {
                specialChartSeries1DateFromTextBox,
                specialChartSeries2DateFromTextBox,
                specialChartSeries3DateFromTextBox
            };

            SpecialChartInput.UntilTextBoxes = new TextBox[] {
                specialChartSeries1DateUntilTextBox,
                specialChartSeries2DateUntilTextBox,
                specialChartSeries3DateUntilTextBox,
            };

            SpecialChartInput.CheckBoxes = new CheckBox[] {
                specialChartSeries1HideCheckBox,
                specialChartSeries2HideCheckBox,
                specialChartSeries3HideCheckBox
            };


            for (int i = 0; i < 3; i++)
            {
                SpecialChartInput.FromTextBoxes[i].Leave += Update;
                SpecialChartInput.FromTextBoxes[i].KeyPress += SpecialChartInputTextBoxEnterPressedEvent;
                SpecialChartInput.UntilTextBoxes[i].Leave += Update;
                SpecialChartInput.UntilTextBoxes[i].KeyPress += SpecialChartInputTextBoxEnterPressedEvent;
                SpecialChartInput.CheckBoxes[i].CheckedChanged += Update;
            }

            DateTime curMonthFrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime curMonthUntil = curMonthFrom.AddMonths(1).AddDays(-1);
            DateTime prevMonthFrom = curMonthFrom.AddMonths(-1);
            DateTime prevMonthUntil = curMonthFrom.AddDays(-1);

            specialChartSeries1DateFromTextBox.Text = curMonthFrom.ToString("dd.MM.yyyy");
            specialChartSeries1DateUntilTextBox.Text = curMonthUntil.ToString("dd.MM.yyyy");
            specialChartSeries2DateFromTextBox.Text = prevMonthFrom.ToString("dd.MM.yyyy");
            specialChartSeries2DateUntilTextBox.Text = prevMonthUntil.ToString("dd.MM.yyyy");

            specialChartIntervalComboBox.Text = SPECIAL_CHART_INTERVAL_PAIRS.First().Key;
            specialChartIntervalComboBox.TextChanged += Update;
        }

        public override void Update()
        {
            Update(null, null);
        }


        public void Update(object? sender = null, EventArgs e = null)
        {

            chart.ChartAreas[0].AxisY.Maximum = 0;

            for (int i = 0; i < 3; i++)
            {
                chart.Series[i].Points.Clear();

                if (SpecialChartInput.CheckBoxes[i].Checked)
                {
                    continue;
                }

                if (IsDateCorrect(ref SpecialChartInput.FromTextBoxes[i], out DateTime fromDate) &&
                    IsDateCorrect(ref SpecialChartInput.UntilTextBoxes[i], out DateTime untilDate))
                {
                    double maximum = chart.ChartAreas[0].AxisY.Maximum;
                    (int d, int m, int y) interval = GetSpecialChartDateTimeInterval(out string dateTimeFormatString);
                    int moneyInterval;

                    foreach (StatisticDataUnit data in getSpecialDataFunc(fromDate, untilDate, interval))
                    {
                        int index = chart.Series[i].Points.AddXY(data.date.ToString(dateTimeFormatString), data.Value);
                        chart.Series[i].Points[index].Font = new Font(font.FontFamily, font.Size + 1, FontStyle.Bold);

                        if (data.date > DateTime.Now)
                            chart.Series[i].Points[index].IsEmpty = data.Value == 0;

                        maximum = Math.Max(maximum, data.Value);
                    }


                    if (maximum == 0)
                    {
                        moneyInterval = DEFAULT_CHART_AXIS_Y_INTERVAL;
                        chart.ChartAreas[0].AxisY.Maximum = DEFAULT_CHART_AXIS_Y_INTERVAL * DEFAULT_CHART_AXIS_Y_INTERVAL_COUNT;
                    }
                    else
                    {
                        if (maximum < chart.ChartAreas[0].AxisY.Maximum)
                            maximum = GetNearestRoundValue(maximum);

                        chart.ChartAreas[0].AxisY.Maximum = maximum;
                        moneyInterval = Convert.ToInt32(maximum / DEFAULT_CHART_AXIS_Y_INTERVAL_COUNT);
                    }

                    chart.ChartAreas[0].AxisY.Interval = moneyInterval;
                }
            }
        }


        private void SpecialChartInputTextBoxEnterPressedEvent(object? sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)ConsoleKey.Enter)
            {
                Update();
                chart.Focus();
            }
        }


        private (int d, int m, int y) GetSpecialChartDateTimeInterval(out string dateTimeFormatString)
        {
            switch (SPECIAL_CHART_INTERVAL_PAIRS.GetValueOrDefault(specialChartIntervalComboBox.Text))
            {
                case SpecialChartInterval.Day:
                    dateTimeFormatString = "dd";
                    return (1, 0, 0);
                case SpecialChartInterval.Month:
                    dateTimeFormatString = "MMMM";
                    return (0, 1, 0);
                case SpecialChartInterval.Year:
                default:
                    dateTimeFormatString = "yyyy";
                    return (0, 0, 1);
            }
        }



        private bool IsDateCorrect(ref TextBox textBox, out DateTime dateTime)
        {
            if (DateTime.TryParse(textBox.Text, out dateTime))
            {
                textBox.Text = dateTime.ToString("dd.MM.yyyy");
                return true;
            }
            return false;
        }


        private enum SpecialChartInterval
        {
            Day,
            Month,
            Year
        }

    }
}
