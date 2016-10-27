using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxiDispatcher.Core;

namespace TaxiDispatcher.Models
{
    public class Car : IEntity
    {

        public string MarkOfCar { get; set; }

        public string ModelOfCar { get; set; }

        public string Number { get; set; }

        public string Color { get; set; }

        public string Type { get; set; }

        /// <summary>
        /// Год выпуска
        /// </summary>
        public int YearOfIssue { get; set; }

    }
}
