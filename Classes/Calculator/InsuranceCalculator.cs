using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using Diplom.Classes;

namespace Diplom.Classes.Calculator
{
    public class InsuranceCalculator
    {
        private readonly VSK_DBEntities _context;

        public InsuranceCalculator(VSK_DBEntities context)
        {
            _context = context;
        }

        public double CalculateInsuranceCost(DateTime? startDate, DateTime? endDate, int enginePower,
            List<Drivers> drivers, string regionCoefficient)
        {
            try
            {
                if (!startDate.HasValue || !endDate.HasValue || enginePower <= 0 || drivers == null || drivers.Count == 0 || string.IsNullOrEmpty(regionCoefficient))
                {
                    throw new ArgumentException("Некорректные входные данные: проверьте даты, мощность двигателя, список водителей и региональный коэффициент.");
                }

                double tb = (1646 + 7535) / 2;

                if (!double.TryParse(regionCoefficient, NumberStyles.Any, CultureInfo.InvariantCulture, out double kt))
                {
                    throw new FormatException($"Не удалось преобразовать региональный коэффициент '{regionCoefficient}' в число.");
                }

                double kvs = drivers.Min(d => CalculateKVS(d));

                double km = CalculateKM(enginePower);

                double ks = CalculateKS(startDate.Value, endDate.Value);

                double ko = drivers.Count > 1 ? 2.32 : 1.0;

                double kbm = drivers.Max(d => CalculateKBM(d));

                double cost = tb * kt * kbm * kvs * km * ks * ko;
                return Math.Round(cost, 2);
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при расчете стоимости страховки: {ex.Message}", ex);
            }
        }

        public double CalculateKVS(Drivers driver)
        {
            int age = DateTime.Now.Year - driver.DateOfBirth.Year;
            if (driver.DateOfBirth > DateTime.Now.AddYears(-age)) age--;

            if (age <= 22)
            {
                if (driver.DrivingExperience <= 3) return 1.93;
                return 1.66;
            }
            else if (age <= 34)
            {
                if (driver.DrivingExperience <= 3) return 1.60;
                return 1.04;
            }
            else if (age <= 59)
            {
                if (driver.DrivingExperience <= 3) return 1.40;
                return 0.96;
            }
            else
            {
                if (driver.DrivingExperience <= 3) return 1.60;
                return 1.04;
            }
        }

        public double CalculateKM(int enginePower)
        {
            if (enginePower <= 50) return 0.66;
            if (enginePower <= 70) return 1.0;
            if (enginePower <= 100) return 1.1;
            if (enginePower <= 120) return 1.22;
            if (enginePower <= 150) return 1.44;
            return 1.66;
        }

        public double CalculateKS(DateTime startDate, DateTime endDate)
        {
            int months = (endDate.Year - startDate.Year) * 12 + (endDate.Month - startDate.Month);
            switch (months)
            {
                case 3:
                    return 0.5;
                case 6:
                    return 0.7;
                case 12:
                    return 1.0;
                default:
                    return 1.0;
            }
        }
        public double CalculateKBM(Drivers driver)
        {
            try
            {
                var history = _context.DriverInsuranceHistory
                    .Where(h => h.DriverID == driver.DriverID)
                    .OrderByDescending(h => h.LastUpdated)
                    .ToList();

                if (!history.Any())
                {
                    return 1.17; 
                }

                var latest = history.First();
                double currentKBM = (double)latest.KBM;
                int currentClass = GetClassFromKBM(currentKBM);

                if (latest.LastUpdated.Year < DateTime.Now.Year)
                {
                    int newClass;

                    if (latest.HadAccident)
                    {
                        newClass = Math.Max(currentClass - 2, -1);
                    }
                    else
                    {
                        newClass = Math.Min(currentClass + 1, 13);
                    }

                    return GetKBMFromClass(newClass);
                }

                return currentKBM;
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при расчете КБМ для водителя {driver.DriverID}: {ex.Message}", ex);
            }
        }

        public int GetClassFromKBM(double kbm)
        {
            switch (kbm)
            {
                case 2.45:
                    return -1;
                case 2.30:
                    return 0;
                case 1.55:
                    return 1;
                case 1.40:
                    return 2;
                case 1.00:
                    return 3;
                case 0.95:
                    return 4;
                case 0.90:
                    return 5;
                case 0.85:
                    return 6;
                case 0.80:
                    return 7;
                case 0.75:
                    return 8;
                case 0.70:
                    return 9;
                case 0.65:
                    return 10;
                case 0.60:
                    return 11;
                case 0.55:
                    return 12;
                case 0.50:
                    return 13;
                default:
                    return 3;
            }
        }

        public double GetKBMFromClass(int classValue)
        {
            switch (classValue)
            {
                case -1:
                    return 2.45;
                case 0:
                    return 2.30;
                case 1:
                    return 1.55;
                case 2:
                    return 1.40;
                case 3:
                    return 1.00;
                case 4:
                    return 0.95;
                case 5:
                    return 0.90;
                case 6:
                    return 0.85;
                case 7:
                    return 0.80;
                case 8:
                    return 0.75;
                case 9:
                    return 0.70;
                case 10:
                    return 0.65;
                case 11:
                    return 0.60;
                case 12:
                    return 0.55;
                case 13:
                    return 0.50;
                default:
                    return 1.00;
            }
        }
    }
}