using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using WpfSimpleMVVM.Exeptions;
namespace WpfSimpleMVVM.Models
{

    [DataContract]
    class Person : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged Impl
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        private string _firstName;
        private string _email;
        private DateTime _date;
        private bool _isAdult;
        private string _sunSign;
        private string _chineseSign;
        private bool _isBirthDay;
        private string _secondName;
        public static int ID = 0;

        #region Constructors
        public Person(string firstName, string secondName, string email, DateTime date)
        {
            FirstName = firstName;
            SecondName = secondName;
            EMail = email;
            Date = date;
            ChineseSign = CalculateChinaSign();
            SunSign = CalculateSimpleSign();
            IsBirthday = IsBithdaymethod();
            IsAdult = _isAdult;
            ThisID = ID++;
            Birthday = Date.ToShortDateString();

        }

        #endregion

        [DataMember]
        public int ThisID { get; set; }
        [DataMember]
        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                OnPropertyChanged();
            }
        }
        [DataMember]
        public string SecondName
        {
            get => _secondName;
            set
            {
                _secondName = value;
                OnPropertyChanged();
            }
        }
        [DataMember]
        public string EMail
        {
            get => _email;
            set
            {
                if (Validation(value))
                    throw new EMailExeption("INVALID EMAIL");
                else
                    _email = value;
                OnPropertyChanged();

            }
        }
        [DataMember]
        private DateTime Date
        {
            get => _date;
            set
            {
                _date = value;
                int age = CalculateAge();
                if (age > 135)
                    throw new AgeExeption("INVALID AGE", age);
                else if (age < 0)
                    throw new DateExeption("INVALID DATE", value.ToShortDateString());

            }
        }
        [DataMember]
        public string Birthday { get; set; }
        [DataMember]
        public string SunSign { get => _sunSign; set => _sunSign = value; }
        [DataMember]
        public string ChineseSign { get => _chineseSign; set => _chineseSign = value; }
        [DataMember]
        public bool IsBirthday { get => _isBirthDay; set => _isBirthDay = value; }
        [DataMember]
        public bool IsAdult { get => _isAdult; set => _isAdult = value; }



        #region Method
        private bool Validation(string email)
        {

            string[] parts = email.Split('@');

            if (parts.Length != 2) return true;
            return (parts[1].Split('.').Length < 2);

        }
        private int CalculateAge()
        {

            if (DateTime.Now.Month < Date.Month || DateTime.Now.Month == Date.Month && DateTime.Now.Day < Date.Day)
                return DateTime.Now.Year - Date.Year - 1;
            return DateTime.Now.Year - Date.Year;
        }

        private bool IsAdultMethod()
        {
            return (CalculateAge() > 18);
        }


        private bool IsBithdaymethod()
        {
            return DateTime.Now.Month == Date.Month && DateTime.Now.Day == Date.Day;
        }

        private string CalculateSimpleSign()
        {

            if (Date.Day <= 20 && Date.Month <= 1)
                return "Козерог";
            if (Date.Day <= 18 && Date.Month <= 2)
                return "Водолій";
            else if (Date.Day <= 20 && Date.Month <= 3)
                return "Риби";
            else if (Date.Day <= 20 && Date.Month <= 4)
                return "Овен";
            else if (Date.Day <= 20 && Date.Month <= 5)
                return "Телець";
            else if (Date.Day <= 21 && Date.Month <= 6)
                return "Близнюки";
            else if (Date.Day <= 22 && Date.Month <= 7)
                return "Рак";
            else if (Date.Day <= 22 && Date.Month <= 8)
                return "Лев";
            else if (Date.Day <= 22 && Date.Month <= 9)
                return "Діва";
            else if (Date.Day <= 23 && Date.Month <= 10)
                return "Віси";
            else if (Date.Day <= 22 && Date.Month <= 11)
                return "Scorpions√";
            else
                return "Стрілок";



        }

        private string CalculateChinaSign()
        {


            int sign = Date.Year % 12;

            switch (sign)
            {
                case 0:
                    return "Год Обезьяны";

                case 1:
                    return "Год Петуха";
                case 2:
                    return "Год Собаки";
                case 3:
                    return "Год Свинні";
                case 4:
                    return "Год Криси";
                case 5:
                    return "Год Бика";
                case 6:
                    return "Год Тигра";
                case 7:
                    return "Год Кролика";
                case 8:
                    return "Год Дракона";
                case 9:
                    return "Год Змії";
                case 10:
                    return "Год Коня";
                case 11:
                    return "Год Кози";

            }
            return "";


        }
        #endregion
    }
}
