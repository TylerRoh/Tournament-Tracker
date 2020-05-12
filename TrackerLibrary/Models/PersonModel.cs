using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary.Models
{
    public class PersonModel
    {
        /// <summary>
        /// Represents the persons first name.
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Represents the persons last name.
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// Represents the persons email address.
        /// </summary>
        public string EmailAddress { get; set; }
        /// <summary>
        /// Represents the persons phone number.
        /// </summary>
        public string PhoneNumber { get; set; }
    }
}
