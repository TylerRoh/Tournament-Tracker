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
        /// The unique identifier for the prize
        /// </summary>
        public int Id { get; set; }
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

        public PersonModel()
        {

        }

        public PersonModel(string firstName, string lastName, string emailAddress, string phoneNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            EmailAddress = emailAddress;
            PhoneNumber = phoneNumber;
        }
    }
}
