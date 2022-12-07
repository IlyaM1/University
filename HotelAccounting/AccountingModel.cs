using System;

namespace HotelAccounting
{
    public class AccountingModel : ModelBase
    {
        private double _price;
        public double Price
        {
            get => _price;
            set
            {
                if (value < 0) 
                    throw new ArgumentException();

                _price = value;

                Notify(nameof(Price));
                Notify(nameof(Total));
            }
        }

        private int _nightsCount;
        public int NightsCount
        {
            get => _nightsCount;
            set
            {
                if (value <= 0) 
                    throw new ArgumentException();

                _nightsCount = value;

                Notify(nameof(NightsCount));
                Notify(nameof(Total));
            }
        }

        private double _discount;
        public double Discount
        {
            get => _discount;
            set
            {
                _discount = value;
                _total = CountTotal();

                if (_total < 0) 
                    throw new ArgumentException();

                Notify(nameof(Discount));
                Notify(nameof(Total));
            }
        }

        private double _total;
        public double Total
        {
            get => CountTotal();
            set
            {
                if (value <= 0) 
                    throw new ArgumentException();

                _total = value;
                _discount = 100 * (1 - _total / (_price * _nightsCount));

                Notify(nameof(Total));
                Notify(nameof(Discount));
            }
        }

        private double CountTotal()
        {
            return _price * _nightsCount * (1 - _discount / 100);
        }
    }
}