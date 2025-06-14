using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Diplom.Classes;

namespace Diplom.Classes.Validator
{
    public class ValidationResult
    {
        public bool IsValid { get; }
        public string ErrorMessage { get; }

        public ValidationResult(bool isValid, string errorMessage = null)
        {
            IsValid = isValid;
            ErrorMessage = errorMessage;
        }
    }

    public class ClientValidator
    {
        private readonly VSK_DBEntities _context;

        public ClientValidator(VSK_DBEntities context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public ValidationResult ValidateClientType(ClientTypes clientType)
        {
            if (clientType == null)
                return new ValidationResult(false, "Выберите тип клиента.");
            return new ValidationResult(true);
        }

        public ValidationResult ValidateLastName(string lastName)
        {
            if (string.IsNullOrWhiteSpace(lastName) || !Regex.IsMatch(lastName, @"^[А-Яа-яЁё\-]{2,50}$"))
                return new ValidationResult(false, "Фамилия: только кириллица и дефис, 2–50 символов.");
            return new ValidationResult(true);
        }

        public ValidationResult ValidateFirstName(string firstName)
        {
            if (string.IsNullOrWhiteSpace(firstName) || !Regex.IsMatch(firstName, @"^[А-Яа-яЁё\-]{2,50}$"))
                return new ValidationResult(false, "Имя: только кириллица и дефис, 2–50 символов.");
            return new ValidationResult(true);
        }

        public ValidationResult ValidateMiddleName(string middleName)
        {
            if (string.IsNullOrWhiteSpace(middleName) || !Regex.IsMatch(middleName, @"^[А-Яа-яЁё\-]{2,50}$"))
                return new ValidationResult(false, "Отчество: только кириллица и дефис, 2–50 символов.");
            return new ValidationResult(true);
        }

        public ValidationResult ValidateCompanyName(string companyName)
        {
            if (string.IsNullOrWhiteSpace(companyName) || !Regex.IsMatch(companyName, @"^[А-Яа-яЁё0-9\s.,\-]{3,100}$"))
                return new ValidationResult(false, "Название компании: буквы, цифры, пробелы, 3–100 символов.");
            return new ValidationResult(true);
        }

        public ValidationResult ValidatePassportNumber(string passportNumber)
        {
            if (!string.IsNullOrEmpty(passportNumber) && !Regex.IsMatch(passportNumber, @"^\d{4}\s\d{6}$"))
                return new ValidationResult(false, "Номер паспорта: XXXX XXXXXX (4 цифры, пробел, 6 цифр).");
            return new ValidationResult(true);
        }

        public ValidationResult ValidateINN(string inn, int clientTypeId)
        {
            if (string.IsNullOrWhiteSpace(inn))
                return new ValidationResult(false, "ИНН обязателен.");
            if (clientTypeId == 1 && !Regex.IsMatch(inn, @"^\d{12}$"))
                return new ValidationResult(false, "ИНН для физического лица: 12 цифр.");
            if (clientTypeId == 2 && !Regex.IsMatch(inn, @"^\d{10}$"))
                return new ValidationResult(false, "ИНН для юридического лица: 10 цифр.");
            return new ValidationResult(true);
        }

        public ValidationResult ValidatePhone(string phone, int? existingClientId = null)
        {
            if (string.IsNullOrWhiteSpace(phone) || !Regex.IsMatch(phone, @"^(\+7|8)\d{10}$"))
                return new ValidationResult(false, "Телефон: +7XXXXXXXXXX или 8XXXXXXXXXX.");
            if (_context.Clients.Any(c => c.Phone == phone && (!existingClientId.HasValue || c.ClientID != existingClientId)))
                return new ValidationResult(false, "Телефон уже используется.");
            return new ValidationResult(true);
        }

        public ValidationResult ValidateEmail(string email, int? existingClientId = null)
        {
            if (string.IsNullOrWhiteSpace(email) || !Regex.IsMatch(email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
                return new ValidationResult(false, "Некорректный формат email.");
            if (_context.Clients.Any(c => c.Email == email && (!existingClientId.HasValue || c.ClientID != existingClientId)))
                return new ValidationResult(false, "Email уже используется.");
            return new ValidationResult(true);
        }

        public ValidationResult ValidateClient(
            ClientTypes clientType,
            string lastName,
            string firstName,
            string middleName,
            string companyName,
            string passportNumber,
            string inn,
            string phone,
            string email)
        {
            var typeResult = ValidateClientType(clientType);
            if (!typeResult.IsValid) return typeResult;

            if (clientType.ClientTypeID == 1) 
            {
                var lastNameResult = ValidateLastName(lastName);
                if (!lastNameResult.IsValid) return lastNameResult;

                var firstNameResult = ValidateFirstName(firstName);
                if (!firstNameResult.IsValid) return firstNameResult;

                var middleNameResult = ValidateMiddleName(middleName);
                if (!middleNameResult.IsValid) return middleNameResult;
            }
            else if (clientType.ClientTypeID == 2) 
            {
                var companyNameResult = ValidateCompanyName(companyName);
                if (!companyNameResult.IsValid) return companyNameResult;
            }

            var passportResult = ValidatePassportNumber(passportNumber);
            if (!passportResult.IsValid) return passportResult;

            var innResult = ValidateINN(inn, clientType.ClientTypeID);
            if (!innResult.IsValid) return innResult;

            var phoneResult = ValidatePhone(phone);
            if (!phoneResult.IsValid) return phoneResult;

            var emailResult = ValidateEmail(email);
            if (!emailResult.IsValid) return emailResult;

            return new ValidationResult(true);
        }
    }

    public class DriverValidator
    {
        private readonly VSK_DBEntities _context;

        public DriverValidator(VSK_DBEntities context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public ValidationResult ValidateLastName(string lastName)
        {
            if (string.IsNullOrWhiteSpace(lastName) || !Regex.IsMatch(lastName, @"^[А-Яа-яЁё\-]{2,50}$"))
                return new ValidationResult(false, "Фамилия: только кириллица и дефис, 2–50 символов.");
            return new ValidationResult(true);
        }

        public ValidationResult ValidateFirstName(string firstName)
        {
            if (string.IsNullOrWhiteSpace(firstName) || !Regex.IsMatch(firstName, @"^[А-Яа-яЁё\-]{2,50}$"))
                return new ValidationResult(false, "Имя: только кириллица и дефис, 2–50 символов.");
            return new ValidationResult(true);
        }

        public ValidationResult ValidateMiddleName(string middleName)
        {
            if (string.IsNullOrWhiteSpace(middleName) || !Regex.IsMatch(middleName, @"^[А-Яа-яЁё\-]{2,50}$"))
                return new ValidationResult(false, "Отчество: только кириллица и дефис, 2–50 символов.");
            return new ValidationResult(true);
        }

        public ValidationResult ValidateDateOfBirth(DateTime? dateOfBirth)
        {
            if (!dateOfBirth.HasValue || dateOfBirth.Value > DateTime.Today.AddYears(-18))
                return new ValidationResult(false, "Водитель должен быть старше 18 лет.");
            return new ValidationResult(true);
        }

        public ValidationResult ValidateLicenseNumber(string licenseNumber)
        {
            if (string.IsNullOrWhiteSpace(licenseNumber) || !Regex.IsMatch(licenseNumber, @"^[А-Я]{2}\s\d{2}\s\d{6}$"))
                return new ValidationResult(false, "Номер удостоверения: XX XX XXXXXX (2 буквы, 2 цифры, 6 цифр).");
            if (_context.Drivers.Any(d => d.LicenseNumber == licenseNumber))
                return new ValidationResult(false, "Номер удостоверения уже зарегистрирован.");
            return new ValidationResult(true);
        }

        public ValidationResult ValidateDrivingExperience(int? experience, DateTime dateOfBirth)
        {
            if (!experience.HasValue || experience < 0 || experience > 100)
                return new ValidationResult(false, "Стаж: от 0 до 100 лет.");

            var today = DateTime.Today;
            int age = today.Year - dateOfBirth.Year;
            if (dateOfBirth.Date > today.AddYears(-age)) age--;

            int maxExperience = age - 18;
            if (maxExperience < 0) maxExperience = 0;

            if (experience > maxExperience)
            {
                return new ValidationResult(false, $"Стаж: не может превышать {maxExperience} лет (ограничено возрастом и 18 годами).");
            }

            return new ValidationResult(true);
        }

        public ValidationResult ValidatePassportNumber(string passportNumber)
        {
            if (string.IsNullOrWhiteSpace(passportNumber) || !Regex.IsMatch(passportNumber, @"^\d{4}\s\d{6}$"))
                return new ValidationResult(false, "Номер паспорта: XXXX XXXXXX (4 цифры, пробел, 6 цифр).");
            return new ValidationResult(true);
        }

        public ValidationResult ValidateINN(string inn)
        {
            if (string.IsNullOrWhiteSpace(inn) || !Regex.IsMatch(inn, @"^\d{12}$"))
                return new ValidationResult(false, "ИНН: 12 цифр.");
            return new ValidationResult(true);
        }

        public ValidationResult ValidatePhone(string phone, int? existingClientId = null)
        {
            if (string.IsNullOrWhiteSpace(phone) || !Regex.IsMatch(phone, @"^(\+7|8)\d{10}$"))
                return new ValidationResult(false, "Телефон: +7XXXXXXXXXX или 8XXXXXXXXXX.");
            if (_context.Clients.Any(c => c.Phone == phone && (!existingClientId.HasValue || c.ClientID != existingClientId)))
                return new ValidationResult(false, "Телефон уже используется.");
            return new ValidationResult(true);
        }

        public ValidationResult ValidateEmail(string email, int? existingClientId = null)
        {
            if (string.IsNullOrWhiteSpace(email) || !Regex.IsMatch(email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
                return new ValidationResult(false, "Некорректный формат email.");
            if (_context.Clients.Any(c => c.Email == email && (!existingClientId.HasValue || c.ClientID != existingClientId)))
                return new ValidationResult(false, "Email уже используется.");
            return new ValidationResult(true);
        }

        public ValidationResult ValidateDriver(
            string lastName,
            string firstName,
            string middleName,
            DateTime? dateOfBirth,
            string licenseNumber,
            int? experience,
            string passportNumber,
            string inn,
            string phone,
            string email)
        {
            var lastNameResult = ValidateLastName(lastName);
            if (!lastNameResult.IsValid) return lastNameResult;

            var firstNameResult = ValidateFirstName(firstName);
            if (!firstNameResult.IsValid) return firstNameResult;

            var middleNameResult = ValidateMiddleName(middleName);
            if (!middleNameResult.IsValid) return middleNameResult;

            var dobResult = ValidateDateOfBirth(dateOfBirth);
            if (!dobResult.IsValid) return dobResult;

            var licenseResult = ValidateLicenseNumber(licenseNumber);
            if (!licenseResult.IsValid) return licenseResult;

            var experienceResult = ValidateDrivingExperience(experience, dateOfBirth.Value);
            if (!experienceResult.IsValid) return experienceResult;

            var passportResult = ValidatePassportNumber(passportNumber);
            if (!passportResult.IsValid) return passportResult;

            var innResult = ValidateINN(inn);
            if (!innResult.IsValid) return innResult;

            var phoneResult = ValidatePhone(phone);
            if (!phoneResult.IsValid) return phoneResult;

            var emailResult = ValidateEmail(email);
            if (!emailResult.IsValid) return emailResult;

            return new ValidationResult(true);
        }
    }

    public class VehicleValidator
    {
        private readonly VSK_DBEntities _context;

        public VehicleValidator(VSK_DBEntities context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public ValidationResult ValidateVehicleMake(VehicleMakes vehicleMake)
        {
            if (vehicleMake == null)
                return new ValidationResult(false, "Выберите марку автомобиля.");
            return new ValidationResult(true);
        }

        public ValidationResult ValidateVehicleModel(VehicleModels vehicleModel)
        {
            if (vehicleModel == null)
                return new ValidationResult(false, "Выберите модель автомобиля.");
            return new ValidationResult(true);
        }

        public ValidationResult ValidateVIN(string vin, int? vehicleId = null)
        {
            if (string.IsNullOrWhiteSpace(vin))
                return new ValidationResult(false, "VIN не может быть пустым");
            vin = vin.ToUpperInvariant();
            if (!Regex.IsMatch(vin, @"^[A-HJ-NPR-Z0-9]{17}$"))
                return new ValidationResult(false, "VIN: строго 17 символов (только латиница, без I, O, Q, без пробелов, только заглавные)");
            
            if (vehicleId.HasValue)
            {
                var existingVehicle = _context.Vehicles.FirstOrDefault(v => v.VIN == vin && v.VehicleID != vehicleId.Value);
                if (existingVehicle != null)
                    return new ValidationResult(false, "VIN уже зарегистрирован на другой автомобиль.");
            }
            else
            {
                if (_context.Vehicles.Any(v => v.VIN == vin))
                    return new ValidationResult(false, "VIN уже зарегистрирован.");
            }
            return new ValidationResult(true);
        }

        public ValidationResult ValidateYear(int? year)
        {
            int currentYear = DateTime.Now.Year + 1;
            if (!year.HasValue || year < 1900 || year > currentYear)
                return new ValidationResult(false, $"Год выпуска: от 1900 до {currentYear}.");
            if (year.Value.ToString().Length != 4)
                return new ValidationResult(false, "Год выпуска: строго 4 цифры.");
            return new ValidationResult(true);
        }

        public ValidationResult ValidateLicensePlate(string licensePlate)
        {
            if (string.IsNullOrWhiteSpace(licensePlate))
                return new ValidationResult(false, "Номер не может быть пустым");
            licensePlate = licensePlate.ToUpperInvariant();
            if (!Regex.IsMatch(licensePlate, @"^[А-Я]{1}\d{3}[А-Я]{2}\d{2,3}$"))
                return new ValidationResult(false, "Формат номера: Х123ХХ12 или Х123ХХ123 (строго: 1 буква, 3 цифры, 2 буквы, 2 или 3 цифры, только кириллица, без пробелов)");
            return new ValidationResult(true);
        }

        public ValidationResult ValidateRegion(object region)
        {
            if (region == null)
                return new ValidationResult(false, "Выберите регион регистрации.");
            return new ValidationResult(true);
        }

        public ValidationResult ValidateEnginePower(int? enginePower)
        {
            if (!enginePower.HasValue || enginePower < 1 || enginePower > 1000)
                return new ValidationResult(false, "Мощность: от 1 до 1000 л.с.");
            return new ValidationResult(true);
        }

        public ValidationResult ValidateVehicle(
            VehicleMakes vehicleMake,
            VehicleModels vehicleModel,
            string vin,
            int? year,
            string licensePlate,
            object region,
            int? enginePower)
        {
            var makeResult = ValidateVehicleMake(vehicleMake);
            if (!makeResult.IsValid) return makeResult;

            var modelResult = ValidateVehicleModel(vehicleModel);
            if (!modelResult.IsValid) return modelResult;

            var vinResult = ValidateVIN(vin);
            if (!vinResult.IsValid) return vinResult;

            var yearResult = ValidateYear(year);
            if (!yearResult.IsValid) return yearResult;

            var plateResult = ValidateLicensePlate(licensePlate);
            if (!plateResult.IsValid) return plateResult;

            var regionResult = ValidateRegion(region);
            if (!regionResult.IsValid) return regionResult;

            var powerResult = ValidateEnginePower(enginePower);
            if (!powerResult.IsValid) return powerResult;

            return new ValidationResult(true);
        }
    }

    public class PolicyValidator
    {
        public ValidationResult ValidatePolicyType(PolicyTypes policyType)
        {
            if (policyType == null)
                return new ValidationResult(false, "Выберите тип страхования.");
            return new ValidationResult(true);
        }

        public ValidationResult ValidateStatus(PolicyStatuses status)
        {
            if (status == null)
                return new ValidationResult(false, "Выберите статус полиса.");
            return new ValidationResult(true);
        }

        public ValidationResult ValidateStartDate(DateTime? startDate)
        {
            if (!startDate.HasValue || startDate.Value < DateTime.Today)
                return new ValidationResult(false, "Дата начала не может быть раньше текущей даты.");
            return new ValidationResult(true);
        }

        public ValidationResult ValidateEndDate(DateTime? endDate, DateTime? startDate)
        {
            if (!endDate.HasValue || !startDate.HasValue || endDate.Value <= startDate.Value)
                return new ValidationResult(false, "Дата окончания должна быть позже даты начала.");
            if (endDate.Value > startDate.Value.AddYears(1))
                return new ValidationResult(false, "Срок действия полиса не может превышать 1 год.");
            return new ValidationResult(true);
        }

        public ValidationResult ValidateInsuranceAmount(decimal? amount)
        {
            if (!amount.HasValue || amount <= 0)
                return new ValidationResult(false, "Сумма страхования должна быть больше 0.");
            return new ValidationResult(true);
        }

        public ValidationResult ValidateClients(ICollection<Clients> clients)
        {
            if (clients == null || !clients.Any())
                return new ValidationResult(false, "Добавьте хотя бы одного клиента.");
            return new ValidationResult(true);
        }

        public ValidationResult ValidateDrivers(ICollection<Drivers> drivers)
        {
            if (drivers == null || !drivers.Any())
                return new ValidationResult(false, "Добавьте хотя бы одного водителя.");
            return new ValidationResult(true);
        }

        public ValidationResult ValidatePolicy(
            PolicyTypes policyType,
            PolicyStatuses status,
            DateTime? startDate,
            DateTime? endDate,
            decimal? insuranceAmount,
            ICollection<Clients> clients,
            ICollection<Drivers> drivers)
        {
            var typeResult = ValidatePolicyType(policyType);
            if (!typeResult.IsValid) return typeResult;

            var statusResult = ValidateStatus(status);
            if (!statusResult.IsValid) return statusResult;

            var startDateResult = ValidateStartDate(startDate);
            if (!startDateResult.IsValid) return startDateResult;

            var endDateResult = ValidateEndDate(endDate, startDate);
            if (!endDateResult.IsValid) return endDateResult;

            var amountResult = ValidateInsuranceAmount(insuranceAmount);
            if (!amountResult.IsValid) return amountResult;

            var clientsResult = ValidateClients(clients);
            if (!clientsResult.IsValid) return clientsResult;

            var driversResult = ValidateDrivers(drivers);
            if (!driversResult.IsValid) return driversResult;

            return new ValidationResult(true);
        }
    }

    public class LifeInsuranceValidator
    {
        private readonly VSK_DBEntities _context;

        public LifeInsuranceValidator(VSK_DBEntities context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public ValidationResult ValidateAge(int? age)
        {
            if (!age.HasValue || age < 0 || age > 120)
                return new ValidationResult(false, "Возраст должен быть в диапазоне от 0 до 120 лет.");
            return new ValidationResult(true);
        }

        public ValidationResult ValidateGender(string gender)
        {
            if (string.IsNullOrWhiteSpace(gender))
                return new ValidationResult(false, "Выберите пол.");
            if (gender != "Мужской" && gender != "Женский")
                return new ValidationResult(false, "Некорректное значение пола.");
            return new ValidationResult(true);
        }

        public ValidationResult ValidateHealthCondition(HealthConditions healthCondition)
        {
            if (healthCondition == null)
                return new ValidationResult(false, "Выберите состояние здоровья.");
            return new ValidationResult(true);
        }

        public ValidationResult ValidateDates(DateTime? startDate, DateTime? endDate)
        {
            if (!startDate.HasValue || !endDate.HasValue)
                return new ValidationResult(false, "Укажите даты начала и окончания.");
            
            if (startDate.Value < DateTime.Today)
                return new ValidationResult(false, "Дата начала не может быть раньше текущей даты.");
                
            if (endDate.Value <= startDate.Value)
                return new ValidationResult(false, "Дата окончания должна быть позже даты начала.");
            return new ValidationResult(true);
        }

        public ValidationResult ValidateOccupation(string occupation)
        {
            if (string.IsNullOrWhiteSpace(occupation))
                return new ValidationResult(false, "Укажите род занятий.");
            if (!Regex.IsMatch(occupation, @"^[А-Яа-яЁё\s\-]{2,100}$"))
                return new ValidationResult(false, "Род занятий: только кириллица, пробелы и дефис, 2-100 символов.");
            return new ValidationResult(true);
        }

        public ValidationResult ValidateInsuranceAmount(decimal? amount)
        {
            if (!amount.HasValue || amount <= 0)
                return new ValidationResult(false, "Сумма страхования должна быть больше 0.");
            return new ValidationResult(true);
        }

        public ValidationResult ValidatePolicyType(PolicyTypes policyType)
        {
            if (policyType == null)
                return new ValidationResult(false, "Выберите тип полиса.");
            return new ValidationResult(true);
        }

        public ValidationResult ValidateStatus(PolicyStatuses status)
        {
            if (status == null)
                return new ValidationResult(false, "Выберите статус полиса.");
            return new ValidationResult(true);
        }

        public ValidationResult ValidateClients(ICollection<Clients> clients)
        {
            if (clients == null || !clients.Any())
                return new ValidationResult(false, "Добавьте хотя бы одного клиента.");
            return new ValidationResult(true);
        }

        public ValidationResult ValidateLifeInsurance(
            int? age,
            string gender,
            HealthConditions healthCondition,
            string occupation,
            decimal? insuranceAmount,
            DateTime? startDate,
            DateTime? endDate,
            PolicyTypes policyType,
            PolicyStatuses status,
            ICollection<Clients> clients)
        {
            var ageResult = ValidateAge(age);
            if (!ageResult.IsValid) return ageResult;

            var genderResult = ValidateGender(gender);
            if (!genderResult.IsValid) return genderResult;

            var healthResult = ValidateHealthCondition(healthCondition);
            if (!healthResult.IsValid) return healthResult;

            var occupationResult = ValidateOccupation(occupation);
            if (!occupationResult.IsValid) return occupationResult;

            var amountResult = ValidateInsuranceAmount(insuranceAmount);
            if (!amountResult.IsValid) return amountResult;

            var datesResult = ValidateDates(startDate, endDate);
            if (!datesResult.IsValid) return datesResult;

            var policyTypeResult = ValidatePolicyType(policyType);
            if (!policyTypeResult.IsValid) return policyTypeResult;

            var statusResult = ValidateStatus(status);
            if (!statusResult.IsValid) return statusResult;

            var clientsResult = ValidateClients(clients);
            if (!clientsResult.IsValid) return clientsResult;

            return new ValidationResult(true);
        }
    }

    public class PropertyInsuranceValidator
    {
        private readonly VSK_DBEntities _context;

        public PropertyInsuranceValidator(VSK_DBEntities context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public ValidationResult ValidatePropertyType(PropertyTypes propertyType)
        {
            if (propertyType == null)
                return new ValidationResult(false, "Выберите тип имущества.");
            return new ValidationResult(true);
        }

        public ValidationResult ValidateAddress(string address)
        {
            if (string.IsNullOrWhiteSpace(address))
                return new ValidationResult(false, "Укажите адрес имущества.");
            if (!Regex.IsMatch(address, @"^[А-Яа-яЁё0-9\s.,\-]{5,200}$"))
                return new ValidationResult(false, "Адрес: буквы, цифры, пробелы, 5-200 символов.");
            return new ValidationResult(true);
        }

        public ValidationResult ValidateArea(decimal? area)
        {
            if (!area.HasValue || area <= 0)
                return new ValidationResult(false, "Площадь должна быть больше 0.");
            if (area > 1000000)
                return new ValidationResult(false, "Площадь не может превышать 1 000 000 м².");
            return new ValidationResult(true);
        }

        public ValidationResult ValidateValue(decimal? value)
        {
            if (!value.HasValue || value <= 0)
                return new ValidationResult(false, "Стоимость должна быть больше 0.");
            if (value > 1000000000)
                return new ValidationResult(false, "Стоимость не может превышать 1 000 000 000 рублей.");
            return new ValidationResult(true);
        }

        public ValidationResult ValidateDates(DateTime? startDate, DateTime? endDate)
        {
            if (!startDate.HasValue || !endDate.HasValue)
                return new ValidationResult(false, "Укажите даты начала и окончания.");

            if (startDate.Value < DateTime.Today)
                return new ValidationResult(false, "Дата начала не может быть раньше текущей даты.");

            if (endDate.Value <= startDate.Value)
                return new ValidationResult(false, "Дата окончания должна быть позже даты начала.");
            return new ValidationResult(true);
        }

        public ValidationResult ValidateInsuranceAmount(decimal? amount)
        {
            if (!amount.HasValue || amount <= 0)
                return new ValidationResult(false, "Сумма страхования должна быть больше 0.");
            if (amount > 1000000000)
                return new ValidationResult(false, "Сумма страхования не может превышать 1 000 000 000 рублей.");
            return new ValidationResult(true);
        }

        public ValidationResult ValidatePolicyType(PolicyTypes policyType)
        {
            if (policyType == null)
                return new ValidationResult(false, "Выберите тип полиса.");
            return new ValidationResult(true);
        }

        public ValidationResult ValidateStatus(PolicyStatuses status)
        {
            if (status == null)
                return new ValidationResult(false, "Выберите статус полиса.");
            return new ValidationResult(true);
        }

        public ValidationResult ValidateClients(ICollection<Clients> clients)
        {
            if (clients == null || !clients.Any())
                return new ValidationResult(false, "Добавьте хотя бы одного клиента.");
            return new ValidationResult(true);
        }

        public ValidationResult ValidateProperties(ICollection<Properties> properties)
        {
            if (properties == null || !properties.Any())
                return new ValidationResult(false, "Добавьте хотя бы одно имущество.");
            return new ValidationResult(true);
        }

        public ValidationResult ValidatePolicyData(
            DateTime? startDate,
            DateTime? endDate,
            decimal? insuranceAmount,
            PolicyTypes policyType,
            PolicyStatuses status,
            ICollection<Clients> clients,
            ICollection<Properties> properties)
        {
            var datesResult = ValidateDates(startDate, endDate);
            if (!datesResult.IsValid) return datesResult;

            var amountResult = ValidateInsuranceAmount(insuranceAmount);
            if (!amountResult.IsValid) return amountResult;

            var policyTypeResult = ValidatePolicyType(policyType);
            if (!policyTypeResult.IsValid) return policyTypeResult;

            var statusResult = ValidateStatus(status);
            if (!statusResult.IsValid) return statusResult;

            var clientsResult = ValidateClients(clients);
            if (!clientsResult.IsValid) return clientsResult;

            var propertiesResult = ValidateProperties(properties);
            if (!propertiesResult.IsValid) return propertiesResult;

            return new ValidationResult(true);
        }

        public ValidationResult ValidatePropertyInsurance(
            PropertyTypes propertyType,
            string address,
            decimal? area,
            decimal? value,
            DateTime? startDate,
            DateTime? endDate,
            decimal? insuranceAmount,
            PolicyTypes policyType,
            PolicyStatuses status,
            ICollection<Clients> clients,
            ICollection<Properties> properties)
        {
            var propertyTypeResult = ValidatePropertyType(propertyType);
            if (!propertyTypeResult.IsValid) return propertyTypeResult;

            var addressResult = ValidateAddress(address);
            if (!addressResult.IsValid) return addressResult;

            var areaResult = ValidateArea(area);
            if (!areaResult.IsValid) return areaResult;

            var valueResult = ValidateValue(value);
            if (!valueResult.IsValid) return valueResult;

            var datesResult = ValidateDates(startDate, endDate);
            if (!datesResult.IsValid) return datesResult;

            var amountResult = ValidateInsuranceAmount(insuranceAmount);
            if (!amountResult.IsValid) return amountResult;

            var policyTypeResult = ValidatePolicyType(policyType);
            if (!policyTypeResult.IsValid) return policyTypeResult;

            var statusResult = ValidateStatus(status);
            if (!statusResult.IsValid) return statusResult;

            var clientsResult = ValidateClients(clients);
            if (!clientsResult.IsValid) return clientsResult;

            var propertiesResult = ValidateProperties(properties);
            if (!propertiesResult.IsValid) return propertiesResult;

            return new ValidationResult(true);
        }
    }
}