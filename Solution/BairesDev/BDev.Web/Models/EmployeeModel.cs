namespace BDev.Web.Models
{
    using BDev.Common;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class EmployeeModel
    {
        /// <summary>
        /// Primary Key on the database
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Name of the employee
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Type of the Employee (Developer, tester...)
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "The Type field is required")]
        public EmployeeType Type { get; set; }

        /// <summary>
        /// Uploaded image for the employee
        /// </summary>
        [Required]
        public byte[] Image { get; set; }

        /// <summary>
        /// The byte array image converted to a single string to be set on the src property of the img Html tag
        /// </summary>
        public string ImageString { get; set; }
        
        /// <summary>
        /// Main constructor
        /// </summary>
        public EmployeeModel()
        {

        }

        /// <summary>
        /// Overloaded signature to create the object from the Common.Employee object
        /// </summary>
        /// <param name="e"></param>
        public EmployeeModel(Employee e)
        {
            this.ID = e.ID;
            this.Name = e.Name;
            this.Type = e.Type;
            this.Image = e.Image;
            if(e.Image != null)
            {
                var base64 = Convert.ToBase64String(e.Image);
                var imgSrc = string.Format("data:image/gif;base64,{0}", base64);
                this.ImageString = imgSrc;
            }
        }

        /// <summary>
        /// Converts the EmployeeModel object to a single Common.Employee object
        /// </summary>
        /// <returns>Employee object</returns>
        public Employee toEmployee()
        {
            return new Employee()
            {
                ID = this.ID,
                Name = this.Name,
                Type = this.Type,
                Image = this.Image
            };
        }
    }
}