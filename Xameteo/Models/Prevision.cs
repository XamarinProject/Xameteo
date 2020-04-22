using System;
using System.Collections.Generic;
using System.Text;

namespace Xameteo.Models
{
    public class Prevision
    {
        public Prevision(string date, string moment, string tendency, string temperature)
        {
            this.Date = date;
            this.Tendency = tendency;
            this.Temperature = temperature;
            this.Moment = moment;
        }

        private string date;
        public string Date
        {
            get => date;
            set => date = value;
        }

        private string tendency;
        public string Tendency
        {
            get => tendency;
            set => tendency = value;
        }

        private string temperature;

        public string Temperature
        {
            get => temperature;
            set => temperature = value;
        }

        private string moment;
        public string Moment
        {
            get => moment;
            set => moment = value;
        }
    }
}
