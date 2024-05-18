using LiveCharts;
using LiveCharts.Wpf;
using QuanLiNhaSach.Model.Service;
using QuanLiNhaSach.View.Admin.ThongKe.DoanhThu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace QuanLiNhaSach.ViewModel.AdminVM.ThongKeVM
{
    public partial class ThongKeViewModel
    {
        private SeriesCollection _revenueSeries;
        public SeriesCollection RevenueSeries
        {
            get { return _revenueSeries; }
            set
            {
                _revenueSeries = value;
                OnPropertyChanged(nameof(RevenueSeries));
            }
        }

        private string[] _labels;
        public string[] Labels
        {
            get { return _labels; }
            set
            {
                _labels = value;
                OnPropertyChanged(nameof(Labels));
            }
        }

        private Func<int, string> _yFormatter;
        public Func<int, string> YFormatter
        {
            get { return _yFormatter; }
            set
            {
                _yFormatter = value;
                OnPropertyChanged(nameof(YFormatter));
            }
        }

        private decimal _sumBillTotalPaid;
        public decimal SumBillTotalPrice
        {
            get { return _sumBillTotalPaid; }
            set
            {
                _sumBillTotalPaid = value;
                OnPropertyChanged(nameof(SumBillTotalPrice));
            }
        }


        private async Task LoadRevenueData(Frame p = null)
        {
            SumBillTotalPrice = 0;

            if (p != null)
            {
                p.Content = new DoanhThuTable();
            }

            List<int> revenueValues = new List<int>();
            List<DateTime> dates = new List<DateTime>();
            DateTime currentDate = SelectedDateFrom;
            DateTime UpDate = SelectedDateTo.AddDays(1);

            while (currentDate < UpDate)
            {
                int revenue = await BillService.Ins.getBillByDate(currentDate);
                revenueValues.Add(revenue);
                SumBillTotalPrice += revenue;
                dates.Add(currentDate);
                currentDate = currentDate.AddDays(1);
            }

            string[] dateStrings = dates.Select(date => date.ToString("dd/MM/yyyy")).ToArray();

            // Cập nhật trong UI thread
            Application.Current.Dispatcher.Invoke(() =>
            {
                RevenueSeries = new SeriesCollection
                {
                    new LineSeries
                    {
                        Title = "Doanh thu",
                        Values = new ChartValues<int>(revenueValues)
                    }
                };
                Labels = dateStrings;
                YFormatter = value => value.ToString("N");

                OnPropertyChanged(nameof(RevenueSeries));
                OnPropertyChanged(nameof(Labels));
                OnPropertyChanged(nameof(YFormatter));
            });
        }
    }
}

